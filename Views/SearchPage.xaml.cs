using System;
using AlexandreApp.Model;
using AlexandreApp.ModelView;
using Microsoft.Maui.Controls;

namespace AlexandreApp.Views
{
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
            BindingContext = new MoviesViewModel();

            // Abonnez-vous à l'événement MovieSelected
            var viewModel = (MoviesViewModel)BindingContext;
            viewModel.MovieSelected += OnMovieSelected;
        }

        private async void OnMovieSelected(object sender, Movie e)
        {
            // Naviguer vers la page de détail du film avec le film sélectionné
            await Navigation.PushAsync(new MovieDetailPage(e));
        }
    }
}