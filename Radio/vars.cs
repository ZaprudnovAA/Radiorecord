using System;

namespace Radio
{
    class vars
    {
        //Глобальные переменные
        public static int WhoIsPlaying = 0;
        public static string WhoIsPlaying_str = "Stop";
        public string aName = "Radiorecord";
        public int mInterval = 180000;
        public int notifyTimeout = 30000;
        public string tNotifInfo = "info";
        public string tNotifWarn = "warning";
        public string tNotifErro = "error";

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

            return radio_url_[id] + bitrate + ".m3u";
        }
        public static string[] radio_url_ = new String[22]
        {
            "",
            "http://online.radiorecord.ru:8102/brks_",
            "",
            "http://online.radiorecord.ru:8102/chil_",
            "http://online.radiorecord.ru:8102/club_",
            "http://online.radiorecord.ru:8102/dc_",
            "http://online.radiorecord.ru:8102/deep_",
            "http://online.radiorecord.ru:8102/dub_",
            "http://online.radiorecord.ru:8102/gop_",
            "http://online.radiorecord.ru:8102/mdl_",
            "http://online.radiorecord.ru:8102/mix_",
            "http://online.radiorecord.ru:8102/ps_",
            "http://online.radiorecord.ru:8102/pump_",
            "http://online.radiorecord.ru:8102/rock_",
            "http://online.radiorecord.ru:8102/rus_",
            "http://online.radiorecord.ru:8102/sd90_",
            "http://online.radiorecord.ru:8102/techno_",
            "http://online.radiorecord.ru:8102/teo_",
            "http://online.radiorecord.ru:8102/tm_",
            "http://online.radiorecord.ru:8102/trap_",
            "http://online.radiorecord.ru:8102/vip_",
            "http://online.radiorecord.ru:8102/yo_"
        };
        public static string[] radio_url = new String[22]
        {
            "",
            "http://online.radiorecord.ru:8102/brks_320.m3u",
            "",
            "http://online.radiorecord.ru:8102/chil_320.m3u",
            "http://online.radiorecord.ru:8102/club_320.m3u",
            "http://online.radiorecord.ru:8102/dc_320.m3u",
            "http://online.radiorecord.ru:8102/deep_320.m3u",
            "http://online.radiorecord.ru:8102/dub_320.m3u",
            "http://online.radiorecord.ru:8102/gop_320.m3u",
            "http://online.radiorecord.ru:8102/mdl_320.m3u",
            "http://online.radiorecord.ru:8102/mix_320.m3u",
            "http://online.radiorecord.ru:8102/ps_320.m3u",
            "http://online.radiorecord.ru:8102/pump_320.m3u",
            "http://online.radiorecord.ru:8102/rock_320.m3u",
            "http://online.radiorecord.ru:8102/rus_320.m3u",
            "http://online.radiorecord.ru:8102/sd90_320.m3u",
            "http://online.radiorecord.ru:8102/techno_320.m3u",
            "http://online.radiorecord.ru:8102/teo_320.m3u",
            "http://online.radiorecord.ru:8102/tm_320.m3u",
            "http://online.radiorecord.ru:8102/trap_320.m3u",
            "http://online.radiorecord.ru:8102/vip_320.m3u",
            "http://online.radiorecord.ru:8102/yo_320.m3u"
        };
        public static string[] radio_label = new String[21]
        {
            "Record Breaks",
            "Record Chill-Out",
            "Record Club",
            "Record Dancecore",
            "Record Deep",
            "Record Dubstep",
            "Гоп FM",
            "Медляк FM",
            "Megamix",
            "Pirate Station",
            "Pump'n'Klubb",
            "Record Rock",
            "Russian Mix",
            "Супердискотека 90-х",
            "Record Techno",
            "Record Hardstyle",
            "Trancemission",
            "Record Trap",
            "Vip Mix",
            "Yo! FM",
            ""
        };
    }
}
