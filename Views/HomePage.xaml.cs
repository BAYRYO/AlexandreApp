
using AlexandreApp.Model;

namespace AlexandreApp.Views
{
    public partial class HomePage : ContentPage
    {
        public List<CarouselItem> CarouselItems { get; set; }
        public HomePage() // Constructeur par défaut sans paramètres
        {
            InitializeComponent();
            LoadCarouselItems();
        }

        private void LoadCarouselItems()
        {
            // Définir les éléments du carrousel
            CarouselItems = new List<CarouselItem>
            {
                new CarouselItem { Title = "Image 1", Image = "image1.jpg" },
                new CarouselItem { Title = "Image 2", Image = "image2.jpg" }
            };

            // Définir le contexte de liaison pour le CarouselView
            carouselView.BindingContext = this;
        }

        private async void OnGifButtonClicked(object sender, EventArgs e)
        {
            // Naviguer vers la page contenant le GIF
            await Navigation.PushAsync(new GifPage());
        }
    }
}
