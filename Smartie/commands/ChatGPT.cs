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
    [Group("chatgpt")]
    public class ChatGPT : BaseCommandModule
    {
        [Command("chatgpt")]
        public async Task askChatGPT(CommandContext ctx, params string[] userinput)
        {
            DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder
            {
                Title = "Loading...",
                Description = "Waiting for the answer from chatgpt",
                Color = DiscordColor.Green,
            };
            var message = await ctx.Channel.SendMessageAsync(embed: embededMessage);
            try
            {
                string userinputString = string.Join(' ', userinput);

                OpenAIAPI openAiApi = new OpenAIAPI("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");

                var chat = openAiApi.Chat.CreateConversation();
                chat.AppendSystemMessage("Type in a query");

                chat.AppendUserInput(userinputString);

                string response = await chat.GetResponseFromChatbotAsync();

                embededMessage = new DiscordEmbedBuilder()
                {
                    Title = "Answer to the question: " + userinputString,
                    Description = response,
                    Color = DiscordColor.Green,
                };
            }
            catch (Exception e)
            {
                embededMessage = new DiscordEmbedBuilder()
                {
                    Title = "Error",
                    Description = "It seems like something went wrong, please try again.",
                    Color = DiscordColor.Red,
                };
            }
            await message.DeleteAsync();
            await ctx.Channel.SendMessageAsync(embed: embededMessage);
        }

        [Command("help")]
        public async Task help(CommandContext ctx)
        {
            var embededMessage = new DiscordEmbedBuilder
            {
                Title = "ChatGPT help",
                Description = "chatgpt has the following command:\n" +
                "- chatgpt ask <what you want to ask> //asks chatgpt whatever you typed" +
                "\n\n**Remember**: Commands should always start with 'Hey Smartie '",
                Color = DiscordColor.Blue,
            };
            await ctx.Channel.SendMessageAsync(embed: embededMessage);
        }
    }
}