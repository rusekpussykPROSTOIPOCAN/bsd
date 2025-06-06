using bsd.Base;
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
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace bsd
{
    /// <summary>
    /// Логика взаимодействия для WorkSpaceForLibrarions.xaml
    /// </summary>
    public partial class WorkSpaceForLibrarions : Window
    {
        public ObservableCollection<BooksBase> books { get; set; } = new ObservableCollection<BooksBase>();
        public WorkSpaceForLibrarions()
        {
            InitializeComponent();
            DataContext = this;
            LoadBooks();
        }

       private async void LoadBooks()
        {
            try
            {
                var response = await App.SupabaseClient.From<BooksBase>().Get();
                if (response != null && response.Models
                    != null)
                {
                    books.Clear();
                    foreach (var book in response.Models)
                    {
                        books.Add(book);
                    }
                }
                else
                {
                    Console.WriteLine("");
                }
            } catch (Exception ex)
            {
                Console.WriteLine("");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newform = new MainWindow();
            newform.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var newform = new HistoryPage();
            newform.Show();
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
