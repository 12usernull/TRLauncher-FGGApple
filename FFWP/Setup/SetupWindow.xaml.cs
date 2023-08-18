using ModernWpf.Media.Animation;
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

namespace FFWP.Extra_Windows
{
    /// <summary>
    /// Setup.xaml etkileşim mantığı
    /// </summary>
    /// 
    public partial class Setup : Window
    {
        private NavigationTransitionInfo _transitionInfo = null;
        public static System.Windows.Controls.Frame AppFrame { get; private set; }
        public Setup()
        {
            InitializeComponent();
            _transitionInfo = new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight };
            AppFrame = MainFrame;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(FFWP.Setup.SetupPage), null, _transitionInfo);
        }
    }
}
