using bsd.Base;
using Supabase;
using Supabase.Interfaces;
using System.Collections.Immutable;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bsd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        public MainWindow()
        {
            InitializeComponent();
        }
    
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string log = Log.Text;
            string pass = Pass.Password;
           
                var dataU = await App.SupabaseClient.From<Readers>().Where(x => x.Pass == pass && x.Email == log).Get();
                var u = dataU.Models;
            if (!u.Any() )
            {
                var dataL = await App.SupabaseClient.From<Librarions>().Where(x => x.pass == pass && x.login == log).Get();
                var l = dataL.Models;
                if (!l.Any())
                {
                    var dataP = await App.SupabaseClient.From<projectdeparts>().Where(x => x.pass == pass && x.login == log).Get();
                    var p = dataP.Models;
                    if (!p.Any())
                    {
                        MessageBox.Show("Ничего");
                    }
                    else
                    {
                        foreach (var item in p)
                        {
                          App.NameDep = item.Id;

                        }
                        var newform = new AdminPage();
                        newform.Show();
                        this.Close();
                    }
                }
                else
                {
                    foreach (var item in l)
                    {
                      App.NameLib = item.Id;

                    }
                    var newform = new WorkSpaceForLibrarions();
                    newform.Show();
                    this.Close();
                }
            }
            else {
                foreach (var item in u)
                {
                   App. NameUser = item.Id;
                    App.BlackList = item.BlackList;
                }
                var newform = new PageForUsers();
                newform.Show();
                this.Close();
            }
        }
    }
}