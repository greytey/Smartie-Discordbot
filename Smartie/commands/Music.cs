using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.VoiceNext;
using MediaToolkit.Model;
using MediaToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;
using System.Diagnostics;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using CliWrap;
using System.IO;
using System.Runtime.CompilerServices;

namespace Smartie.commands
{
    [Group("music")]
    public class Music : BaseCommandModule
    {
        [Command("join")]
        public async Task join(CommandContext ctx)
        {
            var voiceClient = ctx.Client.GetVoiceNext();
            var connection = voiceClient.GetConnection(ctx.Guild);
            if (connection != null)
            {
                await ctx.Channel.SendMessageAsync("Already connected to a channel in this server.");
            }
            else
            {
                var channel = ctx.Member?.VoiceState?.Channel;
                if (channel == null)
                {
                    await ctx.Channel.SendMessageAsync("Sorry, you have to be in a channel.");

                }
                else
                {
                    connection = await voiceClient.ConnectAsync(channel);
                    await ctx.Channel.SendMessageAsync("Connected");
                }
            }

        }

        [Command("leave")]
        public async Task leave(CommandContext ctx)
        {
            var voiceClient = ctx.Client.GetVoiceNext();

            var connection = voiceClient.GetConnection(ctx.Guild);
            if (connection == null)
            {
                await ctx.Channel.SendMessageAsync("Not connected to this server currently.");
            }
            else
            {
                connection.Disconnect();
                await ctx.Channel.SendMessageAsync("Disconnected");
            }

        }

        [Command("play")]
        public async Task play(CommandContext ctx, [RemainingText] string url)
        {
            var voiceClient = ctx.Client.GetVoiceNext();

            var connection = voiceClient.GetConnection(ctx.Guild);
            if (connection == null)
            {
                await ctx.Channel.SendMessageAsync("Not connected to this server currently. Use 'join' to make me join your channel.");

            }
            else
            {
                YoutubeClient yt = new YoutubeClient();
                var streamManifest = await yt.Videos.Streams.GetManifestAsync(url);
                var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                var stream = await yt.Videos.Streams.GetAsync(streamInfo);
                var memoryStream = new MemoryStream();
                try
                {
                    var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30)); // set timeout to 30 seconds
                    await Cli.Wrap("ffmpeg")
                        .WithArguments(" -hide_banner -loglevel panic -i pipe:0 -ac 2 -f s16le -ar 48000 -b:a 128k -f opus pipe:1")
                        .WithStandardInputPipe(PipeSource.FromStream(stream))
                        .WithStandardOutputPipe(PipeTarget.ToStream(memoryStream))
                        .ExecuteAsync(cancellationTokenSource.Token);
                    Console.WriteLine($"Downloaded {memoryStream.Length} bytes of audio stream from {url}");
                    await connection.SendSpeakingAsync(true);

                    VoiceTransmitSink transmit = connection.GetTransmitSink();
                    transmit.VolumeModifier = 1.5f;
                    await memoryStream.CopyToAsync(connection.GetTransmitSink());

                    await connection.SendSpeakingAsync(false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    await ctx.Channel.SendMessageAsync("Error: " + ex.Message);
                }
            }
                 
        }
    }
}
