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

    /// <summary>
    /// Starts the Discord bot asynchronously.
    /// </summary>
    /// <remarks>
    /// This method initializes the Discord client, logs in with the provided token, and starts the bot.
    /// </remarks>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task StartBotAsync()
    {

        this.client.Log += LogFuncAsync;
        await this.client.LoginAsync(TokenType.Bot, TOKEN);
        await this.client.StartAsync();
        await Task.Delay(-1);

        Task LogFuncAsync(LogMessage message) { Console.Write(message.ToString()); return Task.CompletedTask; }
    }

    /// <summary>
    /// Handles incoming messages received by the bot.
    /// </summary>
    /// <param name="message">The message received by the bot.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private async Task MessageHandler(SocketMessage message)
    {
        if (message.Author.IsBot) return;

        await ReplyAsync(message, "C# response works!");
    }

    /// <summary>
    /// Sends a reply message to the channel where the original message was received.
    /// </summary>
    /// <param name="message">The original message to reply to.</param>
    /// <param name="response">The response message to send.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private async Task ReplyAsync(SocketMessage message, string response) => await message.Channel.SendMessageAsync(response);
}
