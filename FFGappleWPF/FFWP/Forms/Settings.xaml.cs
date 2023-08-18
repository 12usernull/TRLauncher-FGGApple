using FFWP.Modules;
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

namespace FFWP.Forms
{
    /// <summary>
    /// Settings.xaml etkileşim mantığı
    /// </summary>
    public partial class Settings : Page
    {
        string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
        string Xversion = "WPF 1.1 Demo";

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        string comboselected;
        public Settings()
        {
            InitializeComponent();
            settext();
        }

        public void settext()
        {
            maintext.Text = FFWP.Language.Strings.set4;
            exitafter.Text = FFWP.Language.Strings.setname;
            updates.Text = FFWP.Language.Strings.set1;
            language.Text = FFWP.Language.Strings.set3;
        }

       
    }
}
