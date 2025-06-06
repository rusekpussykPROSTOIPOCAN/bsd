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

namespace bsd
{
    
    public partial class NewUser : Window
    {
        public NewUser()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var model = new Readers
            {
                Fname = Name.Text,
                Lname=LastName.Text,
                Pass= Pass.Text,
                Email= Email.Text,
                SeriaAndNum= SerNum.Text,
                Sex= Sex.Text,
                IsueByWhom= Whom.Text,
                Code= Code.Text,
                DateOfIssue = DateV.Text
            };
        }
    }
}
