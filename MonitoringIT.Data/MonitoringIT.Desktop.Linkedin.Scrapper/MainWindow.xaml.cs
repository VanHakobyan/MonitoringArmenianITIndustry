using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MonitoringIT.Desktop.Linkedin.Scrapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Data.Linkedin.Scrapper.Lib.Linkedin _linkedin;
        private static List<string> links;
        public MainWindow()
        {
            InitializeComponent();
            var countPage = links.Count / 30 + 1;
            PageUrl.ItemsSource = Enumerable.Range(1, countPage).ToList();
            Url.ItemsSource = links.Take(30);
        }

        static MainWindow()
        {
            _linkedin = new Data.Linkedin.Scrapper.Lib.Linkedin();
            links = Data.Linkedin.Scrapper.Lib.Linkedin._linkedinLinks;

        }

        private async void ScrapButton_Click(object sender, RoutedEventArgs e)
        {
            var link = Url.Text;
            var alLinkedinProfiles = _linkedin.GetAlLinkedinProfiles(new List<string> { link });
            if (alLinkedinProfiles != null) JSONcontent.Text = alLinkedinProfiles.FirstOrDefault();
            await Task.Delay(100);
        }

        private void PageUrl_OnSelected(object sender, RoutedEventArgs e)
        {
            Url.ItemsSource = links.Skip(((int)PageUrl.SelectedValue - 1) * 30).Take(30);
        }
    }
}
