using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.IO;

namespace BrandonVanLoggerenberg_TimeKeeper
{
    /// <summary>
    /// Interaction logic for DatabasePreview.xaml
    /// </summary>
    public partial class DatabasePreview : Window
    {
        public DatabasePreview()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (StreamReader streamReader = new StreamReader(new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"\SQLExtract.txt", FileMode.Open, FileAccess.Read), Encoding.UTF8))
            {
                rtb_DBContent.Document = new FlowDocument(new Paragraph(new Run(streamReader.ReadToEnd()))); 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CopyDataToClipboard(rtb_DBContent.Document);
            //test
        }

        private void CopyDataToClipboard(FlowDocument flowDoc)
        {
            TextRange range = new TextRange(flowDoc.ContentStart,flowDoc.ContentEnd);
            using (Stream stream = new MemoryStream())
            {
                range.Save(stream, DataFormats.Text);
                Clipboard.SetData(DataFormats.Text,Encoding.UTF8.GetString((stream as MemoryStream).ToArray()));
            }
        }
    }
}
