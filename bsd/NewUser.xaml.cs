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
        public static string sex;
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            User(Name, LastName, Pass, Email, SerNum, Whom, Code, DateV);
        }
        public async static void User(TextBox Name, TextBox LastName, TextBox Pass, TextBox Email, TextBox SerNum,  TextBox Whom, TextBox Code, TextBox DateV)
        {
            try
            {

            var model = new Readers
            {
                Fname = Name.Text,
                Lname = LastName.Text,
                Pass = Pass.Text,
                Email = Email.Text,
                SeriaAndNum = SerNum.Text,
                Sex = sex,
                IsueByWhom = Whom.Text,
                Code = Code.Text,
                DateOfIssue = Convert.ToDateTime(DateV.Text)
            };
            await App.SupabaseClient.From<Readers>().Insert(model);
            MessageBox.Show("Пользователь успешно добавлен в базу.");
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Ошибка в данных");
            }
            
            Name.Clear();
            LastName.Clear();
            Pass.Clear();
            Email.Clear();
            SerNum.Clear();
            Whom.Clear();
            Code.Clear();
            DateV.Clear();
            

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            sex = radio.Content.ToString();
        }
    }
}
