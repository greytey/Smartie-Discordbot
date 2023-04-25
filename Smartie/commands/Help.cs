using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartie.commands
{
    public class Help : BaseCommandModule
    {
        [Command("help")]
        public async Task helpCommand(CommandContext ctx)
        {
            var embededMessage = new DiscordEmbedBuilder
            {
                Title = "Smartie help",
                Description = "Commmands:" +
                "\n- help //To get all Smartie-Commands" +
                "\n- dnd help //different dnd commands and what they do" +
                "\n- chatgpt help //more about the chatgpt command" +
                "\n- music help //more about the music player commands" +
                "\n- sun help //more about the sun commands" +
                "\n\n**Remember**: Commands should always start with 'Hey Smartie '",
                Color = DiscordColor.Blue,
            };
            await ctx.Channel.SendMessageAsync(embed: embededMessage);
        }
    }
}