using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace nativeGapple
{
    public class Modules
    {
        public static System.Timers.Timer _timer;
        static string popup = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/content/popup.gif";
        static string content = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/content/";
        static string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/";
        static string req = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/req";
        static string backup = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/req/backup.xml";
        static string basedata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/req/base.xml";
        static string basecopy = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/user/base.xml";
        static string backupcopy = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/user/backup.xml";
        static string usr = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/user/";
        static string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
        static string localdir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/";
        public static string userjson = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"//FFGapple Profiles//FFGapple Launcher//user//FFGapple.1337";
        static WebClient webClient = new WebClient();
        public static string OSResult;
        public static string NavStatus;
        public static string ProfileMode;
        public static string Refresh;
        public static string UserAgree;
        public static string ProfileEditing;
        public static string EditingMode;
        public static string ProfileData;
        public static string NativeRuntime = "4.5.0.0";
        public static string SquirrelDone;
        public static bool Online, Profiles, Settings, IsEnabled;
        static RegistryKey Controller = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FFGappleLNC");
        static RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FFGappleLNC");

        public static void DirectoryControl()
        {
            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                if (!Directory.Exists(req))
                {
                    Directory.CreateDirectory(req);
                }
                if (!Directory.Exists(usr))
                {
                    Directory.CreateDirectory(usr);
                }
                if (!Directory.Exists(localdir))
                {
                    Directory.CreateDirectory(localdir);
                }
                if (!Directory.Exists(content))
                {
                    Directory.CreateDirectory(content);
                }
            }
            catch
            {
            
                Environment.Exit(-1);
            }
        }

        public static void StartTimer()
        {
            _timer = new System.Timers.Timer(100);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }

        public static void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                string X3 = key.GetValue("X3").ToString();
            }
            catch
            {
                
                key.SetValue("X3", "");

            }
            try
            {
                string X2 = key.GetValue("X2").ToString();
            }
            catch
            {
                key.SetValue("X2", "");
            }
            try
            {
                string X1 = key.GetValue("X1").ToString();
            }
            catch
            {

                key.SetValue("X1", "");
            }
          
          

            try
            {
                string X3 = key.GetValue("X3").ToString();
                string X2 = key.GetValue("X2").ToString();
                string X1 = key.GetValue("X1").ToString();
                if (X3 == "Enabled")
                {
                    nativeGapple.Modules.Settings = true;
                }
                else
                {
                    nativeGapple.Modules.Settings = false;
                }
                if (X2 == "Enabled")
                {
                    nativeGapple.Modules.Online = true;
                }
                else
                {
                    nativeGapple.Modules.Online = false;
                }
                if (X1 == "Enabled")
                {
                    nativeGapple.Modules.Profiles = true;
                }
                else
                {
                    nativeGapple.Modules.Profiles = false;
                }
            }
            catch 
            {

              
            }
        }

        public static void FileControl()
        {
            if (!File.Exists(userjson))
            {
                FileStream mvs = File.Create(userjson);
                mvs.Close();
            }
            //if (!File.Exists(basedata))
            //{
            //    try
            //    {
            //        webClient.DownloadFile("https://raw.githubusercontent.com/ffgapple/ffgapple.github.io/master/backup/base.xml", basedata);
            //    }
            //    catch 
            //    {
            //        MessageBox.Show("Base");
            //        Environment.Exit(-1);
            //    }
            //}
            if (!File.Exists(popup))
            {
                try
                {
                    webClient.DownloadFile("https://raw.githubusercontent.com/ffgapple/ffgapple.github.io/master/images/popup.gif", popup);
                }
                catch
                {
                   
                    Environment.Exit(-1);
                }
            }

        }

        public static void SettingsControl()
        {
            if (!File.Exists(localfile))
            {

                try
                {
                    webClient.DownloadFile("https://ffgapple.github.io/backup/local.ini", localfile);
                }
                catch
                {
                 
                    Environment.Exit(-1);
                }
            }
        }

        public static void CheckOS()
        {
            var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                        select x.GetPropertyValue("Caption")).FirstOrDefault();
            string opsys = name.ToString();

            if (opsys.Contains("11"))
            {
                OSResult = "supported";
            }
            else if (opsys.Contains("10"))
            {
                OSResult = "supported";
            }
            else if (opsys.Contains("8.1"))
            {
                OSResult = "unsupported";
            }
            else if (opsys.Contains("7"))
            {
                OSResult = "unsupported";
            }
            else
            {
                OSResult = "unsupported";
            }
        
    }
    }
}
