
using BarcodeStandard;
using bsd.Base;
using bsd.ViewModel;
using SkiaSharp;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Input;


namespace bsd
{
    /// <summary>
    /// Логика взаимодействия для PageForUsers.xaml
    /// </summary>
    public partial class PageForUsers : Window
    {
        private const int BarWeight = 1;

        public PageForUsers()
        {
            InitializeComponent();

            DataContext = new BookDeliveryViewModel();
            BookDeliveryViewModel.Tops();

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newform = new UserHistory();
            newform.Show();

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



        private void BooksSelect_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            bool b = App.BlackList;
            int id = App.NameUser;
            if (e.OriginalSource is FrameworkElement sourse && sourse.DataContext != null && BooksSelect.SelectedItem != null)
            {
                var selectedItem = (BooksBase)BooksSelect.SelectedItem;
                if (b)
                {
                    MessageBox.Show("Вы заблокированы");
                }
                else if (MessageBox.Show("Хотите оформить бронь книги на 7 дней?", "Окно бронирования", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Thread.Sleep(100);
                    if (selectedItem.condition != "Свободна")
                    {
                        MessageBox.Show("Книга занята!");
                    }
                    else
                    {

                        var model = new History
                        {
                            DateOfIssueOrBooking = DateTime.Now,
                            id_book = selectedItem.Id,
                            id_reader = id,
                            PrelimDateOfDel = DateTime.Now.AddDays(7),
                            BookingOrExtradition = true
                        };

                        App.SupabaseClient.From<History>().Insert(model);
                        App.SupabaseClient.From<BooksBase>().Where(x => x.Id == selectedItem.Id).Set(x => x.condition, "Забронированно").Update();
                        MessageBox.Show($"{selectedItem.Title}  забронированно!");
                        App.InvNum = selectedItem.InventaryNum;
                        var newform = new Code();
                        newform.Show();

                        BookDeliveryViewModel.LoadBooks();
                        selectedItem = null;
                    }








                }

            }

        }
        
    }
}
