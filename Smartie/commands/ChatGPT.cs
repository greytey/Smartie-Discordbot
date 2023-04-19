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
        [Command("ask")]
        public async Task askChatGPT(CommandContext ctx, params string[] userinput)
        {
            string userinputString = string.Join(' ', userinput);

            OpenAIAPI openAiApi = new OpenAIAPI("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");

            var chat = openAiApi.Chat.CreateConversation();
            chat.AppendSystemMessage("Type in a query");

            chat.AppendUserInput(userinputString);

            string response = await chat.GetResponseFromChatbotAsync();
           

            var outputEmbeded = new DiscordEmbedBuilder()
            {
                Title = "Antwort auf die Frage: " + userinputString,
                Description = response,
                Color = DiscordColor.Green,
            };

            await ctx.Channel.SendMessageAsync(embed: outputEmbeded);
        }

        [Command("help")]
        public async Task help(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("chatgpt has the following command:\n" +
                "- chatgpt ask <what you want to ask> : asks chatgpt whatever you typed" +
                "\n\n**Remember**: Commands should always start with 'Hey Smartie '");
        }
    }
}