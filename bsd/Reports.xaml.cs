using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace bsd
{
    /// <summary>
    /// Логика взаимодействия для Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {
        public Reports()
        {
            InitializeComponent();
            FormComboBox.Items.Add("Отчет за день");
            FormComboBox.Items.Add("Отчет за месяц");
        }

        private void FormComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Form1.Visibility = Visibility.Collapsed;
            Form2.Visibility = Visibility.Collapsed;

            if(FormComboBox.SelectedItem != null)
            {
                string selecterdForm = FormComboBox.SelectedItem.ToString();
                if(selecterdForm == "Отчет за день")
                {
                    Form1.Visibility = Visibility.Visible;
                }
                else if(selecterdForm == "Отчет за месяц")
                {
                    Form2.Visibility = Visibility.Visible;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if(Form1.Visibility == Visibility.Visible)
            {
                string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Отчет_за_день.pdf";
                PdfDocument pdfDocument = new PdfDocument();
                PdfPage page = pdfDocument.AddPage();

                RenderTargetBitmap rtb = new RenderTargetBitmap((int)Form1.ActualWidth, (int)Form1.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                rtb.Render(Form1);

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    byte[] imageBytes = ms.ToArray();

                    XImage img = XImage.FromStream(new MemoryStream(imageBytes));
                    
                    XRect box = new XRect(-60, 0, 720, 842);

                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    gfx.DrawImage(img, box);
                }

                pdfDocument.Save(directoryPath);

            }
            else if (Form2.Visibility == Visibility.Visible)
            {
                string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Отчет_за_месяц.pdf";
                PdfDocument pdfDocument = new PdfDocument();
                PdfPage page = pdfDocument.AddPage();

                RenderTargetBitmap rtb = new RenderTargetBitmap((int)Form2.ActualWidth, (int)Form2.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                rtb.Render(Form2);

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    byte[] imageBytes = ms.ToArray();

                    XImage img = XImage.FromStream(new MemoryStream(imageBytes));

                    XRect box = new XRect(-60, 0, 720, 842);

                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    gfx.DrawImage(img, box);
                }

                pdfDocument.Save(directoryPath);
            }
            MessageBox.Show("Проверьте свою папку Документы!!!");
        }
    }
}
