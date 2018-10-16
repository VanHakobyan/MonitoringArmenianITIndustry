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

namespace MonitoringIT.Desktop.Linkedin.Scrapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Data.LinkedinPageParser.Linkedin _linkedin;
        public MainWindow()
        {
            InitializeComponent();
        }

        static MainWindow()
        {
            _linkedin=new Data.LinkedinPageParser.Linkedin();
        }
        private void ScrapButton_Click(object sender, RoutedEventArgs e)
        {
            var link = LinkBox.Text;
            _linkedin.GetAlLinkedinProfiles(new List<string>{link});
        }
    }
}
