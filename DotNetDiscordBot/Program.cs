namespace DotNetDiscordBot;

internal class Program
{
    static void Main(string[] args) => new DiscordClientUtilities().StartBotAsync().GetAwaiter().GetResult();
}
