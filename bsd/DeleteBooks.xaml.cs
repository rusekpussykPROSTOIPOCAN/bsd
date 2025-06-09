using bsd.Base;
using bsd.ViewModel;
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
    /// Логика взаимодействия для DeleteBooks.xaml
    /// </summary>
    public partial class DeleteBooks : Window
    {
        public DeleteBooks()
        {
            InitializeComponent();
            DataContext = new BookDeliveryViewModel();
        }

        private void BooksSelect_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement sourse && sourse.DataContext != null && BooksSelect.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить книгу из базы?", "Окно удаления", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var selectedItem = (BooksBase)BooksSelect.SelectedItem;
                    App.SupabaseClient.From<BooksBase>().Where(x => x.Id==selectedItem.Id).Delete();
                    BookDeliveryViewModel.LoadBooks();
                    selectedItem = null;

                }
            }
        }
    }
}
