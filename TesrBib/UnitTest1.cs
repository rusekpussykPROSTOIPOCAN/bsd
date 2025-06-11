using bsd;
using bsd.Base;
using bsd.ViewModel;
using Microsoft.Extensions.Configuration;
using NPOI.SS.Formula.Functions;
using Supabase;
using System.Windows;
using System.Windows.Controls;
namespace TesrBib
{
    public class Tests
    {
        public static Supabase.Client SupabaseClient { get; private set; }
        public static IConfiguration Configuration { get; private set; }
        [SetUp]
        public void Setup()
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
        
        [Test, Apartment(ApartmentState.STA)]
        public  void BibVieT1()
        {
            App.start();
            TextBox A = new TextBox() { Text = "Culebaka" };
            TextBox T = new TextBox() { Text = "Culebaka" };
            TextBox Y = new TextBox() { Text = "04-08-2009" };
            TextBox I = new TextBox() { Text = "67342789" };
            TextBox C = new TextBox() { Text = "Cool literature" };
            TextBox IN = new TextBox() { Text = "98469309" };
            BookDeliveryViewModel.AddBooks(A,T,Y,I,C,IN);
            var bse =  SupabaseClient.From<BooksBase>().Where(x => x.ISBN == IN.Text).Get();
          
            Assert.NotNull(bse);
          
            SupabaseClient.From<BooksBase>().Where(x => x.ISBN == IN.Text).Delete();
            
        }
        [Test, Apartment(ApartmentState.STA)]
        public void BibVieT2()
        {
            App.start();
            TextBox Name = new TextBox() { Text = "Culebaka" };
            TextBox LastName = new TextBox() { Text = "Culebaka" };
            TextBox Pass = new TextBox() { Text = "Culebaka" };
            TextBox Email = new TextBox() { Text = "Culebaka" };
            TextBox SerNum = new TextBox() { Text = "3246987438" };
            string sex = "Ì";
            TextBox Whom = new TextBox() { Text = "Culebaka" };
            TextBox Code = new TextBox() { Text = "500-300" };
            TextBox DateV = new TextBox() { Text = "2000-12-09" };
            BookDeliveryViewModel.User(Name,LastName,Pass,Email,SerNum,sex,Whom,Code,DateV);
            var bse = SupabaseClient.From<Readers>().Where(x => x.Email == Email.Text).Get();

            Assert.NotNull(bse);
             SupabaseClient.From<Readers>().Where(x => x.Email == Email.Text).Delete();
        }
      
        public void BibVieT3()
        {
            App.start();
            BookDeliveryViewModel.LoadReaders();
            foreach (var item in BookDeliveryViewModel._allR)
            {
                Console.WriteLine(item.Email);
            }
        }
    }
}