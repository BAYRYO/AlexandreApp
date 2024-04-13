using AlexandreApp.Model;
using AlexandreApp.ViewModel;

namespace AlexandreApp.Views
{
    public partial class AddMoviePage : ContentPage
    {
        private MoviesViewModel viewModel;
        public AddMoviePage()
        {
            InitializeComponent();
            viewModel = MoviesViewModel.Instance;
        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            // Vérifiez que les entrées sont initialisées
            if (TitleEntry != null && ImdbIdEntry != null && PosterUrlEntry != null)
            {
                // Créez un nouvel objet Movie avec les valeurs des entrées
                Movie newMovie = new Movie
                {
                    title = TitleEntry.Text,
                    imdbId = ImdbIdEntry.Text,
                    posterURL = PosterUrlEntry.Text
                };
                
                // Ajoutez le nouveau film en utilisant le ViewModel
                viewModel.AddMovie(newMovie);

                // Revenir à la page précédente
                Navigation.PopAsync();
            }
            else
            {
                // Gérez le cas où les entrées ne sont pas initialisées correctement
                // Vous pouvez afficher un message d'erreur ou prendre une autre action appropriée
                Console.WriteLine("Error: Entries are not initialized correctly");
            }
        }
    }
}