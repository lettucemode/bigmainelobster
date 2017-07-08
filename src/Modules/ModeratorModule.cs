using Discord.Commands;
using Discord.WebSocket;
using bigmainelobster.Preconditions;
using System.Threading.Tasks;
using System.Linq;

namespace bigmainelobster.Modules
{
    [Name("Moderator")]
    [RequireContext(ContextType.Guild)]
    public class ModeratorModule : ModuleBase<SocketCommandContext>
    {
        [Command("kick")]
        [Remarks("Kick the specified user.")]
        [MinPermissions(AccessLevel.ServerMod)]
        public async Task Kick([Remainder]SocketGuildUser user)
        {
            await ReplyAsync($"cya {user.Mention} :wave:");
            await user.KickAsync();
        }

        [Command("say")]
        [Remarks("Make the bot say something in another channel.")]
        [MinPermissions(AccessLevel.ServerMod)]
        public async Task Say(SocketTextChannel channel, [Remainder]string text)
        {
            await channel.SendMessageAsync(text);
        }

        [Command("announce"), Alias("announcersay")]
        [Remarks("Make the bot say something in the #announcements channel.")]
        public async Task Announce([Remainder]string text)
        {
            var user = Context.User as SocketGuildUser;
            if (user != null)
            {
                if (user.Roles.Any(r => r.Name == "Announcers"))
                {
                    var channel = Context.Guild.TextChannels.FirstOrDefault(x => x.Name == "announcements");
                    if (channel != null) await channel.SendMessageAsync(text);
                }
                else
                {
                    await ReplyAsync("Sorry, only Announcers can use the !annouce command.");
                }
            }
        }
    }
}
