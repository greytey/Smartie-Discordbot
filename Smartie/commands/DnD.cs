using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System.Net;

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

        [Command("roll")]
        public async Task rollDice(CommandContext ctx, string dice)
        {
            lastAsked = null;
            try
            {
                string possibleNumber;
                if (dice.StartsWith("d"))
                {
                    possibleNumber = dice.Substring(1);
                } else
                {
                    possibleNumber = dice;
                }
                int dicelimit = int.Parse(possibleNumber);
                int diceroll = random.Next(dicelimit) + 1;
                if(diceroll == 1 && dicelimit == 20) 
                {
                    DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
                    {
                        Description = "oof nat 1 :/",
                        Title = "my deepest sympathy",
                        Color = DiscordColor.DarkRed
                    };
                    await ctx.Channel.SendMessageAsync(embed: embededMessage);
                }
                else if (diceroll == 20 && dicelimit == 20) {
                    DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
                    {
                        Description = "NAT FREAKING 20!!",
                        Title = "nice one",
                        Color = DiscordColor.DarkRed
                    };
                    await ctx.Channel.SendMessageAsync(embed: embededMessage);
                }
                else
                {
                    DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
                    {
                        Description = "You rolled a d" + dicelimit + " and got a " + diceroll + ".",
                        Title = "rolled a dice",
                        Color = DiscordColor.DarkRed
                    };
                    await ctx.Channel.SendMessageAsync(embed: embededMessage);
                }
            } catch (Exception ex)
            {
                DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
                {
                    Title = "Error",
                    Description = "I'm sorry, you gave me no correct dice format. Your message should be 'Hey Smartie dnd roll d<max number of die>' or Hey Smartie dnd roll <max number of die>' . Try again!",
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: embededMessage);
            }

        }

        [Command("create")]
        public async Task createCharacter(CommandContext ctx)
        {
            int indexRace = random.Next(races.Length);
            int indexBackground = random.Next(backgrounds.Length);
            int indexClass = random.Next(classes.Length);
            string pronoun = "a ";
            string firstPart = "Searching for a new character?";
            if(backgrounds.ElementAt(indexBackground).StartsWith("A")|| backgrounds.ElementAt(indexBackground).StartsWith("E") ||
                backgrounds.ElementAt(indexBackground).StartsWith("I") || backgrounds.ElementAt(indexBackground).StartsWith("O") ||
                backgrounds.ElementAt(indexBackground).StartsWith("U"))
            {
                pronoun = "an ";
            }
            if(ctx.Member == lastAsked)
            {
                firstPart = "Something else?";
            }
            lastAsked = ctx.Member;
            DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
            {
                Description = "How about " + pronoun + backgrounds.ElementAt(indexBackground) + " " + races.ElementAt(indexRace) + " " + classes.ElementAt(indexClass) + "?",
                Title = firstPart,
                Color = DiscordColor.DarkRed
            };
            await ctx.Channel.SendMessageAsync(embed: embededMessage);
        }

        [Command("help")]
        public async Task help(CommandContext ctx)
        {
            DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
            {
                Title = "DnD help",
                Description = "dnd has the following commands:\n" +
                "- dnd roll d<number> or dnd roll <number> //rolls a dice with the given amount\n" +
                "- dnd create //creates a new character for you (background, race and class)" +
                "\n\n**Remember**: Commands should always start with 'Hey Smartie '",
                Color = DiscordColor.Blue
            };
            await ctx.Channel.SendMessageAsync(embed: embededMessage);
        }
    }
}
