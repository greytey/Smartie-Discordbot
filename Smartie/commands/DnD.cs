using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace Smartie.commands
{
    [Group("dnd")]
    public class DnD : BaseCommandModule
    {

        Random random = new Random();
        string[] races = new string[] { "Dragonborn", "Dwarf", "Elf", "Gnome", "Half-Elf", "Half-Orc", "Halfling", "Human", "Tiefling",
                "Arakocra", "Aasimar", "Changeling", "Deep Gnome", "Duergar", "Eladrin", "Fairy", "Firbolg", "Genasi", "Githyanki", "Githzerai", "Goliath",
                "Harengon", "Kenku", "Locathah", "Owlin", "Satyr", "Sea Elf", "Shadar-Kai", "Tabaxi", "Tortle", "Triton", "Verdan", "Bugbear", "Centaur",
                "Goblin", "Grung", "Hobgoblin", "Kobold", "Lizardfolk", "Minotaur", "Orc", "Shifter", "Yuan-Ti" };
        string[] backgrounds = new string[] { "Acolyte", "Anthropoligist", "Archaeologist", "Athelete", "Charlatan", "City Witch", "Clan Crafter",
                "Cloistered Scholar", "Courtier", "Criminal", "Entertainer", "Faceless", "Faction Agent", "Far Traveler", "Feylost", "Fisher", "Folk Hero",
                "Gladiator", "Guild Artisan", "Guild Merchant", "Haunted One", "Hermit", "House Agent", "Inheritor", "Investigator", "Knight",
                "Knight of the Order", "Marine", "Mercenary Veteran", "Noble", "Outlander", "Pirate", "Sage", "Sailor", "Shipwright", "Smuggler",
                "Soldier", "Spy", "Urban Bounty Hunter", "Urchin", "Uthgardt Tribe Member", "Waterdhavina Noble", "Witchlight Hand" };
        string[] classes = new string[] { "Artificer", "Barbarian", "Bard", "Cleric", "Druid", "Fighter", "Monk",
                "Paladin", "Ranger", "Rogue", "Sorcerer", "Warlock", "Wizard" };
        DiscordMember lastAsked = null;

        [Command("dice")]
        public async Task rollDice(CommandContext ctx, string dice)
        {
            string possibleNumber = dice.Substring(1);
            try
            {
                int dicelimit = int.Parse(possibleNumber);
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

        [Command("create")]
        public async Task createCharacter(CommandContext ctx)
        {
            int indexRace = random.Next(races.Length);
            int indexBackground = random.Next(backgrounds.Length);
            int indexClass = random.Next(classes.Length);
            string pronoun = "a ";
            string firstPart = "Searching for a new character? How about ";
            if(backgrounds.ElementAt(indexBackground).StartsWith("A")|| backgrounds.ElementAt(indexBackground).StartsWith("E") ||
                backgrounds.ElementAt(indexBackground).StartsWith("I") || backgrounds.ElementAt(indexBackground).StartsWith("O") ||
                backgrounds.ElementAt(indexBackground).StartsWith("U"))
            {
                pronoun = "an ";
            }
            if(ctx.Member == lastAsked)
            {
                firstPart = "Something else? How about ";
            }
            lastAsked = ctx.Member;
            await ctx.Channel.SendMessageAsync(firstPart + pronoun + backgrounds.ElementAt(indexBackground) + " " + races.ElementAt(indexRace) + " " + classes.ElementAt(indexClass) + "?");

        }
    }
}
