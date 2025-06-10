
using bsd.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Supabase.Gotrue.Mfa;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace bsd.ViewModel
{
    partial class BookDeliveryViewModel : ObservableObject
    {
        public static ObservableCollection<BooksBase> books { get; set; } = new ObservableCollection<BooksBase>();
        public static ObservableCollection<BooksBase> _allB { get; set; } = new ObservableCollection<BooksBase>();
        public static ObservableCollection<Readers> readers { get; set; } = new ObservableCollection<Readers>();
        public static ObservableCollection<Readers> _allR { get; set; } = new ObservableCollection<Readers>();
        public static ObservableCollection<BooksBase> topbooks { get; set; } = new ObservableCollection<BooksBase>();


        [ObservableProperty]
        private BooksBase _selectedBook;
        [ObservableProperty]
        private Readers _selectedReaders;

        public BookDeliveryViewModel()
        {
            LoadBooks();
            LoadReaders();
        }
        public static void AddBooks(TextBox Autor, TextBox Title, TextBox Year,TextBox InvNum, TextBox Chapter, TextBox ISBN)
        {
            var model = new BooksBase
            {
                Autor = Autor.Text,
                Title = Title.Text,
                Year = DateOnly.Parse(Year.Text),
                InventaryNum = InvNum.Text,
                Chapter = Chapter.Text,
                ISBN = ISBN.Text
            };
            App.SupabaseClient.From<BooksBase>().Insert(model);
        }
        public async static void Tops()
        {
            var topbooksGet = await App.SupabaseClient.From<BooksBase>().Order(x => x.CountBooking, ordering: Supabase.Postgrest.Constants.Ordering.Descending).Limit(50).Get();

            topbooks.Clear();
            foreach (var item in topbooksGet.Models)
            {
                topbooks.Add(item);
            }

        }
        [RelayCommand]
        public static async Task GetBooksFromExel()
        {
            string path = "";
            OpenFileDialog f = new OpenFileDialog();
            bool? su = f.ShowDialog();
            if (su == true)
            {
               path  = f.FileName;


            }
            try
            {
                IWorkbook workbook;
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    workbook = new XSSFWorkbook(fs);
                }
                ISheet sheet = workbook.GetSheetAt(0);
                for (int row = 1; row < sheet.LastRowNum; row++)
                {
                    IRow c = sheet.GetRow(row);
                    if (c == null)
                    {
                        continue;
                    }
                    for (global::System.Int32 i = 0; i < c.LastCellNum; i += 5)
                    {
                        ICell cell = c.GetCell(i);
                        if (cell == null) { continue; }

                        var model = new BooksBase
                        {
                            Autor = GetCellValue(c.GetCell(i)),
                            Title = GetCellValue(c.GetCell(i + 1)),
                            Year = DateOnly.Parse(GetCellValue(c.GetCell(i + 2))),
                            Chapter = GetCellValue(c.GetCell(i + 3)),
                            InventaryNum = GetCellValue(c.GetCell(i + 4)),
                            ISBN = GetCellValue(c.GetCell(i + 5))
                        };
                        await App.SupabaseClient.From<BooksBase>().Insert(model);
                        i++;
                    }
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Проверьте формат данных");
            }
            LoadBooks();
            MessageBox.Show("Готово");


        }
       public static string GetCellValue(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                        return cell.DateOnlyCellValue.ToString();
                    else
                        return cell.NumericCellValue.ToString();
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Formula:
                    return cell.CellFormula;
                default:
                    return string.Empty;
            }
        }
       

        public static async void LoadBooks()
        {
            try
            {
                var response = await App.SupabaseClient.From<BooksBase>().Get();
                if (response != null && response.Models != null)
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
            }
        }

        public static void SearchBook(TextBox Search)
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
                    if (Search.Text == item.Title || Search.Text == item.Autor || Search.Text == item.ISBN.ToString() || Search.Text == item.condition)
                    {
                        books.Add(item);
                    }
                }
            }
        }

        public static void SearchReader(TextBox Search)
        {

            if (string.IsNullOrWhiteSpace(Search.Text))
            {
                LoadReaders();
            }
            else
            {
                readers.Clear();
                foreach (var item in _allR)
                {
                    if (Search.Text == item.Fname || Search.Text == item.Lname || Search.Text == item.SeriaAndNum || Search.Text == item.Email)
                    {
                        readers.Add(item);
                    }
                }
            }
        }

        public static async void LoadReaders()
        {
            try
            {
                var response = await App.SupabaseClient.From<Readers>().Get();
                if (response != null && response.Models != null)
                {
                    readers.Clear();
                    _allR.Clear();
                    foreach (var reader in response.Models)
                    {
                        readers.Add(reader);
                        _allR.Add(reader);
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
        public async static void User(TextBox Name, TextBox LastName, TextBox Pass, TextBox Email, TextBox SerNum,string sex ,TextBox Whom, TextBox Code, TextBox DateV)
        {
          
            try
            {

                var model = new Readers
                {
                    Fname = Name.Text,
                    Lname = LastName.Text,
                    Pass = Pass.Text,
                    Email = Email.Text,
                    SeriaAndNum = SerNum.Text,
                    Sex = sex,
                    IsueByWhom = Whom.Text,
                    Code = Code.Text,
                    DateOfIssue = Convert.ToDateTime(DateV.Text),
                    BlackList = false
                };
                await App.SupabaseClient.From<Readers>().Insert(model);
                MessageBox.Show("Пользователь успешно добавлен в базу.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в данных");
            }

            Name.Clear();
            LastName.Clear();
            Pass.Clear();
            Email.Clear();
            SerNum.Clear();
            Whom.Clear();
            Code.Clear();
            DateV.Clear();


        }

        [RelayCommand]
        private async Task LendBook()
        {
            if(SelectedBook != null && SelectedReaders != null)
            {
                var model = new History
                {
                    DateOfIssueOrBooking = DateTime.Now,
                    id_book = SelectedBook.Id,
                    id_reader = SelectedReaders.Id,
                    PrelimDateOfDel = DateTime.Now.AddDays(30)
                };
                int currentCountBooking = SelectedBook.CountBooking;
                int updateCountBooking = currentCountBooking + 1;
                await App.SupabaseClient.From<History>().Insert(model);
                var hus = await App.SupabaseClient.From<History>().Where(x => x.id_reader == SelectedReaders.Id && x.id_book == SelectedBook.Id ).Get();
                if (SelectedBook.condition != "Занята" )
                {
                    if(SelectedBook.condition != "Свободна")
                        foreach (var item in hus.Models)
                    {
                    if (item.BookingOrExtradition )
                    {
                        await App.SupabaseClient.From<BooksBase>().Where(x => x.Id == SelectedBook.Id).Set(x => x.CountBooking, updateCountBooking).Update();
                        await App.SupabaseClient.From<BooksBase>().Where(x => x.Id == SelectedBook.Id).Set(x => x.condition, "Занята").Update();
                        MessageBox.Show($"{SelectedBook.Title} выдано пользователю: {SelectedReaders.Fname} {SelectedReaders.Lname}");
                        SelectedBook = null;
                        SelectedReaders = null;
                                LoadBooks();
                            break;
                    }
                        
                    else
                    {
                        MessageBox.Show("Книга забронированна другим пользователем");
                                break;
                    }
                    }
                    else
                    {
                        await App.SupabaseClient.From<BooksBase>().Where(x => x.Id == SelectedBook.Id).Set(x => x.CountBooking, updateCountBooking).Update();
                        await App.SupabaseClient.From<BooksBase>().Where(x => x.Id == SelectedBook.Id).Set(x => x.condition, "Занята").Update();
                        MessageBox.Show($"{SelectedBook.Title} выдано пользователю: {SelectedReaders.Fname} {SelectedReaders.Lname}");
                        SelectedBook = null;
                        SelectedReaders = null;
                        LoadBooks();
                    }

                }
                else
                {
                    MessageBox.Show("Книга выдана");
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу и читателя!");
            }
        }
        
       


    }
}
