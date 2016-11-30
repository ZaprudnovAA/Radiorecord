using System;

namespace Radio
{
    class vars
    {
        public static int WhoIsPlaying = 0;
        public static string aName = "Radiorecord";
        public static int notifyTimeout = 30000;
        public static string tNotifInfo = "info";
        public static string tNotifWarn = "warning";
        public static string tNotifErro = "error";

        public static string _url = "http://air2.radiorecord.ru:805/";
        public static string[] _prefixes = new String[25] { "", "rr", "mix", "deep", "club", "fut", "tm", "chil", "mini", "ps", "rus", "vip", "sd90", "brks", "dub", "dc", "techno", "teo", "trap", "pump", "rock", "mdl", "gop", "yo", "rave" };
        public static string[] _names = new String[25] {
            ""
            , "Radio Record"
            , "Megamix"
            , "Record Deep"
            , "Record EDM"
            , "Future House"
            , "Trancemission"
            , "Record Chill-Out"
            , "Minimal/Tech"
            , "Pirate Station"
            , "Russian Mix"
            , "Vip House"
            , "Супердиско 90-х"
            , "Record Breaks"
            , "Record Dubstep"
            , "Record Dancecore"
            , "Record Techno"
            , "Record Hardstyle"
            , "Record Trap"
            , "Pump"
            , "Record Rock"
            , "Медляк FM"
            , "Гоп FM"
            , "Yo!FM"
            , "Rave FM" };

        public static string radio_url_bitrate(int bitrate, int id)
        {
            switch (bitrate)
            {
                case 64:
                    bitrate = 64;
                    break;
                case 128:
                    bitrate = 128;
                    break;
                case 320:
                    bitrate = 320;
                    break;
                default:
                    bitrate = 128;
                    break;
            }
            return string.Format("{0}{1}_{2}.m3u", _url, _prefixes[id], bitrate);
        }
    }
}
