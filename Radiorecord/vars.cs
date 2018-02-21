using System.Collections.Generic;
using System.Drawing;
using Radio.Properties;

namespace Radio
{
    internal class Vars
    {
        public static int WhoIsPlaying = 0;
        public const string AName = "Radiorecord";
        public const int NotifyTimeout = 30000;
        public const string NotifInfo = "info";
        public const string NotifWarn = "warning";
        public const string NotifErro = "error";
        public const int PlayPauseButtonId = 50;
        public const int DefaultBitrate = 128;

        public const string Url = "http://air.radiorecord.ru:805/";
        public static List<Stations> StationList = new List<Stations>();

        public static string radio_url_bitrate(int bitrate, int id)
        {
            bitrate = bitrate == 64 || bitrate == 128 || bitrate == 320 ? bitrate : 128;
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
            StationList.Add(new Stations { Id = 3, Prefix = "deep", Name = "Record Deep", Image = Resources.deep });
            StationList.Add(new Stations { Id = 4, Prefix = "club", Name = "Record EDM", Image = Resources.club });
            StationList.Add(new Stations { Id = 5, Prefix = "fut", Name = "Future House", Image = Resources.fut });
            StationList.Add(new Stations { Id = 6, Prefix = "tm", Name = "Trancemission", Image = Resources.tm });
            StationList.Add(new Stations { Id = 7, Prefix = "chil", Name = "Record Chill-Out", Image = Resources.chil });
            StationList.Add(new Stations { Id = 8, Prefix = "mini", Name = "Minimal/Tech", Image = Resources.mini });
            StationList.Add(new Stations { Id = 9, Prefix = "ps", Name = "Pirate Station", Image = Resources.ps });
            StationList.Add(new Stations { Id = 10, Prefix = "rus", Name = "Russian Mix", Image = Resources.rus });
            StationList.Add(new Stations { Id = 11, Prefix = "vip", Name = "Vip House", Image = Resources.vip });
            StationList.Add(new Stations { Id = 12, Prefix = "sd90", Name = "Супердиско 90-х", Image = Resources.sd90 });
            StationList.Add(new Stations { Id = 13, Prefix = "brks", Name = "Record Breaks", Image = Resources.brks });
            StationList.Add(new Stations { Id = 14, Prefix = "dub", Name = "Record Dubstep", Image = Resources.dub });
            StationList.Add(new Stations { Id = 15, Prefix = "dc", Name = "Record Dancecore", Image = Resources.dc });
            StationList.Add(new Stations { Id = 16, Prefix = "techno", Name = "Record Techno", Image = Resources.techno });
            StationList.Add(new Stations { Id = 17, Prefix = "teo", Name = "Record Hardstyle", Image = Resources.teo });
            StationList.Add(new Stations { Id = 18, Prefix = "trap", Name = "Record Trap", Image = Resources.trap });
            StationList.Add(new Stations { Id = 19, Prefix = "pump", Name = "Pump", Image = Resources.pump });
            StationList.Add(new Stations { Id = 20, Prefix = "rock", Name = "Record Rock", Image = Resources.rock });
            StationList.Add(new Stations { Id = 21, Prefix = "mdl", Name = "Медляк FM", Image = Resources.mdl });
            StationList.Add(new Stations { Id = 22, Prefix = "gop", Name = "Гоп FM", Image = Resources.gop });
            StationList.Add(new Stations { Id = 23, Prefix = "yo", Name = "Yo!FM", Image = Resources.yo });
            StationList.Add(new Stations { Id = 24, Prefix = "rave", Name = "Rave FM", Image = Resources.rave });
            StationList.Add(new Stations { Id = 25, Prefix = "trop", Name = "Tropical", Image = Resources.trop });
            StationList.Add(new Stations { Id = 26, Prefix = "naft", Name = "Нафталин ФМ", Image = Resources.naft });
            StationList.Add(new Stations { Id = 27, Prefix = "goa", Name = "GOA/PSY", Image = Resources.goa });
            StationList.Add(new Stations { Id = 28, Prefix = "gold", Name = "Gold", Image = Resources.gold });
            StationList.Add(new Stations { Id = 29, Prefix = "mf", Name = "Маятник Фуко", Image = Resources.mf });
            StationList.Add(new Stations { Id = 30, Prefix = "fbass", Name = "Future Bass", Image = Resources.fbass });
            StationList.Add(new Stations { Id = 31, Prefix = "rmx", Name = "Remix", Image = Resources.rmx });
            StationList.Add(new Stations { Id = 32, Prefix = "gast", Name = "Гастарбайтер", Image = Resources.gast });
            StationList.Add(new Stations { Id = 33, Prefix = "hbass", Name = "Hard Bass", Image = Resources.hbass });
            StationList.Add(new Stations { Id = 34, Prefix = "ansh", Name = "Аншлаг FM", Image = Resources.ansh });
            StationList.Add(new Stations { Id = 35, Prefix = "ibiza", Name = "Innocence (Ibiza)", Image = Resources.ibiza });
        }
    }
}
