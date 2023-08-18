using Microsoft.Win32;
using Mono.Cecil.Cil;
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
    /// ParentalControls.xaml etkileşim mantığı
    /// </summary>
    public partial class ParentalControls : ModernWpf.Controls.ContentDialog
    {

        public ParentalControls()
        {
            InitializeComponent();
            settext();
        }

        private void settext()
        {
            explain.Text = FFWP.Language.Strings.pc;
            fw.Content = FFWP.Language.Strings.next;
            RegistryKey Controller = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FFGappleLNC");
            if (Controller != null)
            {
              
            }
            else
            {
               
            }
        }

        private async void fw_Click(object sender, RoutedEventArgs e)
        {
            
            this.Hide();
            Dialogs.FFPass FP = new Dialogs.FFPass("ParentalControlCreate");
            await FP.ShowAsync();
        }
    }
}
