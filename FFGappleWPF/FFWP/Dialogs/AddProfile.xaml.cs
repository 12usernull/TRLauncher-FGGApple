using CmlLib.Core;
using FFWP.Modules;
using ICSharpCode.SharpZipLib.Zip;
using ModernWpf.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization.Json;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
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
using Windows.Media.Protection.PlayReady;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Ionic.Zip;
using static FFWP.Dialogs.AddProfileOffline;
using CmlLib.Core.Version;
using SharpCompress.Common;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Data;
using System.Security.Cryptography;
using FFWP.JsonData;
using System.Data.SqlTypes;
using System.Windows.Threading;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Management;
using System.Globalization;
//using Windows.UI.Xaml;

namespace FFWP.Dialogs
{
    /// <summary>
    /// AddProfileOffline.xaml etkileşim mantığı
    /// </summary>
    public partial class AddProfileOffline : ModernWpf.Controls.ContentDialog
    {
        String id, online, directport, width, heigth, directserver, version, directory, email, pass;
        String encryptedemail, encryptedpass, decryptedemail, decryptedpass;
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
        public string ProfileEdit;
        public string ProfileData;
        public string ProfileMode;
        string SelectedMinecraft;
        public string DownloadString;
        public bool Create;
        string url = @"https://ffgapple.github.io/JSON/GappleList.json";
        string jsonVerisi = "";

        WebClient client = new WebClient();
        int ramgoritmaninrami;

        public AddProfileOffline()
        {
            string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
            FFWP.Modules.SaveINI first = new FFWP.Modules.SaveINI(localfile);
            string langu = first.Read("LocalSettingsForGapple", "Lang");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langu);
            InitializeComponent();
            settext();
            TakeAll("Vanilla");
            double systemMemoryGB = SystemInfo.GetPhysicalAvailableMemoryInGB();
            ramslider.Maximum = systemMemoryGB;

        }
        public class ThumbToolTipValueConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                double ramValue = (double)value;
                return $"{ramValue:N2} GB";
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
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

        public void EditSetText(string Mode)
        {

            this.Title = FFWP.Language.Strings.edittool;
            addbtt.Content = FFWP.Language.Strings.addbtt;
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
            ReadyVersion.Visibility = Visibility.Visible;
            if (version.Contains("fabric"))
            {
                FabricTOG.IsOn = true;
                //TakeAll("Fabric");
            }
            if (version.Contains("forge"))
            {
                ForgeTOG.IsOn = true;
                TakeAll("Forge");
            }
            else
            {
                TakeAll("Vanilla");
            }
            if (Mode == "Online")
            {
                profilename.Text = nativeGapple.Modules.ProfileData;
                if(width == "")
                {
                    if(heigth == "")
                    {
                        heigthtxt.Text = FFWP.Language.Strings.yuksektext;
                        widthtxt.Text = FFWP.Language.Strings.genistext;
                    }
                }
                if (heigth == "")
                {
                    if (width == "")
                    {
                        heigthtxt.Text = FFWP.Language.Strings.yuksektext;
                        widthtxt.Text = FFWP.Language.Strings.genistext;
                    }
                }
                widthtxt.Text = width;
                heigthtxt.Text = heigth;
                customdir.Text = directory;
                mail.Text = email;
                passtxt.Password = pass;
                ReadyVersion.Text = version;
            }
            if(Mode == "Offline")
            {
                username.Text = id;
                profilename.Text = nativeGapple.Modules.ProfileData;
                widthtxt.Text = width;
                heigthtxt.Text = heigth;
                customdir.Text = directory;
                ReadyVersion.Text = version;
            }
        }

        public void EditRoutine()
        {
            try
            {
                XDocument xDoc = XDocument.Load(usr + nativeGapple.Modules.ProfileData);
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
                    email = profile.Element("email").Value;
                    pass = profile.Element("pass").Value;
                    online = profile.Element("online").Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (online == "yes")
            {
                mail.Visibility = Visibility.Visible;
                passtxt.Visibility = Visibility.Visible;
                username.Visibility = Visibility.Hidden;
                decrypt(email, pass);
                EditSetText("Online");
            }
           else
            {
                mail.Visibility = Visibility.Hidden;
                passtxt.Visibility = Visibility.Hidden;
                EditSetText("Offline");
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
            if (passholder.Text == FFWP.Language.Strings.place2)
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
                passholder.Text = FFWP.Language.Strings.place2;
        }

        public void settext()
        {
            if (nativeGapple.Modules.ProfileMode == "Offline")
            {
                mail.Visibility = Visibility.Hidden;
                passtxt.Visibility = Visibility.Hidden;
                passholder.Visibility = Visibility.Hidden;
            }
           else if (nativeGapple.Modules.ProfileMode == "Online")
            {
                mail.Visibility = Visibility.Visible;
                passtxt.Visibility = Visibility.Visible;
                username.Visibility = Visibility.Hidden;
                passholder.Visibility = Visibility.Visible;
             
            }
            else
            {
                this.Hide();
            }
            this.Title = FFWP.Language.Strings.addprofile;
            addbtt.Content = FFWP.Language.Strings.addbtt;
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
            
            passholder.Text = FFWP.Language.Strings.place2;
           
            profilename.Text = FFWP.Language.Strings.profilenametext;
            username.Text = FFWP.Language.Strings.usernametext;
            widthtxt.Text = FFWP.Language.Strings.genistext;
            heigthtxt.Text = FFWP.Language.Strings.yuksektext;
            customdir.Text = FFWP.Language.Strings.privdir;
            hizli.Text = FFWP.Language.Strings.faster;


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        public void checkstatus()
        {


            string selectedvers = vercombo.Text;

            if(nativeGapple.Modules.ProfileMode == "Online")
            {
                if (profilename.Text == FFWP.Language.Strings.profilenametext || mail.Text == "E-Mail"  || passtxt.Password == "" || string.IsNullOrWhiteSpace(selectedvers))
                {

                    profilnameerror.Visibility = Visibility.Visible;
                    crederror.Visibility = Visibility.Visible;
                    mcvererror.Visibility = Visibility.Visible;
                    if ((profilename.Text == FFWP.Language.Strings.profilenametext) == false)
                    {
                        profilnameerror.Visibility = Visibility.Hidden;
                    }
                    if(!(passtxt.Password == ""))
                    {
                        if(!(mail.Text == "E-Mail"))
                        {
                            crederror.Visibility = Visibility.Hidden;
                        }
                    }
                    if (!(mail.Text == "E-Mail"))
                    {
                        if (!(passtxt.Password == ""))
                        {
                            crederror.Visibility = Visibility.Hidden;
                        }
                    }




                    if (!string.IsNullOrWhiteSpace(selectedvers))
                    {
                        mcvererror.Visibility = Visibility.Hidden;
                    }

                }
                else
                {
                    checkcustom();
                }
            }

            if(nativeGapple.Modules.ProfileMode == "Offline")
            {
                if (profilename.Text == FFWP.Language.Strings.profilenametext || username.Text == FFWP.Language.Strings.usernametext || string.IsNullOrWhiteSpace(selectedvers))
                {

                    profilnameerror.Visibility = Visibility.Visible;
                    crederror.Visibility = Visibility.Visible;
                    mcvererror.Visibility = Visibility.Visible;



                    if ((profilename.Text == FFWP.Language.Strings.profilenametext) == false)
                    {
                        profilnameerror.Visibility = Visibility.Hidden;
                    }


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

                    if (!string.IsNullOrWhiteSpace(selectedvers))
                    {
                        mcvererror.Visibility = Visibility.Hidden;
                    }

                }
                else
                {
                    checkcustom();
                }
            }
            if (nativeGapple.Modules.ProfileMode == "XBOX")
            {
                if (profilename.Text == FFWP.Language.Strings.profilenametext || string.IsNullOrWhiteSpace(selectedvers))
                {

                    profilnameerror.Visibility = Visibility.Visible;
                    if (!string.IsNullOrWhiteSpace(selectedvers))
                    {
                        mcvererror.Visibility = Visibility.Hidden;
                    }

                }
                else
                {
                    checkcustom();
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

        public async void cleanedtoken()
        {
            WebClient client = new WebClient();
            string customtoken = customdir.Text + "/launcher_profiles.json";
            string standarttoken = MinecraftPath.GetOSDefaultPath() + "/launcher_profiles.json";
            CMLauncher cmLauncher = new CMLauncher(new MinecraftPath());
           if(FabricTOG.IsOn == false)
            {
                if(ForgeTOG.IsOn == false)
                {

                }
            }

           else if (ForgeTOG.IsOn == false)
            {
                if(FabricTOG.IsOn == false)
                {

                }
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

            if (customdir.Text == FFWP.Language.Strings.privdir)
            {
                if (!File.Exists(standarttoken))
                {
                    try
                    {
                        client.DownloadFile(new Uri("https://ffgapple.github.io/preauth/ffgapple.json"), standarttoken);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        Environment.Exit(-1);

                    }
                }
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
                        MessageBox.Show("2");
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
                        MessageBox.Show("3");
                        Environment.Exit(-1);

                    }
                }
            }
        }

        public void copybase()
        {
            string basexml = basedata;
            string copyxml = usr + profilename.Text + ".xml";
            try
            {
                File.Copy(basexml, copyxml, true);
            }
            catch (Exception ex)
            {

                mesajbox(FFWP.Language.Strings.xmlerr, "FFNet 1.0", "OK");
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Environment.Exit(-1);
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
        private static Random random = new Random();
        private static string RandomString(int length)
        {
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789@!?";
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                var c = pool[random.Next(0, pool.Length)];
                builder.Append(c);
            }

            return builder.ToString();
        }


       


        class Profile
        {
            public string username { get; set; }
            public string idjson { get; set; }
            public string secret { get; set; }
            public string directserver { get; set; }
            public string directport { get; set; }
            public string width { get; set; }
            public string heigth { get; set; }
            public string xversion { get; set; }
            public string rungapple { get; set; }
            public string codename { get; set; }
            public string directory { get; set; }
            public string online { get; set; }
            public string creationdate { get; set; }
            public string modded { get; set; }
            public string customversion { get; set; }
            public string img { get; set; }
            public string email { get; set; }
            public string pass { get; set; }
            public string ffmem { get; set; }
        }

        class RootObject
        {
            public List<Profile> Profiles { get; set; }
        }

       
        public void jsonadd()
        {
            string json = File.ReadAllText(FFWP.JsonData.Value.userjson);

            var data = JsonConvert.DeserializeObject<RootObject>(json);

            string directserver;
            string directport;
            string width;
            string heigth;
            string directory;
            string online;
            string email;
            string pass;
            string version;
            string modded;
            string customversion;
            string DecideImage;
            string UsernameTXT;
            string XBOX;
            string FFMem;
            string RunGapple;

            if(hizliTOG.IsOn == true)
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
                directserver =  serveript.Text;
            }

            if (serverportt.Text == "Port")
            {
                directport = "";
            }
            else
            {
                directport =serverportt.Text;
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
            else
            {
                customversion = "Vanilla";
                version = vercombo.SelectedItem.ToString();
            }
            double RamSizeGB = ramslider.Value;
            FFMem = GetRamValueInMB(RamSizeGB).ToString();

            //if (vercombo.SelectedItem.ToString() == "1.19.4 Fabric")
            //{
            //    //version = "fabric-loader-0.14.21-1.19.3";
            //    customversion = "Fabric";
            //}
            //if (vercombo.SelectedItem.ToString() == "1.19.3 Fabric")
            //{
            //    version = "fabric-loader-0.14.21-1.19.3";
            //    customversion = "Fabric";
            //}
            //if (vercombo.SelectedItem.ToString() == "1.19.2 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.19.2";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.19.1 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.19.1";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.19 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.19";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.18.2 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.18.2";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.18.1 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.18.1";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.18 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.18";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.17.1 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.17.1";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.17 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.17";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.16.5 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.16.5";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.16.4 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.16.4";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.16.3 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.16.3";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.16.2 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.16.2";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.16.1 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.16.1";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.16 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.16";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.15.2 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.15.2";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.15.1 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.15.1";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.15 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.15";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.14.4 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.14.4";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.14.3 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.14.3";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.14.2 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.14.2";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.14.1 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.14.1";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.14 Fabric")
            //{
            //    version = "fabric-loader-0.14.9-1.14";
            //    customversion = "Fabric";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.19.2 Forge")
            //{
            //    version = "1.19.2-forge-43.1.32";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.19.1 Forge")
            //{
            //    version = "1.19.1-forge-42.0.9";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.19 Forge")
            //{
            //    version = "1.19-forge-41.1.0";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.18.1 Forge")
            //{
            //    version = "1.18.1-forge-39.1.2";
            //    customversion = "Forge";
            //}

            //else if (vercombo.SelectedItem.ToString() == "1.18 Forge")
            //{
            //    version = "1.18-forge-38.0.17";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.17.1 Forge")
            //{
            //    version = "1.17.1-forge-37.1.1";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.16.5 Forge")
            //{
            //    version = "1.16.5-forge-36.2.39";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.16.4 Forge")
            //{
            //    version = "1.16.4-forge-35.1.37";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.16.3 Forge")
            //{
            //    version = "1.16.3-forge-34.1.42";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.16.2 Forge")
            //{
            //    version = "1.16.2-forge-33.0.61";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.16.1 Forge")
            //{
            //    version = "1.16.1-forge-32.0.108";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.15.2 Forge")
            //{
            //    version = "1.15.2-forge-31.2.57";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.15.1 Forge")
            //{
            //    version = "1.15.1-forge-30.0.51";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.15 Forge")
            //{
            //    version = "1.15-forge-29.0.4";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.14.3 Forge")
            //{
            //    version = "1.14.3-forge-27.0.60";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.14.2 Forge")
            //{
            //    version = "1.14.2-forge-26.0.63";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.13.2 Forge")
            //{
            //    version = "1.13.2-forge-25.0.223";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.12.2 Forge")
            //{
            //    version = "1.12.2-forge-14.23.5.2860";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.12.1 Forge")
            //{
            //    version = "1.12.1-forge1.12.1-14.22.1.2485";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.12 Forge")
            //{
            //    version = "1.12-forge1.12-14.21.1.2443";
            //    customversion = "Forge";
            //}
            //else if (vercombo.SelectedItem.ToString() == "1.8.9 Forge")
            //{
            //    version = "1.8.9-forge1.8.9-11.15.1.2318-1.8.9";
            //    customversion = "Forge";
            //}
            //else
            //{
            //    version = vercombo.SelectedItem.ToString();
            //    //customversion = "Vanilla";
            //}


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
                pass =  "";
                UsernameTXT = username.Text;
            }
          
            if (username.Text == FFWP.Language.Strings.usernametext)
            {
                DecideImage = "pack://application:,,,/FFWP;component/GUI/unknown.png";

            }
            else if(nativeGapple.Modules.ProfileMode == "Online")
            {
                DecideImage = "pack://application:,,,/FFWP;component/GUI/unknown.png";
            }
            else
            {
                DecideImage = "https://minotar.net/cube/" + username.Text + "/100.png";
            }


            Profile newProfile = new Profile
            {

                idjson = profilename.Text,
                username = UsernameTXT,
                secret = RandomString(12),
                directserver = directserver,
                directport = directport,
                width = width,
                heigth = heigth,
                xversion = version,
                codename = vercombo.SelectedItem.ToString(),
                customversion = customversion,
                modded = null,
                directory = directory,
                online = online,
                email = email,
                pass = pass,
                creationdate = DateTime.Now.ToString("MM/dd/yyyy"),
                img = DecideImage,
                ffmem = FFMem,
                rungapple = RunGapple,
            };



            CheckCred();
          
            if(Create == true)
            {
                //data.Profiles.Add(newProfile);

                //string updatedJson = JsonConvert.SerializeObject(data, Formatting.Indented);

                //File.WriteAllText(FFWP.JsonData.Value.userjson, updatedJson);
                string filePath = FFWP.JsonData.Value.userjson;

                if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
                {
                    data.Profiles.Add(newProfile);

                    string updatedJson = JsonConvert.SerializeObject(data, Formatting.Indented);

                    File.WriteAllText(FFWP.JsonData.Value.userjson, updatedJson);
                }
                else
                {
                    // Dosya boş veya yok, yeni JSON oluştur
                   
                    data.Profiles.Add(newProfile);
                    string newJson = JsonConvert.SerializeObject(data, Formatting.Indented);

                    File.WriteAllText(filePath, newJson);
                }
            }
            else
            {

            }
           

        }

        private int GetRamValueInMB(double valueInGB)
        {
            return (int)Math.Floor(valueInGB * 1024); 
        }

        public void CheckCred()
        {
            string json = File.ReadAllText(FFWP.JsonData.Value.userjson);

            if(json == "")
            {
                Create = true;
            }
            else
            {

                var data = JsonConvert.DeserializeObject<FFWP.JsonData.Root>(json);


                string profileIdToDelete = profilename.Text;


                FFWP.JsonData.Profile profileToDelete = data.Profiles.Find(p => p.idjson == profileIdToDelete);
                if (profileToDelete != null)
                {
                    Create = false;
                    MessageBox.Show(FFWP.Language.Strings.alex);
                }
                else
                {
                    Create = true;
                }
            }

        }

        public void xmladd()
        {
            try
            {
                XDocument xDoc = XDocument.Load(usr + profilename.Text + ".xml");
                XElement tree = xDoc.Root;

                XElement newElement = new XElement("offline");
                XAttribute idAttribute = new XAttribute("id", username.Text);
                XElement directserver;
                XElement directport;
                XElement width;
                XElement heigth;
                XElement directory; 
                XElement online;
                XElement email;
                XElement pass;
                if (serveript.Text == "Server IP")
                {

                    directserver = new XElement("directserver", "");
                }
                else
                {
                    directserver = new XElement("directserver", serveript.Text);
                }

                if(serverportt.Text == "Port")
                {
                     directport = new XElement("directport", "");
                }
                else
                {
                    directport = new XElement("directport", serverportt.Text);
                }

                if(widthtxt.Text == FFWP.Language.Strings.genistext)
                {
                    width = new XElement("width", "");
                }
                else
                {
                    width = new XElement("width", widthtxt.Text);
                }

                if (customdir.Text == FFWP.Language.Strings.privdir)
                {
                    directory = new XElement("directory", "");
                }
                else
                {
                    directory = new XElement("directory", customdir.Text);
                }

                if (heigthtxt.Text == FFWP.Language.Strings.yuksektext)
                {
                    heigth = new XElement("heigth", "");
                }
                else
                {
                    heigth = new XElement("heigth", heigthtxt.Text);
                }

                if (nativeGapple.Modules.ProfileMode == "Online")
                {
                    encrypt(mail.Text, passtxt.Password);
                    online = new XElement("online", "yes");
                    email = new XElement("email", encryptedemail);
                    pass = new XElement("pass", encryptedpass);
                }
                else
                {
                    online = new XElement("online", "no");
                    email = new XElement("email", "");
                    pass = new XElement("pass", "");
                }

              

                //if(ramtrack.Value == 1)
                //{

                //    XElement ram = new XElement("ram", "1024");
                //}

                if (vercombo.SelectedItem.ToString() == "1.19.2 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.19.2");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.19.1 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.19.1");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.19 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.19");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.18.2 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.18.2");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.18.1 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.18.1");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.18 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.18");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.17.1 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.17.1");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.17 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.17");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.16.5 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.16.5");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.16.4 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.16.4");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.16.3 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.16.3");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.16.2 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.16.2");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.16.1 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.16.1");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.16 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.16");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.15.2 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.15.2");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.15.1 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.15.1");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.15 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.15");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.14.4 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.14.4");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.14.3 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.14.3");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.14.2 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.14.2");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.14.1 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.14.1");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.14 Fabric")
                {
                    XElement version = new XElement("version", "fabric-loader-0.14.9-1.14");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.19.2 Forge")
                {
                    XElement version = new XElement("version", "1.19.2-forge-43.1.32");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.19.1 Forge")
                {
                    XElement version = new XElement("version", "1.19.1-forge-42.0.9");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.19 Forge")
                {
                    XElement version = new XElement("version", "1.19-forge-41.1.0");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.18.1 Forge")
                {
                    XElement version = new XElement("version", "1.18.1-forge-39.1.2");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.18 Forge")
                {
                    XElement version = new XElement("version", "1.18-forge-38.0.17");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.17.1 Forge")
                {
                    XElement version = new XElement("version", "1.17.1-forge-37.1.1");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.16.5 Forge")
                {
                    XElement version = new XElement("version", "1.16.5-forge-36.2.39");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.16.4 Forge")
                {
                    XElement version = new XElement("version", "1.16.4-forge-35.1.37");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.16.3 Forge")
                {
                    XElement version = new XElement("version", "1.16.3-forge-34.1.42");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.16.2 Forge")
                {
                    XElement version = new XElement("version", "1.16.2-forge-33.0.61");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.16.1 Forge")
                {
                    XElement version = new XElement("version", "1.16.1-forge-32.0.108");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.15.2 Forge")
                {
                    XElement version = new XElement("version", "1.15.2-forge-31.2.57");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.15.1 Forge")
                {
                    XElement version = new XElement("version", "1.15.1-forge-30.0.51");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.15 Forge")
                {
                    XElement version = new XElement("version", "1.15-forge-29.0.4");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.14.3 Forge")
                {
                    XElement version = new XElement("version", "1.14.3-forge-27.0.60");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.14.2 Forge")
                {
                    XElement version = new XElement("version", "1.14.2-forge-26.0.63");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.13.2 Forge")
                {
                    XElement version = new XElement("version", "1.13.2-forge-25.0.223");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.12.2 Forge")
                {
                    XElement version = new XElement("version", "1.12.2-forge-14.23.5.2860");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.12.1 Forge")
                {
                    XElement version = new XElement("version", "1.12.1-forge1.12.1-14.22.1.2485");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.12 Forge")
                {
                    XElement version = new XElement("version", "1.12-forge1.12-14.21.1.2443");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else if (vercombo.SelectedItem.ToString() == "1.8.9 Forge")
                {
                    XElement version = new XElement("version", "1.8.9-forge1.8.9-11.15.1.2318-1.8.9");
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "yes");
                }
                else
                {
                    XElement version = new XElement("version", vercombo.SelectedItem.ToString());
                    newElement.Add(idAttribute, directserver, directport, width, heigth, version, directory, online, email, pass);
                    XElement modded = new XElement("modded", "");
                }


                tree.Add(newElement);
                xDoc.Save(usr + profilename.Text + ".xml");

                dowfab();
                dowforg();
            }
            catch (Exception ex)
            {

                mesajbox(ex.ToString(), "FFNet 1.0", "OK");
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Environment.Exit(-1);
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
            //if (vercombo.SelectedItem.ToString() == "1.19.2 Forge")
            //{
            //    try
            //    {
                    
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.19.1 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.19 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.18.1 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.18 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.17.1 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.16.5 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.16.4 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.16.3 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.16.2 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.16.1 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.15.2 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.15.1 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.15 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.14.3 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.14.2 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.13.2 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.12.2 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.12.1 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.12 Forge")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/forge.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}


        }

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

        string dowver;
        public void dowfab()
        {
          
            cleandir();
            dowver = "fabric";
            if(customdir.Text != FFWP.Language.Strings.privdir)
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
            //if (vercombo.SelectedItem.ToString() == "1.19.2 Fabric")
            //{
            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + @"/versions/fabric.zip");




            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.19.1 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.19 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();

            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.18.2 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.18.1 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();

            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.18 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();

            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.17.1 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.17 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();

            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.16.5 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.16.4 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.16.3 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.16.2 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();

            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.16.1 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.16 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.15.2 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.15.1 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.15 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.14.4 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.14.3 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.14.2 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");



            //        ext();
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.14.1 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();

            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //if (vercombo.SelectedItem.ToString() == "1.14 Fabric")
            //{

            //    try
            //    {
            //        client.DownloadFile(new Uri(DownloadString), customdir.Text + "/versions/fabric.zip");


            //        ext();


            //    }
            //    catch (Exception ex)
            //    {


            //    }
            //}

        }

        public void checkcustom()
        {

            if (FabricTOG.IsOn == true)
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            checkstatus();
          
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

        public void Timers(string Com)
        {
            System.Windows.Threading.DispatcherTimer Toggle1 = new System.Windows.Threading.DispatcherTimer();
            Toggle1.Tick += new EventHandler(Toggle1_Tick);
            Toggle1.Interval = new TimeSpan(0, 0, 2);

            System.Windows.Threading.DispatcherTimer Toggle2 = new System.Windows.Threading.DispatcherTimer();
            Toggle2.Tick += new EventHandler(Toggle2_Tick);
            Toggle2.Interval = new TimeSpan(0, 0, 2);

            System.Windows.Threading.DispatcherTimer Toggle3 = new System.Windows.Threading.DispatcherTimer();
            Toggle3.Tick += new EventHandler(Toggle3_Tick);
            Toggle3.Interval = new TimeSpan(0, 0, 2);



            if(Com == "T1")
            {
                Toggle1.Start();
            }
            if(Com == "T2")
            {
                Toggle2.Start();
            }
            if(Com == "T3")
            {
                Toggle3.Start();
            }
            if(Com == "TALL")
            {
                Toggle1.Start();
                Toggle2.Start();
                Toggle3.Start();
            }
            if(Com == "T1S")
            {
                Toggle1.Stop();
            }
            if(Com == "T2S")
            {
                Toggle2.Stop();
            }
            if(Com == "T3S")
            {
                Toggle3.Stop();
            }
            if(Com == "TALLS")
            {
                Toggle1.Stop();
                Toggle2.Stop();
                Toggle3.Stop();
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

        public bool PXDone = false;
        public string RealName;
        public string Developer;
        public async void vercombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (vercombo.SelectedItem == null)
            {
                datedata.Text = "?!";
                pubdata.Text = "?!";
                devdata.Text = "?!";
            }

            try
            {
                WebClient response = new WebClient();
                using (WebClient client = new WebClient())
                {
                    jsonVerisi = response.DownloadString(url);

                }
                DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jsonVerisi);
                DataTable dataTable = dataSet.Tables[SelectedMinecraft];
                if (vercombo.Items.Count == 0)
                {

                }
                else
                {
                    webdata.Text = dataTable.Rows[vercombo.SelectedIndex]["Website"].ToString();
                    datedata.Text = dataTable.Rows[vercombo.SelectedIndex]["Date"].ToString();
                    pubdata.Text = dataTable.Rows[vercombo.SelectedIndex]["Publisher"].ToString();
                    devdata.Text = dataTable.Rows[vercombo.SelectedIndex]["Developer"].ToString();
                    Developer = dataTable.Rows[vercombo.SelectedIndex]["Developer"].ToString();
                    DownloadString = dataTable.Rows[vercombo.SelectedIndex]["Download"].ToString();
                    if(Developer == "Fabric Team")
                    {
                        RealName = dataTable.Rows[vercombo.SelectedIndex]["RLFab"].ToString();
                    }
                    else if(Developer == "Forge Team")
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

      
        private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void username_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void username_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            
        }

        private void ramslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ramslider.Value = Math.Round(ramslider.Value, 0);
        }

        bool PDone = false;
        bool Fetch = true;
        public async void TakeAll(string MCVer)
        {
            try
            {
                vercombo.Items.Clear();
                if(Fetch == true)
                {
                   

                    // Veriyi indir ve oku
                    //string dataUrl = "https://ffgapple.github.io/JSON/GappleList.json";
                    //string jsonData = await DownloadDataAsync(dataUrl);

                    // Veri okunduğunda ProcessRing'i gizle
                 
                    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                    using (WebClient response = new WebClient())
                    {
                        jsonVerisi = response.DownloadString(url);
                    }
                    //PRING.Visibility = Visibility.Hidden;
                    Fetch = false;
                }
                else
                {

                }
                JObject o = JObject.Parse(jsonVerisi);
                JArray a = (JArray)o[MCVer];

                if (MCVer == "Vanilla")
                {
                    SelectedMinecraft = "Vanilla";
                    IList<Vanilla> MCList = a.ToObject<IList<Vanilla>>();
                    foreach (Vanilla VAN in MCList)
                    {
                        if (MCVer == "Vanilla")
                        {
                            vercombo.Items.Add(VAN.MCVan);

                        }
                    }
                }
                if (MCVer == "Fabric")
                {
                    SelectedMinecraft = "Fabric";
                    IList<Fabric> MCList = a.ToObject<IList<Fabric>>();
                    foreach (Fabric FAB in MCList)
                    {

                        if (MCVer == "Fabric")
                        {

                            vercombo.Items.Add(FAB.MCFab);
                        }

                    }
                }
                if (MCVer == "Forge")
                {
                    SelectedMinecraft = "Forge";
                    IList<Forge> MCList = a.ToObject<IList<Forge>>();
                    foreach (Forge FOR in MCList)
                    {

                        if (MCVer == "Forge")
                        {

                            vercombo.Items.Add(FOR.MCForge);
                        }

                    }
                }



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

       

        private void Toggle3_Tick(object sender, EventArgs e)
        {
            
        }

        private void Toggle2_Tick(object sender, EventArgs e)
        {
            
        }

        private void Toggle1_Tick(object sender, EventArgs e)
        {
          
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

           
            this.Hide();
        }
        

        private void Fabric_Toggled()
        {
          
           
        }

        private void FabricTOG_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void Fabric_Toggled(object sender, RoutedEventArgs e)
        {
           if (FabricTOG.IsOn == true)
            {
                TakeAll("Fabric");
                ForgeTOG.IsEnabled = false;
            }
            else
            {
                TakeAll("Vanilla");
                ForgeTOG.IsEnabled = true;
            }
        }

        private void Forge_Toggled(object sender, RoutedEventArgs e)
        {
            if (ForgeTOG.IsOn == true)
            {
                TakeAll("Forge");
                FabricTOG.IsEnabled = false;
            }
            else
            {
                TakeAll("Vanilla");
                FabricTOG.IsEnabled = true;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                customdir.Text = dialog.SelectedPath;
            }
        }
    }
}
