using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using Squirrel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using Windows.Media.Protection.PlayReady;

namespace FFWP.Dialogs
{
    /// <summary>
    /// UpdateNotify.xaml etkileşim mantığı
    /// </summary>
    public partial class UpdateNotify : ModernWpf.Controls.ContentDialog
    {
        RegistryKey Controller = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FFGappleLNC");
        RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FFGappleLNC");
        int GG;
        public UpdateNotify()
        {
            InitializeComponent();
            pr.IsActive = true;
            Done();
            //Sign();
            UpdateMNGR();


        }

        public async void Sign()
        {
            System.Windows.MessageBox.Show("HereM");
            HttpClient client = new HttpClient();
            try
            {
                string DA = key.GetValue("DataAgreed").ToString();
                if (DA == "Agreed")
                {
                    System.Windows.MessageBox.Show("Here");
                    UpdateMNGR();
                }
                else
                {
                    try
                    {
                        string Installation = key.GetValue("InstallationAPI").ToString();
                        if (Installation == "RequestFailure")
                        {
                            System.Windows.MessageBox.Show("Here1");
                            try
                            {
                                string API = "https://ffgapple.pythonanywhere.com/api/v1/installation";
                                HttpResponseMessage response = await client.GetAsync(API);
                                if (response.IsSuccessStatusCode)
                                {
                                    key.SetValue("InstallationAPI", "RequestSent");
                                    key.SetValue("DataAgreed", "Agreed");
                                }
                                else
                                {
                                    key.SetValue("InstallationAPI", "RequestFailure");
                                }
                            }
                            catch
                            {
                                System.Windows.MessageBox.Show("Here3");

                            }
                        }
                    }
                    catch
                    {
                        System.Windows.MessageBox.Show("Here4");
                        this.Hide();
                        FFWP.Dialogs.APIReq AR = new FFWP.Dialogs.APIReq();
                        await AR.ShowAsync();
                    }



                }

            }
            catch
            {

            }
        }
           
        

        public async void Done()
        {
            try
            {
                await Task.Run(() =>
                {
                    string webmain = new StreamReader(WebRequest.Create("https://raw.githubusercontent.com/ffgapple/ffgapple.github.io/master/data/webdata.html").GetResponse().GetResponseStream()).ReadToEnd();
                    int startIndex = webmain.IndexOf("<o0>") + 4;
                    int length = webmain.Substring(startIndex).IndexOf("</o0>");
                    GG = (int)Convert.ToInt16(webmain.Substring(startIndex, length));
                    
                    if (GG == 1337)
                    {
                        Environment.Exit(0);
                    }
                
                });

            }
            catch (Exception ex)
            {
              
            }
        }

        public async Task UpdateMNGR()
        {
                try
                {
                    using (var mgr = await UpdateManager.GitHubUpdateManager("https://github.com/ffgapple/FFGappleLauncher/"))
                    {
                        if(FFWP.JsonData.Value.CheckUp == true)
                    {
                        var release = await mgr.UpdateApp();
                        await Dispatcher.InvokeAsync(Hide);
                    }
                    else
                    {
                        //bildirim("FFNet", FFWP.Language.Strings.upwont, FFWP.JsonData.Value.content + "/popup.gif");
                        await Dispatcher.InvokeAsync(Hide);
                    }
                       
                    }
                    
                }
                catch (Exception ex)
                {
                    bildirim("FFNet", FFWP.Language.Strings.uperr, FFWP.JsonData.Value.content + "/popup.gif");
                    await Dispatcher.InvokeAsync(Hide);
                }
        }

        //public async void UpdateMNGR()
        //{

        //    using (var manager = new UpdateManager("https://github.com/ffgapple/FFGapple-Launcher"))
        //    {
        //        try
        //        {
        //            var updateInfo = await manager.CheckForUpdate();

        //            if (updateInfo.ReleasesToApply.Any())
        //            {
        //                bildirim("FFNet", FFWP.Language.Strings.ffupdating, FFWP.JsonData.Value.content + "/popup.gif");
        //                await manager.DownloadReleases(updateInfo.ReleasesToApply);
        //                await manager.ApplyReleases(updateInfo);
        //                pr.IsActive = false;
        //                this.Hide();
        //            }
        //            else
        //            {
        //                pr.IsActive = false;
        //                this.Hide();
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //            bildirim("FFNet", FFWP.Language.Strings.uperr, FFWP.JsonData.Value.content + "/popup.gif");
        //        }
        //    }
        //}

        public void bildirim(string baslik, string icerik, string resimurl)
        {
            new ToastContentBuilder()

                                   .AddText(baslik)
                                   .AddText(icerik)
                                   .AddHeroImage(new Uri(resimurl))

                                   .Show();
        }
    }
}
