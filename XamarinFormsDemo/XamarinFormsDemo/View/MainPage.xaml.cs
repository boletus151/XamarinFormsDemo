using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFDemo.ViewModel;

namespace XFDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var vm = ServiceLocator.Current.GetInstance<FirstViewModel>();
            vm?.OnAppearingCommand.Execute(null);
        }
    }
}
