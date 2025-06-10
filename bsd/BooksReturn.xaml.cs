using bsd.Base;
using bsd.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace bsd
{
    /// <summary>
    /// Логика взаимодействия для BooksReturn.xaml
    /// </summary>
    public partial class BooksReturn : Window
    {
        public BooksReturn()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            getId(InventNum);
        }
       public async static void getId(TextBox InventNum)
        {
            await App.SupabaseClient.From<BooksBase>().Where(x => x.InventaryNum == InventNum.Text).Set(x => x.condition, "Свободно").Update();
            int id = 0;
            var a = await App.SupabaseClient.From<BooksBase>().Where(x => x.InventaryNum == InventNum.Text).Get();
            foreach (var item in a.Models)
            {
                id = item.Id;
            }
           await App.SupabaseClient.From<History>().Where(x => x.id_book == id).Set(x => x.End, true ).Update();
           await App.SupabaseClient.From<History>().Where(x => x.id_book == id).Set(x => x.dateOfDel, DateTime.UtcNow.Date ).Update();
            BookDeliveryViewModel.LoadBooks();

        }
    }
}
