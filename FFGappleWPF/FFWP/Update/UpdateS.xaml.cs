using Microsoft.Toolkit.Uwp.Notifications;
using Squirrel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using Windows.Media.ClosedCaptioning;

namespace FFWP.Update
{
    /// <summary>
    /// UpdateS.xaml etkileşim mantığı
    /// </summary>
    public partial class UpdateS : Page
    {
        int versionprogram;
        int GG;
        bool outdated = false;
        public UpdateS()
        {
            InitializeComponent();
            Done();
        }

        //public async void CheckUser()
        //{

        //    if ((Properties.Settings.Default.useragree == "agreed") == false)
        //    {

        //        FFWP.Dialogs.UserAgree ug = new FFWP.Dialogs.UserAgree();
        //        await ug.ShowAsync();
        //    }
        //    else
        //    {
               
        //    }
        //}


        public  async void Done()
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
                Environment.Exit(0);
            }
        }
        public async void checkversion()
        {
            try
            {
                await Task.Run(() =>
                {
                    string webmain = new StreamReader(WebRequest.Create("https://raw.githubusercontent.com/ffgapple/ffgapple.github.io/master/data/webdata.html").GetResponse().GetResponseStream()).ReadToEnd();
                    int startIndex = webmain.IndexOf("<vN>") + 4;
                    int length = webmain.Substring(startIndex).IndexOf("</vN>");
                    this.versionprogram = (int)Convert.ToInt16(webmain.Substring(startIndex, length));
                    if (versionprogram == 440)
                    {
                        nativeGapple.Modules.SquirrelDone = "Yes";
                        outdated = false;
                        pr.IsActive = false;
                    }
                    else
                    {
                        outdated = true;
                    }

                    if (outdated == true)
                    {
                        uptext.Text = FFWP.Language.Strings.ffupdating;
                        bildirim("FFNet", FFWP.Language.Strings.ffupdating, FFWP.JsonData.Value.content + "/popup.gif");
                        UpdateMNGR();
                        Debug("New Update Is Ready. Updating with Squirell Update Manager.");
                    }
                });

            }
            catch (Exception ex)
            {
                //error = true;
                //Debug("Update Check Failure : " + ex.ToString());
                //System.Diagnostics.Process.Start(Application.ExecutablePath);
                //Application.Exit();
            }


        }

        public void Debug(String Message)
        {
        //    StreamWriter sw = null;
        //    try
        //    {
               
        //        sw.WriteLine(DateTime.Now.ToString() + " : " + Message);
        //        sw.Flush();
        //        sw.Close();
        //        //if (error == true)
        //        //{
        //        //    toolTip1.SetToolTip(errlogo, Message);
        //        //    toolTip1.BackColor = Color.Indigo;
        //        //}
        //    }
        //    catch
        //    {
                
        //    }
        }
        public async void UpdateMNGR()
        {
                using (var manager = new UpdateManager("https://github.com/ffgapple/FFGapple-Launcher"))
                {
                    try
                    {
                        var updateInfo = await manager.CheckForUpdate();

                        if (updateInfo.ReleasesToApply.Any())
                        {
                            uptext.Text = FFWP.Language.Strings.ffupdating;
                            bildirim("FFNet", FFWP.Language.Strings.ffupdating, FFWP.JsonData.Value.content + "/popup.gif");
                            Debug("New Update Is Ready. Updating with Squirell Update Manager.");
                            await manager.DownloadReleases(updateInfo.ReleasesToApply);
                            await manager.ApplyReleases(updateInfo);
                            pr.IsActive = false;
                        }
                        else
                        {
                            outdated = false;
                            pr.IsActive = false;
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug("Update Service Is Not Working." + ex.ToString());
                        bildirim("FFNet", FFWP.Language.Strings.uperr, FFWP.JsonData.Value.content + "/popup.gif");
                    }
                }
        }

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
