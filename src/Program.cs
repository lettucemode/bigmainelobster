using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Example
{
    public class Program
    {
        public static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandHandler _commands;

        public async Task StartAsync()
        {
            // Ensure the configuration file has been created.
            Configuration.EnsureExists();
            
            // Create a new instance of DiscordSocketClient.
            _client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Verbose, // Specify console verbose information level.
                MessageCacheSize = 1000         // Tell discord.net how long to store messages (per channel).
            });

            // Register the console log event.
            _client.Log += (l)
                => Console.Out.WriteLineAsync(l.ToString());
            
            //log in and start client
            await _client.LoginAsync(TokenType.Bot, Configuration.Load().Token);
            await _client.StartAsync();

            // Initialize the command handler service and other events
            _commands = new CommandHandler();
            await _commands.InstallAsync(_client);

            // Run forever
            await Task.Delay(-1);
        }
    }
}
