using CmlLib.Core.Auth;
using CmlLib.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;

namespace FFWP.JsonData
{

    public class Value
    {
        public static String str, id, directport, width, heigth, directserver, version, directory, online, email, pass, afterclose, updateoption, secret;
        public static int codirectport, cowidth, coheigth;
        public static string decryptedmail, decryptedpass, decryptedusername, lcontent;
        public static string serial = "4.3";
        public static string popup = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/content/popup.gif";
        public static string content = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/content/";
        public static string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/";
        public static string req = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/req";
        public static string backup = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/req/backup.xml";
        public static string basedata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/req/base.xml";
        public static string basecopy = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/user/base.xml";
        public static string backupcopy = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/user/backup.xml";
        public static string usr = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/user/";
        public static string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
        public static string localdir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/";
        public static string userjson = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"//FFGapple Profiles//FFGapple Launcher//user//FFGapple.1337";
        public static string locallang = CultureInfo.CurrentUICulture.ToString();
        public static bool CheckUp;
        public static bool Checked;
        public static bool ParentalControls;
        double ramgoritmaninrami;
        WebClient webClient = new WebClient();
        MLaunchOption option = new MLaunchOption();
        private MSession session;
        string opsys;
        private static string versionl;
        bool outdated;
        int versionprogram;
        public static bool developer;
        string sessionid;
        bool firstime;
        bool error;
        string RunningNow;
        bool Stop;
        string StopFr;
        bool DontRefresh;
        bool RefreshValue;
        bool RefreshAll;
        string jsondat;
        public static string ExecVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
    public class Root
    {
        public List<Profile> Profiles { get; set; }
        public List<Update> Update { get; set; }
    }

    public class Profile
    {
        public string username { get; set; }
        public string idjson { get; set; }
        public string secret { get; set; }
        public string directserver { get; set; }
        public string directport { get; set; }
        public string width { get; set; }
        public string heigth { get; set; }
        public string xversion { get; set; }
        public string codename { get; set; }
        public string directory { get; set; }
        public string online { get; set; }
        public string creationdate { get; set; }
        public string modded { get; set; }
        public string customversion { get; set; }
        public string img { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
        public string rungapple { get; set; }
        public string ffmem { get; set; }
    }
    public class Update
    {
        public string Version { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public string Date { get; set; }
        public string German { get; set; }
        public string Spanish { get; set; }
        public string French { get; set; }
        public string English { get; set; }
        public string Turkish { get; set; }
        public string Chinese { get; set; }
    }


   
}
