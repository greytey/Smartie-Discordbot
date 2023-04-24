using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using OpenAI_API;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using OpenAI_API.Completions;

namespace Smartie.commands
{
    [Group("sunriseset")]
    public class SunriseSet : BaseCommandModule
    {
        [Command("sunriseset")]
        public async Task TestCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Hello");
        }

        [Command("help")]
        public async Task help(CommandContext ctx)
        {
            DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
            {
                Title = "SunriseSet help",
                Description = "SunriseSet has the following commands:\n" +
                "- sunriseset ask\\ bot joines the channel the user is currently in\n" +
                "\n\n**Remember**: Commands should always start with 'Hey Smartie '",
                Color = DiscordColor.Blue
            };
            await ctx.Channel.SendMessageAsync(embed: embededMessage);
        }
    }
}