using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot
{
    public class LeaderboardImageHelper
    {
        public static async Task<string> GetLeaderboardImageURL(List<LevelUser> LevelMembers, GuildLevelSettings settings)
        {
            var img = BuildImage(LevelMembers, settings.DefaultBaseLevelXp, settings.LevelMultiplier);
            img.Save($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Leaderboard.jpg", ImageFormat.Jpeg);
            return await PingGenerator.GetImageLink($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Leaderboard.jpg");
        }

        public static Image LeaderboardImage = new Bitmap(1500, 900);
        public static Graphics LeaderboardGraphics = Graphics.FromImage(LeaderboardImage);
        public static Color BackgroundColor = Color.FromArgb(47, 49, 54);
        public static Pen WhitePen = new Pen(new SolidBrush(Color.White), 4);
        public static Pen WhitePenSS = new Pen(new SolidBrush(Color.White), 1);
        public static Pen WhitePenS = new Pen(new SolidBrush(Color.White), 2);
        static Font TitleFont = new Font("Bahnschrift", 20);
        static Font SideFont = new Font("Bahnschrift", 10);
        static SolidBrush WhiteBrush = new SolidBrush(Color.White);
        public static float Spacer = 15;
        public static StringFormat Format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        private static Image BuildImage(List<LevelUser> Levels, uint DefaultXp = 30, double LevelMultiplier = 1.10409)
        {
            LeaderboardGraphics.Clear(BackgroundColor);
            LeaderboardGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            // Draw our bounding lines
            int _b = LeaderboardImage.Height - 50;
            int _w = LeaderboardImage.Width - 5;
            LeaderboardGraphics.DrawLine(WhitePen, 80, 5, 80, _b); // Y
            LeaderboardGraphics.DrawLine(WhitePen, 78, _b, _w, _b); // X

            // Draw "Levels" on the side
            LeaderboardGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            LeaderboardGraphics.TranslateTransform(30, LeaderboardImage.Height / 2);
            LeaderboardGraphics.RotateTransform(-90);
            LeaderboardGraphics.DrawString("Level", TitleFont, WhiteBrush, 0, 0, new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            });
            LeaderboardGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            LeaderboardGraphics.ResetTransform();
            // Calculate our "Working Area"
            int WorkingHeight = (int)(((LeaderboardImage.Height - 80) - 5) * 0.9);
            int WorkingWidth = ((LeaderboardImage.Width - 5) - 78);

            //Find 5% of the workign Height, as this will act as Padding
            int HeightPadding = (int)(((LeaderboardImage.Height - 80) - 5) * 0.05);

            // Calculate our mins & maxs
            uint _maxLevel = Levels.Max(x => x.CurrentLevel);
            uint _minLevel = Levels.Min(x => x.CurrentLevel);

            
            // Calculate our Pixel per XP & Pixel per Level
            float PPL = (float)WorkingHeight / ((float)_maxLevel - (float)_minLevel);

            // Calculate the Width of each "block" with the Spacer and WorkingWidth
            float BoxWidth = ((WorkingWidth - (Spacer * (Levels.Count + 1))) / Levels.Count);

            // Calculate the Level Spacing
            uint LevelSpacingCount = (_maxLevel - _minLevel) / 20;

            // Draw our background lines
            bool odd = false;
            for (int i = 0; i != (_maxLevel + 2) - _minLevel; i++)
            {
                if (i % LevelSpacingCount == 0)
                {
                    float Height = _b - (((_minLevel + i) - _minLevel) * PPL) - HeightPadding;

                    LeaderboardGraphics.DrawLine(
                        WhitePenSS,
                        78,
                        Height,
                        _w,
                        Height
                    );
                    if (odd)
                    {
                        LeaderboardGraphics.DrawLine(
                        WhitePenSS,
                        70,
                        Height,
                        78,
                        Height
                        );
                        LeaderboardGraphics.DrawString(
                            $"{_minLevel + i}",
                            SideFont,
                            WhiteBrush,
                            70,
                            Height,
                            new StringFormat()
                            {
                                Alignment = StringAlignment.Far,
                                LineAlignment = StringAlignment.Center
                            }
                        );
                        odd = false;
                    }
                    else
                        odd = true;
                }

            }

            // Calculate and draw our Name area
            int nameMax = Levels.Max(x => x.Username.Length);
            var BiggestName = Levels.Find(x => x.Username.Length == nameMax).Username;
            var size = LeaderboardGraphics.MeasureString("1st: " + BiggestName, TitleFont);
            float TextWidth = size.Width;
            float TextHeight = size.Height;
            float TextPadding = 5f;

            LeaderboardGraphics.FillRectangle(
                new SolidBrush(BackgroundColor),
                (LeaderboardImage.Width - TextWidth) - 11f,
                -1,
                TextWidth + 11,
                ((Levels.Count + 1) * TextPadding) + Levels.Count * TextHeight
            );

            LeaderboardGraphics.DrawRectangle(
                WhitePenS,
                (LeaderboardImage.Width - TextWidth) - 11f,
                -1,
                TextWidth + 11,
                ((Levels.Count + 1) * TextPadding) + Levels.Count * TextHeight
            );

            for (int i = 0; i != Levels.Count; i++)
            {
                var Level = Levels[i];

                Color color = Color.FromArgb(114, 137, 218);
                if (i == 0)
                    color = Color.Gold;
                else if (i == 1)
                    color = Color.Silver;
                else if (i == 2)
                    color = Color.FromArgb(205, 127, 50);

                LeaderboardGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                LeaderboardGraphics.DrawString(
                    (i + 1).DisplayWithSuffix() + ": " + Level.Username,
                    TitleFont,
                    new SolidBrush(color),
                    (LeaderboardImage.Width - TextWidth) - 10f,
                    ((TextHeight * i) + TextPadding * (i + 1))
                );
                LeaderboardGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            }

            // Start Drawing!
            bool Lifted = false;
            for (int i = Levels.Count - 1; i != -1; i--) // <- we draw from right to left
            {
                var Level = Levels[i];

                float Height = (Level.CurrentLevel - _minLevel) * PPL;
                float XpLevelOffset = ((float)Level.CurrentXP / (float)Level.NextLevelXP) * PPL;
                Height += HeightPadding;
                Height += XpLevelOffset;
                Color color = Color.FromArgb(114, 137, 218);
                if (i == 0)
                    color = Color.Gold;
                else if (i == 1)
                    color = Color.Silver;
                else if (i == 2)
                    color = Color.FromArgb(205, 127, 50);

                int PaddingOffset = (int)Spacer * (i + 1);

                float XPos = 80 + PaddingOffset + (BoxWidth * i);
                float YPos = _b - Height;
                LeaderboardGraphics.FillRectangle(new SolidBrush(color),
                    XPos,        // X
                    YPos,       //  Y
                    BoxWidth,  //   Width
                    Height - 2 //    Height
                );
                LeaderboardGraphics.DrawString(
                    $"Level {Level.CurrentLevel}",
                    TitleFont,
                    WhiteBrush,
                    (80 + PaddingOffset + (BoxWidth * i)) + (BoxWidth / 2),
                    LeaderboardImage.Height - 40,
                    new StringFormat()
                    {
                        Alignment = StringAlignment.Center
                    }
                );
            }



            return LeaderboardImage;
        }
    }
}
