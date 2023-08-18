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
using Windows.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;

namespace FFWP.Dialogs
{
    /// <summary>
    /// ServerInfo.xaml etkileşim mantığı
    /// </summary>
    public partial class ServerInfo : ModernWpf.Controls.ContentDialog
    {
        string GVid, GDec, GMic, GName;

        private void agree_Click(object sender, RoutedEventArgs e)
        {

        }

        public ServerInfo(string Video, string Description, string Name, string Microsoft)
        {
            InitializeComponent();
            GVid = Video;
            GDec = Description;
            GName = Name;
            GMic = Microsoft;
            Load();
        }

        private void ContentDialog_Closing(ModernWpf.Controls.ContentDialog sender, ModernWpf.Controls.ContentDialogClosingEventArgs args)
        {
            servervid.Stop();
            servervid.Dispose();
            
        }

        public async void Load()
        {
            this.Title = GName;
            await servervid.EnsureCoreWebView2Async();
            servervid.CoreWebView2.Navigate(GVid);
            serverdec.Text = GDec;
            
           
        }
    }
}
