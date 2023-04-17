using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
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
            await ctx.Channel.SendMessageAsync("Commmands:" +
                "\n- help //To get all Smartie-Commands" +
                "\n- dnd help //different dnd commands and what they do" +
                "\n\nCommands should always start with 'Hey Smartie '");

        }

    }
}
