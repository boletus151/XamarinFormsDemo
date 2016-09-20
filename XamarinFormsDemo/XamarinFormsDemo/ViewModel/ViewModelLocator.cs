/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:XamarinFormsDemo"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

namespace XamarinFormsDemo.ViewModel
{
    using Constants;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;
    using Microsoft.Practices.ServiceLocation;
    using Services;
    using View;

    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        ///     Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            SetLocatorProvider();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public InfiniteListViewViewModel InfiniteList => ServiceLocator.Current.GetInstance<InfiniteListViewViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        public static void SetLocatorProvider()
        {
            if (!ServiceLocator.IsLocationProviderSet)
            {
                ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
                RegisterInIocContainer();
            }

        }

        public static ExtendedNavigationService ConfigureNavigationPages()
        {
            var nav = new ExtendedNavigationService();

            nav.Configure(AppConstants.NavigationPages.MainPage, typeof(MainPage));
            nav.Configure(AppConstants.NavigationPages.ControlTemplatePage, typeof(ControlTemplatePage));
            nav.Configure(AppConstants.NavigationPages.InfiniteScrollingPage, typeof(InfiniteScrollingPage));

            return nav;
        }

        public static void RegisterInIocContainer()
        {
            SimpleIoc.Default.Register<IMessenger, Messenger>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            var nav = ConfigureNavigationPages();
            SimpleIoc.Default.Register<INavigationService>(()=>nav);

            SimpleIoc.Default.Register<ParentViewModel>(true);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<InfiniteListViewViewModel>(true);
        }
    }
}