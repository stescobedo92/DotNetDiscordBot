using Discord;
using Discord.WebSocket;

namespace DotNetDiscordBot;

public class DiscordClientUtilities
{
    private readonly DiscordSocketClient client;
    private const string TOKEN = "ADD_TOKEN_HERE";

    public DiscordClientUtilities()
    {
        this.client = new DiscordSocketClient();
        this.client.MessageReceived += MessageHandler;
    }

    public async Task StartBotAsync()
    {

        this.client.Log += LogFuncAsync;
        await this.client.LoginAsync(TokenType.Bot, TOKEN);
        await this.client.StartAsync();
        await Task.Delay(-1);

        Task LogFuncAsync(LogMessage message) { Console.Write(message.ToString()); return Task.CompletedTask; }
    }

    private async Task MessageHandler(SocketMessage message)
    {
        if (message.Author.IsBot) return;

        await ReplyAsync(message, "C# response works!");
    }

    private async Task ReplyAsync(SocketMessage message, string response) => await message.Channel.SendMessageAsync(response);
}
