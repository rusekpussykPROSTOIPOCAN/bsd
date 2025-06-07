using bsd.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace bsd.ViewModel
{
    partial class BookDeliveryViewModel : ObservableObject
    {
        public static ObservableCollection<BooksBase> books { get; set; } = new ObservableCollection<BooksBase>();
        public static ObservableCollection<BooksBase> _allB { get; set; } = new ObservableCollection<BooksBase>();
        public static ObservableCollection<Readers> readers { get; set; } = new ObservableCollection<Readers>();
        public static ObservableCollection<Readers> _allR { get; set; } = new ObservableCollection<Readers>();

        [ObservableProperty]
        private BooksBase _selectedBook;
        [ObservableProperty]
        private Readers _selectedReaders;

        public BookDeliveryViewModel()
        {
            LoadBooks();
            LoadReaders();
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

        [RelayCommand]
        private void LendBook()
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
                App.SupabaseClient.From<History>().Insert(model);
                App.SupabaseClient.From<BooksBase>().Where(x=>x.Id==SelectedBook.Id).Set(x=>x.CountBooking, updateCountBooking).Update();
                MessageBox.Show($"{SelectedBook.Title} выдано пользователю: {SelectedReaders.Fname} {SelectedReaders.Lname}");
                SelectedBook = null;
                SelectedReaders = null;
            }
            else
            {
                MessageBox.Show("Выберите книгу и читателя!");
            }
        }


    }
}
