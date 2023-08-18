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
    /// Interaktionslogik für UserAgree.xaml
    /// </summary>
    public partial class UserAgree : ContentDialog
    {
        public UserAgree()
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
            UsrAg.Title = FFWP.Language.Strings.kulsoz;
            Agreement.Text = FFWP.Language.Strings.fabdik1 + "\n\n" + FFWP.Language.Strings.fabdik2 + "\n\n" + FFWP.Language.Strings.fabdik3 + "\n\n" + FFWP.Language.Strings.fabdik4;
            agree.Content = FFWP.Language.Strings.kabulet;
            disagree.Content = FFWP.Language.Strings.reddet;
        }

        private void disagree_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(-1);
        }

        private void agree_Click(object sender, RoutedEventArgs e)
        {
            nativeGapple.Modules.UserAgree = "";
            Properties.Settings.Default.useragree = "agreed";
            Properties.Settings.Default.Save();
            System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Environment.Exit(-1);
        }
    }
}
