using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FFWP.Dialogs
{
    /// <summary>
    /// FFPass.xaml etkileşim mantığı
    /// </summary>
    public partial class FFPass : ModernWpf.Controls.ContentDialog
    {
        bool CreateNew;
        string GMethod;
        string EncryptedPass;
        string DecryptedPass;
        RegistryKey Controller = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FFGappleLNC");
        RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FFGappleLNC");
        string Pass = "ParentalControlPass";
        public FFPass(string Method)
        {
            InitializeComponent();
            settext();
            GMethod = Method;
        }

        private void settext()
        {
            object ValuePass =  key.GetValue(Pass);
            if(ValuePass != null)
            {
                this.Title = FFWP.Language.Strings.passw;
            }
            else
            {
                this.Title = FFWP.Language.Strings.passnew;
            }

            devam.Content = FFWP.Language.Strings.next;
        }


       
        private async void Parental(string Mode)
        {
            
            if(Mode == "ParentalControlCreate")
            {
                encrypt(pass.Password);
                key.SetValue("ParentalControl", "Activated");
                key.SetValue("ParentalControlPass", EncryptedPass);
                //key.Close();
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Environment.Exit(-1);
            }

            else if ( Controller != null )
            {
               
                if (Mode == "ParentalControlDisable")
                {
                    string Password = key.GetValue("ParentalControlPass").ToString();
                    decrypt(Password);
                    if(DecryptedPass != pass.Password)
                    {

                    }
                    else
                    {
                        key.SetValue("ParentalControl", "Disabled");
                        System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(-1);
                    }
                }
                else if(Mode == "ParentalControlEnable")
                {
                    string Password = key.GetValue("ParentalControlPass").ToString();
                    decrypt(Password);
                    if (DecryptedPass != pass.Password)
                    {
                       
                    }
                    else
                    {
              
                        key.SetValue("ParentalControl", "Activated");
                        System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(-1);
                        // key.Close();
                    }
                }
                else if (Mode == "ParentalControlSettings")
                {
                    string Password = key.GetValue("ParentalControlPass").ToString();
                    decrypt(Password);
                    if (DecryptedPass != pass.Password)
                    {

                    }
                    else
                    {
                        this.Hide();
                        FFWP.Dialogs.ParentalSettings PS = new ParentalSettings();
                        await PS.ShowAsync();
                    }
                }
            }
              
       
        }

        public void encrypt(string passv)
        {
            try
            {
                var Pass = System.Text.Encoding.UTF8.GetBytes(passv);
                EncryptedPass = System.Convert.ToBase64String(Pass);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        public void decrypt(string passv)
        {

            try
            {
                var Pass = System.Convert.FromBase64String(passv);
                DecryptedPass = System.Text.Encoding.UTF8.GetString(Pass);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        public static bool Validator()
        {
            RegistryKey winLogonKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FFGappleLNC", true);
            return (winLogonKey.GetValueNames().Contains("ParentalControl"));
        }

       

        private void devam_Click(object sender, RoutedEventArgs e)
        {
            bool VRes = Validator();
           
            if (VRes == false)
            {
                Parental("ParentalControlCreate");
            }
            else
            {

                if (GMethod.Contains("Parent"))
                {
                    if (GMethod == "ParentalControlEnable")
                    {
                        
                        Parental("ParentalControlEnable");
                    }
                    else if (GMethod == "ParentalControlDisable")
                    {
                        Parental("ParentalControlDisable");
                    }
                    else if (GMethod == "ParentalControlSettings")
                    {
                        Parental("ParentalControlSettings");
                    }
                    else
                    {
                        this.Hide();
                    }
                }
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
