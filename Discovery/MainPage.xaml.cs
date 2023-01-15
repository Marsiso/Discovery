using Discovery.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Discovery;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        hamburgerButton.Image = (FileImageSource)ImageSource.FromFile("burgericon.png");

        List<string> list = new()
        {
            "Home",
            "Browse",
            "Favourite"
        };

        listView.ItemsSource = list;
    }

    void hamburgerButton_Clicked(object sender, EventArgs e)
    {
        navigationDrawer.ToggleDrawer();
    }

    private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        /*     if (e.SelectedItem.ToString() == "Home")
             {
                 contentLabel.Text = "Home";
             }
             else if (e.SelectedItem.ToString() == "Browse")
             {
                 contentLabel.Text = "Browse";
             }
             else if (e.SelectedItem.ToString() == "Favourite")
             {
                 contentLabel.Text = "Favourite";
             }*/

        navigationDrawer.ToggleDrawer();
    }

    private async void ItemsView_OnRemainingItemsThresholdReached(object sender, EventArgs e)
    {
        if (mainPageViewModel is not null && mainPageViewModel.PhotoPage is not null)
        {
            /*await mainPageViewModel.GetNextCarouselDataAsync(mainPageViewModel.PhotoPage.page + 1);*/
        }
    }

    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();

            var photoPage = await App.CarouselService.GetAllCategorizedImages("nature");
            var photoEntities = await App.DatabaseService.GetAllPhotos();
            var temp = photoEntities?.Count > 0
                ? new ObservableCollection<PhotoEntity>(photoEntities)
                : new ObservableCollection<PhotoEntity>();
            if (photoPage?.photos?.Count > 0)
            {
                foreach (var photo in photoPage.photos)
                {
                    if (photoEntities?.Count > 0 && photoEntities.Any(x => x.Id == photo.id))
                    {
                        continue;
                    }

                    var photoEntityToCreate = new PhotoEntity
                    {
                        Id = photo.id,
                        Alt = photo.alt,
                        Url = photo.source.portrait,
                        Photographer = photo.photographer
                    };

                    await App.DatabaseService.CreatePhoto(photoEntityToCreate);
                    temp.Add(photoEntityToCreate);
                }
            }

            mainPageViewModel.PhotoPage = photoPage;
            mainPageViewModel.Photos = temp;
        }
        catch (Exception ex) { }
    }
}