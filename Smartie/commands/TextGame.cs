using DSharpPlus.CommandsNext;
using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using OpenAI_API;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using OpenAI_API.Completions;
using DSharpPlus.Interactivity.EventHandling;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.Interactivity.Extensions;
using System.ComponentModel;

namespace Smartie.commands
{
    [Group("textgame")]
    public class TextGame : BaseCommandModule
    {
        private string selectedCharacter; // variable to store selected character
        private int playerHealth = 100; // variable to store player's health
        private Enemy selectedEnemy;
      

        [Command("start")]
        public async Task StartGame(CommandContext ctx)
        {
            if (!string.IsNullOrEmpty(selectedCharacter))
            {
                // check if a character is already selected
                await ctx.Channel.SendMessageAsync($"You have already selected {selectedCharacter}. You can't select another character during the game.");
                return;
            }
            DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
            {
                Title = "Welcome to the TextGame! Please select a character:",
                Description = " - Type 'Hades' to select Hades\n" +
                "- Type 'Hercules' to select Hercules\n" +
                "- Type 'Ares' to select Ares",
                Color = DiscordColor.Red
            };
            await ctx.Channel.SendMessageAsync(embed: embededMessage);

            

            // wait for user's character selection
            var interactivity = ctx.Client.GetInteractivity();
            var characterSelection = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && !x.Author.IsBot, TimeSpan.FromMinutes(1));

            if (characterSelection.Result != null)
            {
                // validate and set selected character
                switch (characterSelection.Result.Content.ToLower())
                {
                    case "hades":
                        selectedCharacter = "Hades";
                        await ctx.Channel.SendMessageAsync("You have selected Hades.");
                        await ChooseEnemy(ctx);
                        break;
                    case "hercules":
                        selectedCharacter = "Hercules";
                        await ctx.Channel.SendMessageAsync("You have selected Hercules.");
                        await ChooseEnemy(ctx);
                        break;
                    case "ares":
                        selectedCharacter = "Ares";
                        await ctx.Channel.SendMessageAsync("You have selected Ares.");
                        await ChooseEnemy(ctx);
                        break;
                    default:
                        await ctx.Channel.SendMessageAsync("Invalid character selected.");
                        await StartGame(ctx);
                        break;
                }

            }
            else
            {
                await ctx.Channel.SendMessageAsync("No character selected.");
            }
        }

        public async Task ChooseEnemy(CommandContext ctx)
        {

            // show available enemies
            DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
            {
                Title = "Please select an enemy",
                Description = "- Type 'Goblin' to select Goblin\n" +
                "- Type 'Troll' to select Troll\n" +
                "- Type 'Dragon' to select Dragon",
                Color = DiscordColor.Red
            };
            await ctx.Channel.SendMessageAsync(embed: embededMessage);

            // wait for user's enemy selection
            var interactivity = ctx.Client.GetInteractivity();
            var enemySelection = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && !x.Author.IsBot, TimeSpan.FromMinutes(1));

            // validate and set selected enemy
            if (enemySelection.Result != null)
            {
                switch (enemySelection.Result.Content.ToLower())
                {
                    case "goblin":
                        selectedEnemy = new Enemy("Goblin", 30, 5, 10);
                        await ctx.Channel.SendMessageAsync("You have selected Goblin.");
                        // display enemy stats to the player
                        await ctx.Channel.SendMessageAsync($"You have encountered a {selectedEnemy.Name} with {selectedEnemy.Health} health.\n");
                        break;
                    case "troll":
                        selectedEnemy = new Enemy("Troll", 50, 10, 20);
                        await ctx.Channel.SendMessageAsync("You have selected Troll.");
                        // display enemy stats to the player
                        await ctx.Channel.SendMessageAsync($"You have encountered a {selectedEnemy.Name} with {selectedEnemy.Health} health.\n");
                        break;
                    case "dragon":
                        selectedEnemy = new Enemy("Dragon", 100, 20, 30);
                        await ctx.Channel.SendMessageAsync("You have selected Dragon.");
                        // display enemy stats to the player
                        await ctx.Channel.SendMessageAsync($"You have encountered a {selectedEnemy.Name} with {selectedEnemy.Health} health.\n");
                        break;
                    default:
                        await ctx.Channel.SendMessageAsync("Invalid enemy selected.");
                        break;
                }


                // start the game
                await AttackEnemy(ctx);
            }
            else
            {
                await ctx.Channel.SendMessageAsync("No enemy selected.");
            }
        }

            /* if (selectedEnemy.IsDefeated())
            {
                selectedEnemy = null;
                playerHealth = 100;

                // ask user if they want to play again
                await ctx.Channel.SendMessageAsync("Do you want to play again? Type 'yes' or 'no'.");

                var interactivity = ctx.Client.GetInteractivity();
                var playAgain = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && !x.Author.IsBot, TimeSpan.FromMinutes(1));

                if (playAgain.Result != null)
                {
                    switch (playAgain.Result.Content.ToLower())
                    {
                        case "yes":
                            await StartBattle(ctx);
                            break;

                        case "no":
                            await ctx.Channel.SendMessageAsync("Thanks for playing!");
                            break;

                    }
                }
                else
                {
                    await ctx.Channel.SendMessageAsync("No battle action selected.");
                }
            } */ //This is a replay function that is not finished

                

        private async Task AttackEnemy(CommandContext ctx)
        {
            int damage = 0;
            string attackName = "";
            switch (selectedCharacter)
            {

                #region Hades
                case "Hades":
                    var shadowdamage = new Random().Next(10, 16);
                    var invisibility = new Random().Next(30, 60);
                    DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
                    {
                        Title = "What attack do you want to use?",
                        Description = "- Type 'fist' to use Fists of Hercules deals low damage\n" +
                        "- Type 'slam' to use Thunderous Slam deals low damage and has a chance to stun the enemy for 1 turn",
                        Color = DiscordColor.Red
                    };


                    attackName = "Shadow-Attack";



                    break;
                #endregion

                #region Hercules
                case "Hercules":
                    var fistDamage = new Random().Next(20, 31);
                    var slamDamage = new Random().Next(10, 21);
                    embededMessage = new DiscordEmbedBuilder()
                    {
                        Title = "What attack do you want to use?",
                        Description = "- Type 'fist' to use Fists of Hercules deals low damage\n" +
                        "- Type 'slam' to use Thunderous Slam deals low damage and has a chance to stun the enemy for 1 turn",
                        Color = DiscordColor.Red
                    };
                    await ctx.Channel.SendMessageAsync(embed: embededMessage);
                   
                    var interactivity = ctx.Client.GetInteractivity();
                    var herculesAction = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && !x.Author.IsBot, TimeSpan.FromMinutes(1));
                    if (herculesAction.Result != null)
                    {
                        switch (herculesAction.Result.Content.ToLower())
                        {
                            #region fist
                            case "fist":
                                damage = fistDamage;
                                attackName = "Fists of Hercules";
                                selectedEnemy.Health -= damage;

                                int enemyDamage = selectedEnemy.Attack();
                                playerHealth -= enemyDamage;

                                


                                if (selectedEnemy.Health <= 0)
                                {
                                    await ctx.Channel.SendMessageAsync($"You have defeated the enemy with your {attackName} and won the battle!");
                                    return;
                                    
                                }
                                else
                                {
                                    await ctx.Channel.SendMessageAsync($"You use {attackName} and dealt {damage} to the enemy. The enemy has {selectedEnemy.Health} health remaining.");
                                    
                                    if (playerHealth <= 0)
                                    {
                                        await ctx.Channel.SendMessageAsync($"The {selectedEnemy.Name} hit you with an attack and you lost the battle!");
                                    }
                                    else
                                    {
                                        await ctx.Channel.SendMessageAsync($"The {selectedEnemy.Name} hit you with {enemyDamage} damage. You have {playerHealth} health remaining.");
                                    }

                                    await AttackEnemy(ctx);
                                }


                                return;
                                
                                break;

                            #endregion

                            #region slam
                            case "slam":
                                damage = slamDamage;
                                attackName = "Thunderous Slam";
                                bool enemyStunned = false;

                                Random random = new Random();
                                int randomNumber = random.Next(1, 3);
                                if (randomNumber == 1)
                                {
                                    enemyStunned = true;
                                }

                                selectedEnemy.Health -= damage;

                                if (selectedEnemy.Health <= 0)
                                {
                                    await ctx.Channel.SendMessageAsync($"You have defeated the enemy with your {attackName} and won the battle!");
                                    return;
                                }
                                else
                                {
                                    if (enemyStunned == true)
                                    {
                                        await ctx.Channel.SendMessageAsync($"You use {attackName}, deal {damage}, and stun the enemy for 1 turn. The enemy has {selectedEnemy.Health} health remaining.");
                                    }

                                    else
                                    {
                                        await ctx.Channel.SendMessageAsync($"You use {attackName}, deal {damage}, the stun failed. The enemy has {selectedEnemy.Health} health remaining.");
                                    }

                                    if (enemyStunned == false)
                                    {
                                        // enemy attacks the player
                                        enemyDamage = selectedEnemy.Attack();
                                        playerHealth -= enemyDamage;

                                        if (playerHealth <= 0)
                                        {
                                            await ctx.Channel.SendMessageAsync($"The {selectedEnemy.Name} hit you with an attack and you lost the battle!");
                                        }
                                        else
                                        {
                                            await ctx.Channel.SendMessageAsync($"The {selectedEnemy.Name} hit you with {enemyDamage} damage. You have {playerHealth} health remaining.");
                                        }

                                    }

                                }
                                
                                break;
                            default:
                                await ctx.Channel.SendMessageAsync("Invalid attack selected.");
                                await AttackEnemy(ctx);
                                break;

                                #endregion
                        }
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync("No attack selected.");
                    }
                    await AttackEnemy(ctx);
                    break;

                #endregion

                #region Ares
                case "Ares":
                    damage = new Random().Next(20, 26);
                    attackName = "War Strike";
                    break;
            }
            #endregion

        }


        [Command("help")]
        public async Task help(CommandContext ctx)
        {
            var embededMessage = new DiscordEmbedBuilder
            {
                Title = "TextGame help",
                Description = "TextGame has the following command:\n" +
                "- textgame start // starts the game" +
                "\n\n**Remember**: Commands should always start with 'Hey Smartie '"
                + "no other commands necessary since it's always obvious what you have to type in"
                ,
                Color = DiscordColor.Blue,
            };
            await ctx.Channel.SendMessageAsync(embed: embededMessage);
        }
    }
}

public class Enemy
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int MinDamage { get; set; }
    public int MaxDamage { get; set; }

    public Enemy(string name, int health, int minDamage, int maxDamage)
    {
        Name = name;
        Health = health;
        MinDamage = minDamage;
        MaxDamage = maxDamage;
    }

    public int Attack()
    {
        Random random = new Random();
        return random.Next(MinDamage, MaxDamage + 1);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public bool IsDefeated()
    {
        return Health <= 0;
    }
}
