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
using System.Xml.Linq;
using bsd.ViewModel;

namespace bsd
{
    
    public partial class NewUser : Window
    {
        public NewUser()
        {
            InitializeComponent();
        }
        public static string sex;
        private void Button_Click(object sender, RoutedEventArgs e)
        {

             BookDeliveryViewModel.User(Name, LastName, Pass, Email, SerNum,sex ,Whom, Code, DateV);
        }
     

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            sex = radio.Content.ToString();
        }
    }
}
