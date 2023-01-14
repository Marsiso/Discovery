using Xamarin.Forms;

namespace Discovery
{
    public partial class App : Application
    {
        private const string syncfusionLicence = "OTM3NjU0QDMyMzAyZTM0MmUzMGNnZGdKcVpPTkdtRWVoUEpqNWRHZG4rdXk2RzZVSFZpM2FGL2h5NXN2aTA9";

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
