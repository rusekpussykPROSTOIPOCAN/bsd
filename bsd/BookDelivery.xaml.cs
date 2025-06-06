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

namespace bsd
{
    /// <summary>
    /// Логика взаимодействия для BookDelivery.xaml
    /// </summary>
    public partial class BookDelivery : Window
    {
        public BookDelivery()
        {
            InitializeComponent();

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            var newform = new WorkSpaceForLibrarions();
            newform.Show();
            this.Close();
        }
    }
}
