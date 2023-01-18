using Discovery.Models;
using PexelsDotNetSDK.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Discovery.ViewModels;

public sealed class FavouritesPageViewModel : INotifyPropertyChanged
{
    private IList<PhotoEntity> photos = new List<PhotoEntity>();
    private string? searchTerm;

    public string? SearchTerm
    {
        get => searchTerm;
        set
        {
            if (searchTerm != value)
            {
                searchTerm = value;
                _ = GetFavouritePhotos(value);

                OnPropertyChanged(nameof(SearchTerm));
            }
        }
    }


    public PhotoPage? PhotoPage { get; set; }

    public IList<PhotoEntity> Photos
    {
        get => photos;
        set
        {
            if (value is not null)
            {
                photos = value;

                OnPropertyChanged(nameof(Photos));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public Command<object> RemoveFromFavouritesCommand { get; set; }
    public Command<object> AddToBlacklistCommand { get; set; }
    public Command<object> DownloadCommand { get; set; }

    public FavouritesPageViewModel()
    {
        RemoveFromFavouritesCommand = new Command<object>(RemoveFromFavouritesCommandMethod);
        AddToBlacklistCommand = new Command<object>(AddToBlacklistCommandMethod);
        DownloadCommand = new Command<object>(DownloadCommandMethod);
    }

    public async Task GetFavouritePhotos(string? searchTerm = default!)
    {
        var photoEntities = await App.DatabaseService.GetAllPhotos();
        List<PhotoEntity>? temp;
        if (string.IsNullOrEmpty(searchTerm))
        {
            temp = new List<PhotoEntity>(photoEntities.Where(photo => photo.IsFavourite is true));
        }
        else
        {
            searchTerm = searchTerm!.ToLower();
            temp = new List<PhotoEntity>(photoEntities.Where(photo => photo.IsFavourite is true
                && photo.Photographer.ToLower().Contains(searchTerm)
                && photo.Alt.ToLower().Contains(searchTerm)));
        }

        Photos = temp;
    }

    private async void RemoveFromFavouritesCommandMethod(object obj)
    {
        var photo = obj as PhotoEntity;
        if (photo is not null)
        {
            photo.IsVisible = true;
            photo.IsFavourite = false;
            photo.IsBlackListed = false;
            await App.DatabaseService.UpdatePhoto(photo);
            Photos = Photos.Where(x => x.Id != photo.Id).ToList();
        }
    }

    private async void AddToBlacklistCommandMethod(object obj)
    {
        var photo = obj as PhotoEntity;
        if (photo is not null)
        {
            photo.IsVisible = false;
            photo.IsFavourite = false;
            photo.IsBlackListed = true;
            await App.DatabaseService.UpdatePhoto(photo);
            Photos = Photos.Where(x => x.Id != photo.Id).ToList();
        }
    }

    private void DownloadCommandMethod(object obj)
    {
        var photo = obj as PhotoEntity;
        if (photo is not null)
        {
            GetPhotoFromUrl(photo.Id.ToString(), photo.Url);
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void GetPhotoFromUrl(string photoName, string url)
    {
        using var webClient = new WebClient();
        var imageBytes = webClient.DownloadData(url);
        if (imageBytes is not null && imageBytes.Length > 0)
        {
            SavePhoto(photoName, imageBytes, "Photos");
        }
    }

    private void SavePhoto(string photoName, byte[] data, string location = "temp")
    {
        var downloadsPath = Android.App.Application.Context.GetExternalFilesDir(string.Empty).AbsolutePath;
        if (Path.HasExtension(photoName) is false)
        {
            photoName += ".jpeg";
        }

        var filePath = Path.Combine(downloadsPath, photoName);

        File.WriteAllBytes(filePath, data);
    }
}
