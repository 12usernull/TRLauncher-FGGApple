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

namespace FFWP.Setup
{
    /// <summary>
    /// SetupPage.xaml etkileşim mantığı
    /// </summary>
    public partial class SetupPage : Page
    {
        public SetupPage()
        {
            InitializeComponent();
            settext();
            ContentGrid.Visibility = Visibility.Visible;
        }

        private void langcombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
         
        public void settext()
        {
            setup.Text = FFWP.Language.Strings.letsdone;
            licnesetxt.Text = FFWP.Language.Strings.legal;
        }
    }
}
