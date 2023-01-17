using Discovery.Models;
using PexelsDotNetSDK.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Discovery.ViewModels;

public sealed class BrowsePageViewModel : INotifyPropertyChanged
{
    private IList<PhotoEntity> photos = new List<PhotoEntity>();
    private PhotoEntity? TappedItem;
    private string? searchTerm = "nature";

    public string? SearchTerm
    {
        get => searchTerm;
        set
        {
            if (searchTerm != value)
            {
                searchTerm = value;

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

    public Command<object> AddToFavouritesCommand { get; set; }
    public Command<object> AddToBlacklistCommand { get; set; }
    public Command<object> DownloadCommand { get; set; }

    public BrowsePageViewModel()
    {
        AddToFavouritesCommand = new Command<object>(AddToFavouritesCommandMethod);
        AddToBlacklistCommand = new Command<object>(AddToBlacklistCommandMethod);
        DownloadCommand = new Command<object>(DownloadCommandMethod);
    }

    private async void AddToFavouritesCommandMethod(object obj)
    {
        var photo = obj as PhotoEntity;
        if (photo is not null)
        {
            photo.IsVisible = false;
            //await App.DatabaseService.UpdatePhoto(photo);
            Photos = Photos.Where(x => x.Id != photo.Id).ToList();
        }
    }

    private async void AddToBlacklistCommandMethod(object obj)
    {
        var photo = obj as PhotoEntity;
        if (photo is not null)
        {
            photo.IsVisible = false;
            //await App.DatabaseService.UpdatePhoto(photo);
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
