using Discovery.Models;
using Syncfusion.XForms.Backdrop;
using System;
using System.Collections.ObjectModel;
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

    private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var app = Application.Current;
        if (app is not null)
        {
            //    if (e.SelectedItem.ToString() == "Home")
            //    {
            //        //contentLabel.Text = "Home";
            //        navigationDrawer.ToggleDrawer();
            //        app.MainPage = new HomePage();
            //    }
            //    else if (e.SelectedItem.ToString() == "Browse")
            //    {
            //        //contentLabel.Text = "Browse";
            //        navigationDrawer.ToggleDrawer();
            //        app.MainPage = new BrowsePage();
            //    }
            //    else if (e.SelectedItem.ToString() == "Favourites")
            //    {
            //        //contentLabel.Text = "Favourite";
            //        navigationDrawer.ToggleDrawer();
            //        app.MainPage = new FavouritesPage();
            //    }

            //    navigationDrawer.ToggleDrawer();
        }
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