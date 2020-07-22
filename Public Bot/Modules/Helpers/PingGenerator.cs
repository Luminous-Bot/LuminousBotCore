using Discord.WebSocket;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot
{
    
    class PingGenerator
    {
        
        public class PingData
        {
            public partial class DiscordApiPing
            {
                public Period Period { get; set; }
                public List<MetricElement> Metrics { get; set; }
                public DiscordApiPingSummary Summary { get; set; }
            }

            public partial class MetricElement
            {
                public MetricMetric Metric { get; set; }
                public MetricSummary Summary { get; set; }
                public List<Datum> Data { get; set; }
            }

            public partial class Datum
            {
                public long Timestamp { get; set; }
                public long Value { get; set; }
            }

            public partial class MetricMetric
            {
                public string Name { get; set; }
                public string MetricIdentifier { get; set; }
                public DateTimeOffset CreatedAt { get; set; }
                public DateTimeOffset UpdatedAt { get; set; }
                public string Id { get; set; }
                public string MetricsProviderId { get; set; }
                public string MetricsDisplayId { get; set; }
                public DateTimeOffset MostRecentDataAt { get; set; }
                public bool Backfilled { get; set; }
                public DateTimeOffset LastFetchedAt { get; set; }
                public long BackfillPercentage { get; set; }
            }

            public partial class MetricSummary
            {
                public double Sum { get; set; }
                public double Mean { get; set; }
            }

            public partial class Period
            {
                public long Count { get; set; }
                public long Interval { get; set; }
                public string Identifier { get; set; }
            }

            public partial class DiscordApiPingSummary
            {
                public double Sum { get; set; }
                public double Mean { get; set; }
                public long Last { get; set; }
            }
        }
        static Image ChartImage = new Bitmap(950, 600);
        static Graphics ChartGraphics = Graphics.FromImage(ChartImage);
        static Pen BlurplePen = new Pen(new SolidBrush(Color.FromArgb(114, 137, 218)), 3);
        static Pen WhitePen = new Pen(new SolidBrush(Color.FromArgb(255, 255, 255)), 4);
        static Pen WhitePenS = new Pen(new SolidBrush(Color.FromArgb(255, 255, 255)), 2);
        static Pen WhitePenSS = new Pen(new SolidBrush(Color.FromArgb(255, 255, 255)), 1);
        static Font Font = new Font("Bahnschrift", 9);
        static Font TitleFont = new Font("Bahnschrift", 18);
        public static async Task<Image> Generate(PingData.DiscordApiPing data)
        {
            ChartGraphics.Clear(Color.FromArgb(44, 47, 51));
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var curTime = DateTime.UtcNow;
            var mets = data.Metrics.First().Data;
            int yMin = 0;
            int yMax = (int)mets.Max(x => x.Value) + 10;
            float yOffset = ((float)ChartImage.Height - 120) / (yMax - yMin);
            float xOffset = ((float)ChartImage.Width - 120) / mets.Count;

            ChartGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            ChartGraphics.DrawLine(WhitePen, new PointF(ChartImage.Width - 60f, ChartImage.Height - 60f), new PointF(ChartImage.Width - 60f, 60f));
            ChartGraphics.DrawLine(WhitePen, new PointF(30f, ChartImage.Height - 60f), new PointF(ChartImage.Width - 60f, ChartImage.Height - 60f));

            ChartGraphics.DrawString("Time (Hours)", TitleFont, new SolidBrush(Color.White), ChartImage.Width / 2, ChartImage.Height - 30, new StringFormat() { Alignment = StringAlignment.Center });
            ChartGraphics.DrawString("Discord Ping (Past 24 Hours)", TitleFont, new SolidBrush(Color.White), ChartImage.Width / 2, 20, new StringFormat() { Alignment = StringAlignment.Center });

            for (float i = 1; i != yMax + 1; i++)
            {
                if (i % 5 == 0)
                {
                    ChartGraphics.DrawString($"{i}ms", Font, new SolidBrush(Color.FromArgb(255, 255, 255)), new PointF(ChartImage.Width - 50f, (ChartImage.Height - 60) - i * yOffset - 6));
                    ChartGraphics.DrawLine(WhitePenS, new PointF(ChartImage.Width - 60f, (ChartImage.Height - 60) - i * yOffset), new PointF(ChartImage.Width - 50f, (ChartImage.Height - 60) - i * yOffset));
                }
                if (i % 10 == 0)
                {
                    ChartGraphics.DrawLine(WhitePenSS, new PointF(30f, (ChartImage.Height - 60) - i * yOffset), new PointF(ChartImage.Width - 60f, (ChartImage.Height - 60) - i * yOffset));
                }
            }
            var hSpace = (ChartImage.Width - 120) / 24;
            for (float i = 1; i != hSpace; i++)
            {
                if (i % 4 == 0)
                {
                    ChartGraphics.DrawLine(WhitePenSS, new PointF((ChartImage.Width - 60) - i * hSpace, (ChartImage.Height - 60)), new PointF((ChartImage.Width - 60) - i * hSpace, 60f));
                    ChartGraphics.DrawString($"-{i}", Font, new SolidBrush(Color.FromArgb(255, 255, 255)), new PointF((ChartImage.Width - 60) - i * hSpace, (ChartImage.Height - 50)), new StringFormat() { Alignment = StringAlignment.Center });
                }
            }
            ChartGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            for (int i = 0; i != mets.Count - 2; i++)
            {
                var cur = mets[i];
                var nxt = mets[i + 1];
                ChartGraphics.DrawLine(BlurplePen, new PointF(xOffset * i + 60f, (ChartImage.Height - 60) - ((cur.Value) * yOffset)), new PointF(xOffset * (i + 1) + 60f, (ChartImage.Height - 60) - ((nxt.Value) * yOffset)));
            }

            return ChartImage;
        }
        public static async Task<string> GetImageLink(string fPath)
        {
            var client = new RestClient("https://drive.hapsy.net/upload");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "b3bb9d86-ca90-44ea-8836-ce44b08b52a0");
            request.AddFile("file", fPath);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content);
            return json["url"].ToString();
        }
    }
}
