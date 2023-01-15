using Discovery.Models;
using PexelsDotNetSDK.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Discovery.ViewModels;

public sealed class MainPageViewModel : INotifyPropertyChanged
{
    private ObservableCollection<PhotoEntity> _photos = new();

    public PhotoPage? PhotoPage { get; set; }

    public ObservableCollection<PhotoEntity> Photos
    {
        get => _photos;
        set
        {
            if (value is not null)
            {
                _photos = value;

                OnPropertyChanged(nameof(Photos));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;


    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
