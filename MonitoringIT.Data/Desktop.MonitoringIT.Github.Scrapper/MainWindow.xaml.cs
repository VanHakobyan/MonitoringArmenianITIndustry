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
using Lib.MonitoringIT.DATA.Github.Scrapper;
using Newtonsoft.Json;

namespace Desktop.MonitoringIT.Github.Scrapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GithubScrapper _githubScrapper;
        private List<string> urlsProfileInDb;
        public MainWindow()
        {
            InitializeComponent();
            _githubScrapper = new GithubScrapper();
            _githubScrapper.LoadProxy();
            urlsProfileInDb = _githubScrapper.LoadUrlInDb();

            var countPage = urlsProfileInDb.Count / 30 + 1;
            PageUrl.ItemsSource = Enumerable.Range(1, countPage).ToList();
            Url.ItemsSource = urlsProfileInDb.Take(30);
        }

        private void PageUrl_OnSelected(object sender, SelectionChangedEventArgs e)
        {
            Url.ItemsSource = urlsProfileInDb.Skip(((int)PageUrl.SelectedValue - 1) * 30).Take(30);
        }

        private async void ScrapButton_Click(object sender, RoutedEventArgs e)
        {
            var profile = await _githubScrapper.GetNewGithubProfile(Url.Text);
            if (profile!=null)
            {
                var jsonString = JsonConvert.SerializeObject(profile, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                JSONcontent.Text = jsonString; 
            }
            else MessageBox.Show("Error");
            await Task.Delay(100);
        }
    }
}
