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
                "\n- dnd dice d<max number of die> //To roll a die with a set max amount of sides" +
                "\n- dnd create //Creates a new dnd character with background, race and class" +
                "\n\nCommands should always start with 'Hey Smartie '");

        }
    }
}
