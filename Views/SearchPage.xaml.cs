using System;
using System.ComponentModel;
using AlexandreApp.Model;
using AlexandreApp.ViewModel;
using Microsoft.Maui.Controls;

namespace AlexandreApp.Views
{
    public partial class SearchPage : ContentPage
    {
        MoviesViewModel viewModel;

        public SearchPage()
        {
            viewModel = MoviesViewModel.Instance;
            BindingContext = viewModel;
            InitializeComponent();
            // Abonnez-vous à l'événement MovieSelected
            viewModel.MovieSelected += OnMovieSelected;
        }

        private async void OnMovieSelected(object sender, Movie e)
        {
            // Naviguer vers la page de détail du film avec le film sélectionné
            await Navigation.PushAsync(new MovieDetailPage(e));
        }
    
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // S'abonner à l'événement PropertyChanged de la vue modèle
            MoviesCollectionView.ItemsSource = viewModel.Movies;
            Console.WriteLine("OnAppearing: " + viewModel.Movies?.Count);
            Console.WriteLine(MoviesCollectionView.ItemsSource);
            if (viewModel.Movies != null)
                foreach (var movie in viewModel.Movies)
                {
                    Console.WriteLine(movie.title);
                }
        }
    }
}