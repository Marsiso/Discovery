using Discovery.Models;
using PexelsDotNetSDK.Models;
using System.Collections.Generic;
using System.ComponentModel;
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

    public Command<object> TapGestureCommand { get; set; }

    public BrowsePageViewModel()
    {
        TapGestureCommand = new Command<object>(TappedGestureCommandMethod);
    }

    private void TappedGestureCommandMethod(object obj)
    {
        var tappedItemData = obj as PhotoEntity;
        if (tappedItemData is null)
        {
            return;
        }

        if (TappedItem is not null && TappedItem.IsVisible)
        {
            TappedItem.IsVisible = false;
        }

        if (TappedItem == tappedItemData)
        {
            TappedItem = null;

            return;
        }

        TappedItem = tappedItemData;
        TappedItem.IsVisible = true;
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
