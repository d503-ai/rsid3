using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using MessageBox = System.Windows.Forms.MessageBox;

namespace RSID3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_folder_click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            textbox_folder.Text = dialog.SelectedPath;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_search_click(object sender, RoutedEventArgs e)
        {
            if (textbox_folder.Text.Trim() != "")
            {
                string folder = textbox_folder.Text.Trim();
                string title = textbox_title.Text.Trim();
                string artist = textbox_artist.Text.Trim();
                string album = textbox_album.Text.Trim();
                string year = textbox_year.Text.Trim();
                string genre = textbox_genre.Text.Trim();
                uint counter = 0;

                string[] parames = { title, artist, album, year, genre };

                foreach (string param in parames)
                {
                    if (param != "")
                    {
                        break;
                    }
                    else
                    { counter++; }
                }
                if (counter >= 5)
                {
                    parames = null;
                }
                FactoryMP3 ft = new FactoryMP3(folder);
                List<MP3> MP3List = new List<MP3> { };
                FactoryMP3.DirSearch(folder, MP3List, parames);
                dataGrid.ItemsSource = MP3List;
            }
            else 
            {
                MessageBox.Show("Please, choose folder to search in");
            }
        }
    }
}
