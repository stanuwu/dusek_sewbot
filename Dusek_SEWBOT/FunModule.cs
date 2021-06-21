using Discord;
using Discord.Commands;
using ImageProcessor;
using ImageProcessor.Imaging.Filters.Photo;
using ImageProcessor.Imaging.Formats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dusek_SEWBOT
{
    public class FunModule : ModuleBase<SocketCommandContext>
    {
        [Command("filter")]
        [Summary("Applies filter to attatched image.")]
        public async Task ImageFilterAsync(string filter = null, [Remainder] string xargs = null)
        {
            bool comp = false;
            EmbedBuilder embed = new EmbedBuilder
            {
                Title = "Image Filter",
                Color = Color.Red,
                Footer = new EmbedFooterBuilder { Text = Defaults.dEmbedFooter },
            };
            embed.WithCurrentTimestamp();

            string send = null;
            byte[] fimg = null;

            switch (Context.Message.Attachments.Count)
            {
                case 0:
                    embed.WithDescription("No image attached!");
                    comp = true;
                    break;
                default:
                    Attachment att = Context.Message.Attachments.FirstOrDefault();
                    send = att.Url;

                    //bruh

                    using (var httpClient = new System.Net.Http.HttpClient())
                    {
                        var response = await httpClient.GetByteArrayAsync(send);
                        try
                        {
                            using MemoryStream inStream = new MemoryStream(response);
                            using ImageFactory imageFactory = new ImageFactory(preserveExifData: true);
                            using MemoryStream outStream = new MemoryStream();
                            imageFactory.Load(inStream);
                            switch (filter)
                            {
                                case null:
                                    embed.WithDescription("No filter given!");
                                    comp = true;
                                    break;
                                case "invert":
                                    imageFactory.Filter(MatrixFilters.Invert);
                                    break;
                                case "greyscale":
                                    imageFactory.Filter(MatrixFilters.BlackWhite);
                                    break;
                                case "comic":
                                    imageFactory.Filter(MatrixFilters.Comic);
                                    break;
                                case "gotham":
                                    imageFactory.Filter(MatrixFilters.Gotham);
                                    break;
                                case "hisatch":
                                    imageFactory.Filter(MatrixFilters.HiSatch);
                                    break;
                                case "lomograph":
                                    imageFactory.Filter(MatrixFilters.Lomograph);
                                    break;
                                case "losatch":
                                    imageFactory.Filter(MatrixFilters.LoSatch);
                                    break;
                                case "polaroid":
                                    imageFactory.Filter(MatrixFilters.Polaroid);
                                    break;
                                case "sepia":
                                    imageFactory.Filter(MatrixFilters.Sepia);
                                    break;
                                default:
                                    embed.WithDescription("Filter not found!");
                                    comp = true;
                                    break;
                            }
                            imageFactory.Save(outStream);
                            fimg = outStream.ToArray();
                        }
                        catch (Exception)
                        {
                            embed.WithDescription("Error converting image!");
                            comp = true;
                        }
                    }
                    break;
            }

            switch (comp)
            {
                case true:
                    await ReplyAsync(embed: embed.Build());
                    break;
                default:
                    using (var stream = new MemoryStream(fimg))
                        await Context.Channel.SendFileAsync(stream, "image.png");
                    break;
            }
            
        }
    }
}