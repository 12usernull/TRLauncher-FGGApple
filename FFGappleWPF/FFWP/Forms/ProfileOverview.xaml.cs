using CmlLib.Core.Version;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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
using Windows.System;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using static FFWP.Forms.ProfileOverview;
using System.Xaml.Schema;
using System.Collections;
using CmlLib.Core;
using System.Diagnostics;

namespace FFWP.Forms
{
    /// <summary>
    /// ProfileOverview.xaml etkileşim mantığı
    /// </summary>
    public partial class ProfileOverview : Page
    {


        String creationdate, id, directport, width, heigth, directserver, version, directory, online, email, pass, afterclose, updateoption, secret;

        private async void modButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Profile X = (Profile)mylst.SelectedItems[0];
                string dr = X.directory;
                Profile XR = (Profile)mylst.SelectedItems[0];
                string drX = XR.customversion;
                if (!(drX.Contains("Vanilla")))
                {
                    if (dr != @"")
                    {
                        Dialogs.AddMod addmod = new Dialogs.AddMod(dr);
                        await addmod.ShowAsync();
                    }
                    else
                    {
                        mesajbox(FFWP.Language.Strings.direr, "FFGapple", "OK");
                    }
                }
                else
                {
                    mesajbox(FFWP.Language.Strings.bugligapple2332ezwinbusurumevanilyacort, "FFGapple", "OK");
                }
            }
            catch 
            {

             
            }

          
          
        }

        private async void editButton_Click(object sender, RoutedEventArgs e)
        {
           if(mylst.SelectedItem != null)
            {
                nativeGapple.Modules.ProfileMode = "";
                Profile X = (Profile)mylst.SelectedItems[0];
                string dr = X.secret;
                string OP = X.online;
                if(OP == "no")
                {
                    nativeGapple.Modules.ProfileMode = "Offline";
                    Dialogs.ProfileMenu EP = new Dialogs.ProfileMenu(dr);
                    await EP.ShowAsync();
                }
                else if(OP == "yes")
                {
                    nativeGapple.Modules.ProfileMode = "Online";
                    Dialogs.ProfileMenu EP = new Dialogs.ProfileMenu(dr);
                    await EP.ShowAsync();
                }
                else
                {

                }
               
            }
            else
            {
                mesajbox(FFWP.Language.Strings.seled, "FFGapple", "OK");
            }
            
        }

        private void folderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Profile X = (Profile)mylst.SelectedItems[0];
                string dr = X.directory;
                if (dr != @"")
                {
                    Process.Start(dr);
                }
                else
                {
                    string mcdef = MinecraftPath.GetOSDefaultPath();
                    Process.Start(mcdef);
                }
            }
            catch
            {


            }
        }

        public async void mesajbox(string icerik, string baslik, string buton)
        {
            string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
            FFWP.Modules.SaveINI first = new FFWP.Modules.SaveINI(localfile);
            string langu = first.Read("LocalSettingsForGapple", "Lang");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langu);
            await new ModernWpf.Controls.ContentDialog()
            {
                Title = baslik,
                Content = icerik,
                PrimaryButtonText = buton,

            }.ShowAsync();


        }
        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
          if(mylst.SelectedItem != null)
            {
                if (mylst.Items.Count == 1)
                {
                    mesajbox(FFWP.Language.Strings.delerr, "FFGapple", "OK");
                }
                else
                {
                    Profile idx = (Profile)mylst.SelectedItems[0];
                    string age = idx.idjson;
                    string agex = idx.idjson;
                    string json = File.ReadAllText(FFWP.JsonData.Value.userjson);


                    var data = JsonConvert.DeserializeObject<Root>(json);


                    string profileIdToDelete = age;


                    Profile profileToDelete = data.Profiles.Find(p => p.idjson == profileIdToDelete);

                    var conDL = new ModernWpf.Controls.ContentDialog()
                    {
                        Title = "FFGapple",
                        Content = String.Format(FFWP.Language.Strings.suredel, agex),
                        PrimaryButtonText = FFWP.Language.Strings.evt,
                        SecondaryButtonText = FFWP.Language.Strings.hyr,

                    };
                    var ConRes = await conDL.ShowAsync();


                    if (ConRes == ModernWpf.Controls.ContentDialogResult.Primary)
                    {
                        if (profileToDelete != null)
                        {
                            data.Profiles.Remove(profileToDelete);
                            string updatedJson = JsonConvert.SerializeObject(data, Formatting.Indented);

                            File.WriteAllText(FFWP.JsonData.Value.userjson, updatedJson);
                            //mylst.Items.Refresh();
                            LoadData();
                        }
                        else
                        {

                        }

                    }
                    else
                    {

                    }


                }
            }
            else
            {

            }



        }

     
      
        private void addButton_Checked(object sender, RoutedEventArgs e)
        {
          
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            FFWP.Dialogs.ModeSelector MS = new Dialogs.ModeSelector();
            MS.ShowAsync();
        }

        private void mylst_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        public void settext()
        {
            if (nativeGapple.Modules.IsEnabled == true)
            {
                if (nativeGapple.Modules.Profiles == true)
                {
                    parental.Visibility = Visibility.Visible;
                    modButton.IsEnabled = false;
                    addButton.IsEnabled = false;
                    deleteButton.IsEnabled = false;
                    editButton.IsEnabled = false;
                    folderButton.IsEnabled = false;
                    mylst.IsEnabled = false;
                }
                else
                {
                    parental.Visibility = Visibility.Hidden;
                    modButton.IsEnabled = true;
                    addButton.IsEnabled = true;
                    deleteButton.IsEnabled = true;
                    editButton.IsEnabled = true;
                    folderButton.IsEnabled = true;
                    mylst.IsEnabled = true;
                }
            }
            else
            {
                parental.Visibility = Visibility.Hidden;
                modButton.IsEnabled = true;
                addButton.IsEnabled = true;
                deleteButton.IsEnabled = true;
                editButton.IsEnabled = true;
                folderButton.IsEnabled = true;
                mylst.IsEnabled = true;
            }

          
            modButton.Label = FFWP.Language.Strings.addmod;
            addButton.Label = FFWP.Language.Strings.addprofile;
            deleteButton.Label = FFWP.Language.Strings.minustool;
            editButton.Label = FFWP.Language.Strings.edittool;
            folderButton.Label = FFWP.Language.Strings.prodir;
        }

        List<Profile> names;


        private void LoadData()
        {
            // JSON dosyasının yolunu belirleyin
            string jsonFilePath = FFWP.JsonData.Value.userjson;

            // JSON dosyasını okuyun
            string json = File.ReadAllText(jsonFilePath);

            // JSON verilerini ListView'e aktarın
            var profiles = JsonConvert.DeserializeObject<Root>(json).Profiles;
            mylst.ItemsSource = profiles;
            
        }

        public ProfileOverview()
        {
           
            InitializeComponent();
            string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
            FFWP.Modules.SaveINI first = new FFWP.Modules.SaveINI(localfile);
            string langu = first.Read("LocalSettingsForGapple", "Lang");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langu);
            settext();
            LoadData();
            //var profiles = JsonConvert.DeserializeObject<List<Profile>>(json);
            //foreach (var profile in profiles)
            //{
            //    // ListView'e profile verilerini ekleyin
            //    ListViewItem item = new ListViewItem(profile.username);
            //    item.SubItems.Add(profile.idjson);
            //    item.SubItems.Add(profile.secret);
            //    // Diğer özellikleri de burada ListView'e ekleyebilirsiniz
            //    listView1.Items.Add(item);
            //}
            //// JSON dosyasını oku
            //string json = File.ReadAllText(FFWP.JsonData.Value.userjson);

            //// JSON verilerini deserialize et
            //RootObject root = JsonConvert.DeserializeObject<RootObject>(json);

            //// Profiles verilerini ListView'e ekle
            ////foreach (Profile profile in root.Profiles)
            ////{
            ////    version = profile.xversion;
            ////    creationdate = profile.creationdate;
            ////    id = profile.idjson;
            ////   //string DesPro = JsonConvert.DeserializeObject(json);
            ////    var  DesPro = JsonConvert.DeserializeObject<List<RootObject>>(json);
            ////    //List<Profile> DesPro = JsonConvert.DeserializeObject<List<Profile>>(json);
            ////    mylst.ItemsSource = DesPro;
            ////    MessageBox.Show(profile.idjson + profile.creationdate + profile.xversion);
            ////    //ListViewItem item = new ListViewItem(profile.idjson);
            ////    //item.SubItems.Add(profile.creationdate);
            ////    //item.SubItems.Add(profile.xversion);
            ////    // names = new List<Profile>();
            ////    //listView1.Items.Add(item);
            ////}

            // names.Add(new Profile() { idjson = id, xversion = version, creationdate = creationdate });
            //mylst.ItemsSource = names;
            //TakeJSON();
            //SetValues();
            //Jobs = new ObservableCollection<ProfileData>();
            //string url = FFWP.JsonData.Value.userjson;
            //string jsonx = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"//FFGapple Profiles//FFGapple Launcher//user//FFGapple.1337");
            //List<ProfileData> veriListesi = JsonConvert.DeserializeObject<List<ProfileData>>(jsonx);


            //mylst.ItemsSource = veriListesi;

            // Data = new List<ProfileData>();
            //Data.Add(new ProfileData() { creationdate = creationdate, idjson = id, xversion = version });


            //mylst.ItemsSource = Data;
        }

        public void TakeJSON()
        {

            try
            {
                //string url = FFWP.JsonData.Value.userjson;
                //var jsonVerisi = File.ReadAllText(url);

                //using (StreamReader r = new StreamReader(url))
                //{
                //    string json = r.ReadToEnd();
                //    r.Close();
                //    //JObject result = JObject.Parse(json);

                //    FFWP.JsonData.Value.userjson = json;
                //}


                //DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(FFWP.JsonData.Value.userjson);
                //var data = (JObject)JsonConvert.DeserializeObject(FFWP.JsonData.Value.userjson);
                //DataTable dataTable = dataSet.Tables["Profiles"];
                //List<ProfileData> Data = JsonConvert.DeserializeObject<List<ProfileData>>(FFWP.JsonData.Value.userjson);
                // JObject o = JObject.Parse(jsonVerisi);
                //  JArray a = (JArray)o["Profiles"];
                // JObject ax = (JObject)o["Profiles"];
              


                //dataSet.Clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show("FFNet Can't Parse The Version Data!" + ex.ToString());
                Environment.Exit(-1);
            }
        }

        public void SetValues()
        {
           
                try
                {
                    DataSet x = JsonConvert.DeserializeObject<DataSet>(FFWP.JsonData.Value.userjson);
                    var data = (JObject)JsonConvert.DeserializeObject(FFWP.JsonData.Value.userjson);
                    DataTable xz = x.Tables["Profiles"];

                    secret = xz.Columns["secret"].ToString();
                    id = xz.Columns["idjson"].ToString();
                    directserver = xz.Columns["directserver"].ToString();
                    directport = xz.Columns["directport"].ToString();
                    width = xz.Columns["width"].ToString();
                    heigth = xz.Columns["heigth"].ToString();
                    version = xz.Columns["xversion"].ToString();
                    directory = xz.Columns["directory"].ToString();
                    online = xz.Columns["online"].ToString();
                    email = xz.Columns["email"].ToString();
                    pass = xz.Columns["pass"].ToString();
                    creationdate = xz.Columns["creationdate"].ToString();
            }
                catch (Exception ex)
                {


                }
            
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
            public string directory { get; set; }
            public string online { get; set; }
            public string email { get; set; }
            public string pass { get; set; }
            public string creationdate { get; set; }
            public string modded { get; set; }
            public string customversion { get; set; }
            public string img { get; set; }
        }

        public class Root
        {
            public List<Profile> Profiles { get; set; }
        }
    }
}
