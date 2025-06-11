using bsd.Base;
using Microsoft.Extensions.Configuration;
using SkiaSharp;
using Supabase;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace bsd
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static int NameLib;
        public static int NameUser;
        public static int NameDep;
        public static int IdBooks;
        public static string InvNum;
        public static bool BlackList;

        public static Supabase.Client SupabaseClient { get; private set; }
        public static IConfiguration Configuration { get; private set; }
        
        protected override async void OnStartup(StartupEventArgs e)
        {
            start();
           


        }
        public async static void start()
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
            await SupabaseClient.InitializeAsync();
        }
        

    }

}