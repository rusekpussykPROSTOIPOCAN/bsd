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

namespace bsd.ReportViews
{
    /// <summary>
    /// Логика взаимодействия для ReportForMonth.xaml
    /// </summary>
    public partial class ReportForMonth : UserControl
    {
        public static ObservableCollection<BooksBase> TopThreeBooksInMonth { get; set; } = new ObservableCollection<BooksBase>();
        public static ObservableCollection<Readers> TopThreeReadersInMonth { get; set; } = new ObservableCollection<Readers>();
        public static int CountBooksInMonth { get; set; }
        public ReportForMonth()
        {
            InitializeComponent();
            LoadLib();
            TopsBooks();
        }

        public async static void TopsBooks()
        {
            var topbooksGet = await App.SupabaseClient.From<BooksBase>().Order(x => x.CountBooking, ordering: Supabase.Postgrest.Constants.Ordering.Descending).Limit(3).Get();

            TopThreeBooksInMonth.Clear();
            foreach (var item in topbooksGet.Models)
            {
                TopThreeBooksInMonth.Add(item);
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
                    TopThreeReadersInMonth.Add(new Readers
                    {
                        Id = reader.Id,
                        Fname = reader.Fname,
                        Lname = reader.Lname,
                    });
                }
            }
        }

        //public async static void SetCountBooks(TextBox TextBoxForCountBooksInMonth)
        //{
        //    var historyResponseBook = await App.SupabaseClient.From<History>().Select("DateOfIssueOrBooking").Where(x => x.DateOfIssueOrBooking.Month == DateTime.Now.Month).Get();
        //    TextBoxForCountBooksInMonth.Text = historyResponseBook.Models.Count.ToString();
        //} Исправляй короче

        private async void LoadLib()
        {
            int id = App.NameLib;
            var tryLib = await App.SupabaseClient.From<Librarions>().Where(x => x.Id == id).Get();
            foreach (var item in tryLib.Models)
            {
                TextBoxForNameLibInMonth.Text = item.firstname + " " + item.lastname;
            }
        }
    }
}
