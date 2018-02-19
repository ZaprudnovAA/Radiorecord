using System.Collections.Generic;

namespace Radio
{
    class vars
    {
        public static int WhoIsPlaying = 0;
        public const string aName = "Radiorecord";
        public const int notifyTimeout = 30000;
        public const string tNotifInfo = "info";
        public const string tNotifWarn = "warning";
        public const string tNotifErro = "error";

        public const string _url = "http://air.radiorecord.ru:805/";
        public static List<Stations> StationList = new List<Stations>();

        public static void ListOfStations()
        {
            StationList.Add(new Stations() { _id = 1, _prefix = "rr", _name = "Radio Record", _image = global::Radio.Properties.Resources.rr });
            StationList.Add(new Stations() { _id = 2, _prefix = "mix", _name = "Megamix", _image = global::Radio.Properties.Resources.mix });
            StationList.Add(new Stations() { _id = 3, _prefix = "deep", _name = "Record Deep", _image = global::Radio.Properties.Resources.deep });
            StationList.Add(new Stations() { _id = 4, _prefix = "club", _name = "Record EDM", _image = global::Radio.Properties.Resources.club });
            StationList.Add(new Stations() { _id = 5, _prefix = "fut", _name = "Future House", _image = global::Radio.Properties.Resources.fut });
            StationList.Add(new Stations() { _id = 6, _prefix = "tm", _name = "Trancemission", _image = global::Radio.Properties.Resources.tm });
            StationList.Add(new Stations() { _id = 7, _prefix = "chil", _name = "Record Chill-Out", _image = global::Radio.Properties.Resources.chil });
            StationList.Add(new Stations() { _id = 8, _prefix = "mini", _name = "Minimal/Tech", _image = global::Radio.Properties.Resources.mini });
            StationList.Add(new Stations() { _id = 9, _prefix = "ps", _name = "Pirate Station", _image = global::Radio.Properties.Resources.ps });
            StationList.Add(new Stations() { _id = 10, _prefix = "rus", _name = "Russian Mix", _image = global::Radio.Properties.Resources.rus });
            StationList.Add(new Stations() { _id = 11, _prefix = "vip", _name = "Vip House", _image = global::Radio.Properties.Resources.vip });
            StationList.Add(new Stations() { _id = 12, _prefix = "sd90", _name = "Супердиско 90-х", _image = global::Radio.Properties.Resources.sd90 });
            StationList.Add(new Stations() { _id = 13, _prefix = "brks", _name = "Record Breaks", _image = global::Radio.Properties.Resources.brks });
            StationList.Add(new Stations() { _id = 14, _prefix = "dub", _name = "Record Dubstep", _image = global::Radio.Properties.Resources.dub });
            StationList.Add(new Stations() { _id = 15, _prefix = "dc", _name = "Record Dancecore", _image = global::Radio.Properties.Resources.dc });
            StationList.Add(new Stations() { _id = 16, _prefix = "techno", _name = "Record Techno", _image = global::Radio.Properties.Resources.techno });
            StationList.Add(new Stations() { _id = 17, _prefix = "teo", _name = "Record Hardstyle", _image = global::Radio.Properties.Resources.teo });
            StationList.Add(new Stations() { _id = 18, _prefix = "trap", _name = "Record Trap", _image = global::Radio.Properties.Resources.trap });
            StationList.Add(new Stations() { _id = 19, _prefix = "pump", _name = "Pump", _image = global::Radio.Properties.Resources.pump });
            StationList.Add(new Stations() { _id = 20, _prefix = "rock", _name = "Record Rock", _image = global::Radio.Properties.Resources.rock });
            StationList.Add(new Stations() { _id = 21, _prefix = "mdl", _name = "Медляк FM", _image = global::Radio.Properties.Resources.mdl });
            StationList.Add(new Stations() { _id = 22, _prefix = "gop", _name = "Гоп FM", _image = global::Radio.Properties.Resources.gop });
            StationList.Add(new Stations() { _id = 23, _prefix = "yo", _name = "Yo!FM", _image = global::Radio.Properties.Resources.yo });
            StationList.Add(new Stations() { _id = 24, _prefix = "rave", _name = "Rave FM", _image = global::Radio.Properties.Resources.rave });
            StationList.Add(new Stations() { _id = 25, _prefix = "trop", _name = "Tropical", _image = global::Radio.Properties.Resources.trop });
            StationList.Add(new Stations() { _id = 26, _prefix = "naft", _name = "Нафталин ФМ", _image = global::Radio.Properties.Resources.naft });
            StationList.Add(new Stations() { _id = 27, _prefix = "goa", _name = "GOA/PSY", _image = global::Radio.Properties.Resources.goa });
            StationList.Add(new Stations() { _id = 28, _prefix = "gold", _name = "Gold", _image = global::Radio.Properties.Resources.gold });
            StationList.Add(new Stations() { _id = 29, _prefix = "mf", _name = "Маятник Фуко", _image = global::Radio.Properties.Resources.mf });
            StationList.Add(new Stations() { _id = 30, _prefix = "fbass", _name = "Future Bass", _image = global::Radio.Properties.Resources.fbass });
            StationList.Add(new Stations() { _id = 31, _prefix = "rmx", _name = "Remix", _image = global::Radio.Properties.Resources.rmx });
            StationList.Add(new Stations() { _id = 32, _prefix = "gast", _name = "Гастарбайтер", _image = global::Radio.Properties.Resources.gast });
            StationList.Add(new Stations() { _id = 33, _prefix = "hbass", _name = "Hard Bass", _image = global::Radio.Properties.Resources.hbass });
            StationList.Add(new Stations() { _id = 34, _prefix = "ansh", _name = "Аншлаг FM", _image = global::Radio.Properties.Resources.ansh });
            StationList.Add(new Stations() { _id = 35, _prefix = "ibiza", _name = "Innocence (Ibiza)", _image = global::Radio.Properties.Resources.ibiza });
        }

        public static string radio_url_bitrate(int bitrate, int id)
        {
            bitrate = (bitrate == 64 || bitrate == 128 || bitrate == 320) ? bitrate : 128;
            return string.Format("{0}{1}_{2}.m3u", _url, StationList.Find((x) => x._id == id)._prefix, bitrate);
        }

        public class Stations
        {
            public int _id { get; set; }
            public string _prefix { get; set; }
            public string _name { get; set; }
            public System.Drawing.Image _image { get; set; }
        }
    }
}
