
﻿using bsd.Base;
using bsd.ViewModel;
using Supabase.Gotrue;
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
        public PageForUsers()
        {
            InitializeComponent();
         
            DataContext = new BookDeliveryViewModel();
            BookDeliveryViewModel.Tops();
           
        }
        
      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newform = new UserHistory();
            newform.Show();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var newform = new MainWindow();
            newform.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BookDeliveryViewModel.SearchBook(Search);
        }
        
       

        private void BooksSelect_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            bool b = App.BlackList;
            int id = App.NameUser;
            if (e.OriginalSource is FrameworkElement sourse && sourse.DataContext != null && BooksSelect.SelectedItem != null)
            {
                var selectedItem = (BooksBase)BooksSelect.SelectedItem;
                if (b)
                {
                    MessageBox.Show("Вы заблокированы");
                }
                else if(MessageBox.Show("Хотите оформить бронь книги на 7 дней?","Окно бронирования", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (selectedItem.condition != "Свободно")
                    {
                        MessageBox.Show("Книга занята!");
                    }
                    else
                    {

            var model = new History
            {
                DateOfIssueOrBooking = DateTime.Now,
                id_book = selectedItem.Id,
                id_reader = id,
                PrelimDateOfDel = DateTime.Now.AddDays(7),
                BookingOrExtradition = true
            };
           
            App.SupabaseClient.From<History>().Insert(model);
            App.SupabaseClient.From<BooksBase>().Where(x => x.Id == selectedItem.Id).Set(x => x.condition, "Забронированно").Update();
            MessageBox.Show($"{selectedItem.Title}  забронированно!");
                        BookDeliveryViewModel.LoadBooks();
            selectedItem = null;
                    }
                    
                    
        

           

        

    }
                
            }
            
        }
    }
}
