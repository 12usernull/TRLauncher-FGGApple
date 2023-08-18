using FFWP.Modules;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using Windows.Globalization;
using Windows.UI.Xaml.Controls;

namespace FFWP.Dialogs
{
    /// <summary>
    /// Interaktionslogik für SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : ModernWpf.Controls.ContentDialog
    {

        string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
        string BefSel;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        string comboselected;
        public SettingsDialog()
        {
            string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
            FFWP.Modules.SaveINI first = new FFWP.Modules.SaveINI(localfile);
            string langu = first.Read("LocalSettingsForGapple", "Lang");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langu);
            InitializeComponent();
            settext();
            CheckSettings();
            System.Windows.Controls.ComboBoxItem typeItem = (System.Windows.Controls.ComboBoxItem)langcombo.SelectedItem;
            BefSel = typeItem.Content.ToString();
        }

        

        public void settext()
        {
           if(nativeGapple.Modules.IsEnabled == true)
            {
                if (nativeGapple.Modules.Settings == true)
                {
                    parental.Visibility = Visibility.Visible;
                    langcombo.IsEnabled = false;
                    exitafterT.IsEnabled = false;
                    updatesT.IsEnabled = false;
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
            SetDia.Title = FFWP.Language.Strings.set4;
            exitafter.Text = FFWP.Language.Strings.setname;
            updates.Text = FFWP.Language.Strings.set1;
            language.Text = FFWP.Language.Strings.set3;
            uid.Text = "FFNet: " + nativeGapple.Modules.NativeRuntime;
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

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ComboBoxItem typeItem = (System.Windows.Controls.ComboBoxItem)langcombo.SelectedItem;
            string xSe = typeItem.Content.ToString();
            SaveSettings();
            if(BefSel == xSe)
            {
                this.Hide();
            }
            else
            {
                this.Hide();
                //mesajbox(FFWP.Language.Strings.temelayar, "FGapple", "OK");
                var conDL = new ModernWpf.Controls.ContentDialog()
                {
                    Title = "FFGapple",
                    Content = FFWP.Language.Strings.temelayar,
                    PrimaryButtonText = "OK",

                };
                var ConRes = await conDL.ShowAsync();
              

                if (ConRes == ModernWpf.Controls.ContentDialogResult.Primary)
                {
                    System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    Environment.Exit(-1);

                }
                else
                {

                }
              
                //var T = ShowAsync();
            }
          
        }

        public void CheckSettings()
        {
            try
            {
                SaveINI set = new SaveINI(localfile);
                string afterclose = set.Read("LocalSettingsForGapple", "ExitAfterLaunch");
                string checkupdates = set.Read("LocalSettingsForGapple", "CheckUpdates");

                string languag = set.Read("LocalSettingsForGapple", "Lang");

                if (afterclose == "YES")
                {
                    exitafterT.IsOn = true;
                }
                if (afterclose == "NO")
                {
                    exitafterT.IsOn = false;
                }
                if (checkupdates == "YES")
                {
                    updatesT.IsOn = true;
                }
                if (checkupdates == "NO")
                {
                    updatesT.IsOn = false;
                }
                if(languag == "de-DE")
                {
                    langcombo.SelectedIndex = 0;
                }
                if (languag == "en-US")
                {
                    langcombo.SelectedIndex = 3;
                }
                if (languag == "es-ES")
                {
                    langcombo.SelectedIndex = 1;
                }
                if (languag == "fr-FR")
                {
                    langcombo.SelectedIndex = 2;
                }
                if (languag == "tr-TR")
                {
                    langcombo.SelectedIndex = 4;
                }
                if (languag == "zh")
                {
                    langcombo.SelectedIndex = 5;
                }
                System.Windows.Controls.ComboBoxItem typeItem = (System.Windows.Controls.ComboBoxItem)langcombo.SelectedItem;
                if (typeItem.Content.ToString() == "Deutsch")
                {
                    comboselected = "de-DE";
                }
                else if (typeItem.Content.ToString() == "Español")
                {
                    comboselected = "es-ES";
                }
                else if (typeItem.Content.ToString() == "Français")
                {
                    comboselected = "fr-FR";
                }
                else if (typeItem.Content.ToString() == "English")
                {
                    comboselected = "en-US";
                }
                else if (typeItem.Content.ToString() == "Türkçe")
                {
                    comboselected = "tr-TR";
                }
                else if (typeItem.Content.ToString() == "中文")
                {
                    comboselected = "zh";
                }


              

             
            }
            catch
            {
                Environment.Exit(-1);

            }
        }

        public void SaveSettings()
        {
            try
            {
                SaveINI set = new SaveINI(localfile);
                string afterclose = set.Read("LocalSettingsForGapple", "ExitAfterLaunch");
                string checkupdates = set.Read("LocalSettingsForGapple", "CheckUpdates");
                string showother = set.Read("LocalSettingsForGapple", "ShowOtherVersions");
                string languag = set.Read("LocalSettingsForGapple", "Lang");
                if (exitafterT.IsOn == true)
                {
                    set.Write("LocalSettingsForGapple", "ExitAfterLaunch", "YES");
                }
                if (updatesT.IsOn == true)
                {
                    set.Write("LocalSettingsForGapple", "CheckUpdates", "YES");

                }

                if (exitafterT.IsOn == false)
                {
                    set.Write("LocalSettingsForGapple", "ExitAfterLaunch", "NO");
                }
                if (updatesT.IsOn == false)
                {
                    set.Write("LocalSettingsForGapple", "CheckUpdates", "NO");

                }
                System.Windows.Controls.ComboBoxItem typeItem = (System.Windows.Controls.ComboBoxItem)langcombo.SelectedItem;
                if(typeItem.Content.ToString() == "Deutsch")
                {
                    set.Write("LocalSettingsForGapple", "Lang", "de-DE");
                }
                else if (typeItem.Content.ToString() == "Español")
                {
                    set.Write("LocalSettingsForGapple", "Lang", "es-ES");
                }
                else if (typeItem.Content.ToString() == "Français")
                {
                    set.Write("LocalSettingsForGapple", "Lang", "fr-FR");
                }
                else if (typeItem.Content.ToString() == "English")
                {
                    set.Write("LocalSettingsForGapple", "Lang", "en-US");
                }
                else if (typeItem.Content.ToString() == "Türkçe")
                {
                    set.Write("LocalSettingsForGapple", "Lang", "tr-TR");
                }
                else if (typeItem.Content.ToString() == "中文")
                {
                    set.Write("LocalSettingsForGapple", "Lang", "zh");
                }
            }
            catch
            {


            }
        }

        private void langcombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBoxItem typeItem = (System.Windows.Controls.ComboBoxItem)langcombo.SelectedItem;
            if (typeItem.Content.ToString() == "Deutsch")
            {
                flag.Source = new BitmapImage(
                new Uri("pack://application:,,,/Flags/germany.png"));
            }
            else if (typeItem.Content.ToString() == "English")
            {
                flag.Source = new BitmapImage(
                new Uri("pack://application:,,,/Flags/usa.png"));
            }
            else if(typeItem.Content.ToString() == "Español")
            {
                flag.Source = new BitmapImage(
                new Uri("pack://application:,,,/Flags/spain.png"));
            }
            else if(typeItem.Content.ToString() == "Français")
            {
                flag.Source = new BitmapImage(
                new Uri("pack://application:,,,/Flags/france.png"));
            }
            else if(typeItem.Content.ToString() == "Türkçe")
            {
                flag.Source = new BitmapImage(
                new Uri("pack://application:,,,/Flags/turkey.png"));
            }
            else if (typeItem.Content.ToString() == "中文")
            {
                flag.Source = new BitmapImage(
                new Uri("pack://application:,,,/Flags/china.png"));
            }
            else
            {
              
            }
        }
    }
}
