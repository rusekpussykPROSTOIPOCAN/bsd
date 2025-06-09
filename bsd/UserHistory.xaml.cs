using bsd.Base;
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
    /// Логика взаимодействия для UserHistory.xaml
    /// </summary>
    public partial class UserHistory : Window
    {
        public ObservableCollection<ForHistoryView> Hview { get; set; } = new ObservableCollection<ForHistoryView>();
     
        public UserHistory()
        {
            InitializeComponent();
            DataContext = this;
            LoadBooks();
        }
        private async void LoadBooks()
        {
            int id = App.NameUser;
            try
            {
                var response = await App.SupabaseClient.From <ForHistoryView>().Where(x=>x.id_reader == id).Get();
                if (response != null && response.Models != null)
                {
                    Hview.Clear();
                    foreach (var story in response.Models)
                    {
                        Hview.Add(story);
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
    }
}
