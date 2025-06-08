using bsd.Base;
using bsd.ViewModel;
using System.Collections.ObjectModel;

using System.Windows;

using System.Windows.Controls;

namespace bsd
{
    /// <summary>
    /// Логика взаимодействия для WorkSpaceForLibrarions.xaml
    /// </summary>
    public partial class WorkSpaceForLibrarions : Window
    {
        public static ObservableCollection<BooksBase> books { get; set; } = new ObservableCollection<BooksBase>();
        public static ObservableCollection<BooksBase> _allB { get; set; } = new ObservableCollection<BooksBase>();
        public WorkSpaceForLibrarions()
        {
            InitializeComponent();
            DataContext = new BookDeliveryViewModel();
            LoadBooks();
        }

       public static async void LoadBooks()
        {
            try
            {
                var response = await App.SupabaseClient.From<BooksBase>().Get();
               
                if (response != null && response.Models
                    != null)
                {
                    books.Clear();
                    _allB.Clear();
                    foreach (var book in response.Models)
                    {
                        books.Add(book);
                        _allB.Add(book);
                    }

                }
                else
                {
                    Console.WriteLine("");
                }
            } catch (Exception ex)
            {
                Console.WriteLine("");
            }
        }

        public static   void SearchBookLike( TextBox Search)
        {

            if (string.IsNullOrWhiteSpace(Search.Text))
            {
                LoadBooks();
            }
            else
            {
                books.Clear();
                var serch = Search.Text.ToLower();
                foreach (var item in _allB)
                {
                    var pop = new List<string> {
                        item.ISBN.ToString(),
                        item.Title,
                        item.Year.ToString(),
                        item.Chapter.ToString(),
                        item.condition,
                        item.CountBooking.ToString(),
                        item.Autor
                    };
                    if (pop.Any(p => p.ToLower().Contains(serch)))
                    {
                        books.Add(item);

                    }
                }

            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newform = new MainWindow();
            newform.Show();
            this.Close();
        }


        private void BookDelivery_Click(object sender, RoutedEventArgs e)
        {
            var newform = new BookDelivery();
            newform.Show();
            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var newform = new HistoryPage();
            newform.Show();
            

        }

      

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var newform = new NewUser();
            newform.Show();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            BookDeliveryViewModel.SearchBook(Search);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

            SearchBookLike(Search);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var newform = new HistoryPage();
            newform.Show();
        }

        private void PageForReport_Click(object sender, RoutedEventArgs e)
        {
            var newform = new Reports();
            newform.Show();
        }
    }
}
