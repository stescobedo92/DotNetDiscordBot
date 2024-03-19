using Discord;
using Discord.WebSocket;
using DotNetDiscordBot;
using Moq;

namespace DotNetDiscordBotTest;

[TestFixture]
public class DotNetDiscordBotTest
{
    private readonly DiscordSocketClient? _mockClient;
    private readonly DiscordClientUtilities? _clientUtilities;

    [SetUp]
    public void Setup() => Setup(_mockClient, _clientUtilities);
    
    public void Setup(DiscordSocketClient mockClient, DiscordClientUtilities clientUtilities)
    {
        mockClient = new Mock<DiscordSocketClient>().Object;
        clientUtilities = new DiscordClientUtilities();
    }

    [Test]
    public async Task StartBotAsync_ClientLoginIsCalled()
    {
        // Act
        await _clientUtilities.StartBotAsync();

        var mockClientLogin = _mockClient?.LoginAsync(TokenType.Bot, It.IsAny<string>());

        // Assert
        Mock.Get(_mockClient).Verify(mockClientLogin, Times.Once);
    }

    [Test]
    public async Task StartBotAsync_ClientStartAsyncIsCalled()
    {
        // Act
        await _clientUtilities.StartBotAsync();

        // Assert
        Mock.Get(_mockClient).Verify(c => c.StartAsync(), Times.Once);
    }

    [Test]
    public async Task MessageHandler_WhenAuthorIsBot_NoReplyIsSent()
    {
        // Arrange
        var mockMessage = new Mock<SocketMessage>();
        mockMessage.Setup(m => m.Author.IsBot).Returns(true);

        // Act
        await _clientUtilities.MessageHandler(mockMessage.Object);

        // Assert
        Mock.Get(_mockClient).Verify(c => c.SendMessageAsync(It.IsAny<SocketMessage>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task MessageHandler_WhenAuthorIsNotBot_ReplyIsSent()
    {
        // Arrange
        var mockMessage = new Mock<SocketMessage>();
        mockMessage.Setup(m => m.Author.IsBot).Returns(false);

        // Act
        await _clientUtilities.MessageHandler(mockMessage.Object);

        // Assert
        Mock.Get(_mockClient).Verify(c => c.SendMessageAsync(It.IsAny<SocketMessage>(), "C# response works!"), Times.Once);
    }
}