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
    /// Логика взаимодействия для Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {
        public Reports()
        {
            InitializeComponent();
            FormComboBox.Items.Add("Отчет за день");
            FormComboBox.Items.Add("Отчет за месяц");
        }

        private void FormComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Form1.Visibility = Visibility.Collapsed;
            Form2.Visibility = Visibility.Collapsed;

            if(FormComboBox.SelectedItem != null)
            {
                string selecterdForm = FormComboBox.SelectedItem.ToString();
                if(selecterdForm == "Отчет за день")
                {
                    Form1.Visibility = Visibility.Visible;
                }
                else if(selecterdForm == "Отчет за месяц")
                {
                    Form2.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
