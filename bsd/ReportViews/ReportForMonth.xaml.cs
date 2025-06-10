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
using static Supabase.Postgrest.Constants;

namespace bsd.ReportViews
{
    /// <summary>
    /// Логика взаимодействия для ReportForMonth.xaml
    /// </summary>
    public partial class ReportForMonth : UserControl
    {
        public static ObservableCollection<BooksBase> TopThreeBooksInMonth { get; set; } = new ObservableCollection<BooksBase>();
        public static ObservableCollection<ReaderWithCount>? TopThreeReadersInMonth { get; set; } = new ObservableCollection<ReaderWithCount>();
        public static int CountBooksInMonth { get; set; }
        public ReportForMonth()
        {
            InitializeComponent();
            DataContext = this;
            LoadThreeReaders();
            LoadLib();
            TopsBooks();
            SetCountBooks(TextBoxForCountBooksInMonth);
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

        private static async void LoadThreeReaders()
        {
            await LoadTopThreeReadersForCurrentMonth();
        }

       private static async Task LoadTopThreeReadersForCurrentMonth()
        {
            try
            {
                DateTime now = DateTime.Now;
                DateTime startDate = new DateTime(now.Year, now.Month, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                var historyResponse = await App.SupabaseClient.From<History>().Select("id_reader, DateOfIssueOrBooking").Filter("DateOfIssueOrBooking", Operator.GreaterThanOrEqual, startDate.ToString("yyyy-MM-dd")).Filter("DateOfIssueOrBooking", Operator.LessThanOrEqual, endDate.ToString("yyyy-MM-dd")).Get();
                var readerCounts = historyResponse.Models.Where(h => h.id_reader.HasValue && h.DateOfIssueOrBooking.HasValue).GroupBy(h => h.id_reader.Value).Select(g => new { ReaderId = g.Key, Count = g.Count() }).OrderByDescending(x => x.Count).Take(3).ToList();
                var topReadersList = new ObservableCollection<ReaderWithCount>();
                TopThreeReadersInMonth.Clear();
                foreach (var readerCount in readerCounts)
                {
                    var readerResponse = await App.SupabaseClient.From<Readers>().Where(r => r.Id == readerCount.ReaderId).Get();
                    if (readerResponse.Models.Any())
                    {
                        var reader = readerResponse.Models.First();
                        TopThreeReadersInMonth.Add(new ReaderWithCount
                        {
                            Id = reader.Id,
                            FirstName = reader.Fname,
                            LastName = reader.Lname,
                            Count = readerCount.Count
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке списка пользователей: {ex.Message}");
            }
        }

        public async static void SetCountBooks(TextBox TextBoxForCountBooksInMonth)
        {
            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(now.Year, now.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            var historyResponse = await App.SupabaseClient
            .From<History>()
            .Select("id")
            .Filter("DateOfIssueOrBooking", Operator.GreaterThanOrEqual, startDate.ToString("yyyy-MM-dd"))
            .Filter("DateOfIssueOrBooking", Operator.LessThanOrEqual, endDate.ToString("yyyy-MM-dd"))
            .Get();

            TextBoxForCountBooksInMonth.Text = historyResponse.Models.Count.ToString();
        }

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
