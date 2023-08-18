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
using System.IO;
using ModernWpf;
using ModernWpf.Controls;

namespace FFWP.Dialogs
{
    /// <summary>
    /// Interaktionslogik für AddMod.xaml
    /// </summary>
    public partial class AddMod : ContentDialog
    {
        string pathusrforall;
        public AddMod(string pathusr)
        {
            string localfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FFGapple Profiles/FFGapple Launcher/local/local.ini";
            FFWP.Modules.SaveINI first = new FFWP.Modules.SaveINI(localfile);
            string langu = first.Read("LocalSettingsForGapple", "Lang");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langu);
            InitializeComponent();
            settext();

            if(!Directory.Exists(pathusr + "/mods/"))
            {
                Directory.CreateDirectory(pathusr + "/mods/");
            }

            var files = Directory.GetFiles(pathusr + "/mods/").Select(System.IO.Path.GetFileName);
            foreach (string file in files)

                if (file.Contains(".jar"))
                {
                
                    modlist.Items.Add(file);

                }

            if(modlist.Items.Count == 0)
            {
                ddimage.Visibility = Visibility.Visible;
            }
            else
            {
                ddimage.Visibility = Visibility.Hidden;
            }

            pathusrforall = pathusr;
           
        }

        public void settext()
        {
            this.Title = FFWP.Language.Strings.addmod;
            info.Text = FFWP.Language.Strings.addmodinf;
            deleteButton.Content = FFWP.Language.Strings.minustool;
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                string filename = System.IO.Path.GetFileName(files[0]);

                string filepath = System.IO.Path.GetFullPath(files[0]);

                if (modlist.Items.Contains(filename) == false)
                {
                    if (filename.Contains(".jar"))
                    {
                        modlist.Items.Add(filename);
                        File.Copy(filepath, pathusrforall + "/mods/" + filename);

                    }


                }
            }

            if (modlist.Items.Count == 0)
            {
                ddimage.Visibility = Visibility.Visible;
            }
            else
            {
                ddimage.Visibility = Visibility.Hidden;
            }

        }

        private void modlist_SourceUpdated(object sender, DataTransferEventArgs e)
        {

           
        }

        private void modlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void modlist_DragEnter(object sender, DragEventArgs e)
        {
          
        }

        private void modlist_Drop(object sender, DragEventArgs e)
        {

        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void modlist_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if(modlist.Items.Count == 0) {

                ModListContext.IsEnabled = false;
            }
            else
            {
                ModListContext.IsEnabled = true;
            }

           
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(modlist.Items.Count == 0) { 
                

                }
                else
                {

                    File.Delete(pathusrforall + "/mods/" + modlist.SelectedItem.ToString());
                    modlist.Items.Remove(modlist.SelectedItem);
                }
            }
            catch
            {


            }
        }
    }
}
