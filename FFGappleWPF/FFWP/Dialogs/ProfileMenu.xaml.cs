using CmlLib.Core.Version;
using CmlLib.Core;
using ModernWpf.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Xml.Linq;
using Windows.AI.MachineLearning;
using Newtonsoft.Json;
using System.Data;
using Windows.Media.Protection.PlayReady;
using System.CodeDom;
using FFWP.Modules;

namespace FFWP.Dialogs
{
    /// <summary>
    /// Interaktionslogik für ProfileMenu.xaml
    /// </summary>
    /// 

    public partial class ProfileMenu : ContentDialog
    {

        String creationdate, customversion, usernameS, id, directport, rungapple, width, heigth, directserver, version, directory, online, email, pass, afterclose, updateoption, secret, modded, ffmem;
        String encryptedemail, encryptedpass, decryptedemail, decryptedpass;
        String codename;
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
        String GPSecret, GProfileMode, GProfileEdit, GFileName;
        String SelectedMinecraft, DownloadString;
        string url = @"https://ffgapple.github.io/JSON/GappleList.json";
        string jsonVerisi = "";
        string alreadyversion;
        public bool CustomON = false;
        public ProfileMenu(string PSecret) //Thx to Alexej https://stackoverflow.com/a/37538081
        {
            string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
            FFWP.Modules.SaveINI first = new FFWP.Modules.SaveINI(localfile);
            string langu = first.Read("LocalSettingsForGapple", "Lang");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langu);
            GPSecret = PSecret;
            InitializeComponent();
            double systemMemoryGB = SystemInfo.GetPhysicalAvailableMemoryInGB();
            ramslider.Maximum = systemMemoryGB;
            EditRoutine();
            if(customversion == "Fabric")
            {
                SelectedMinecraft = "Fabric";
            }
            else if(customversion == "Forge")
            {
                SelectedMinecraft = "Forge";
            }
            else if (customversion == "Vanilla")
            {
                SelectedMinecraft = "Vanilla";
            }
          
            else
            {
                this.Hide();
            }
            vercombo.SelectedItem = codename;

        }

        public void decrypt(string emailvuln, string passvuln)
        {

            try
            {
                var willdecryptemail = System.Convert.FromBase64String(emailvuln);
                decryptedemail = System.Text.Encoding.UTF8.GetString(willdecryptemail);
                var willdecryptpass = System.Convert.FromBase64String(passvuln);
                decryptedpass = System.Text.Encoding.UTF8.GetString(willdecryptpass);
            }
            catch (Exception ex)
            {


            }
        }

        public class Fabric
        {
            public string MCFab { get; set; }
            public string RLFab { get; set; }
            public string Publisher { get; set; }
            public string Developer { get; set; }
            public string Website { get; set; }
            public string Date { get; set; }
            public string Download { get; set; }
        }

        public class Forge
        {
            public string MCForge { get; set; }
            public string RLForg { get; set; }
            public string Publisher { get; set; }
            public string Developer { get; set; }
            public string Website { get; set; }
            public string Date { get; set; }
            public string Download { get; set; }
        }

        public class Root
        {
            public List<Vanilla> Vanilla { get; set; }
            public List<Fabric> Fabric { get; set; }
            public List<Forge> Forge { get; set; }
        }

        public class Vanilla
        {
            public string MCVan { get; set; }
            public string Publisher { get; set; }
            public string Developer { get; set; }
            public string Website { get; set; }
            public string Date { get; set; }
            public string Download { get; set; }
        }
        bool Fetch = true;
        public void TakeAll(string MCVer)
        {
            try
            {
                vercombo.Items.Clear();
                if(Fetch == true)
                {
                    string url = @"https://ffgapple.github.io/JSON/GappleList.json";
                    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                    using (WebClient response = new WebClient())
                    {
                        jsonVerisi = response.DownloadString(url);
                    }
                    Fetch = false;
                }
                JObject o = JObject.Parse(jsonVerisi);
                JArray a = (JArray)o[MCVer];

                if (MCVer == "Vanilla")
                {
                    IList<Vanilla> MCList = a.ToObject<IList<Vanilla>>();
                    foreach (Vanilla VAN in MCList)
                    {
                        if (MCVer == "Vanilla")
                        {
                            vercombo.Items.Add(VAN.MCVan);
                            SelectedMinecraft = "Vanilla";
                        }
                    }
                }
                if (MCVer == "Fabric")
                {
                    IList<Fabric> MCList = a.ToObject<IList<Fabric>>();
                    foreach (Fabric FAB in MCList)
                    {

                        if (MCVer == "Fabric")
                        {

                            vercombo.Items.Add(FAB.MCFab);
                            SelectedMinecraft = "Fabric";
                        }

                    }
                }
                if (MCVer == "Forge")
                {
                    IList<Forge> MCList = a.ToObject<IList<Forge>>();
                    foreach (Forge FOR in MCList)
                    {

                        if (MCVer == "Forge")
                        {

                            vercombo.Items.Add(FOR.MCForge);
                            SelectedMinecraft = "Forge";
                        }

                    }
                }



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        public void doit()
        {
            //if (File.Exists(usr + profilename.Text + ".xml"))
            //{
            //    mesajbox(FFWP.Language.Strings.alex, "FFGapple", "OK");
            //}
            //else
            //{
            //copybase();

            try
            {


                jsonadd();
                this.Hide();



            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.ToString());
                mesajbox(FFWP.Language.Strings.proferr + "\n" + ex.ToString(), "FFGapple", "OK");
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Environment.Exit(-1);

            }
            //}
        }
        public void checkcustom()
        {

            if (CustomON == true)
            {
               
                if ((customdir.Text == FFWP.Language.Strings.privdir) == false)
                {
                    cleanedtoken();
                    doit();
                   
                }
                else
                {
                    mesajbox(FFWP.Language.Strings.fabricdir, "FFGapple", "Ok");

                }
            }
            else
            {
                cleanedtoken();
               
                doit();
         
            }

        }
        public string RealName;
        public string Developer;
        public void jsonadd()
        {
            
            string jsonContent = File.ReadAllText(FFWP.JsonData.Value.userjson);

            JObject jsonData = JObject.Parse(jsonContent);


            string directserver = "";
            string directport = "";
            string width = "";
            string heigth = "";
            string directory = "";
            string online = "";
            string email = "";
            string pass = "";
            string version = alreadyversion;
            string modded = "";
            string customversion = SelectedMinecraft;
            string DecideImage = "";
            string UsernameTXT = "";
            string XBOX = "";
            string FFMem = "";
            string RunGapple = "";

            if (hizliTOG.IsOn == true)
            {
                RunGapple = "Yes";
            }
            else
            {
                RunGapple = "Disabled";
            }

            if (serveript.Text == "Server IP")
            {
                directserver = "";
            }
            else
            {
                directserver = serveript.Text;
            }

            if (serverportt.Text == "Port")
            {
                directport = "";
            }
            else
            {
                directport = serverportt.Text;
            }

            if (widthtxt.Text == FFWP.Language.Strings.genistext)
            {
                width = "";
            }
            else
            {
                width = widthtxt.Text;
            }

            if (customdir.Text == FFWP.Language.Strings.privdir)
            {
                directory = "";
            }
            else
            {
                directory = customdir.Text;
                directory.Replace(@"\", @"\\");
            }

            if (heigthtxt.Text == FFWP.Language.Strings.yuksektext)
            {
                heigth = "";
            }
            else
            {
                heigth = heigthtxt.Text;
            }
            if (vercombo.SelectedItem.ToString().Contains("Fabric"))
            {
               
                customversion = "Fabric";
                version = RealName;
                dowfab();
            }
            else if (vercombo.SelectedItem.ToString().Contains("Forge"))
            {
              
                customversion = "Forge";
                version = RealName;
                dowforg();
            }
            else if (SelectedMinecraft == "Vanilla")
            {
                customversion = "Vanilla";
                version = vercombo.SelectedItem.ToString();
                if(vercombo.SelectedItem.ToString() == alreadyversion)
                {
                    version = alreadyversion;
                    customversion = SelectedMinecraft;
                }
                else
                {
                   
                    version = vercombo.SelectedItem.ToString();
                    customversion = "Vanilla";
                }
            }
            else
            {
                this.Hide();
            }

            if (nativeGapple.Modules.ProfileMode == "Online")
            {
                encrypt(mail.Text, passtxt.Password);
                online = "yes";
                email = encryptedemail;
                pass = encryptedpass;
                UsernameTXT = profilename.Text;
            }
            else
            {
                online = "no";
                email = "";
                pass = "";
                UsernameTXT = username.Text;
            }

            if (username.Text == FFWP.Language.Strings.usernametext)
            {
                DecideImage = "pack://application:,,,/FFWP;component/GUI/unknown.png";

            }
            else if (nativeGapple.Modules.ProfileMode == "Online")
            {
                DecideImage = "pack://application:,,,/FFWP;component/GUI/unknown.png";
            }
            else
            {
                DecideImage = "https://minotar.net/cube/" + username.Text + "/100.png";
            }

            double RamSizeGB = ramslider.Value;
            FFMem = GetRamValueInMB(RamSizeGB).ToString();

            JArray profiles = (JArray)jsonData["Profiles"];
            foreach (JObject profile in profiles)
            {
                string secret = (string)profile["secret"];
                if (secret == GPSecret)
                {
                    profile["username"] = UsernameTXT;
                    profile["directserver"] = directserver;
                    profile["directport"] = directport;
                    profile["width"] = width;
                    profile["heigth"] = heigth;
                    profile["xversion"] = version;
                    profile["codename"] = vercombo.SelectedItem.ToString();
                    profile["directory"] = directory;
                    profile["online"] = online;
                    profile["email"] = email;
                    profile["pass"] = pass;
                    profile["customversion"] = customversion;
                    profile["img"] = DecideImage;
                    profile["ffmem"] = FFMem;
                    profile["rungapple"] = RunGapple;

                    break;
                }
            }

            string updatedJsonContent = jsonData.ToString();
            File.WriteAllText(FFWP.JsonData.Value.userjson, updatedJsonContent);
            this.Hide();






        }

        private int GetRamValueInMB(double valueInGB)
        {
            return (int)Math.Floor(valueInGB * 1024);
        }

        public void encrypt(string email, string pass)
        {
            try
            {
                var emailwill = System.Text.Encoding.UTF8.GetBytes(email);
                encryptedemail = System.Convert.ToBase64String(emailwill);

                var passwill = System.Text.Encoding.UTF8.GetBytes(pass);
                encryptedpass = System.Convert.ToBase64String(passwill);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        public async void cleanedtoken()
        {
            WebClient client = new WebClient();
            string customtoken = customdir.Text + "/launcher_profiles.json";
            string standarttoken = MinecraftPath.GetOSDefaultPath() + "/launcher_profiles.json";
            CMLauncher cmLauncher = new CMLauncher(new MinecraftPath());
            if (FabricTOG.IsOn == false)
            {
                if (ForgeTOG.IsOn == false)
                {

                }
            }
           else if (ForgeTOG.IsOn == false)
            {
                if (FabricTOG.IsOn == false)
                {

                }
            }
            else
            {
               if(vercombo.SelectedItem.ToString() == null)
                {
                   
                }
                else
                {
                    if (!Directory.Exists(MinecraftPath.GetOSDefaultPath()))
                    {

                        Directory.CreateDirectory(cmLauncher.ToString());
                    }
                    ServicePointManager.DefaultConnectionLimit = 256;
                    MVersionCollection AsyncVersion = await cmLauncher.GetAllVersionsAsync();
                    MVersion MKMVER = await AsyncVersion.GetVersionAsync(vercombo.SelectedItem.ToString());
                }
            }

            if (customdir.Text == directory)
            {
                //if (!File.Exists(standarttoken))
                //{
                //    try
                //    {
                //        client.DownloadFile(new Uri("https://ffgapple.github.io/preauth/ffgapple.json"), standarttoken);
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.ToString());
                //        Environment.Exit(-1);

                //    }
                //}
            }
            else if(customdir.Text == "")
            {

               
            }
            else if(customdir.Text == FFWP.Language.Strings.privdir)
            {

            }
            else
            {
                if (File.Exists(customtoken))
                {
                    try
                    {
                        File.Delete(customtoken);
                        client.DownloadFile(new Uri("https://ffgapple.github.io/preauth/ffgapple.json"), customtoken);
                    }
                    catch
                    {
                        MessageBox.Show("sussybaka");
                        Environment.Exit(-1);

                    }


                }
                else
                {
                    try
                    {

                        client.DownloadFile(new Uri("https://ffgapple.github.io/preauth/ffgapple.json"), customtoken);
                    }
                    catch
                    {
                        MessageBox.Show("sussybaka");
                        Environment.Exit(-1);

                    }
                }
            }
        }

        public async void mesajbox(string icerik, string baslik, string buton)
        {
            MessageBox.Show(icerik, baslik, MessageBoxButton.OK);

            //await new ModernWpf.Controls.ContentDialog()
            //{
            //    Title = baslik,
            //    Content = icerik,
            //    PrimaryButtonText = buton,

            //}.ShowAsync();


        }

        public void EditSetText(string Mode)
        {
            
            this.Title = FFWP.Language.Strings.edittool + " (" + id + " / " + version + ")";
            addbtt.Content = FFWP.Language.Strings.edittool;
            profilename.GotFocus += profileremove;
            profilename.LostFocus += profileadd;
            username.GotFocus += userremove;
            username.LostFocus += useradd;
            widthtxt.GotFocus += widthremove;
            widthtxt.LostFocus += widthadd;
            heigthtxt.GotFocus += heightremove;
            heigthtxt.LostFocus += heightadd;
            customdir.GotFocus += customremove;
            customdir.LostFocus += customadd;
            mail.GotFocus += mailremove;
            mail.LostFocus += mailadd;
            passtxt.GotFocus += passremove;
            passtxt.LostFocus += passadd;
            serverportt.GotFocus += serverportremove;
            serverportt.LostFocus += serverportadd;
            serveript.GotFocus += serveripremove;
            serveript.LostFocus += serveripadd;
         
            TakeAll("Vanilla");
            if (Mode == "Online")
            {
                profilename.Text = id;
                passholder.Text = FFWP.Language.Strings.place2;
                if (width == "")
                {
                    if (heigth == "")
                    {
                        heigthtxt.Text = FFWP.Language.Strings.yuksektext;
                        widthtxt.Text = FFWP.Language.Strings.genistext;
                    }
                }
                else
                {
                    widthtxt.Text = width;
                    heigthtxt.Text = heigth;
                }
                if (heigth == "")
                {
                    if (width == "")
                    {
                        heigthtxt.Text = FFWP.Language.Strings.yuksektext;
                        widthtxt.Text = FFWP.Language.Strings.genistext;
                    }
                }
                else
                {
                    widthtxt.Text = width;
                    heigthtxt.Text = heigth;
                }

                if (directory != "")
                {
                    customdir.Text = directory;
                }
                else
                {
                    customdir.Text = FFWP.Language.Strings.privdir;
                }
                mail.Text = decryptedemail;
                passtxt.Password = decryptedpass;
              
            }
            if (Mode == "Offline")
            {
                username.Text = usernameS;
                profilename.Text = id;
                if (width == "")
                {
                    if (heigth == "")
                    {
                        heigthtxt.Text = FFWP.Language.Strings.yuksektext;
                        widthtxt.Text = FFWP.Language.Strings.genistext;
                    }
                }
                else
                {
                    widthtxt.Text = width;
                    heigthtxt.Text = heigth;
                }
                if (heigth == "")
                {
                    if (width == "")
                    {
                        heigthtxt.Text = FFWP.Language.Strings.yuksektext;
                        widthtxt.Text = FFWP.Language.Strings.genistext;
                    }
                }
                else
                {
                    widthtxt.Text = width;
                    heigthtxt.Text = heigth;
                }
                if(directory != "")
                {
                    customdir.Text = directory;
                }
                else
                {
                    customdir.Text = FFWP.Language.Strings.privdir;
                }

              
               
            }
        }


        public void checkstatus()
        {

            
            string selectedvers = vercombo.Text;
           
            if (nativeGapple.Modules.ProfileMode == "Online")
            {
              
                if (profilename.Text == FFWP.Language.Strings.profilenametext || mail.Text == "E-Mail" || passtxt.Password == "" || string.IsNullOrWhiteSpace(selectedvers))
                {
                 
                   
                    crederror.Dispatcher.Invoke(() =>
                    {
                        crederror.Visibility = Visibility.Visible;
                    });
               
                 
                 


                    if ((username.Text == FFWP.Language.Strings.usernametext) == false)
                    {
                        crederror.Visibility = Visibility.Hidden;
                    }


                    if ((mail.Text == "E-Mail") == false)
                    {
                        if ((passtxt.Password == FFWP.Language.Strings.passplace) == false)
                        {
                            crederror.Visibility = Visibility.Hidden;
                        }


                    }

                    if ((passtxt.Password == FFWP.Language.Strings.passplace) == false)
                    {
                        if ((mail.Text == "E-Mail") == false)
                        {
                            crederror.Visibility = Visibility.Hidden;
                        }
                    }

                    //if (!string.IsNullOrWhiteSpace(selectedvers))
                    //{
                    //    mcvererror.Visibility = Visibility.Hidden;
                    //}

                }
                else
                {
                    checkcustom();
                }
            }

            if (nativeGapple.Modules.ProfileMode == "Offline")
            {
                
                if (profilename.Text == FFWP.Language.Strings.profilenametext || username.Text == FFWP.Language.Strings.usernametext || string.IsNullOrWhiteSpace(selectedvers))
                {
                    profilnameerror.Dispatcher.Invoke(() =>
                    {
                        profilnameerror.Visibility = Visibility.Visible;
                    });
                   

                    

                    if ((username.Text == FFWP.Language.Strings.usernametext) == false)
                    {
                        profilnameerror.Visibility = Visibility.Hidden;
                    }


                    //if ((mail.Text == "E-Mail") == false)
                    //{
                    //    if ((passtxt.Password == FFWP.Language.Strings.passplace) == false)
                    //    {
                    //        crederror.Visibility = Visibility.Hidden;
                    //    }


                    //}

                    //if ((passtxt.Password == FFWP.Language.Strings.passplace) == false)
                    //{
                    //    if ((mail.Text == "E-Mail") == false)
                    //    {
                    //        crederror.Visibility = Visibility.Hidden;
                    //    }
                    //}

                    //if (!string.IsNullOrWhiteSpace(selectedvers))
                    //{
                    //    mcvererror.Visibility = Visibility.Hidden;
                    //}

                }
                else
                {
                   
                    checkcustom();
                }
            }




        }

        string alreadycustom, rungp;

        public void EditRoutine()
        {
            string jsonFilePath = FFWP.JsonData.Value.userjson;
            string jsonString = File.ReadAllText(jsonFilePath);
            JObject jsonObject = JObject.Parse(jsonString);
            JArray profiles = (JArray)jsonObject["Profiles"];
            JObject targetProfile = profiles
                .Children<JObject>()
                .FirstOrDefault(p => (string)p["secret"] == GPSecret);

            if (targetProfile != null)
            {

                customversion =  targetProfile["customversion"].ToString();
                encryptedpass = targetProfile["pass"].ToString();
                encryptedemail = targetProfile["email"].ToString();
                online = targetProfile["online"].ToString();
                directory = targetProfile["directory"].ToString();
                heigth = targetProfile["heigth"].ToString();
                width = targetProfile["width"].ToString();
                directport = targetProfile["directport"].ToString();
                directserver = targetProfile["directserver"].ToString();
                secret = targetProfile["secret"].ToString();
                id = targetProfile["idjson"].ToString();
                usernameS = targetProfile["username"].ToString();
                creationdate = targetProfile["creationdate"].ToString();
                version = targetProfile["xversion"].ToString();
                alreadyversion = targetProfile["xversion"].ToString();
                codename = targetProfile["codename"].ToString();
                rungp = targetProfile["rungapple"].ToString();
                ffmem = targetProfile["ffmem"].ToString();
                double RamInDouble = Convert.ToDouble(ffmem);
                double FinalRam = RamInDouble / 1024;
                ramslider.Value = FinalRam;
                alreadycustom = codename;
                if(rungp == "Yes")
                {
                    hizliTOG.IsOn = true;
                }
                else if(rungp == "No")
                {
                    hizliTOG.IsOn = true;
                }
                else
                {
                    hizliTOG.IsOn = false;
                }
            }
            else
            {
               
            }
            hizli.Text = FFWP.Language.Strings.faster;
            if (online == "yes")
            {
                mail.Visibility = Visibility.Visible;
                passtxt.Visibility = Visibility.Visible;
                username.Visibility = Visibility.Hidden;
                decrypt(encryptedemail, encryptedpass);
                EditSetText("Online");
            }
            else
            {
                mail.Visibility = Visibility.Hidden;
                passtxt.Visibility = Visibility.Hidden;
                EditSetText("Offline");
            }
        }

        private void FabricTOG_Toggled(object sender, RoutedEventArgs e)
        {
           

            if (FabricTOG.IsOn == true)
            {
                CustomON = true;
                TakeAll("Fabric");
                ForgeTOG.IsEnabled = false;
            }
            else
            {
                CustomON = false;
                TakeAll("Vanilla");
                ForgeTOG.IsEnabled = true;
            }
        }

        private void ForgeTOG_Toggled(object sender, RoutedEventArgs e)
        {
            if (ForgeTOG.IsOn == true)
            {
                CustomON = true;
                TakeAll("Forge");
                FabricTOG.IsEnabled = false;
            }
            else
            {
                CustomON = false;
                TakeAll("Vanilla");
                FabricTOG.IsEnabled = true;
            }
        }
        WebClient client = new WebClient();
        string dowver;

        public void ext()
        {
            try
            {
                using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(customdir.Text + @"/versions/" + dowver + ".zip"))
                {
                    foreach (Ionic.Zip.ZipEntry e in zip)
                    {
                        e.Extract(customdir.Text + @"/versions/");
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        public void cleandir()
        {
            try
            {
                if (Directory.Exists(customdir.Text + @"/versions/"))
                {
                    Directory.Delete(customdir.Text + @"/versions/");
                }
            }
            catch (Exception)
            {

                System.IO.DirectoryInfo di = new DirectoryInfo(customdir.Text + @"/versions/");
                try
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }

                }
                catch
                {

                }
            }


        }

        public void dowforg()
        {
            cleandir();
            dowver = "forge";
            if (customdir.Text != FFWP.Language.Strings.privdir)
            {
                if (!Directory.Exists(customdir.Text + @"/versions/"))
                {
                    Directory.CreateDirectory(customdir.Text + @"/versions/");

                }
            }
            else
            {

            }
            client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");
            ext();
        }

        private void ramslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ramslider.Value = Math.Round(ramslider.Value, 0);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                customdir.Text = dialog.SelectedPath;
            }
        }

        public void dowfab()
            {

            cleandir();
            dowver = "fabric";
            if (customdir.Text != FFWP.Language.Strings.privdir)
            {
                if (!Directory.Exists(customdir.Text + @"/versions/"))
                {
                    Directory.CreateDirectory(customdir.Text + @"/versions/");

                }
            }
            else
            {
            }

            client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/fabric.zip");
            ext();
        }

            private void vercombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vercombo.SelectedItem == null)
            {
                
                datedata.Text = "?!";
                pubdata.Text = "?!";
                devdata.Text = "?!";
            }
            else
            {
               
                try
                {
                    DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jsonVerisi);
                    DataTable dataTable = dataSet.Tables[SelectedMinecraft];
                    //if (vercombo.SelectedIndex >= 0 && vercombo.SelectedIndex < dataTable.Rows.Count)
                    //{
                    //    datedata.Text = dataTable.Rows[vercombo.SelectedIndex]["Date"].ToString();
                    //    pubdata.Text = dataTable.Rows[vercombo.SelectedIndex]["Publisher"].ToString();
                    //    devdata.Text = dataTable.Rows[vercombo.SelectedIndex]["Developer"].ToString();
                    //    DownloadString = dataTable.Rows[vercombo.SelectedIndex]["Download"].ToString();
                    //}

                    if (vercombo.SelectedIndex >= 0)
                    {
                        datedata.Text = dataTable.Rows[vercombo.SelectedIndex]["Date"].ToString();
                        pubdata.Text = dataTable.Rows[vercombo.SelectedIndex]["Publisher"].ToString();
                        devdata.Text = dataTable.Rows[vercombo.SelectedIndex]["Developer"].ToString();
                        Developer = dataTable.Rows[vercombo.SelectedIndex]["Developer"].ToString();
                        DownloadString = dataTable.Rows[vercombo.SelectedIndex]["Download"].ToString();
                        if (Developer == "Fabric Team")
                        {
                            RealName = dataTable.Rows[vercombo.SelectedIndex]["RLFab"].ToString();
                        }
                        else if (Developer == "Forge Team")
                        {
                            RealName = dataTable.Rows[vercombo.SelectedIndex]["RLForg"].ToString();
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("FFNet Can't Parse The Version Data!");
                    Environment.Exit(-1);
                }
            }
        }

        public void profileremove(object sender, EventArgs e)
        {
            if (profilename.Text == FFWP.Language.Strings.profilenametext)
            {
                profilename.Text = "";
            }
        }

        public void userremove(object sender, EventArgs e)
        {
            if (username.Text == FFWP.Language.Strings.usernametext)
            {
                username.Text = "";
            }
        }

        public void widthremove(object sender, EventArgs e)
        {
            if (widthtxt.Text == FFWP.Language.Strings.genistext)
            {
                widthtxt.Text = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void addbtt_Click(object sender, RoutedEventArgs e)
        {
            checkstatus();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public void heightremove(object sender, EventArgs e)
        {
            if (heigthtxt.Text == FFWP.Language.Strings.yuksektext)
            {
                heigthtxt.Text = "";
            }
        }

        public void customremove(object sender, EventArgs e)
        {
            if (customdir.Text == FFWP.Language.Strings.privdir)
            {
                customdir.Text = "";
            }
        }

        public void serveripremove(object sender, EventArgs e)
        {
            if (serveript.Text == "Server IP")
            {
                serveript.Text = "";
            }
        }

        public void serverportremove(object sender, EventArgs e)
        {
            if (serverportt.Text == "Port")
            {
                serverportt.Text = "";
            }
        }

        public void mailremove(object sender, EventArgs e)
        {
            if (mail.Text == "E-Mail")
            {
                mail.Text = "";
            }
        }

        public void passremove(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(passtxt.Password))
            {
                passholder.Visibility = Visibility.Hidden;
            }
        }

        public void profileadd(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(profilename.Text))
                profilename.Text = FFWP.Language.Strings.profilenametext;
        }

        public void useradd(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username.Text))
                username.Text = FFWP.Language.Strings.usernametext;
        }

        public void widthadd(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(widthtxt.Text))
                widthtxt.Text = FFWP.Language.Strings.genistext;
        }

        public void heightadd(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(heigthtxt.Text))
                heigthtxt.Text = FFWP.Language.Strings.yuksektext;
        }

        public void customadd(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(customdir.Text))
                customdir.Text = FFWP.Language.Strings.privdir;
        }

        public void serveripadd(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(serveript.Text))
                serveript.Text = "Server IP";
        }

        public void serverportadd(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(serverportt.Text))
                serverportt.Text = "Port";
        }

        public void mailadd(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(mail.Text))
                mail.Text = "E-Mail";
        }

        public void passadd(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(passtxt.Password))
                passholder.Visibility = Visibility.Visible;
        }

    }
}
