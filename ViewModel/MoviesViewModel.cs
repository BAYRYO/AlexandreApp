using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using AlexandreApp.Model;
using AlexandreApp.Views;

namespace AlexandreApp.ModelView
{
    public class MoviesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand MovieTappedCommand { get; }
        private List<Movie>? _movies;
        public List<Movie>? Movies
        {
            get { return _movies; }
            set { _movies = value; NotifyPropertyChanged(); }
        }
        
        private Movie? _selectedMovie;
        public Movie? SelectedMovie
        {
            get { return _selectedMovie; }
            set { _selectedMovie = value; NotifyPropertyChanged(); }
        }

        // Ajoutez la propriété MovieSelected pour déclencher l'événement
        public event EventHandler<Movie>? MovieSelected;

        public MoviesViewModel()
        {
            LoadMoviesFromApi();
            MovieTappedCommand = new Command<Movie>(OnMovieTapped);
        }

        private async void LoadMoviesFromApi()
        {
            // Appel à votre service pour récupérer les données depuis l'API
            ApiService apiService = new ApiService();
            string jsonResult = await apiService.GetDataFromApi("https://api.sampleapis.com/movies/drama");
            Console.WriteLine(jsonResult);
            // Désérialiser la chaîne JSON en une liste d'objets Movie
            Movies = JsonSerializer.Deserialize<List<Movie>>(jsonResult);
        }
        
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private void OnMovieTapped(Movie selectedMovie)
        {
            SelectedMovie = selectedMovie;
            
            // Déclencher l'événement MovieSelected
            MovieSelected?.Invoke(this, selectedMovie);
        }
    }
}