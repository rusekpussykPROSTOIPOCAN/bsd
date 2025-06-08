
﻿using bsd.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using bsd.ViewModel;
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
    }
}
