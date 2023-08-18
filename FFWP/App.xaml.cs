using FFWP.Modules;
using FFWP.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows;
using nativeGapple;

namespace FFWP
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
        bool restart = false;
        string mode;


        string down = "0";
        string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/";
        string req = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/req";
        string ttf = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/req/base.ttf";
        string ttfexe = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/req/fontloader.exe";
        string backup = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/req/backup.xml";
        string basedata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/req/base.xml";
        string basecopy = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/user/base.xml";
        string backupcopy = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/user/backup.xml";
        string usr = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/user/";

        string localdir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/";
        string locallang = CultureInfo.CurrentUICulture.ToString();
        WebClient webClient = new WebClient();


        string opsys;
        App()
        {
            if (CheckInt.IsInternetAvailable() == true)
            {

                try
                {

                    load();
                    nativeGapple.Modules.StartTimer();
                    CHU();
                    

                }
                catch
                {
                    MessageBox.Show("Failure at Load(); Contact via GitHub.");

                }
            }
            else
            {

            }

            async void PCheck()
            {

            }

            async void CHU() {

                try
                {
                    SaveINI set = new SaveINI(localfile);
                    string afterclose = set.Read("LocalSettingsForGapple", "ExitAfterLaunch");
                    string checkupdates = set.Read("LocalSettingsForGapple", "CheckUpdates");

                    string languag = set.Read("LocalSettingsForGapple", "Lang");

                   
                    if (checkupdates == "YES")
                    {
                        FFWP.JsonData.Value.CheckUp = true;
                    }
                    if (checkupdates == "NO")
                    {
                        FFWP.JsonData.Value.CheckUp = false;
                    }
                }
                catch
                {
                    

                }

            }

            async void load()
            {
                nativeGapple.Modules.DirectoryControl();
                nativeGapple.Modules.FileControl();
               
                if (!File.Exists(localfile))
                {
                    nativeGapple.Modules.SettingsControl();
                    if (locallang.Contains("de-DE"))
                    {
                        SaveINI set = new SaveINI(localfile);
                        set.Write("LocalSettingsForGapple", "Lang", "de-DE");
                    }
                    if (locallang.Contains("tr-TR"))
                    {
                        SaveINI set = new SaveINI(localfile);
                        set.Write("LocalSettingsForGapple", "Lang", "tr-TR");
                    }
                    if (locallang.Contains("en-US"))
                    {
                        SaveINI set = new SaveINI(localfile);
                        set.Write("LocalSettingsForGapple", "Lang", "en-US");
                    }
                    if (locallang.Contains("es-ES"))
                    {
                        SaveINI set = new SaveINI(localfile);
                        set.Write("LocalSettingsForGapple", "Lang", "es-ES");
                    }
                    if (locallang.Contains("fr-FR"))
                    {
                        SaveINI set = new SaveINI(localfile);
                        set.Write("LocalSettingsForGapple", "Lang", "fr-FR");
                    }
                    if (locallang.Contains("zh"))
                    {
                        SaveINI set = new SaveINI(localfile);
                        set.Write("LocalSettingsForGapple", "Lang", "zh");
                    }

                    FFWP.Modules.SaveINI first = new FFWP.Modules.SaveINI(localfile);
                    string langu = first.Read("LocalSettingsForGapple", "Lang");
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langu);
                }
                else
                {
                    string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
                    FFWP.Modules.SaveINI first = new FFWP.Modules.SaveINI(localfile);
                    string langu = first.Read("LocalSettingsForGapple", "Lang");
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langu);
                }
                nativeGapple.Modules.CheckOS();
                
                if (nativeGapple.Modules.OSResult == "supported")
                {

                }
                if (nativeGapple.Modules.OSResult == "unsupported")
                {
                    MessageBox.Show(FFWP.Language.Strings.uop, "FFGapple");
                    Environment.Exit(-1);
                }
            }
        }
    }
}


