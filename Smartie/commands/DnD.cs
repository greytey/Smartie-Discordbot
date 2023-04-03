using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartie.commands
{
    [Group("dnd")]
    public class DnD : BaseCommandModule
    {
        [Command("dice")]
        public async Task rollDice(CommandContext ctx, string dice)
        {

            string possibleNumber = dice.Substring(1);
            try
            {
                int dicelimit = int.Parse(possibleNumber);
                Random random = new Random();
                int diceroll = random.Next(dicelimit) + 1;
                if(diceroll == 1) 
                {
                    await ctx.Channel.SendMessageAsync("oof, nat 1 :/ My deepest sympathy");
                }
                if (diceroll == 20 && dicelimit == 20) {
                    await ctx.Channel.SendMessageAsync("NAT FREAKING 20, nice one!");
                }
                else
                {
                    await ctx.Channel.SendMessageAsync("You rolled a " + dice + " and got a " + diceroll);
                }
            } catch (Exception ex)
            {
                await ctx.Channel.SendMessageAsync("I'm sorry, you gave me no correct dice format. Your message should be 'Hey Smartie dnd dice d<max number of die>'. Try again!");
            }

        }
    }
}
