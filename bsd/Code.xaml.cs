using BarcodeStandard;
using SkiaSharp;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Imaging;


namespace bsd
{
    /// <summary>
    /// Логика взаимодействия для Code.xaml
    /// </summary>
    public partial class Code : Window
    {
        public static BitmapImage ImageB;
        public Code()
        {
            InitializeComponent();
            BarCodes(App.InvNum);
            BarCodesd.Source = ImageB;
            DataContext = this;
        }
        public static void BarCodes(string num)
        {
            Barcode barcode = new Barcode()
            {
                IncludeLabel = true, // Показывать текст


                Width = 250, 
                Height = 110, 
                BackColor = SKColors.Transparent,
                ForeColor = SKColors.Black 
            };


          SKImage Image = barcode.Encode(BarcodeStandard.Type.Code128, num);
            using (var stream = new MemoryStream())
            {
                Image.Encode(SKEncodedImageFormat.Png, 100).SaveTo(stream);
                stream.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                ImageB = bitmapImage;
                
            }
        }
        
    }
}
