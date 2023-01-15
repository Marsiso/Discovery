using Discovery.Services;
using System;
using System.IO;
using Xamarin.Forms;

namespace Discovery
{
    public partial class App : Application
    {
        private const string syncfusionLicence = "OTM3NjU0QDMyMzAyZTM0MmUzMGNnZGdKcVpPTkdtRWVoUEpqNWRHZG4rdXk2RzZVSFZpM2FGL2h5NXN2aTA9";
        private static DatabaseService databaseService = default!;
        private static CarouselService carouselService = default!;

        public static DatabaseService DatabaseService
        {
            get => databaseService ??= new DatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PhotoStore.db3"));
        }

        public static CarouselService CarouselService
        {
            get => carouselService ??= new CarouselService();
        }

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncfusionLicence);
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
