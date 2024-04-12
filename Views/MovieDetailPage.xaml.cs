using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexandreApp.Model;

namespace AlexandreApp.Views;

public partial class MovieDetailPage : ContentPage
{
    public MovieDetailPage(Movie movie)
    {
        InitializeComponent();

        // Assigner les valeurs des propriétés du film aux contrôles XAML
        MovieImage.Source = ImageSource.FromUri(new Uri(movie.posterURL));
        MovieTitle.Text = movie.title;
        MovieDescription.Text = movie.imdbId;
    }
    
    private void Button_OnClicked(object? sender, EventArgs e)
    {
        // Retourner à la page précédente
        Navigation.PopAsync();
    }
}