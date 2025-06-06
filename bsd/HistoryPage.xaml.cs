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
using static System.Reflection.Metadata.BlobBuilder;

namespace bsd
{
    /// <summary>
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Window
    {
      public ObservableCollection<History> history { get; set; } = new ObservableCollection<History>();
       public List<string> strings { get; set; }
        public HistoryPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadBooks();
        }
        private async void LoadBooks()
        {
            try
            {
                var response = await App.SupabaseClient.From<History>().Get();
                if (response != null && response.Models != null)
                {
                    history.Clear();
                    foreach (var story in response.Models)
                    {
                        
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
