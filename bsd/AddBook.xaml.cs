using System.Windows;
using bsd.ViewModel;


namespace bsd
{
    /// <summary>
    /// Логика взаимодействия для AddBook.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        public AddBook()
        {
            InitializeComponent();
            DataContext =  new BookDeliveryViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BookDeliveryViewModel.AddBooks(Autor,Title,Year,InvNum,Chapter,ISBN);
            Autor.Clear();
            Title.Clear();
            Year.Clear();
            InvNum.Clear();
            Chapter.Clear();
            ISBN.Clear();
            BookDeliveryViewModel.LoadBooks();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var newform = new AdminPage();
            newform.Show();
            this.Close();
        }
    }
}
