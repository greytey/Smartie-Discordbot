using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using OpenAI_API;
using OpenAI_API.Chat;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace Smartie.commands
{
    public class ChatGPT : BaseCommandModule
    {
        [Command("chatgpt")]
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
    }
}