using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FFWP.Dialogs
{
    /// <summary>
    /// ParentalSettings.xaml etkileşim mantığı
    /// </summary>
    public partial class ParentalSettings : ModernWpf.Controls.ContentDialog
    {
        RegistryKey Controller = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FFGappleLNC");
        RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FFGappleLNC");
        public ParentalSettings()
        {
            InitializeComponent();
            settext();
        }

        public void settext()
        {
            this.Title = FFWP.Language.Strings.parentset;
            settings.Content = FFWP.Language.Strings.paset;
            profiles.Content = FFWP.Language.Strings.papr;
            online.Content = FFWP.Language.Strings.pamm;
            string X3 = key.GetValue("X3").ToString();
            string X2 = key.GetValue("X2").ToString();
            string X1 = key.GetValue("X1").ToString();
            if (X3 == "Enabled")
            {
                settings.IsChecked = true;  
            }
            else
            {
                settings.IsChecked = false;
            }
            if (X2 == "Enabled")
            {
                online.IsChecked = true;
            }
            else
            {
                online.IsChecked = false;
            }
            if (X1 == "Enabled")
            {
                profiles.IsChecked = true;
            }
            else
            {
                profiles.IsChecked = false;
            }
        }

        private void ContentDialog_Closing(ModernWpf.Controls.ContentDialog sender, ModernWpf.Controls.ContentDialogClosingEventArgs args)
        {

        }

        private void settings_Checked(object sender, RoutedEventArgs e)
        {
           
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            if (settings.IsChecked == true)
            {
                key.SetValue("X3", "Enabled");
                nativeGapple.Modules.Settings = false;
            }
            else
            {
                key.SetValue("X3", "Disabled");
                nativeGapple.Modules.Settings = true;
            }
        }

        private void profiles_Click(object sender, RoutedEventArgs e)
        {
            if (profiles.IsChecked == true)
            {
                key.SetValue("X1", "Enabled");
                nativeGapple.Modules.Profiles = true;
            }
            else
            {
                key.SetValue("X1", "Disabled");
                nativeGapple.Modules.Profiles = true;
            }
        }

        private void online_Click(object sender, RoutedEventArgs e)
        {
            if (online.IsChecked == true)
            {
                key.SetValue("X2", "Enabled");
                nativeGapple.Modules.Online = true;
            }
            else
            {
                key.SetValue("X2", "Disabled");
                nativeGapple.Modules.Online = true;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
