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
using System.Windows.Shapes;

namespace FFWP.Update
{
    /// <summary>
    /// UpdateP.xaml etkileşim mantığı
    /// </summary>
    public partial class UpdateP : Window
    {
        public static System.Windows.Controls.Frame AppFrame { get; private set; }
        public UpdateP()
        {
            InitializeComponent();
            string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
            FFWP.Modules.SaveINI first = new FFWP.Modules.SaveINI(localfile);
            string langu = first.Read("LocalSettingsForGapple", "Lang");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langu);
            if (nativeGapple.Modules.SquirrelDone == "Yes")
            {
                MainWindow mv = new MainWindow();
                mv.Show();
                this.Hide();
            }
            else
            {
                AppFrame = MainFrame;
                MainFrame.Navigate(typeof(FFWP.Update.UpdateS), null);
                timers();
            }
           
           
        }

        public void timers()
        {
            System.Windows.Threading.DispatcherTimer CheckerT = new System.Windows.Threading.DispatcherTimer();
            CheckerT.Tick += new EventHandler(CheckerT_tick);
            CheckerT.Interval = new TimeSpan(0, 0, 0, 1);
            CheckerT.Start();
        }

        private void CheckerT_tick(object sender, EventArgs e)
        {
            if (nativeGapple.Modules.SquirrelDone == "Yes")
            {
                nativeGapple.Modules.SquirrelDone = "Close";
                MainWindow mv = new MainWindow();
                mv.Show();
                this.Hide();

            }
        }
    }
}
