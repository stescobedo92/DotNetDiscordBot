using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDiscordBot
{
    public class DiscordClientUtilities
    {
        private readonly DiscordSocketClient client;
        private const string TOKEN = "ADD_TOKEN_HERE";

        public DiscordClientUtilities()
        {
            this.client = new DiscordSocketClient();
            this.client.MessageReceived += MessageHandler;
        }
    }
}
