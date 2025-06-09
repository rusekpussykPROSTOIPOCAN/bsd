using bsd.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
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

namespace bsd.ReportViews
{

    /*public static ObservableCollection<BooksBase> topbooks { get; set; } = new ObservableCollection<BooksBase>();*/

    /// <summary>
    /// Логика взаимодействия для ReportForDay.xaml
    /// </summary>
    public partial class ReportForDay : UserControl
    {
        public static ObservableCollection<BooksBase> TopThreeBooks { get; set; } = new ObservableCollection<BooksBase>();
        public static ObservableCollection<Readers> TopThreeReaders { get; set; } = new ObservableCollection<Readers>();
        public static int CountBooks { get; set; }
        public ReportForDay()
        {
            InitializeComponent();
            DataContext = this;
            TopsBooks();
            TopsReaders();
            SetCountBooks(TextBoxForCountBooks);
            LoadLib();
        }

        public async static void TopsBooks()
        {
            var topbooksGet = await App.SupabaseClient.From<BooksBase>().Order(x => x.CountBooking, ordering: Supabase.Postgrest.Constants.Ordering.Descending).Limit(3).Get();

            TopThreeBooks.Clear();
            foreach (var item in topbooksGet.Models)
            {
                TopThreeBooks.Add(item);
            }
        }

        public async static void TopsReaders()
        {
            var historyResponse = await App.SupabaseClient.From<History>().Select("id_reader").Get();
            var readerCounts = historyResponse.Models.GroupBy(h => h.id_reader).Select(g => new { ReaderId = g.Key, Count = g.Count() }).OrderByDescending(x => x.Count).Take(3).ToList();
            foreach (var readerCount in readerCounts)
            {
                var readerResponse = await App.SupabaseClient.From<Readers>().Where(r => r.Id == readerCount.ReaderId).Get();

                if (readerResponse.Models.Any())
                {
                    var reader = readerResponse.Models.First();
                    TopThreeReaders.Add(new Readers
                    {
                        Id = reader.Id,
                        Fname = reader.Fname,
                        Lname = reader.Lname,
                    });
                }
            }
        }

        public async static void SetCountBooks(TextBox TextBoxForCountBooks)
        {
            var historyResponseBook = await App.SupabaseClient.From<History>().Select("DateOfIssueOrBooking").Where(x => x.DateOfIssueOrBooking == DateTime.Now).Get();
            TextBoxForCountBooks.Text = historyResponseBook.Models.Count.ToString();
        }

        private async void LoadLib()
        {
            int id = App.NameLib;
            var tryLib = await App.SupabaseClient.From<Librarions>().Where(x => x.Id == id).Get();
            foreach (var item in tryLib.Models)
            {
                TextBoxForNameLib.Text = item.firstname + " " + item.lastname;
            }
        }
    }
}
