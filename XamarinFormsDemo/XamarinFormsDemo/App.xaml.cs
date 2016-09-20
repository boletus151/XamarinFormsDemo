namespace XamarinFormsDemo
{
    using Constants;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Views;
    using Microsoft.Practices.ServiceLocation;
    using Services;
    using View;
    using ViewModel;
    using Xamarin.Forms;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new XamarinFormsDemo.MainPage();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnStart()
        {
            ViewModelLocator.SetLocatorProvider();
            var navPage = new NavigationPage(new MainPage());
            var nav = ServiceLocator.Current.GetInstance<INavigationService>() as ExtendedNavigationService;
            nav?.Initialize(navPage);
            this.MainPage = navPage;
        }
    }
}