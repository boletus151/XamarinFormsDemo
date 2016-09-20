namespace XamarinFormsDemo
{
    using Constants;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Views;
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

        public ExtendedNavigationService ConfigureNavigationPages()
        {
            var nav = new ExtendedNavigationService();
            nav.Configure(AppConstants.NavigationPages.MainPage, typeof(MainPage));
            nav.Configure(AppConstants.NavigationPages.ControlTemplatePage, typeof(ControlTemplatePage));
            nav.Configure(AppConstants.NavigationPages.InfiniteScrollingPage, typeof(InfiniteScrollingPage));

            return nav;
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
            var nav = this.ConfigureNavigationPages();
            SimpleIoc.Default.Register<INavigationService>(() => nav);

            var navPage = new NavigationPage(new MainPage());
            nav.Initialize(navPage);
            this.MainPage = navPage;
        }
    }
}