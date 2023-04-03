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
        public async Task testCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("To this point, the only command available is 'help'. Commands start with 'Hey Smartie '");
        }
    }
}
