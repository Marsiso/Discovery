using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Discovery
{
    public partial class MainPage : ContentPage
    {
        Label label1;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ParallaxViewModel();
            hamburgerButton.Image = (FileImageSource)ImageSource.FromFile("burgericon.png");

            List<string> list = new List<string>
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
            if (e.SelectedItem.ToString() == "Home")
            {
                label1.Text = "Home";
            }
            else if (e.SelectedItem.ToString() == "Browse")
            {
                label1.Text = "Browse";
            }
            else if (e.SelectedItem.ToString() == "Favourite")
            {
                label1.Text = "Favourite";
            }

            navigationDrawer.ToggleDrawer();
        }
    }

    public class ParallaxViewModel
    {
        public ImageSource Image { get; set; }

        public ObservableCollection<Contacts> Items { get; set; }
        public ParallaxViewModel()
        {
            Items = new ObservableCollection<Contacts>()
            {
                new Contacts() { Name = "Thriller", Author = "Michael Jackson" },
                new Contacts() { Name = "Like a Prayer", Author = "Madonna" },
                new Contacts() { Name = "When Doves Cry", Author = "Prince" },
                new Contacts() { Name = "I Wanna Dance", Author = "Whitney Houston" },
                new Contacts() { Name = "It’s Gonna Be Me", Author = "N Sync"},
                new Contacts() { Name = "Everybody", Author = "Backstreet Boys"},
                new Contacts() { Name = "Rolling in the Deep", Author = "Adele" },
                new Contacts() { Name = "Don’t Stop Believing", Author = "Journey" },
                new Contacts() { Name = "Billie Jean", Author = "Michael Jackson" },
                new Contacts() { Name = "Firework", Author = "Katy Perry"},
                new Contacts() { Name = "Thriller", Author = "Michael Jackson" },
                new Contacts() { Name = "Like a Prayer", Author = "Madonna" },
                new Contacts() { Name = "When Doves Cry", Author = "Prince" },
                new Contacts() { Name = "I Wanna Dance", Author = "Whitney Houston" },
                new Contacts() { Name = "It’s Gonna Be Me", Author = "N Sync" },
                new Contacts() { Name = "Everybody", Author = "Backstreet Boys" },
                new Contacts() { Name = "Rolling in the Deep", Author = "Adele" },
                new Contacts() { Name = "Don’t Stop Believing", Author = "Journey"},
            };
        }
    }

    public class Contacts
    {
        public string Name
        {
            get;
            set;
        }
        public string Author
        {
            get;
            set;
        }
    }
}
