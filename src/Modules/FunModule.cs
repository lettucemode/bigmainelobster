using Discord.Commands;
using bigmainelobster.Preconditions;
using System.Threading.Tasks;
using System.Net;
using System;

namespace bigmainelobster.Modules
{

    [Name("Fun")]
    [RequireContext(ContextType.Guild)]
    public class FunModule : ModuleBase<SocketCommandContext>
    {
        private string[] chargeupgifs = new string[]
        {
            "http://24.media.tumblr.com/tumblr_m6gejd7WKq1roew1yo1_r1_500.gif",
            "http://68.media.tumblr.com/b4ff7363119c5447029898fd6fd84f64/tumblr_nwth548dSz1sjbr2bo1_500.gif",
            "http://pa1.narvii.com/6365/e20e333904c5a3a1b04d97cd34133336e37e69ca_hq.gif",
            "http://static1.comicvine.com/uploads/original/11128/111281580/5144112-tumblr_n8pd7gtiij1sbx7iyo1_500.gif",
            "http://68.media.tumblr.com/0a579f77174ddad41a55c4df77f82cec/tumblr_ntzvqzh3Dk1u7487lo1_500.gif",
            "http://fanbros.com/wp-content/uploads/2015/04/Buu-power-up.gif",
            "https://s-media-cache-ak0.pinimg.com/originals/26/b9/73/26b973e25ca47f9d5db92c1f8d85fb7b.gif",
            "https://jctunesmusic.files.wordpress.com/2015/11/tumblr_inline_nkm606v00r1sowhjk.gif?w=474",
            "https://i.imgtc.com/qI226wx.gif",
            "http://s8.favim.com/orig/150703/animation-anime-cool-dbz-Favim.com-2891565.gif",
            "https://s-media-cache-ak0.pinimg.com/originals/ca/25/68/ca2568f4c593a04c8d2adc5d5234a466.gif",
            "https://s-media-cache-ak0.pinimg.com/originals/d1/09/62/d10962a2086cf8d65960cbcf97b6f1ee.gif"
        };

        private Random rand = new Random();

        [Command("charge"), Alias("powerup")]
        [Remarks("Charge up your power level.")]
        [MinPermissions(AccessLevel.User)]
        public async Task Charge()
        {
            WebRequest req = WebRequest.Create(chargeupgifs[rand.Next(chargeupgifs.Length)]);
            using (WebResponse resp = req.GetResponseAsync().Result)
                await Context.Channel.SendFileAsync(resp.GetResponseStream(), "thing.gif");
        }
        
    }
}
