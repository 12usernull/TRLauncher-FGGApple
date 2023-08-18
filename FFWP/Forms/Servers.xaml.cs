using CmlLib.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace FFWP.Forms
{
    /// <summary>
    /// Servers.xaml etkileşim mantığı
    /// </summary>
    public partial class Servers : Page
    {
        public Servers()
        {
            InitializeComponent();
            List<Item> itemList = LoadDataFromJsonFile("D:\\Backups\\5.08.23\\Deneme\\BasicServer.json");
            mylst.ItemsSource = itemList;
        }

        private List<Item> LoadDataFromJsonFile(string jsonFilePath)
        {
            List<Item> itemList = new List<Item>();

            try
            {
                using (StreamReader reader = new StreamReader(jsonFilePath))
                {
                    string jsonContent = reader.ReadToEnd();
                    itemList = JsonConvert.DeserializeObject<List<Item>>(jsonContent);
                  
                }
                itemList = itemList.Where(item => item.Activation == "Active").ToList();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("JSON dosyası bulunamadı!");
            }
            catch (JsonException)
            {
                MessageBox.Show("JSON dosyası geçersiz format!");
            }

            return itemList;
        }


        public class Item
        {
            public string Title { get; set; }
            public string ImageUrl { get; set; }
            public string Description { get; set; }
            public string Microsoft { get; set; }
            public string Activation { get; set; }
            public string Versions { get; set; }
            public string Video { get; set; }

        }

        private void StyledGrid_InitWrapGrid(object sender, RoutedEventArgs e)
        {
            // WrapPanel yüklendiğinde çağrılan olay işleyicisi
            WrapPanel wrapPanel = (WrapPanel)sender;
            wrapPanel.Width = 150; // Örnek genişlik değeri (isteğe bağlı)
        }

        private async void mylst_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private async void mylst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Item Data = (Item)mylst.SelectedItems[0];
                string VidURL = Data.Video;
                string Description = Data.Description;
                string Name = Data.Title;
                string Microsoft = Data.Microsoft;
                FFWP.Dialogs.ServerInfo SI = new Dialogs.ServerInfo(VidURL, Description, Name, Microsoft);
                await SI.ShowAsync();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
