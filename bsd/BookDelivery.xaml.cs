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
using CommunityToolkit.Mvvm.ComponentModel;
using bsd.ViewModel;


namespace bsd
{
    /// <summary>
    /// Логика взаимодействия для BookDelivery.xaml
    /// </summary>
    public partial class BookDelivery : Window
    {
        public static string CurrentLibName;
        public BookDelivery()
        {
            InitializeComponent();
            LoadLib();
            DataContext = new BookDeliveryViewModel();
            
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
       
        private async void LoadLib()
        {
            int id = App.NameLib;
            var tryLib = await App.SupabaseClient.From<Librarions>().Where(x => x.Id == id).Get();
            foreach (var item in tryLib.Models)
            {
                TextBoxForLibName.Text = item.firstname + " " + item.lastname;
            }
        }

        
        private void SearchBook_Click(object sender, RoutedEventArgs e)
        {
            BookDeliveryViewModel.SearchBook(SearchBookField);
        }

        private void SearchReader_Click(object sender, RoutedEventArgs e)
        {
            BookDeliveryViewModel.SearchReader(SearchReaderField);
        }
    }
}
