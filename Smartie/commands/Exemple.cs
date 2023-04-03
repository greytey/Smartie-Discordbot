using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartie.commands
{
    public class Exemple : BaseCommandModule
    {
        [Command("test")]
        public async Task testCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Hello World");
        }
    }
}
