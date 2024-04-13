using AlexandreApp.Model;
using AlexandreApp.ViewModel;

namespace AlexandreApp.Views;

public partial class FavoritePage : ContentPage
{
    MoviesViewModel viewModel;
    public FavoritePage()
    {
        viewModel = MoviesViewModel.Instance;
        BindingContext = viewModel;
        InitializeComponent();

        // Abonnez-vous à l'événement MovieSelected
        viewModel.MovieSelected += OnMovieSelected;
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Mettre à jour la liste des films favoris
        FavoritesCollectionView.ItemsSource = viewModel.FavoriteMovies;
    }
    
    private async void OnMovieSelected(object sender, Movie e)
    {
        // Vérifier si l'objet Movie n'est pas null
        if (e != null)
        {
            // Naviguer vers la page de détail du film avec le film sélectionné
            await Navigation.PushAsync(new MovieDetailPage(e));
        }
        else
        {
            // Gérer le cas où l'objet Movie est null
            // Vous pouvez afficher un message d'erreur ou prendre une autre action appropriée
            Console.WriteLine("Error: Selected movie is null");
        }
    }
}