
﻿using bsd.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace bsd
{
    /// <summary>
    /// Логика взаимодействия для PageForUsers.xaml
    /// </summary>
    public partial class PageForUsers : Window
    {
        public static ObservableCollection<BooksBase> topbooks {  get; set; }  = new ObservableCollection<BooksBase>();
        public PageForUsers()
        {
            InitializeComponent();

         ConvertPhotos();
           
        }
        
        public async static void ConvertPhotos()
        {
           var topbooksGet = await App.SupabaseClient.From<BooksBase>().Order(x=>x.CountBooking, ordering:Supabase.Postgrest.Constants.Ordering.Descending).Limit(3).Get();


            foreach (var item in topbooksGet.Models)
            {
                topbooks.Add(item);
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var newform = new MainWindow();
            newform.Show();
            this.Close();
        }
    }
}
