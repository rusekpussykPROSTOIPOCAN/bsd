using bsd.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace bsd.ViewModel
{
    public class LibrarionsViewModel:ObservableObject
    {

        public ObservableCollection<BooksBase> Book { get; set; }

        public  LibrarionsViewModel() {
           
        }
    }
}
