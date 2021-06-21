using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dusek_SEWBOT
{
    public class UtilityModule : ModuleBase<SocketCommandContext>
    {
        [Command("morsecode")]
        [Summary("Convert text from or to morse code.")]
        public async Task MorseCodeConverterAsync([Remainder] string text = null)
        {
            EmbedBuilder embed = new EmbedBuilder
            {
                Title = "Morsecode Converter",
                Color = Color.Red,
                Footer = new EmbedFooterBuilder { Text = Defaults.dEmbedFooter },
                Description = "No text entered!",
            };
            embed.WithCurrentTimestamp();
            
            switch (text)
            {
                case null:
                    await ReplyAsync(embed: embed.Build());
                    return;
            }

            Dictionary<string, string> MORSECODE = new Dictionary<string, string>
            {
                ["/"] = " ",
                ["......."] = " ",
                [".-"] = "a",
                ["-..."] = "b",
                ["-.-."] = "c",
                ["-.."] = "d",
                ["."] = "e",
                ["..-."] = "f",
                ["--."] = "g",
                ["...."] = "h",
                [".."] = "i",
                [".---"] = "j",
                ["-.-"] = "k",
                [".-.."] = "l",
                ["--"] = "m",
                ["-."] = "n",
                ["---"] = "o",
                [".--."] = "p",
                ["--.-"] = "q",
                [".-."] = "r",
                ["..."] = "s",
                ["-"] = "t",
                ["..-"] = "u",
                ["...-"] = "v",
                [".--"] = "w",
                ["-..-"] = "x",
                ["-.--"] = "y",
                ["--.."] = "z",
                [".----"] = "1",
                ["..---"] = "2",
                ["...--"] = "3",
                ["....-"] = "4",
                ["....."] = "5",
                ["-...."] = "6",
                ["--..."] = "7",
                ["---.."] = "8",
                ["----."] = "9",
                ["-----"] = "0",
                ["--..--"] = "",
                [".-.-.-"] = ".",
                ["-.-.-"] = ";",
                ["---..."] = ":",
                ["..--.-"] = "_",
                [".----."] = "'",
                [".-.-."] = "+",
                ["..--.."] = "?",
                ["-...-"] = "=",
                ["-.--.-"] = ")",
                ["-.--."] = "(",
                ["-..-."] = "/",
                [".-..."] = "&",
                ["...-..-"] = "$",
                [".-..-."] = "\"",
                ["-.-.--"] = "!",
                [".--.-."] = "@"
            };

            bool morseflag = false;
            foreach (string s in MORSECODE.Keys)
            {
                if (text.Contains(s)) {
                    morseflag = true;
                }
            }

            string ftext = "";

            switch (morseflag)
            {
                case true:
                    string[] morsetext = text.Split(" ");
                    foreach (string c in morsetext)
                    {
                        try
                        {
                            ftext += MORSECODE[c];
                        }
                        catch { }
                    }
                    break;
                default:
                    text = text.ToLower();
                    foreach (char c in text)
                    {
                        try
                        {
                            ftext += $" {MORSECODE.FirstOrDefault(x => x.Value == c.ToString()).Key} ";
                        }
                        catch { }
                    }
                    break;
            }
            await ReplyAsync(ftext);
        }
    }
}