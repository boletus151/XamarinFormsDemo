using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinFormsDemo
{
    using Constants;
    using GalaSoft.MvvmLight.Views;
    using Microsoft.Practices.ServiceLocation;
    using Services;
    using ViewModel;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new XamarinFormsDemo.MainPage();
        }

        protected override void OnStart()
        {
            this.ConfigureNavigation();
        }

        private void ConfigureNavigation()
        {
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>() as ExtendedNavigationService;
            //navigationService?.NavigateTo(AppConstants.NavigationPages.MainPage);
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
