using bsd.Base;

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
            TryName();
            DataContext = this;
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
        public async static void TryName()
        {
            int id = App.NameLib;
            var tryLib = await App.SupabaseClient.From<Librarions>().Where(x => x.Id == id).Get();
            foreach (var item in tryLib.Models)
            {
                MessageBox.Show(item.firstname);
            }
        }
        public static   void SearchBook( TextBox Search)
        {
           
               if (string.IsNullOrWhiteSpace(Search.Text))
            {
                LoadBooks();
            }
            else
            {
                books.Clear();
                foreach (var item in _allB)
                {
                    if (Search.Text == item.Title || Search.Text == item.Autor || Search.Text == item.ISBN.ToString() || Search.Text == item.condition )
                    {
                        books.Add(item);
                    }
                }
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
            this.Close();
        }

      

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var newform = new NewUser();
            newform.Show();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SearchBook(Search);
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
    }
}
