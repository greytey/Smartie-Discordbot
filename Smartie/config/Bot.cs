using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartie.commands;
using DSharpPlus.VoiceNext;

namespace Smartie.config
{
    public class Bot
    {
        public DiscordClient client { get; private set; }
        public InteractivityExtension interactivity { get; private set; }
        public CommandsNextExtension commands { get; private set; }

        public VoiceNextExtension voice { get; private set; }

        public async Task runAsync()
        {
            // read configuration from json file
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync();

            var configJson = JsonConvert.DeserializeObject<ConfigJSON>(json);

            var config = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = configJson.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            client = new DiscordClient(config);
            client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { configJson.prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false,
            };

            commands = client.UseCommandsNext(commandsConfig);

            // every command class needs to be registered 
            commands.RegisterCommands<Help>();
            commands.RegisterCommands<DnD>();
            commands.RegisterCommands<ChatGPT>();
            commands.RegisterCommands<Music>();
            commands.RegisterCommands<TextGame>();

            voice = client.UseVoiceNext();

            await client.ConnectAsync();
            await Task.Delay(-1);
        }

        private Task onClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}