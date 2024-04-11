using Microsoft.Maui.Controls;
using System;

namespace AlexandreApp.Views
{
    public partial class HomePage : ContentPage
    {
        public List<CarouselItem> CarouselItems { get; set; }
   
        public HomePage()
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
                new CarouselItem { Title = "Image 2", Image = "image2.jpg" },
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
    // Classe de modèle pour les éléments du carrousel
    public class CarouselItem
    {
        public string Title { get; set; }
        public string Image { get; set; }
    }
}