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
        private ObservableCollection<Movie>? _movies;
        public ObservableCollection<Movie>? Movies
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
            if (Movies == null)
            {
                LoadMoviesFromApi();
            }
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
            
            // Déclencher l'événement MovieSelected
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

    }
}