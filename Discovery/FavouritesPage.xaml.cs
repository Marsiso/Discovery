using Syncfusion.XForms.Backdrop;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Discovery;

[XamlCompilation(XamlCompilationOptions.Compile)]
[DesignTimeVisible(false)]
public partial class FavouritesPage : SfBackdropPage
{
    public FavouritesPage()
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
        base.OnAppearing();

        await favouritesPageViewModel.GetFavouritePhotos();
    }

    private void SfCardLayout_CardTapped(object sender, TappedEventArgs e)
    {
        if (popupLayout is not null)
        {
            popupLayout.IsOpen = true;
        }
    }
}