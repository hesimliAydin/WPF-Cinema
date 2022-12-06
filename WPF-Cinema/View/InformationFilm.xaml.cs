using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WPF_Cinema.Model;

namespace WPF_Cinema.View
{
    /// <summary>
    /// Interaction logic for InformationFilm.xaml
    /// </summary>
    public partial class InformationFilm : Window
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null!)
        {
            PropertyChangedEventHandler handler = PropertyChanged!;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        private Movie movie;
        public Movie Movie
        {
            get { return movie; }
            set
            {
                movie = value;
                OnPropertyChanged();
            }
        }
        public InformationFilm(Movie movie)
        {
            InitializeComponent();
            Movie = movie;
            DataContext = this;
        }
    }
}
