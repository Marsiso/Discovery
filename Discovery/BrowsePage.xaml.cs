using Discovery.Models;
using Syncfusion.XForms.Backdrop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Discovery;

[XamlCompilation(XamlCompilationOptions.Compile)]
[DesignTimeVisible(false)]
public partial class BrowsePage : SfBackdropPage
{
    public BrowsePage()
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
            }
            else if (e.SelectedItem.ToString() == "Favourites")
            {
                app.MainPage = new NavigationPage(new FavouritesPage());
            }
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
                ? new List<PhotoEntity>(photoEntities)
                : new List<PhotoEntity>();

            browsePageViewModel.PhotoPage = photoPage;
            browsePageViewModel.Photos = temp;
        }
        catch (Exception ex) { }
    }
}