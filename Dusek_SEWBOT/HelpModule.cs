using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dusek_SEWBOT
{
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        [Summary("Recieve given help page.")]
        public async Task HelpAsync(string page = null, [Remainder] string xargs = null)
        {

            EmbedBuilder embed = new EmbedBuilder
            {
                Title = "Help",
                Color = Color.Green,
                Footer = new EmbedFooterBuilder { Text = Defaults.dEmbedFooter },
                ThumbnailUrl = Defaults.dBotPFPUrl,
            };
            embed.WithCurrentTimestamp();

            switch (page)
            {
                case null:
                    embed.WithTitle("Help Pages:");
                    embed.AddField("General", $"`{Defaults.dPrefix}help general`");
                    embed.AddField("Fun", $"`{Defaults.dPrefix}help fun`");
                    embed.AddField("Utility", $"`{Defaults.dPrefix}help utility`");
                    break;
                case "general":
                    embed.WithTitle("General Help:");
                    embed.WithDescription("This will be the general help page.");
                    break;
                case "fun":
                    string desc = "Applies a filter to an image.\n" +
                        $"Usage: Send an image with the caption `{Defaults.dPrefix}filter <filter>`\n" +
                        "Filters:\n" +
                        "`invert`\n" +
                        "`greyscale`\n" +
                        "`comic`\n" +
                        "`gotham`\n" +
                        "`hisatch`\n" +
                        "`lomograph`\n" +
                        "`losatch`\n" +
                        "`polaroid`\n" +
                        "`sepia`";
                    embed.WithTitle("Fun Commands:");
                    embed.AddField($"{Defaults.dPrefix}filter", desc);
                    break;
                case "utility":
                    embed.AddField($"{Defaults.dPrefix}morsecode", $"Usage: `{Defaults.dPrefix}morsecode <text here>`\nConverts any text to morse code or back.");
                    break;
                default:
                    embed.WithTitle("Error:");
                    embed.WithDescription($"There is no help page called \"{page}\".");
                    embed.WithColor(Color.Red);
                    break;
            }
            await ReplyAsync(embed: embed.Build());
        }
    }
}