using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.VoiceNext;
using System.Diagnostics;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using CliWrap;
using System.IO;
using System.Runtime.CompilerServices;
using DSharpPlus.Entities;

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
                    DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
                    {
                        Title = "Ups",
                        Description = "Already connected to a voice channel in this server.",
                        Color = DiscordColor.Red
                    };
                    await ctx.Channel.SendMessageAsync(embed: embededMessage);
                }
            else
            {
                var channel = ctx.Member?.VoiceState?.Channel;
                if (channel == null)
                {
                    DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
                    {
                        Title = "Ups",
                        Description = "Sorry, you have to be connected to a channel in this server.",
                        Color = DiscordColor.Red
                    };
                    await ctx.Channel.SendMessageAsync(embed: embededMessage);
                }
                else
                {
                    connection = await voiceClient.ConnectAsync(channel);
                    DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
                    {
                        Title = "Connected",
                        Color = DiscordColor.Violet
                    };
                    await ctx.Channel.SendMessageAsync(embed: embededMessage);
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
                    DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
                    {
                        Title = "Ups",
                        Description = "I am not connected to this server currently, make me join with 'Hey Smartie music join'.",
                        Color = DiscordColor.Red
                    };
                    await ctx.Channel.SendMessageAsync(embed: embededMessage);
                }
            else
            {
                connection.Disconnect();
                DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
                {
                    Title = "Disconnected",
                    Color = DiscordColor.Violet
                };
                await ctx.Channel.SendMessageAsync(embed: embededMessage);
            }

        }

        [Command("play")]
        public async Task play(CommandContext ctx, [RemainingText] string url)
        {
            var voiceClient = ctx.Client.GetVoiceNext();

            var connection = voiceClient.GetConnection(ctx.Guild);
            if (connection == null)
            {
                DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
                {
                    Title = "Ups",
                    Description = "I am not connected to this server currently, make me join with 'Hey Smartie music join'.",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: embededMessage);
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
                    await connection.SendSpeakingAsync(true);

                    VoiceTransmitSink transmit = connection.GetTransmitSink();
                    transmit.VolumeModifier = 1.5f;
                    await memoryStream.CopyToAsync(connection.GetTransmitSink());
                    var video = await yt.Videos.GetAsync(url);
                    DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
                    {
                        Title = "Playing...",
                        Description = video.Title + " by " + video.Author,
                        Color = DiscordColor.Violet
                    };
                    await ctx.Channel.SendMessageAsync(embed: embededMessage);
                    await connection.SendSpeakingAsync(false);
                }
                catch (Exception ex)
                {
                    DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
                    {
                        Title = "Error",
                        Description = "There was an error, try again!",
                        Color = DiscordColor.Red
                    };
                    await ctx.Channel.SendMessageAsync(embed: embededMessage);
                }
            }
                 
        }

        [Command("help")]
        public async Task help(CommandContext ctx)
        {
            DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
            {
                Title = "Music help",
                Description = "Music has the following commands:\n" +
                "- music join \\ bot joines the channel the user is currently in\n" +
                "- music leave \\ bot leaves the channel he is currently in\n" +
                "\n\n**Remember**: Commands should always start with 'Hey Smartie '",
                Color = DiscordColor.Blue
            };
            await ctx.Channel.SendMessageAsync(embed: embededMessage);
        }
    }
}
