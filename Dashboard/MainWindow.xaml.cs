using Dashboard.UserCharts;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public static int pos;
        public MainWindow()
        {
            
            InitializeComponent();
            //consume consu = new consume();
            //gg.DataContext = consu;
            GaugeUser gu = new GaugeUser();
             List<consume> consumeList = new List<consume>();
            consumeList.Add(new consume("Jack",30));
            consumeList.Add(new consume("Mark", 40));
            gu.DataContext = consumeList;
            gg.Children.Add(gu);
            consumeList[0].Sales = 75;



        }
    }

    internal class consuVM
    {
        public List<consume> consumeList = new List<consume>();
        public consuVM(consume Consume)
        {
            consumeList.Add(Consume);
        }
    }

    internal class consume
    {
        public string Name { set; get; }
        public int Sales { set; get; }
        public consume(string name, int sales)
        {
            Name = name;
            Sales = sales;
           
            
        }
    }
}
