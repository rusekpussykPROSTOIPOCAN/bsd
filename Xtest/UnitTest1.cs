using bsd;
using bsd.Base;
using bsd.ViewModel;
using Microsoft.Extensions.Configuration;
using NPOI.SS.Formula.Functions;
using Supabase;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace TesrBib
{
    public class Tests
    {
        public static Supabase.Client SupabaseClient { get; private set; }
        public static IConfiguration Configuration { get; private set; }
        
        public  Tests()
        {


            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName, "JCON.json");
            var builder = new ConfigurationBuilder().AddJsonFile(path, optional: true, reloadOnChange: true);
            Configuration = builder.Build();
            var url = Configuration["Supabase:Url"];
            var key = Configuration["Supabase:Key"];
            SupabaseClient = new Client(url, key, new SupabaseOptions
            {
                AutoConnectRealtime = true
            });
            SupabaseClient.InitializeAsync();


        }

        [WpfFact]
        public void BibVieT1()
        {
            App.start();
            TextBox A = new TextBox() { Text = "Culebaka" };
            TextBox T = new TextBox() { Text = "Culebaka" };
            TextBox Y = new TextBox() { Text = "04-08-2009" };
            TextBox I = new TextBox() { Text = "67342789" };
            TextBox C = new TextBox() { Text = "Cool literature" };
            TextBox IN = new TextBox() { Text = "98469309" };
            BookDeliveryViewModel.AddBooks(A, T, Y, I, C, IN);
            var bse = SupabaseClient.From<BooksBase>().Where(x => x.ISBN == IN.Text).Get();

            Assert.NotNull(bse);

            SupabaseClient.From<BooksBase>().Where(x => x.ISBN == IN.Text).Delete();

        }
        [WpfFact]
        public async Task BibVieT2()
        {
            App.start();
            TextBox Name = new TextBox() { Text = "Culebaka" };
            TextBox LastName = new TextBox() { Text = "Culebaka" };
            TextBox Pass = new TextBox() { Text = "Culebaka" };
            TextBox Email = new TextBox() { Text = "Culebaka" };
            TextBox SerNum = new TextBox() { Text = "3246987438" };
            string sex = "М";
            TextBox Whom = new TextBox() { Text = "Culebaka" };
            TextBox Code = new TextBox() { Text = "500-300" };
            TextBox DateV = new TextBox() { Text = "2000-12-09" };
           await BookDeliveryViewModel.User(Name, LastName, Pass, Email, SerNum, sex, Whom, Code, DateV);
            var bse = SupabaseClient.From<Readers>().Where(x => x.Email == Email.Text).Get();

            Assert.NotNull(bse);
           await SupabaseClient.From<Readers>().Where(x => x.Email == Email.Text).Delete();
        }
        [StaFact]
        public async Task BibVieT3()
        {
            App.start();
           await BookDeliveryViewModel.Tops();
            Assert.True( BookDeliveryViewModel.topbooks.Count > 0);

        }
        [WpfFact]
        public async Task BibVieT4()
        {
            App.start();
          await  BookDeliveryViewModel.LoadReaders();
            TextBox textBox = new TextBox() { Text = "Паша" };
             BookDeliveryViewModel.SearchReader(textBox);
            Assert.NotNull(BookDeliveryViewModel.readers);
            string name = "";
            foreach (var item in BookDeliveryViewModel.readers)
            {
                name = item.Fname;
            }
            Assert.Equal(name, textBox.Text);

        }
        [WpfFact]
        public async Task BibVieT5()
        {
            App.start();
            await BookDeliveryViewModel.LoadBooks();
            TextBox textBox = new TextBox() { Text = "Autor1" };
          await  BookDeliveryViewModel.SearchBook(textBox);
            Assert.NotNull(BookDeliveryViewModel.books);
            string name = "";
            foreach (var item in BookDeliveryViewModel.books)
            {
                name = item.Autor;
            }
            Assert.Equal(name, textBox.Text);

        }
        [WpfFact]
        public async Task BibVieT6()
        {
            App.start();
            await BookDeliveryViewModel.LoadBooks();
            Assert.Equal(BookDeliveryViewModel.books.Count,BookDeliveryViewModel._allB.Count);

        }
    }
}