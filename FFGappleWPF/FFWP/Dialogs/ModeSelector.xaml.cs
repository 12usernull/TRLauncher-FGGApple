using ModernWpf.Controls;
using System;
using System.Collections.Generic;
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

namespace FFWP.Dialogs
{
    /// <summary>
    /// ModeSelector.xaml etkileşim mantığı
    /// </summary>
    public partial class ModeSelector : ContentDialog
    {
        public ModeSelector()
        {
            string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
            FFWP.Modules.SaveINI first = new FFWP.Modules.SaveINI(localfile);
            string langu = first.Read("LocalSettingsForGapple", "Lang");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langu);
            InitializeComponent();
            settext();
        }

        public void settext()
        {
            this.Title = FFWP.Language.Strings.addprofile;
            offlinetxt.Text = FFWP.Language.Strings.cevrimdi;
            onlinetxt.Text = FFWP.Language.Strings.userplace;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void On_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            nativeGapple.Modules.ProfileMode = "Online";
            Dialogs.AddProfileOffline ADO = new Dialogs.AddProfileOffline();
            await ADO.ShowAsync();
          
        }

        private async void Of_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            nativeGapple.Modules.ProfileMode = "Offline";
            Dialogs.AddProfileOffline ADO = new Dialogs.AddProfileOffline();
            await ADO.ShowAsync();

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Hide();
            nativeGapple.Modules.ProfileMode = "XBOX";
            Dialogs.AddProfileOffline ADO = new Dialogs.AddProfileOffline();
            await ADO.ShowAsync();
        }
    }
}
