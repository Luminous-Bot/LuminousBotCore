using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot
{
    class ProfileChangeHelper
    {
       
        public static Image PfpImage = new Bitmap(558, 360);
        public static Graphics PfpGraphics = Graphics.FromImage(PfpImage);
        public static WebClient wc = new WebClient();
        public static MemoryStream ms { get; set; }
        public static Color BackgroundColor = Color.FromArgb(47, 49, 54);
        public static Pen WhitePen = new Pen(new SolidBrush(Color.White), 4);
        static Font TitleFont = new Font("Bahnschrift", 26);
        public static async Task<string> BuildImage(string oldpfp, string newpfp, bool profile = true)
        {
            //Console.WriteLine("RUN");
            //await Task.Delay(10000);
            PfpGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            PfpGraphics.Clear(BackgroundColor);
            // Download both images:

            byte[] bytes = wc.DownloadData(oldpfp);
            ms = new MemoryStream(bytes);
            System.Drawing.Image oldPfp = System.Drawing.Image.FromStream(ms);

            byte[] bytes2 = wc.DownloadData(newpfp);
            ms = new MemoryStream(bytes2);
            System.Drawing.Image newPfp = System.Drawing.Image.FromStream(ms);


            PfpGraphics.DrawImage(oldPfp, 10f, ((PfpImage.Height - 20f) - 256f), 256f, 256f);

            PfpGraphics.DrawImage(newPfp, PfpImage.Width - 266, ((PfpImage.Height - 20f) - 256f), 256f, 256f);

            WhitePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            WhitePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            PfpGraphics.DrawLine(WhitePen, (PfpImage.Width / 2), 10, (PfpImage.Width / 2), PfpImage.Height - 10);
            string profle = "Profile";
            if (!profile)
            {
                profle = "Icon";
            }
            PfpGraphics.DrawString($"Old {profle}", TitleFont, new SolidBrush(Color.White), new Point(PfpImage.Width / 4, (PfpImage.Height - 276) / 2), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

            PfpGraphics.DrawString($"New {profle}", TitleFont, new SolidBrush(Color.White), new Point(PfpImage.Width - (PfpImage.Width / 4), (PfpImage.Height - 276) / 2), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

            //get image from hapsy
            PfpImage.Save($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}pfp.jpg", ImageFormat.Jpeg);
            return await PingGenerator.GetImageLink($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}pfp.jpg");
        }
    }
}
