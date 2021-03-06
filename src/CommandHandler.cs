﻿using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;

namespace bigmainelobster
{
    /// <summary> Detect whether a message is a command, then execute it. </summary>
    public class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _cmds;

        public async Task InstallAsync(DiscordSocketClient c)
        {
            _client = c;                                                 // Save an instance of the discord client.
            _cmds = new CommandService();                                // Create a new instance of the commandservice.                              
            
            await _cmds.AddModulesAsync(Assembly.GetEntryAssembly());    // Load all modules from the assembly.
            
            _client.MessageReceived += HandleCommandAsync;               // Register the messagereceived event to handle commands.
            _client.UserJoined += UserJoined;                            // Register a greeting message
        }

        private async Task UserJoined(SocketGuildUser user)
        {
            //welcome user
            var channel = user.Guild.TextChannels.FirstOrDefault(x => x.Name.Contains("all_chat"));
            if (channel != null) await channel.SendMessageAsync("Welcome " + user.Mention + ", to the TNBZ gaming community!");

            //assign guest role
            var guestRole = user.Guild.Roles.Where(r => r.Name == "guests").FirstOrDefault();
            if (guestRole != null) await user.AddRoleAsync(guestRole);
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null)                                          // Check if the received message is from a user.
                return;

            var context = new SocketCommandContext(_client, msg);     // Create a new command context.

            int argPos = 0;                                           // Check if the message has either a string or mention prefix.
            if (msg.HasStringPrefix(Configuration.Load().Prefix, ref argPos) ||
                msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {                                                         // Try and execute a command with the given context.
                var result = await _cmds.ExecuteAsync(context, argPos);

                if (!result.IsSuccess)                                // If execution failed, reply with the error message.
                    await context.Channel.SendMessageAsync(result.ToString());
            }
        }
    }
}
