using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexandreApp.Model;
using AlexandreApp.ViewModel;

namespace AlexandreApp.Views;

public partial class MovieDetailPage : ContentPage
{
    MoviesViewModel viewModel;
    
    Movie movie;
    public MovieDetailPage(Movie movie)
    {
        viewModel = MoviesViewModel.Instance;
        BindingContext = viewModel;
        
        this.movie = movie;
        
        InitializeComponent();
        
        // Assigner les valeurs des propriétés du film aux contrôles XAML
        MovieImage.Source = movie.posterURL;
        MovieTitle.Text = movie.title;
        MovieDescription.Text = movie.imdbId;

        if (viewModel.IsFavorite(movie))
        {
            MovieFavoriteButton.Text = "Remove from Favorites";
        }
        else
        {
            MovieFavoriteButton.Text = "Add to Favorites";
        }
        

    }
    
    private void Button_OnClicked(object? sender, EventArgs e)
    {
        // Retourner à la page précédente
        Navigation.PopAsync();
    }
    
    private void OnFavoriteClicked(object sender, EventArgs e)
    {
        // Implémentez la logique pour marquer ou désélectionner le film comme favori
        // Par exemple, vous pouvez appeler une méthode du ViewModel pour gérer cette action
        viewModel.ToggleFavorite(movie);
        MovieFavoriteButton.Text = viewModel.IsFavorite(movie) ? "Remove from Favorites" : "Add to Favorites";
    }
    
    private void OnDeleteClicked(object sender, EventArgs e)
    {
        // Implémentez la logique pour supprimer le film
        viewModel.RemoveMovie(movie);
        Navigation.PopAsync();
    }
}