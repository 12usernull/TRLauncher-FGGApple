using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using static System.IdentityModel.Tokens.SecurityTokenHandlerCollectionManager;

namespace FFWP.Dialogs
{
    /// <summary>
    /// APIReq.xaml etkileşim mantığı
    /// </summary>
    public partial class APIReq : ModernWpf.Controls.ContentDialog
    {
        RegistryKey Controller = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FFGappleLNC");
        RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FFGappleLNC");

        public APIReq()
        {
            InitializeComponent();
            settext();
           
        }

        public void settext()
        {
            UsrAg.Title = FFWP.Language.Strings.kulsoz;
            Agreement.Text = FFWP.Language.Strings.ipinf;
            agree.Content = FFWP.Language.Strings.kabulet;
            disagree.Content = FFWP.Language.Strings.reddet;
        }

        private void disagree_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(-1);
        }

        private async void agree_Click(object sender, RoutedEventArgs e)
        {
           //I know its terrible but i don't want to spend that much time to fix a code which works xd.
                HttpClient client = new HttpClient();
                try
                {
                    string Installation = key.GetValue("InstallationAPI").ToString();
                    if (Installation == "RequestFailure")
                    {
                    try
                    {
                        string API = "https://ffgapple.pythonanywhere.com/api/v1/installation";
                        HttpResponseMessage response = await client.GetAsync(API);
                        if (response.IsSuccessStatusCode)
                        {
                            key.SetValue("InstallationAPI", "RequestSent");
                            key.SetValue("DataAgreed", "Agreed");
                            System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                            Environment.Exit(-1);

                        }
                        else
                        {
                            key.SetValue("InstallationAPI", "RequestFailure");
                            key.SetValue("DataAgreed", "Failed");
                            System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                            Environment.Exit(-1);
                        }
                    }
                    
                    catch 
                    {

                       
                    }

                    }
                else if(Installation == "NotYet")
                {
                    string API = "https://ffgapple.pythonanywhere.com/api/v1/installation";
                    HttpResponseMessage response = await client.GetAsync(API);
                    if (response.IsSuccessStatusCode)
                    {
                        key.SetValue("InstallationAPI", "RequestSent");
                        key.SetValue("DataAgreed", "Agreed");
                        System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(-1);
                    }
                    else
                    {
                        key.SetValue("InstallationAPI", "RequestFailure");
                        key.SetValue("DataAgreed", "Failed");
                        System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(-1);
                    }
                }

            }
                catch
                {

                try
                {
                    string API = "https://ffgapple.pythonanywhere.com/api/v1/installation";
                    HttpResponseMessage response = await client.GetAsync(API);
                    if (response.IsSuccessStatusCode)
                    {
                        key.SetValue("InstallationAPI", "RequestSent");
                        key.SetValue("DataAgree", "Agreed");
                        System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(-1);
                    }
                    else
                    {
                        key.SetValue("InstallationAPI", "RequestFailure");
                        key.SetValue("DataAgree", "Failed");
                        System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(-1);
                    }

                }
                catch 
                {

                  
                }



                
                }

        }
    }
}
