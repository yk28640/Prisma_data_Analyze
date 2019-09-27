using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Login2
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
         

        }
        protected string ExecuteInterfaceByUrl(string url, FormUrlEncodedContent para=null)
        {
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };

            using (var http = new HttpClient(handler))
            {
                var responseRpt = http.GetAsync(url).Result;
                var resultRpt = responseRpt.Content.ReadAsStringAsync().Result;

                return resultRpt;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(ExecuteInterfaceByUrl(@"https://localhost:44383/api/Todo"));
        }
    }
}
