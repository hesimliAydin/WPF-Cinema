using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
using WPF_Cinema.Model;
using WPF_Cinema.UserControls;
using WPF_Cinema.View;

namespace WPF_Cinema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string jsonStr;
        private List<string> movieDataBasee;
        HttpClient httpclient = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
            movieDataBasee = new();

            DataContext = this;

            movieDataBasee = JsonSerializer.Deserialize<List<string>>(File.ReadAllText("../../../MovieDataBase/MovieDataBase.json"))!;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var m in movieDataBasee)
            {
                jsonStr = await httpclient.GetStringAsync($"http://www.omdbapi.com/?apikey=82bcd4c7&t={m}&plot=full");

                if (!jsonStr.Contains("Error"))
                {
                    var movie = JsonSerializer.Deserialize<Movie>(jsonStr);
                    uniFormGrid.Children.Add(new MovieUC(movie!));
                }
            }
        }

        private async Task Seartch_Film()
        {
            if (string.IsNullOrWhiteSpace(TextBox_Search.Text))
            {
                MessageBox.Show("Enter Movie Name", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            jsonStr = await httpclient.GetStringAsync($@"http://www.omdbapi.com/?apikey=82bcd4c7&t={TextBox_Search.Text}");

            if (!jsonStr.Contains("Error"))
            {
                var movie = JsonSerializer.Deserialize<Movie>(jsonStr);
                uniFormGrid.Children.Add(new MovieUC(movie!));

                InformationFilm more = new(movie);
                more.ShowDialog();

                movieDataBasee.Add(movie?.Title!);
                string str = JsonSerializer.Serialize(movieDataBasee);
                File.WriteAllText("../../../MovieDataBase/MovieDataBase.json", str);
                return;
            }
            else
                MessageBox.Show("Not Found", "Information", MessageBoxButton.OK, MessageBoxImage.Information);


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Seartch_Film();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
