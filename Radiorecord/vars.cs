using System;
using System.Collections.Generic;
using System.Drawing;
using Radio.Properties;

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
        public const int PlayPauseButtonId = 50;
        public const int DefaultBitrate = 128;
        public const int DefaultVolume = 80;
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
            return string.Format("{0}{1}_{2}.m3u", Url, StationList.Find(x => x.Id == id).Prefix, bitrate);
        }

        public class Stations
        {
            public int Id { get; set; }
            public string Prefix { get; set; }
            public string Name { get; set; }
            public Image Image { get; set; }
        }

        public static void ListOfStations()
        {
            StationList.Add(new Stations { Id = 1, Prefix = "rr", Name = "Radio Record", Image = Resources.rr });
            StationList.Add(new Stations { Id = 2, Prefix = "mix", Name = "Megamix", Image = Resources.mix });
            StationList.Add(new Stations { Id = 3, Prefix = "deep", Name = "Deep", Image = Resources.deep });
            StationList.Add(new Stations { Id = 4, Prefix = "club", Name = "EDM", Image = Resources.club });
            StationList.Add(new Stations { Id = 5, Prefix = "fut", Name = "Future House", Image = Resources.fut });
            StationList.Add(new Stations { Id = 6, Prefix = "tm", Name = "Trancemission", Image = Resources.tm });
            StationList.Add(new Stations { Id = 7, Prefix = "chil", Name = "Chill-Out", Image = Resources.chil });
            StationList.Add(new Stations { Id = 8, Prefix = "mini", Name = "Minimal/Tech", Image = Resources.mini });
            StationList.Add(new Stations { Id = 9, Prefix = "ps", Name = "Pirate Station", Image = Resources.ps });
            StationList.Add(new Stations { Id = 10, Prefix = "rus", Name = "Russian Mix", Image = Resources.rus });
            StationList.Add(new Stations { Id = 11, Prefix = "vip", Name = "Vip House", Image = Resources.vip });
            StationList.Add(new Stations { Id = 12, Prefix = "sd90", Name = "Супердиско 90-х", Image = Resources.sd90 });
            StationList.Add(new Stations { Id = 13, Prefix = "brks", Name = "Breaks", Image = Resources.brks });
            StationList.Add(new Stations { Id = 14, Prefix = "dub", Name = "Dubstep", Image = Resources.dub });
            StationList.Add(new Stations { Id = 15, Prefix = "dc", Name = "Dancecore", Image = Resources.dc });
            StationList.Add(new Stations { Id = 16, Prefix = "techno", Name = "Techno", Image = Resources.techno });
            StationList.Add(new Stations { Id = 17, Prefix = "teo", Name = "Hardstyle", Image = Resources.teo });
            StationList.Add(new Stations { Id = 18, Prefix = "trap", Name = "Trap", Image = Resources.trap });
            StationList.Add(new Stations { Id = 19, Prefix = "pump", Name = "Old School", Image = Resources.pump });
            StationList.Add(new Stations { Id = 20, Prefix = "rock", Name = "Rock", Image = Resources.rock });
            StationList.Add(new Stations { Id = 21, Prefix = "mdl", Name = "Медляк FM", Image = Resources.mdl });
            StationList.Add(new Stations { Id = 22, Prefix = "gop", Name = "Гоп FM", Image = Resources.gop });
            StationList.Add(new Stations { Id = 23, Prefix = "yo", Name = "Black", Image = Resources.yo });
            StationList.Add(new Stations { Id = 24, Prefix = "rave", Name = "Rave FM", Image = Resources.rave });
            StationList.Add(new Stations { Id = 25, Prefix = "trop", Name = "Tropical", Image = Resources.trop });
            StationList.Add(new Stations { Id = 26, Prefix = "naft", Name = "Нафталин FM", Image = Resources.naft });
            StationList.Add(new Stations { Id = 27, Prefix = "goa", Name = "GOA/PSY", Image = Resources.goa });
            StationList.Add(new Stations { Id = 28, Prefix = "gold", Name = "Gold", Image = Resources.gold });
            StationList.Add(new Stations { Id = 29, Prefix = "mf", Name = "Маятник Фуко", Image = Resources.mf });
            StationList.Add(new Stations { Id = 30, Prefix = "fbass", Name = "Future Bass", Image = Resources.fbass });
            StationList.Add(new Stations { Id = 31, Prefix = "rmx", Name = "Remix", Image = Resources.rmx });
            StationList.Add(new Stations { Id = 32, Prefix = "gast", Name = "Гастарбайтер", Image = Resources.gast });
            StationList.Add(new Stations { Id = 33, Prefix = "hbass", Name = "Hard Bass", Image = Resources.hbass });
            StationList.Add(new Stations { Id = 34, Prefix = "ansh", Name = "Аншлаг FM", Image = Resources.ansh });
            StationList.Add(new Stations { Id = 35, Prefix = "ibiza", Name = "Innocence (Ibiza)", Image = Resources.ibiza });
            StationList.Add(new Stations { Id = 36, Prefix = "symph", Name = "Симфония FM", Image = Resources.symph });
            StationList.Add(new Stations { Id = 37, Prefix = "elect", Name = "Electro", Image = Resources.elect });
            StationList.Add(new Stations { Id = 38, Prefix = "mt", Name = "Midtempo", Image = Resources.mt });
            StationList.Add(new Stations { Id = 39, Prefix = "mmbt", Name = "Moombahton", Image = Resources.mmbt });
            StationList.Add(new Stations { Id = 40, Prefix = "jackin", Name = "Jackin'/Garage", Image = Resources.jackin });
            StationList.Add(new Stations { Id = 41, Prefix = "progr", Name = "Progressive", Image = Resources.progr });
            StationList.Add(new Stations { Id = 42, Prefix = "househits", Name = "House Hits", Image = Resources.househits });
            StationList.Add(new Stations { Id = 43, Prefix = "synth", Name = "Synthwave", Image = Resources.synth });
            StationList.Add(new Stations { Id = 44, Prefix = "bighits", Name = "Big Hits", Image = Resources.bighits });
            StationList.Add(new Stations { Id = 45, Prefix = "dream", Name = "Dream Dance", Image = Resources.dream });
        }
    }
}
