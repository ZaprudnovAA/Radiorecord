using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace Radio
{
    internal static class Vars
    {
        public static int WhoIsPlaying =
            Convert.ToInt32(RegistryWorker.GetValue(RegistryWorker.FavoriteStationName)) == 0
                ? 1
                : Convert.ToInt32(RegistryWorker.GetValue(RegistryWorker.FavoriteStationName));
        public const string AName = "Radiorecord";
        public const int NotifyTimeout = 30000;
        public const int PlayPauseButtonId = 150;
        private const int DefaultBitrate = 128;
        private const int DefaultVolume = 80;
        public static int UsersBitrate =
            Convert.ToInt32(RegistryWorker.GetValue(RegistryWorker.FavoriteBitrateName)) == 0
                ? DefaultBitrate
                : Convert.ToInt32(RegistryWorker.GetValue(RegistryWorker.FavoriteBitrateName));
        public static int UsersVolume =
            Convert.ToInt32(RegistryWorker.GetValue(RegistryWorker.FavoriteVolumeName)) == 0
                ? DefaultVolume
                : Convert.ToInt32(RegistryWorker.GetValue(RegistryWorker.FavoriteVolumeName));

        private const string Url = "http://air.radiorecord.ru:805/";
        public static readonly List<Stations> StationList = new List<Stations>();

        public static string radio_url_bitrate(int bitrate, int id)
        {
            bitrate = bitrate == 64 || bitrate == 128 || bitrate == 320 ? bitrate : 128;
            UsersBitrate = bitrate;
            return $"{Url}{StationList.Find(x => x.Id == id).Prefix}_{bitrate}.m3u";
        }

        public class Stations
        {
            public int Id { get; set; }
            public string Prefix { get; set; }
            public string Name { get; set; }
            public Image Image { get; set; }
        }

        public static bool ListOfStations()
        {
            var doc = new XmlDocument();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                doc.Load($"{Update.Url}{Update.StationsFile}");

                XmlNodeList nodes = doc.GetElementsByTagName("station");
                if (nodes.Count == 0)
                {
                    MessageBox.Show(@"The list of stations is empty!", AName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new Exception();
                }

                for (var i = 0; i < nodes.Count; i++)
                {
                    StationList.Add(new Stations
                    {
                        Id = Convert.ToInt32(nodes[i].Attributes["id"].Value),
                        Prefix = nodes[i].Attributes["prefix"].Value,
                        Name = nodes[i].Attributes["name"].Value,
                        Image = LoadImage(nodes[i].Attributes["image"].Value)
                    });
                }

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show($@"An error occurred while loading the xml data file! Please check the internet connection. Exception details: {e.Message}",
                        AName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (Program.F != null) Program.F.Close();
                Application.Exit();
            }

            return false;
        }

        private static Image LoadImage(string base64Image)
        {
            byte[] bytes = Convert.FromBase64String(base64Image);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }
    }
}
