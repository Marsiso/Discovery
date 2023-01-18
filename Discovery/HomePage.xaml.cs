using Discovery.Models;
using Syncfusion.XForms.Backdrop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace Discovery;

[DesignTimeVisible(false)]
public partial class HomePage : SfBackdropPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var app = Application.Current;
        if (app is not null)
        {
            if (e.SelectedItem.ToString() == "Home")
            {
                app.MainPage = new NavigationPage(new HomePage());
            }
            else if (e.SelectedItem.ToString() == "Browse")
            {
                app.MainPage = new NavigationPage(new BrowsePage());
            }
            else if (e.SelectedItem.ToString() == "Favourites")
            {
                app.MainPage = new NavigationPage(new FavouritesPage());
            }
        }
    }

    protected override async void OnAppearing()
    {
        var photoEntities = await App.DatabaseService.GetAllPhotos();
        var temp = photoEntities?.Count > 0
            ? new List<PhotoEntity>(photoEntities.Where(x => x.Category == "Curated"))
            : new List<PhotoEntity>();

        try
        {
            base.OnAppearing();

            var photoPage = await App.CarouselService.GetCuratedPhotos();
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
                        Photographer = photo.photographer,
                        Category = "Curated"
                    };

                    await App.DatabaseService.CreatePhoto(photoEntityToCreate);
                    temp.Add(photoEntityToCreate);
                }
            }

            homePageViewModel.PhotoPage = photoPage;
            homePageViewModel.Photos = temp;
        }
        catch (Exception) { }

        homePageViewModel.Photos = temp;
    }
}