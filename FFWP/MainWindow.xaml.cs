using FFWP.Modules;
using FFWP.Modules;
using ModernWpf.Controls;
using ModernWpf.Media.Animation;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
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
using Windows.UI.ViewManagement;
using System.Reflection;
using System.Net;
using System.Security.Policy;
using Microsoft.Win32;
using Mono.Cecil.Cil;
using System.Security.Cryptography.Xml;
using Windows.Media.Protection.PlayReady;
using System.Net.Http;
using static System.Net.WebRequestMethods;

namespace FFWP
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NavigationTransitionInfo _transitionInfo;
        string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
        string ExecVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static System.Windows.Controls.Frame AppFrame { get; private set; }
        public MainWindow()
        {
            string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
            FFWP.Modules.SaveINI first = new FFWP.Modules.SaveINI(localfile);
            string langu = first.Read("LocalSettingsForGapple", "Lang");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langu);
            Width = 901;
            Height = 378;
            if (CheckInt.IsInternetAvailable() == true)
            {
              
                InitializeComponent();

                AppFrame = MainFrame;
                _transitionInfo = new DrillInNavigationTransitionInfo();
                Width = 901;
                Height = 393;
                //CheckVersion();
                Version();



                //if (nativeGapple.Modules.SquirrelDone == "Close")
                //{
                //    Version();
                //}
                //else
                //{
                //    FFWP.Update.UpdateP sw = new Update.UpdateP();
                //    sw.Show();
                //    this.Hide();
                //}
            }
            else
            {
                noint();
            }
        }

   


        public async void Version()
        {
            if(ExecVersion != nativeGapple.Modules.NativeRuntime)
            {
                await new ContentDialog()
                {
                    Title = "nativeGapple",
                    Content = "Runtime is missmatching!",
                    PrimaryButtonText = "Okay!",

                }.ShowAsync();
                Environment.Exit(0);
            }
            else
            {
                CheckUser();
                
              
            }
        }

        public void TakeJSON()
        {
            try
            {
                string url = FFWP.JsonData.Value.usr + @"\FFGapple.1337";
                var jsonVerisi = System.IO.File.ReadAllText(url);
                string checkvoid = System.IO.File.ReadAllText(url);
                if (!System.IO.File.Exists(url))
                {
                    System.IO.File.Create(url);
                    FFWP.Dialogs.ModeSelector MS = new Dialogs.ModeSelector();
                    MS.ShowAsync();
                }
                else if (!(checkvoid.Contains("secret")))
                {
                    FFWP.Dialogs.ModeSelector MS = new Dialogs.ModeSelector();
                    MS.ShowAsync();
                }
                else
                {

                  
                }



            }
            catch (Exception ex)
            {

                MessageBox.Show("FFNet Can't Parse The Version Data!" + ex.ToString());
                Environment.Exit(-1);
            }
        }

        public static bool Validator()
        {
            RegistryKey winLogonKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FFGappleLNC", true);
            return (winLogonKey.GetValueNames().Contains("ParentalControl"));
        }
    
        public async void CheckUser()
        {
            HttpClient client = new HttpClient();
            this.Title = "FFGapple Launcher";
            home.Content = FFWP.Language.Strings.enter;
            profile.Content = FFWP.Language.Strings.profov;
            EPC.Content = FFWP.Language.Strings.epc;
            DPC.Content = FFWP.Language.Strings.dpc;
            APC.Content = FFWP.Language.Strings.apc;
            parent.Content = FFWP.Language.Strings.parentset;
            RegistryKey Controller = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FFGappleLNC");
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FFGappleLNC");
            bool VRes = Validator();
            if (VRes != false)
            {
               
                string PCV = key.GetValue("ParentalControl").ToString();
                if (PCV != "Activated")
                {
                    nativeGapple.Modules.IsEnabled = false;
                    DPC.IsEnabled = false;
                    EPC.IsEnabled = true;
                    APC.IsEnabled = false;
                    FFWP.JsonData.Value.ParentalControls = false;
                }
                else if (PCV == "Activated")
                {
                    nativeGapple.Modules.IsEnabled = true;
                    DPC.IsEnabled = true;
                    EPC.IsEnabled = false;
                    APC.IsEnabled = true;
                    FFWP.JsonData.Value.ParentalControls = true;
                    this.Title = "FFGapple Launcher - " + FFWP.Language.Strings.parenton;
                    
                    //string X3 = key.GetValue("X3").ToString();
                    //string X2 = key.GetValue("X2").ToString();
                    //string X1 = key.GetValue("X1").ToString();
                    //if(X3 == "Enabled")
                    //{
                    //    nativeGapple.Modules.Settings = true;
                    //}
                    //if(X2 == "Enabled")
                    //{
                    //    nativeGapple.Modules.Online = true;
                    //}
                    //if(X1 == "Enabled")
                    //{
                    //    nativeGapple.Modules.Profiles = true;
                    //}
                    
                   
                }
            }
            else
            {
              
            }

            if ((Properties.Settings.Default.useragree == "agreed") == false)
            {
                FFWP.Dialogs.UserAgree ug = new FFWP.Dialogs.UserAgree();

                var result = await ug.ShowAsync();
                if (result == ModernWpf.Controls.ContentDialogResult.Primary)
                {
                   
                        timers();

                        MainFrame.Navigate(typeof(FFWP.Forms.LauncherPage), null, _transitionInfo);
                  
                      
                    
                }
                else if (result == ModernWpf.Controls.ContentDialogResult.Secondary)
                {
                    Environment.Exit(-1);
                }
                else
                {
                    Environment.Exit(-1);
                }
            }
            else
            {
                home.Content = FFWP.Language.Strings.enter;
                profile.Content = FFWP.Language.Strings.profov;
                timers();
                MainFrame.Navigate(typeof(FFWP.Forms.LauncherPage), null, _transitionInfo);
                //string DA = key.GetValue("DataAgreed").ToString();
                //if (DA == "Agreed")
                //{
                //    timers();

                //    MainFrame.Navigate(typeof(FFWP.Forms.LauncherPage), null, _transitionInfo);
                //}
                //else if (DA == "Failed")
                //{
                //    string API = "https://ffgapple.pythonanywhere.com/api/v1/installation";
                //    HttpResponseMessage response = await client.GetAsync(API);
                //    if (response.IsSuccessStatusCode)
                //    {
                //        key.SetValue("InstallationAPI", "RequestSent");
                //        key.SetValue("DataAgreed", "Agreed");
                //        timers();

                //        MainFrame.Navigate(typeof(FFWP.Forms.LauncherPage), null, _transitionInfo);
                //    }
                //    else
                //    {
                //        key.SetValue("InstallationAPI", "RequestFailure");
                //        key.SetValue("DataAgreed", "Failed");
                //        timers();

                //        MainFrame.Navigate(typeof(FFWP.Forms.LauncherPage), null, _transitionInfo);
                //    }
                   
                //}
                //else
                //{
                //    FFWP.Dialogs.APIReq AR = new Dialogs.APIReq();
                //    await AR.ShowAsync();
                //}
             
            }
        }

        public async void noint()
        {
            await new ContentDialog()
            {
                Title = "Oops...",
                Content = FFWP.Language.Strings.inthata,
                PrimaryButtonText = "Okay!",
                
            }.ShowAsync();
            Environment.Exit(0);
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
           
        }

        public void timers()
        {
            System.Windows.Threading.DispatcherTimer CheckerT = new System.Windows.Threading.DispatcherTimer();
            CheckerT.Tick += new EventHandler(CheckerT_tick);
            CheckerT.Interval = new TimeSpan(0, 0, 0,1);
            CheckerT.Start();
        }

        private void CheckerT_tick(object sender, EventArgs e)
        {
           if(nativeGapple.Modules.NavStatus == "disabled")
            {
                DisableNav();
            }
            if (nativeGapple.Modules.NavStatus == "enabled")
            {
                EnableNav();
            }
        }

        public void DisableNav()
        {
            NavigationViewControl.IsEnabled = false;
        }
        public void EnableNav()
        {
            NavigationViewControl.IsEnabled = true;
        }

        private async void NavigationViewControl_ItemInvoked(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewItemInvokedEventArgs args)
        {
          
            if (args.IsSettingsInvoked)
            {
                Dialogs.SettingsDialog SD = new Dialogs.SettingsDialog();
                await SD.ShowAsync();
            }

            switch (sender.MenuItems.IndexOf(args.InvokedItemContainer))
            {

                case 0:
                    MainFrame.Navigate(typeof(FFWP.Forms.LauncherPage), null, _transitionInfo);
                    break;
                case 1:
                    MainFrame.Navigate(typeof(FFWP.Forms.ProfileOverview), null, _transitionInfo);
                    break;
                case 2:
                    MainFrame.Navigate(typeof(FFWP.Forms.LauncherPage), null, _transitionInfo);
                    break;
                case 3:
                  
                    break;


            }
        }

        private void MainFrame_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(-1);
        }

        private async void EPC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialogs.FFPass FP = new Dialogs.FFPass("ParentalControlEnable");
            await FP.ShowAsync();
        }

        private async void DPC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialogs.FFPass FP = new Dialogs.FFPass("ParentalControlDisable");
            await FP.ShowAsync();
        }

        private async void APC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dialogs.FFPass FP = new Dialogs.FFPass("ParentalControlSettings");
            await FP.ShowAsync();
        }
    }
}
