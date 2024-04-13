using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using AlexandreApp.Model;
using AlexandreApp.ModelView;

namespace AlexandreApp.ViewModel
{
    public class MoviesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private static MoviesViewModel instance;

        public static MoviesViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MoviesViewModel();
                }
                return instance;
            }
        }
        public ICommand MovieTappedCommand { get; }
        public ICommand FavoriteMovieTappedCommand { get; }
        private ObservableCollection<Movie>? _movies;
        public ObservableCollection<Movie>? Movies
        {
            get { return _movies; }
            set { _movies = value; NotifyPropertyChanged(); }
        }
        
        private ObservableCollection<Movie> _favoriteMovies;
        public ObservableCollection<Movie> FavoriteMovies
        {
            get { return _favoriteMovies; }
            set { _favoriteMovies = value; NotifyPropertyChanged(); }
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
            FavoriteMovies = new ObservableCollection<Movie>();
            if (Movies == null)
            {
                LoadMoviesFromApi();
            }
            FavoriteMovieTappedCommand = new Command<Movie>(OnMovieTapped);
            MovieTappedCommand = new Command<Movie>(OnMovieTapped);
        }
        
        public async void LoadMoviesFromApi()
        {
            // Appel à votre service pour récupérer les données depuis l'API
            ApiService apiService = new ApiService();
            string jsonResult = await apiService.GetDataFromApi("https://api.sampleapis.com/movies/drama");
            // Désérialiser la chaîne JSON en une liste d'objets Movie
            Movies = JsonSerializer.Deserialize<ObservableCollection<Movie>>(jsonResult);
        }
        
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Console.WriteLine($"Property changed: {Movies?.Count}, {propertyName}");
        }
        
        private void OnMovieTapped(Movie selectedMovie)
        {
            SelectedMovie = selectedMovie;
            MovieSelected?.Invoke(this, selectedMovie);
        }
        
        public void AddMovie(Movie newMovie)
        {
            Console.WriteLine("Movies: " + Movies);

            // Créez une nouvelle instance de ObservableCollection<Movie>
            ObservableCollection<Movie>? tempList = new ObservableCollection<Movie> { newMovie };
            // Copiez les éléments de Movies dans tempList, s'ils existent
            if (Movies != null)
            {
                foreach (var movie in Movies)
                {
                    tempList.Add(movie);
                }
            }
            
            // Mettez à jour Movies avec la nouvelle liste
            Movies = tempList;

            Console.WriteLine(Movies);
            
            // Vous pouvez également déclencher un événement PropertyChanged pour mettre à jour l'affichage
            NotifyPropertyChanged(nameof(Movies));
        }
        
        public void ToggleFavorite(Movie movie)
        {
            // Vérifiez si le film est déjà dans la liste des favoris
            if (FavoriteMovies.Contains(movie))
            {
                // Supprimez le film de la liste des favoris
                FavoriteMovies.Remove(movie); }
            else
            {
                // Ajoutez le film à la liste des favoris
                FavoriteMovies.Add(movie);
            }
            Console.WriteLine("FavoriteMovies: " + FavoriteMovies);
            // Vous pouvez également déclencher un événement PropertyChanged pour mettre à jour l'affichage
            NotifyPropertyChanged(nameof(FavoriteMovies));
        }

        public bool IsFavorite(Movie movie)
        {
            // Vérifiez si le film est dans la liste des favoris
            return FavoriteMovies.Contains(movie);
        }

        public void RemoveMovie(Movie movie)
        {
            if (Movies != null)
            {
                // Supprimez le film de la liste
                Movies.Remove(movie);
                if (FavoriteMovies.Contains(movie))
                {
                    FavoriteMovies.Remove(movie);
                }
                // Vous pouvez également déclencher un événement PropertyChanged pour mettre à jour l'affichage
                NotifyPropertyChanged(nameof(Movies));
            }
        }

    }
}