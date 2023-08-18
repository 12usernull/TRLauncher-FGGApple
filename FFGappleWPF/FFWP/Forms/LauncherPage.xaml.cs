using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FFWP.Modules;
using CmlLib.Core.Auth;
using CmlLib.Core;
using ModernWpf.Controls;
using Windows.System;
using Windows.UI.Xaml.Controls;
using System.Xml.Linq;
using Microsoft.Toolkit.Uwp.Notifications;
using Squirrel;
using System.Threading;
using System.Runtime.Remoting.Contexts;
using System.Timers;
using Windows.Media.Ocr;
using CmlLib.Core.Downloader;
using FFWP.Dialogs;
using Newtonsoft.Json;
using System.Data;
using System.Drawing.Drawing2D;
using System.Runtime.ConstrainedExecution;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using static FFWP.Forms.LauncherPage;
using FFWP.JsonData;
using CmlLib.Core.Auth.Microsoft.UI.Wpf;
using Microsoft.Web.WebView2;
using Microsoft.Web.WebView2.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.Auth.Microsoft.MsalClient;
using Microsoft.Identity.Client;
using CmlLib.Core.Installer;

namespace FFWP.Forms
{
    /// <summary>
    /// Interaktionslogik für LauncherPage.xaml
    /// </summary>

    public partial class LauncherPage : System.Windows.Controls.Page
    {
        String str, id, directport, width, heigth, directserver, rungapple, customversion, version, directory, online, email, codename, pass, afterclose, updateoption, secret, username, img, ffmem;
        int codirectport, cowidth, coheigth;
        string decryptedmail, decryptedpass, decryptedusername, lcontent;
        string serial = "4.3";
        string popup = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/content/popup.gif";
        string content = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/content/";
        string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/";
        string req = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/req";
        string backup = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/req/backup.xml";
        string basedata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/req/base.xml";
        string basecopy = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/user/base.xml";
        string backupcopy = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/user/backup.xml";
        string usr = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/user/";
        string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
        string localdir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/";
        string userjson = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/user/FFGapple.1337";
        string locallang = CultureInfo.CurrentUICulture.ToString();
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
        bool AllesGut = true;

       // public class Profile
        //{
        //    public string idjson { get; set; }
        //    public string secret { get; set; }
        //    public string directserver { get; set; }
        //    public string directport { get; set; }
        //    public string width { get; set; }
        //    public string heigth { get; set; }
        //    public string xversion { get; set; }
        //    public string directory { get; set; }
        //    public string online { get; set; }
        //    public string email { get; set; }
        //    public string pass { get; set; }
        //    public string creationdate { get; set; }
        //}

        //public class Root
        //{
        //    public List<Profile> Profiles { get; set; }
        //}

        public void TakeJSON()
        {
            bool Stop = false;
            string Based = @"{
  ""Profiles"": [
    {
      ""username"": ""ffgapple"",
      ""idjson"": ""FFGapple Launcher"",
      ""secret"": ""1337"",
      ""directserver"": """",
      ""directport"": """",
      ""width"": """",
      ""heigth"": """",
      ""xversion"": ""1.20"",
      ""rungapple"": ""Yes"",
      ""codename"": ""1.20"",
      ""directory"": """",
      ""online"": ""no"",
      ""creationdate"": ""07.08.2023"",
      ""modded"": null,
      ""customversion"": ""Vanilla"",
      ""img"": ""https://minotar.net/cube/ffgapple/100.png"",
      ""email"": """",
      ""pass"": """",
      ""ffmem"": ""1024""
    }
  ]
}

";
            try
            {

                string url = FFWP.JsonData.Value.userjson;
                var jsonVerisi = File.ReadAllText(url);
                string checkvoid = File.ReadAllText(url);
               
                if(checkvoid == "")
                {
                    Stop = true;
                    File.WriteAllText(url, Based);
                    //crash protection
                    System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    Environment.Exit(-1);
                    //FFWP.Dialogs.ModeSelector MS = new Dialogs.ModeSelector();
                    //MS.ShowAsync();
                }
                else if (!(checkvoid.Contains("ffmem")))
                {
                    JObject jsonObject = JObject.Parse(jsonVerisi);
                    JArray profilesArray = (JArray)jsonObject["Profiles"];

                    foreach (JObject profile in profilesArray)
                    {
                        if (profile["ffmem"] == null)
                        {
                            profile["ffmem"] = 1024;
                        }
                    }

                    string updatedJson = jsonObject.ToString(Formatting.Indented);
                    File.WriteAllText(url, updatedJson);
                    //crash protection
                    System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    Environment.Exit(-1);
                }
                else if (!(checkvoid.Contains("secret")))
                {
                    FFWP.Dialogs.ModeSelector MS = new Dialogs.ModeSelector();
                    MS.ShowAsync();
                }
                else
                {
                    

                    try
                    {
                        using (StreamReader r = new StreamReader(url))
                        {
                            string json = r.ReadToEnd();
                            r.Close();
                            jsondat = json;
                        }
                        DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jsondat);
                        var data = (JObject)JsonConvert.DeserializeObject(jsondat);
                        DataTable dataTable = dataSet.Tables["Profiles"];

                        JObject o = JObject.Parse(jsonVerisi);
                        JArray a = (JArray)o["Profiles"];



                        IList<FFWP.JsonData.Profile> MCList = a.ToObject<IList<FFWP.JsonData.Profile>>();
                        foreach (FFWP.JsonData.Profile VAN in MCList)
                        {
                            pcombo.Items.Add(VAN.idjson);
                            pcombo.SelectedIndex = pcombo.Items.Count - 1;
                        }
                        dataSet.Clear();
                    }
                    catch 
                    {

                        FFWP.Dialogs.ModeSelector MS = new Dialogs.ModeSelector();
                        MS.ShowAsync();
                    }
                }
              
               
              
            }
            catch (Exception ex)
            {

                MessageBox.Show("FFNet Can't Parse The Version Data!" + ex.ToString());
                Environment.Exit(-1);
            }
        }

        public void SetValues()
        {
            if (pcombo.Items.Count == 0)
            {

            }
            else
            {
                try
                {
                    DataSet x = JsonConvert.DeserializeObject<DataSet>(jsondat);
                    var data = (JObject)JsonConvert.DeserializeObject(jsondat);
                    DataTable xz = x.Tables["Profiles"];
                    secret = xz.Rows[pcombo.SelectedIndex]["secret"].ToString();
                    username = xz.Rows[pcombo.SelectedIndex]["username"].ToString();
                    id = xz.Rows[pcombo.SelectedIndex]["idjson"].ToString();
                    directserver = xz.Rows[pcombo.SelectedIndex]["directserver"].ToString();
                    directport = xz.Rows[pcombo.SelectedIndex]["directport"].ToString();
                    width = xz.Rows[pcombo.SelectedIndex]["width"].ToString();
                    heigth = xz.Rows[pcombo.SelectedIndex]["heigth"].ToString();
                    version = xz.Rows[pcombo.SelectedIndex]["xversion"].ToString();
                    directory = xz.Rows[pcombo.SelectedIndex]["directory"].ToString();
                    online = xz.Rows[pcombo.SelectedIndex]["online"].ToString();
                    email = xz.Rows[pcombo.SelectedIndex]["email"].ToString();
                    pass = xz.Rows[pcombo.SelectedIndex]["pass"].ToString();
                    codename = xz.Rows[pcombo.SelectedIndex]["codename"].ToString();
                    img = xz.Rows[pcombo.SelectedIndex]["img"].ToString();
                    ffmem = xz.Rows[pcombo.SelectedIndex]["ffmem"].ToString();
                    customversion = xz.Rows[pcombo.SelectedIndex]["customversion"].ToString();
                    rungapple = xz.Rows[pcombo.SelectedIndex]["rungapple"].ToString();
                    int MemInt = Convert.ToInt32(ffmem);
                    int MemGB = MemInt / 1024;
                    BitmapImage bitmapImage = new BitmapImage(new Uri(img));
                    imageBrush.ImageSource = bitmapImage;
                    ramtext.Text = String.Format(FFWP.Language.Strings.ram, MemGB.ToString());
                    //pp.ImageSource = img;
                    Detector();
                }
                catch (Exception ex)
                {

                    
                }
            }
        }

        public void Detector()
        {
            //MessageBox.Show(Version);
            //if(Version == "Fabric")
            //{

            //}
            //else if (Version == "Forge")
            //{
            //    SaveINI set = new SaveINI(localfile);
            //    string langu = set.Read("LocalSettingsForGapple", "Lang");

            //    if (langu == "en-US")
            //    {
            //        launchbtt.Content = FFWP.Language.Strings.play + "Forge MC";
            //    }
            //    if (langu == "de-DE")
            //    {
            //        launchbtt.Content = "Forge MC " + FFWP.Language.Strings.play;
            //    }
            //    if (langu == "es-ES")
            //    {
            //        launchbtt.Content = FFWP.Language.Strings.play + "Forge MC";
            //    }
            //    if (langu == "tr-TR")
            //    {
            //        launchbtt.Content = "Forge MC " + FFWP.Language.Strings.play;
            //    }
            //    if (langu == "zh")
            //    {
            //        launchbtt.Content = FFWP.Language.Strings.play + "Forge MC";
            //    }
            //    if (langu == "fr-FR")
            //    {
            //        launchbtt.Content = FFWP.Language.Strings.play + "Forge MC";
            //    }

            SaveINI set = new SaveINI(localfile);
            string langu = set.Read("LocalSettingsForGapple", "Lang");

            if (langu == "en-US")
            {
                launchbtt.Content = FFWP.Language.Strings.play + " " + codename;
            }
            if (langu == "de-DE")
            {
                launchbtt.Content = codename + " " + FFWP.Language.Strings.play;
            }
            if (langu == "es-ES")
            {
                launchbtt.Content = FFWP.Language.Strings.play + " " + codename;
            }
            if (langu == "tr-TR")
            {
                launchbtt.Content = codename + " " + FFWP.Language.Strings.play;
            }
            if (langu == "zh")
            {
                launchbtt.Content = FFWP.Language.Strings.play + " " + codename;
            }
            if (langu == "fr-FR")
            {
                launchbtt.Content = FFWP.Language.Strings.play + " " + codename;
            }
        }

     
        private void usrinfo_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void pcombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //nativeGapple.Modules.ProfileEditing = "";
            //nativeGapple.Modules.ProfileData = "";
           // nativeGapple.Modules.ProfileMode = "";
            //nativeGapple.Modules.ProfileData = pcombo.SelectedItem.ToString() + ".xml";
            //if (online == "yes")
            //{
            //    nativeGapple.Modules.ProfileMode = "Online";
            //}
            //else if (online == "no")
            //{
            //    nativeGapple.Modules.ProfileMode = "Offline";
            //}

            if (DontRefresh == true)
            {
                DontRefresh = false;
            }
            else
            {
                // TakeRowASF();
                //TakeJSON();
                SetValues();
                settext();
            }
        }

        public void DeleteJSON()
        {
            string json = File.ReadAllText(FFWP.JsonData.Value.userjson);

   
            var data = JsonConvert.DeserializeObject<FFWP.JsonData.Root> (json);

      
            string profileIdToDelete = pcombo.SelectedItem.ToString();

     
            Profile profileToDelete = data.Profiles.Find(p => p.idjson == profileIdToDelete);
            if (profileToDelete != null)
            {
                data.Profiles.Remove(profileToDelete);
                string updatedJson = JsonConvert.SerializeObject(data, Formatting.Indented);

                File.WriteAllText(FFWP.JsonData.Value.userjson, updatedJson);
            }
            else
            {
               
            }
        }

        private void minusbtt_Click(object sender, RoutedEventArgs e)
        {

           
            if (pcombo.Items.Count == 1)
            {
               mesajbox(FFWP.Language.Strings.delerr, "FFGapple", "OK");
            }
            else
            {
                DontRefresh = true;
                //File.Delete(usr + pcombo.SelectedItem.ToString() + ".xml");
               
                DeleteJSON();
                pcombo.Items.Clear();
                //readfrommemory();
                try
                {
                    pcombo.SelectedIndex = pcombo.Items.Count - 1;

                    TakeJSON();
                    settext();
                    setramfortext();

                }
                catch (Exception ex)
                {
                    error = true;
                    Debug("Minus Module Error Due To : " + ex.ToString());
                    System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    Environment.Exit(-1);

                }
            }
        }

        private void folderbtt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(directory);
            }
            catch
            {
                //mesajbox(FFWP.Language.Strings.direr, "FFGapple", "OK");
                string mcdef = MinecraftPath.GetOSDefaultPath();
                Process.Start(mcdef);
            }
        }

        public void timers()
        {
            //System.Windows.Threading.DispatcherTimer ftimer = new System.Windows.Threading.DispatcherTimer();
            //ftimer.Tick += new EventHandler(ftimer_Tick);
            //ftimer.Interval = new TimeSpan(0, 0, 2);
            //ftimer.Start();

            //System.Windows.Threading.DispatcherTimer mtimer = new System.Windows.Threading.DispatcherTimer();
            //mtimer.Tick += new EventHandler(mtimer_Tick);
            //mtimer.Interval = new TimeSpan(0, 0, 2);
            //mtimer.Start();
        }

        public void Stopper()
        {
            //System.Windows.Threading.DispatcherTimer StopProc = new System.Windows.Threading.DispatcherTimer();
            //StopProc.Tick += new EventHandler(StopProc_Tick);
            //StopProc.Interval = new TimeSpan(0, 0, 0,1);
            //StopProc.Start();
        }

        private void StopProc_Tick(object sender, EventArgs e)
        {
            if(Stop == true)
            {
               
            }
           
        }

        private void mtimer_Tick(object sender, EventArgs e)
        {
            //if (pcombo.Items.Count == 1)
            //{
            //    minusbtt.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    minusbtt.Visibility = Visibility.Visible;
            //}
        }

        private void ftimer_Tick(object sender, EventArgs e)
        {
            lcontent = (string)launchbtt.Content;
            if (lcontent.Contains("fabric"))
            {
                SaveINI set = new SaveINI(localfile);
                string langu = set.Read("LocalSettingsForGapple", "Lang");

                if (langu == "en-US")
                {
                    launchbtt.Content = FFWP.Language.Strings.play + "Fabric MC";
                }
                if (langu == "de-DE")
                {
                    launchbtt.Content = "Fabric MC " + FFWP.Language.Strings.play;
                }
                if (langu == "es-ES")
                {
                    launchbtt.Content = FFWP.Language.Strings.play + "Fabric MC";
                }
                if (langu == "tr-TR")
                {
                    launchbtt.Content = "Fabric MC " + FFWP.Language.Strings.play;
                }
                if (langu == "zh")
                {
                    launchbtt.Content = FFWP.Language.Strings.play + "Fabric MC";
                }
                if (langu == "fr-FR")
                {
                    launchbtt.Content = FFWP.Language.Strings.play + "Fabric MC";
                }
            }
            else
            {

            }
        }

        private void ramtext_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //mesajbox(FFWP.Language.Strings.memred,"FFGapple", "OK");
        }

      public void DoStuff()
        {
            TakeJSON();
            Debug("JSON");
            SetValues();
            Debug("JVAL");
            settext();
            Debug("ST");
            timers();
            Debug("T");
            // checkversion();
            Debug("CV");
            //checkempty();
            Debug("CE");
            Debug("Done! FFKernel 1.0");
        }
        public LauncherPage()
        {

            string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
            FFWP.Modules.SaveINI first = new FFWP.Modules.SaveINI(localfile);
            string langu = first.Read("LocalSettingsForGapple", "Lang");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langu);
            firstime = true;
            Debug("Session Created");
            firstime = false;
            InitializeComponent();
            //CheckUser();
            Debug("INIT");
            //setramfortext();
            //Debug("SRFT");
            //readfrommemory();
            //Debug("RFM");
            DisplayContentDialog();
            

        }

        public async void checkempty()
        {
            if(pcombo.Items.Count == 0)
            {
                Dialogs.ModeSelector MS = new ModeSelector();
                await MS.ShowAsync();
            }
        }

        public async void CheckUser()
        {
            
            if ((Properties.Settings.Default.useragree == "agreed") == false)
            {
              
                FFWP.Dialogs.UserAgree ug = new FFWP.Dialogs.UserAgree();
                await ug.ShowAsync();
            }
            else
            {
                //TakeJSON();
            }
        }

        public async void mesajbox(string icerik, string baslik, string buton)
        {
           
            await new ModernWpf.Controls.ContentDialog()
            {
               Title = baslik,
               Content = icerik,
               PrimaryButtonText = buton,
               
            }.ShowAsync();

        
        }

        public void usernamedecrypt(string username)
        {
            try
            {
                var decusr = System.Convert.FromBase64String(username);
                decryptedusername = System.Text.Encoding.UTF8.GetString(decusr);

            }
            catch
            {

                decryptedusername = email;
            }
        }

        public void decrypt(string emailvuln, string passvuln)
        {

            try
            {
                var willdecryptemail = System.Convert.FromBase64String(emailvuln);
                decryptedmail = System.Text.Encoding.UTF8.GetString(willdecryptemail);
                var willdecryptpass = System.Convert.FromBase64String(passvuln);
                decryptedpass = System.Text.Encoding.UTF8.GetString(willdecryptpass);
            }
            catch
            {
                error = true;
                //mesajbox(FFWP.Language.Strings.base64, "FFGapple", "OK");
                decryptedpass = pass;
                decryptedmail = email;

            }
        }

        private async void addbtt_Click(object sender, RoutedEventArgs e)
        {
          

           // Dialogs.ModeSelector MS = new Dialogs.ModeSelector();
           
            //await MS.ShowAsync();
        }

        private void ExitBT_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private async void editbtt_Click(object sender, RoutedEventArgs e)
        {

            //string ProDat = usr + pcombo.SelectedItem.ToString() + ".xml";
            //string FileName = pcombo.SelectedItem.ToString() + ".xml";
            //if (online == "yes")
            //{
            //    Dialogs.ProfileMenu EP = new Dialogs.ProfileMenu("Online", ProDat, "Yes", FileName);
            //    await EP.ShowAsync();
            //}
            //else if (online == "no")
            //{
            //    Dialogs.ProfileMenu EP = new Dialogs.ProfileMenu("Offline", ProDat, "Yes", FileName);
            //    await EP.ShowAsync();
            //}
            //else
            //{
            //    mesajbox(FFWP.Language.Strings.proferr, "FFNet", "Okay");
            //}
           
        }

        private async void maintext_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
         
        }

        private async void addmodsbtt_Click(object sender, RoutedEventArgs e)
        {
            if(directory != "")
            {
                Dialogs.AddMod addmod = new Dialogs.AddMod(directory);
                await addmod.ShowAsync();
            }
            else
            {
                mesajbox(FFWP.Language.Strings.direr, "FFGapple", "OK");
            }
        }

        private async void OfflineButton_Click(object sender, RoutedEventArgs e)
        {
            nativeGapple.Modules.ProfileMode = "Offline";
            Dialogs.AddProfileOffline ADO = new Dialogs.AddProfileOffline();
            await ADO.ShowAsync();
        }

        private async void OnlineButton_Click(object sender, RoutedEventArgs e)
        {
            nativeGapple.Modules.ProfileMode = "Online";
            Dialogs.AddProfileOffline ADO = new Dialogs.AddProfileOffline();
            await ADO.ShowAsync();
        }

        public void bildirim(string baslik, string icerik, string resimurl)
        {
            new ToastContentBuilder()

                                   .AddText(baslik)
                                   .AddText(icerik)
                                   .AddHeroImage(new Uri(resimurl))

                                   .Show();
        }

        public async void UpdateMNGR()
        {




            try
            {
                using (var mgr = await UpdateManager.GitHubUpdateManager("https://github.com/ffgapple/FFGapple-Launcher"))
                {
                    var release = await mgr.UpdateApp();

                }
            }
            catch (Exception ex)
            {
                error = true;
                Debug("Update Service Is Not Working." + ex.ToString());
                bildirim("FFNet", FFWP.Language.Strings.uperr, content + "/popup.gif");
            }
        }

        public async void checkversion()
        {
            try
            {
                await Task.Run(() =>
                {
                    string webmain = new StreamReader(WebRequest.Create("https://raw.githubusercontent.com/ffgapple/ffgapple.github.io/master/data/webdata.html").GetResponse().GetResponseStream()).ReadToEnd();
                    int startIndex = webmain.IndexOf("<vN>") + 4;
                    int length = webmain.Substring(startIndex).IndexOf("</vN>");
                    this.versionprogram = (int)Convert.ToInt16(webmain.Substring(startIndex, length));
                    if (versionprogram == 440)
                    {
                        outdated = false;
                    }
                    else
                    {
                        outdated = true;
                    }

                    if (outdated == true)
                    {
                       


                            bildirim("FFNet", FFWP.Language.Strings.ffupdating, content + "/popup.gif");
                                UpdateMNGR();
                               
                                Debug("New Update Is Ready. Updating with Squirell Update Manager.");
                      

                        
                    }
                });

            }
            catch (Exception ex)
            {
                //error = true;
                //Debug("Update Check Failure : " + ex.ToString());
                //System.Diagnostics.Process.Start(Application.ExecutablePath);
                //Application.Exit();
            }


        }

      

        public async void DisplayContentDialog()
        {
            if(FFWP.JsonData.Value.Checked == true)
            {
                DoStuff();
            }
            else
            {
                
                    
                    FFWP.Dialogs.UpdateNotify ug = new FFWP.Dialogs.UpdateNotify();
                    await ug.ShowAsync();
                    FFWP.JsonData.Value.Checked = true;
                    DoStuff();
                    //if (result == ModernWpf.Controls.ContentDialogResult.Primary)
                    //{
                    //    FFWP.JsonData.Value.Checked = true;
                    //    DoStuff();
                    //}
                    //else if (result == ModernWpf.Controls.ContentDialogResult.Secondary)
                    //{
                    //    FFWP.JsonData.Value.Checked = true;
                    //    DoStuff();
                    //}
                    //else
                    //{
                    //    FFWP.JsonData.Value.Checked = true;
                    //    DoStuff();
                    //}
               
               
              
            }
        }

        public async void addprofiles()
        {
            DirectoryInfo dir = new DirectoryInfo(usr);
            FileInfo[] files = dir.GetFiles();
            DirectoryInfo[] subdirs = dir.GetDirectories();
            if (files.Length == 0 && subdirs.Length == 0)
            {

            }
            else
            {


                try
                {
                  
                    string selcombo = pcombo.SelectedItem.ToString();
                    XDocument xDoc = XDocument.Load(usr + selcombo + ".xml");
                    XElement rootElement = xDoc.Root;


                    foreach (XElement profile in rootElement.Elements())
                    {
                        id = profile.Attribute("id").Value;
                        directserver = profile.Element("directserver").Value;
                        directport = profile.Element("directport").Value;
                        width = profile.Element("width").Value;
                        heigth = profile.Element("heigth").Value;
                        version = profile.Element("version").Value;
                        directory = profile.Element("directory").Value;
                        online = profile.Element("online").Value;
                        email = profile.Element("email").Value;
                        pass = profile.Element("pass").Value;
                    }
                }
                catch (Exception ex)
                {
                    Debug("Session Ended Due To Error : XML Files Are Missing = " + ex.ToString());
                    
                    Environment.Exit(-1);
                }

            }

            if (directory == "")
            {
               // customskin.Enabled = false;
            }
            else
            {
               // customskin.Enabled = true;
            }


        }

        public async void setramfortext()
        {
            try
            {

                ManagementObjectSearcher Search = new ManagementObjectSearcher("Select * From Win32_ComputerSystem");
                foreach (ManagementObject Mobject in Search.Get())
                {
                    double Ram_Bytes = (Convert.ToDouble(Mobject["TotalPhysicalMemory"]));
                    ramgoritmaninrami = (Ram_Bytes / 1073741824);

                }
                if (ramgoritmaninrami >= 0 && ramgoritmaninrami <= 2)
                {
                    ramtext.Text = FFWP.Language.Strings.dusuk;
                }
                if (ramgoritmaninrami >= 2 && ramgoritmaninrami <= 4)
                {

                    ramtext.Text = FFWP.Language.Strings.ram;

                }
                if (ramgoritmaninrami >= 4 && ramgoritmaninrami <= 8)
                {
                    ramtext.Text = FFWP.Language.Strings.ram;

                }
                if (ramgoritmaninrami >= 8 && ramgoritmaninrami <= 16)
                {
                    ramtext.Text = FFWP.Language.Strings.ram;

                }
                if (ramgoritmaninrami >= 16 && ramgoritmaninrami <= 32)
                {
                    ramtext.Text = FFWP.Language.Strings.ram;

                }
                if (ramgoritmaninrami >= 32 && ramgoritmaninrami <= 64)
                {
                    ramtext.Text = FFWP.Language.Strings.ram;

                }
            }
            catch (Exception ex)
            {
                error = true;
                Debug("Can't Set RAM Text Due To : " + ex.ToString());

            }
        }

        public void Debug(String Message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\FFGappleLog.txt", true);
                if (firstime == true)
                {
                    sw.WriteLine("FFGapple WPF (Better Call 0x1337) : Logging...");
                }
                else
                {

                }
                sw.WriteLine(DateTime.Now.ToString() + " : " + Message);
                sw.Flush();
                sw.Close();
                //if (error == true)
                //{
                //    toolTip1.SetToolTip(errlogo, Message);
                //    toolTip1.BackColor = Color.Indigo;
                //}
            }
            catch
            {
                error = true;
            }
        }

        public void missionchecker()
        {

            if (!File.Exists(backup))
            {
                Debug("Session Ended Due To Error : Runtime Folder Missing!");
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Environment.Exit(-1);
            }

            if (!File.Exists(basedata))
            {
                Debug("Session Ended Due To Error : Runtime Folder Missing!");
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Environment.Exit(-1);
            }
            if (!File.Exists(localfile))
            {
                Debug("Session Ended Due To Error : Runtime Folder Missing!");
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Environment.Exit(-1);
            }
            if (!Directory.Exists(dir))
            {
                Debug("Session Ended Due To Error : Runtime Folder Missing!");
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Environment.Exit(-1);
            }
            if (!Directory.Exists(req))
            {
                Debug("Session Ended Due To Error : Runtime Folder Missing!");
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Environment.Exit(-1);
            }
            if (!Directory.Exists(usr))
            {
                Debug("Session Ended Due To Error : Runtime Folder Missing!");
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Environment.Exit(-1);
            }
            if (!Directory.Exists(content))
            {
                Debug("Session Ended Due To Error : Runtime Folder Missing!");
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Environment.Exit(-1);
            }
            if (!Directory.Exists(localdir))
            {
                Debug("Session Ended Due To Error : Runtime Folder Missing!");
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Environment.Exit(-1);
            }
        }
        public void readfrommemory()
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(usr);
                FileInfo[] files = directory.GetFiles();
                DirectoryInfo[] subdirs = directory.GetDirectories();
                if (files.Length == 0 && subdirs.Length == 0)
                {
                   

                }
                else
                {
                    FileInfo[] Files;
                    DirectoryInfo d = new DirectoryInfo(usr);
                    Files = d.GetFiles("*.xml");



                    str = "";
                    foreach (FileInfo file in Files)
                    {

                        pcombo.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file.Name));
                        pcombo.SelectedIndex = pcombo.Items.Count - 1;
                    }
                }
            }
            catch 
            {
               

            }




        }

       
        
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
               
                if (online == "yes")
                {
                    session = null;

                   
                    //var app = MsalMinecraftLoginHelper.CreateDefaultApplicationBuilder("aac3d779-adc2-489b-9795-9436078fa193")
                    //    .Build();
                    //var handler = new MsalMinecraftLoginHandler(app);

                    // MicrosoftLoginWindow loginWindow = new MicrosoftLoginWindow();

                    //loginWindow.Title = "XBOX / Microsoft";
                    //MSession Xsession = await loginWindow.ShowLoginDialog();
                    //try
                    //{
                    //    RunningNow = "Online";
                    //    new Thread((ThreadStart)(() => this.onlinelaunch()))
                    //    {

                    //        IsBackground = true

                    //    }.Start();
                    //}
                    //catch
                    //{
                    //    error = true;
                    //    mesajbox(FFWP.Language.Strings.startfail, "FFGapple", "OK");
                    //}

                    var login = new MLogin();
                    decrypt(email, pass);
                    var response = login.Authenticate(decryptedmail, decryptedpass);

                    if (!response.IsSuccess)
                    {

                        mesajbox(FFWP.Language.Strings.mojhata, "FFGapple", "OK");

                        return;
                    }
                    else
                    {
                        try
                        {
                            RunningNow = "Online";
                            new Thread((ThreadStart)(() => this.onlinelaunch()))
                            {

                                IsBackground = true

                            }.Start();
                        }
                        catch
                        {
                            error = true;
                            mesajbox(FFWP.Language.Strings.startfail, "FFGapple", "OK");
                        }

                        session = response.Session;
                        decryptedmail = "";
                        decryptedpass = "";
                        pass = "";
                        email = "";
                    }
                }
                else
                {
                    try
                    {
                        RunningNow = "Offline";
                        new Thread((ThreadStart)(() => this.offlinelaunch()))
                        {

                            IsBackground = true

                        }.Start();
                    }
                    catch (Exception ex)
                    {
                        error = true;
                        Debug("Start Error Due To : " + ex.ToString());
                        mesajbox(FFWP.Language.Strings.startfail, "FFGapple", "OK");
                    }

                }
            }
            catch (Exception ex)
            {
                error = true;
                Debug("MC Session Error Due To : " + ex.ToString());
            }



        }
        public void converint()
        {
            try
            {
                codirectport = Convert.ToInt32(directport);
                cowidth = Convert.ToInt32(width);
                coheigth = Convert.ToInt32(heigth);
            }
            catch (Exception ex)
            {
                error = true;
                Debug("Can't Create INT32. " + ex.ToString());
                //Environment.Exit(-1);

            }
        }
        public async void onlinelaunch()
        {


            nativeGapple.Modules.NavStatus = "";
            nativeGapple.Modules.NavStatus = "disabled";
            UNlock("lock");
            pinfo.Dispatcher.Invoke(new Action(() =>
            {
                pinfo.Visibility = Visibility.Visible;
            }));
            custominfo.Dispatcher.Invoke(new Action(() =>
            {
                custominfo.Visibility = Visibility.Visible;
            }));
           

            pcombo.Focusable = false;
            launchbtt.Focusable = false;
            converint();

            option.Session = session;

            try
            {
                option.ServerIp = directserver;
                option.ServerPort = codirectport;
                option.ScreenWidth = cowidth;
                option.ScreenHeight = coheigth;


            }
            catch
            {
                error = true;

            }
            int memory = Convert.ToInt32(ffmem);
            option.MaximumRamMb = memory;





            try
            {
                //if (ramgoritmaninrami >= 2 && ramgoritmaninrami <= 4)
                //{

                //    option.MaximumRamMb = 2048;
                //    option.Session = session;
                //}
                //if (ramgoritmaninrami >= 4 && ramgoritmaninrami <= 8)
                //{

                //    option.MaximumRamMb = 4096;
                //    option.Session = session;
                //}
                //if (ramgoritmaninrami >= 8 && ramgoritmaninrami <= 16)
                //{

                //    option.MaximumRamMb = 8192;
                //    option.Session = session;
                //}
                //if (ramgoritmaninrami >= 16 && ramgoritmaninrami <= 32)
                //{

                //    option.MaximumRamMb = 16384;
                //    option.Session = session;
                //}
                //if (ramgoritmaninrami >= 32 && ramgoritmaninrami <= 64)
                //{

                //    option.MaximumRamMb = 32768;
                //    option.Session = session;
                //}
                try
                {
                   
                    pinfo.Visibility = Visibility.Visible;
                    FFWP.Forms.LauncherPage.versionl = version;
                    option.Session = session;
                    option.VersionType = "FFGapple Launcher";
                    option.GameLauncherName = "FFGapple Launcher";
                    option.GameLauncherVersion = "4.3";
                    customstart();
                    if(AllesGut == false)
                    {
                        pinfo.Dispatcher.Invoke(new Action(() =>
                        {
                            pinfo.Visibility = Visibility.Visible;
                        }));
                        custominfo.Dispatcher.Invoke(new Action(() =>
                        {
                            custominfo.Visibility = Visibility.Hidden;
                        }));
                        UNlock("unlock");
                        nativeGapple.Modules.NavStatus = "";
                        nativeGapple.Modules.NavStatus = "enabled";

                    }
                    else
                    {
                        pinfo.Dispatcher.Invoke(new Action(() =>
                        {
                            pinfo.Visibility = Visibility.Hidden;
                        }));
                        custominfo.Dispatcher.Invoke(new Action(() =>
                        {
                            custominfo.Visibility = Visibility.Hidden;
                        }));
                        UNlock("unlock");
                        nativeGapple.Modules.NavStatus = "";
                        nativeGapple.Modules.NavStatus = "enabled";
                        bildirim("FFGapple Launcher", FFWP.Language.Strings.fun, popup);
                        if (afterclose == "YES")
                        {
                            Environment.Exit(-1);
                        }
                    }
                   
                   
                }
                catch (Exception ex)
                {
                    error = true;
                    Debug("Can't Create The Minecraft Session. " + ex.ToString());
                    Environment.Exit(-1);

                }
            }
            catch (Exception ex)
            {
                error = true;
                Debug("Can't Set RAM " + ex.ToString());
                Environment.Exit(-1);
            }
        }

        public async void offlinelaunch()
        {
            
            nativeGapple.Modules.NavStatus = "";
            nativeGapple.Modules.NavStatus = "disabled";
            UNlock("lock");
            pinfo.Dispatcher.Invoke(new Action(() =>
            {
                pinfo.Visibility = Visibility.Visible;
            }));
            custominfo.Dispatcher.Invoke(new Action(() =>
            {
                custominfo.Visibility = Visibility.Visible;
            }));

            converint();

          


            try
            {
                option.ServerIp = directserver;
                option.ServerPort = codirectport;
                option.ScreenWidth = cowidth;
                option.ScreenHeight = coheigth;


            }
            catch
            {
                error = true;

            }

            //try
            //{
            //    ManagementObjectSearcher Search = new ManagementObjectSearcher("Select * From Win32_ComputerSystem");
            //    foreach (ManagementObject Mobject in Search.Get())
            //    {
            //        double Ram_Bytes = (Convert.ToDouble(Mobject["TotalPhysicalMemory"]));
            //        ramgoritmaninrami = (Ram_Bytes / 1073741824);

            //    }

            //}
            //catch
            //{



            //}
            //MinecraftPath usrdrc = new MinecraftPath(directory);
            //usrdrc.BasePath = directory;
            //usrdrc.Library = directory + "/libraries/";
            //usrdrc.Resource = directory + "/resources/";
            //usrdrc.Versions = directory + "/versions/";
            ////myPath.GetVersionJarPath(Application.StartupPath + "/games/versions/1.13/");
            ////usrdrc.GetIndexFilePath(Application.StartupPath + "/versions/");
            //CMLauncher cmLauncher = new CMLauncher(usrdrc);
            //cmLauncher.FileChanged += Launcher_FileChanged;

            int memory = Convert.ToInt32(ffmem);
            option.MaximumRamMb = memory;
            try
            {
                //if (ramgoritmaninrami >= 2 && ramgoritmaninrami <= 4)
                //{

                //    option.MaximumRamMb = 2048;
                //    option.Session = session;
                //}
                //if (ramgoritmaninrami >= 4 && ramgoritmaninrami <= 8)
                //{

                //    option.MaximumRamMb = 4096;
                //    option.Session = session;
                //}
                //if (ramgoritmaninrami >= 8 && ramgoritmaninrami <= 16)
                //{

                //    option.MaximumRamMb = 8192;
                //    option.Session = session;
                //}
                //if (ramgoritmaninrami >= 16 && ramgoritmaninrami <= 32)
                //{

                //    option.MaximumRamMb = 16384;
                //    option.Session = session;
                //}
                //if (ramgoritmaninrami >= 32 && ramgoritmaninrami <= 64)
                //{

                //    option.MaximumRamMb = 32768;
                //    option.Session = session;
                //}
                try
                {
                    option.Session = MSession.GetOfflineSession(username);

                    
                    
                    FFWP.Forms.LauncherPage.versionl = version;
                    //option.Session = session;
                    option.VersionType = "FFGapple Launcher";
                    option.GameLauncherName = "FFGapple Launcher";
                    option.GameLauncherVersion = "4.5";
                    customstart();
                    if (AllesGut == false)
                    {
                      
                        pinfo.Dispatcher.Invoke(new Action(() =>
                       {
                           pinfo.ShowError = true;
                       }));
                        // custominfo.Dispatcher.Invoke(new Action(() =>
                        // {
                        //     custominfo.Visibility = Visibility.Hidden;
                        // }));
                        UNlock("unlock");
                        nativeGapple.Modules.NavStatus = "";
                        nativeGapple.Modules.NavStatus = "enabled";
                        AllesGut = true;
                    }
                    else
                    {
                        pinfo.Dispatcher.Invoke(new Action(() =>
                        {
                            pinfo.Visibility = Visibility.Hidden;
                        }));
                        custominfo.Dispatcher.Invoke(new Action(() =>
                        {
                            custominfo.Visibility = Visibility.Hidden;
                        }));
                        UNlock("unlock");
                        nativeGapple.Modules.NavStatus = "";
                        nativeGapple.Modules.NavStatus = "enabled";
                        bildirim("FFGapple Launcher", FFWP.Language.Strings.fun, popup);
                        if (afterclose == "YES")
                        {
                            Environment.Exit(-1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    error = true;
                    Debug("Can't Create The Minecraft Session. " + ex.ToString());
                    Environment.Exit(-1);
                }
            }
            catch (Exception ex)
            {
                error = true;
                Debug("Can't Set RAM " + ex.ToString());
                Environment.Exit(-1);
            }
        }

        public void UNlock(string command)
        {
            if(command == "unlock")
            {
              
                pcombo.Dispatcher.Invoke(new Action(() =>
                {
                    pcombo.IsEnabled = true;
                }));
                launchbtt.Dispatcher.Invoke(new Action(() =>
                {
                    launchbtt.IsEnabled = true;
                }));
            }
            if(command == "lock")
            {
                
                pcombo.Dispatcher.Invoke(new Action(() =>
                {
                    pcombo.IsEnabled = false;
                }));
                launchbtt.Dispatcher.Invoke(new Action(() =>
                {
                    launchbtt.IsEnabled = false;
                }));
            }
        }

        public void customstart()
        {
            bool EditJSON = false;
           
            try
            {
                if (directory == "")
                {

                    CMLauncher cmLauncher = new CMLauncher(new MinecraftPath());

                    if(rungapple == "No")
                    {
                        EditJSON = false;
                        cmLauncher.GameFileCheckers.AssetFileChecker = null;
                        cmLauncher.GameFileCheckers.LibraryFileChecker.CheckHash = false;
                    }
                    else if(rungapple == "Yes")
                    {
                       
                        EditJSON = true;
                    }
                    else if(rungapple == "Disabled")
                    {
                        EditJSON = false;
                    }
                  
                    cmLauncher.FileChanged += Launcher_FileChanged;

                    if (nativeGapple.Modules.IsEnabled == true)
                    {
                        if (nativeGapple.Modules.Online == false)
                        {
                            var CMP = cmLauncher.CreateProcess(FFWP.Forms.LauncherPage.versionl, option);

                            CMP.Start();
                        }
                        else
                        {
                            var CMP = cmLauncher.CreateProcess(FFWP.Forms.LauncherPage.versionl, option);
                            CMP.StartInfo.Arguments += (" --disableMultiplayer");
                            CMP.Start();
                        }
                    }
                    else
                    {
                        var CMP = cmLauncher.CreateProcess(FFWP.Forms.LauncherPage.versionl, option);

                        CMP.Start();
                    }
                  
                   
                }
                else
                {
                    if (Directory.Exists(directory))
                    {
                        MinecraftPath usrdrc = new MinecraftPath(directory);
                        usrdrc.BasePath = directory;
                        usrdrc.Library = directory + "/libraries/";
                        usrdrc.Resource = directory + "/resources/";
                        usrdrc.Versions = directory + "/versions/";
                        //myPath.GetVersionJarPath(Application.StartupPath + "/games/versions/1.13/");
                        //usrdrc.GetIndexFilePath(Application.StartupPath + "/versions/");
                        CMLauncher cmLauncher = new CMLauncher(usrdrc);
                        cmLauncher.FileChanged += Launcher_FileChanged;
                        if (nativeGapple.Modules.IsEnabled == true)
                        {
                            if (nativeGapple.Modules.Online == false)
                            {
                               
                                var CMP = cmLauncher.CreateProcess(FFWP.Forms.LauncherPage.versionl, option);

                                CMP.Start();
                            }
                            else
                            {
                                var CMP = cmLauncher.CreateProcess(FFWP.Forms.LauncherPage.versionl, option);
                                CMP.StartInfo.Arguments += (" --disableMultiplayer");
                                CMP.Start();
                            }
                        }
                        else
                        {
                            var CMP = cmLauncher.CreateProcess(FFWP.Forms.LauncherPage.versionl, option);

                            CMP.Start();
                        }
                     

                    }
                    else
                    {
                        AllesGut = false;
                    }
                }
                if(EditJSON == true)
                {


                    string jsonContent = File.ReadAllText(FFWP.JsonData.Value.userjson);

                    JObject jsonData = JObject.Parse(jsonContent);
                    JArray profiles = (JArray)jsonData["Profiles"];
                    foreach (JObject profile in profiles)
                    {
                        string secret = (string)profile["secret"];
                        if (secret == this.secret)
                        {
                        
                            profile["rungapple"] = "No";


                            break;
                        }
                    }

                    string updatedJsonContent = jsonData.ToString();
                    File.WriteAllText(FFWP.JsonData.Value.userjson, updatedJsonContent);

                }
                option.Session = null;
               
            }
            catch (Exception ex)
            {
                error = true;
                Debug("Session Manager Error Due To : " + ex.ToString());
            }
        }

        private void Launcher_FileChanged(DownloadFileChangedEventArgs e)
        {
            try
            {
                pinfo.Dispatcher.Invoke(new Action(() =>
                {
                    //pinfo.Maximum = e.TotalFileCount;
                   // pinfo.Value = e.ProgressedFileCount;
                }));

                custominfo.Dispatcher.Invoke(new Action(() =>
                {
                    custominfo.Text = $"{e.FileKind} : {e.FileName} ({e.ProgressedFileCount}/{e.TotalFileCount})";
                }));

                
               
            }
            catch(Exception ex)
            {
                string err = ex.ToString();
                MessageBox.Show(err);
                //mesajbox(err + "FFNet" + "OK");

            }

        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
        
        public void settext()
        {
            try
            {
                SaveINI set = new SaveINI(localfile);
                string langu = set.Read("LocalSettingsForGapple", "Lang");
                afterclose = set.Read("LocalSettingsForGapple", "ExitAfterLaunch");
                if (online == "yes")
                {
                    usernamedecrypt(email);
                    usrinfo.Text = FFWP.Language.Strings.welcome + decryptedusername + " - " + codename;
                }
                else
                {
                    usrinfo.Text = FFWP.Language.Strings.welcome + username + " - " + codename;
                }
                
                //if (langu == "en-US")
                //{

                //    launchbtt.Content = FFWP.Language.Strings.play + version;
                //}
                //if (langu == "de-DE")
                //{
                //    launchbtt.Content = FFWP.Language.Strings.play + version;
                //}
                //if (langu == "es-ES")
                //{
                //    launchbtt.Content = FFWP.Language.Strings.play + version;
                //}
                //if (langu == "tr-TR")
                //{
                //    launchbtt.Content = version + FFWP.Language.Strings.play;
                //}
                //if (langu == "zh")
                //{
                //    launchbtt.Content = FFWP.Language.Strings.play + version;
                //}
                //if (langu == "fr-FR")
                //{
                //    launchbtt.Content = FFWP.Language.Strings.play + version;
                //}
                if(nativeGapple.Modules.IsEnabled == true)
                {
                    fflogo.Source = new BitmapImage(new Uri(@"/GUI/besmile.png", UriKind.Relative));
                    if(nativeGapple.Modules.Online == true) 
                    {
                        parental.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        parental.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    parental.Visibility = Visibility.Hidden;
                }
             
                //onlinetxt.Text = FFWP.Language.Strings.cevrim;

                //settings.Text = FFWP.Language.Strings.set4;
                //tipus.Text = FFWP.Language.Strings.donate;
                //customskin.Text = FFWP.Language.Strings.customski;
                //this.Text = "FFGapple Launcher " + serial + " - " + sessionid;
            }
            catch (Exception ex)
            {
                error = true;
                Debug("LocalINI Error Due To : " + ex.ToString());
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Environment.Exit(-1);
            }
        }

       
}
}
