using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop.MonitoringIT.Desktop.Linkedin.Scrapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static global::Lib.MonitoringIT.Data.Linkedin.Scrapper.Linkedin _linkedin;
        private static List<string> links;

        /// <summary>
        /// Ctor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            var countPage = links.Count / 30 + 1;
            PageUrl.ItemsSource = Enumerable.Range(1, countPage).ToList();
            Url.ItemsSource = links.Take(30);
        }

        /// <summary>
        /// Static ctor
        /// </summary>
        static MainWindow()
        {
            _linkedin = new global::Lib.MonitoringIT.Data.Linkedin.Scrapper.Linkedin();
            links = global::Lib.MonitoringIT.Data.Linkedin.Scrapper.Linkedin._linkedinLinks;

        }

        /// <summary>
        /// Start scrapping
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ScrapButton_Click(object sender, RoutedEventArgs e)
        {
            var link = Url.Text;
            var alLinkedinProfiles = _linkedin.GetAllLinkedinProfiles(new List<string> { link });
            if (alLinkedinProfiles != null) JSONcontent.Text = alLinkedinProfiles.FirstOrDefault();
            await Task.Delay(100);
        }

        /// <summary>
        /// Pagination
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageUrl_OnSelected(object sender, RoutedEventArgs e)
        {
            Url.ItemsSource = links.Skip(((int)PageUrl.SelectedValue - 1) * 30).Take(30);
        }
    }
}
