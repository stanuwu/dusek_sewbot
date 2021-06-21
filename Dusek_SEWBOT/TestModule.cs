using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dusek_SEWBOT
{
    public class TestModule : ModuleBase<SocketCommandContext>
    {
        [Command("test")]
        [Summary("Test if the bot is working.")]
        public async Task TestAsync(string arg = null, [Remainder] string xargs = null)
        {
            await ReplyAsync("hi");
            switch (arg) {
                case null:
                    break;
                default:
                    await Context.Channel.SendMessageAsync(arg);
                    break;
            }
        }
    }
}