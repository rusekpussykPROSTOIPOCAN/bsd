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
using static System.Reflection.Metadata.BlobBuilder;

namespace bsd
{
    /// <summary>
    /// Логика взаимодействия для BookDelivery.xaml
    /// </summary>
    public partial class BookDelivery : Window
    {
        public ObservableCollection<BooksBase> books { get; set; } = new ObservableCollection<BooksBase>();
        public ObservableCollection<Readers> readers { get; set; } = new ObservableCollection<Readers>();
        public BookDelivery()
        {
            InitializeComponent();
            DataContext = this;
            LoadBooks();
            LoadReaders();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            var newform = new WorkSpaceForLibrarions();
            newform.Show();
            this.Close();
        }

        private async void LoadBooks()
        {
            try
            {
                var response = await App.SupabaseClient.From<BooksBase>().Get();
                if (response != null && response.Models!= null)
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
            }
        }

        private async void LoadReaders()
        {
            try
            {
                var response = await App.SupabaseClient.From<Readers>().Get();
                if(response != null && response.Models!= null)
                {
                    readers.Clear();
                    foreach (var reader in response.Models)
                    {
                        readers.Add(reader);
                    }
                }
                else
                {
                    Console.WriteLine("");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("");
            }
        }
    }
}
