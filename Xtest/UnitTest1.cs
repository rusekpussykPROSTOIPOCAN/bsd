using bsd.ViewModel;
using bsd;

namespace Xtest
{
    public class UnitTest1
    {
        [Fact]
       
            public async void BibVieT3()
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