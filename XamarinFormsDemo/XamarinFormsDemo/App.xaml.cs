using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinFormsDemo
{
    using Constants;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Views;
    using Microsoft.Practices.ServiceLocation;
    using Services;
    using View;
    using ViewModel;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new XamarinFormsDemo.MainPage();
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

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public ExtendedNavigationService ConfigureNavigationPages()
        {
            var nav = new ExtendedNavigationService();
            nav.Configure(AppConstants.NavigationPages.MainPage, typeof(MainPage));
            nav.Configure(AppConstants.NavigationPages.ControlTemplatePage, typeof(ControlTemplatePage));

            return nav;
        }
    }
}
