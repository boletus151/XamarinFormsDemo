// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FirstViewModel.cs" company="Pernod Ricard">
//    Pernod Ricard 2017 - Fase 2.0
//  </copyright>
//  <summary>
//    The definition of  FirstViewModel.cs
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace XamarinFormsDemo.ViewModel
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Constants;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;
    using Model;
    using View;
    using Xamarin.Forms;

    public class FirstViewModel : ParentViewModel
    {
        public FirstViewModel(IMessenger messenger, INavigationService navigationService, IDialogService dialogService)
            : base(messenger, navigationService, dialogService)
        {
            this.OnAppearingCommand = new Command(async () => await this.OnAppearing());
            this.GoToCarouselViewCommand = new Command(this.GoToCarouselView);
            this.GoToDynamicListViewScrollingCommand = new Command(this.GoToInfiniteScrollingView);
            this.GoToHorizontalListViewPageCommand = new Command(this.GoToHorizontalListView);
            this.GoToPickersViewCommand = new Command(this.GoToPickersView);
            this.GoToRadioButtonPageCommand = new Command(() => this.NavigationService.NavigateTo(AppConstants.NavigationPages.RadioButtonPage));
            this.GoToToolbarWithPickerViewCommand = new Command(this.GoToToolbarWithPickerView);
        }

        public ICommand GoToCarouselViewCommand { get; }

        public ICommand GoToDynamicListViewScrollingCommand { get; }

        public ICommand GoToHorizontalListViewPageCommand { get; }

        public ICommand GoToPickersViewCommand { get; }

        public ICommand GoToRadioButtonPageCommand { get; }

        public ICommand GoToToolbarWithPickerViewCommand { get; }

        public ICommand OnAppearingCommand { get; }

        private void GoToCarouselView() => this.NavigationService.NavigateTo(AppConstants.NavigationPages.CarouselPage);

        private void GoToHorizontalListView()
        {
            var message = new LoadDataNavigationMessage(this.GetType().Name, typeof(DynamicListViewScrollingViewModel).Name, true);
            this.MessengerService.Send<NavigationMessage, DynamicListViewScrollingViewModel>(message);

            this.NavigationService.NavigateTo(AppConstants.NavigationPages.HorizontalListViewPage);
        }

        private void GoToInfiniteScrollingView()
        {
            var message = new LoadDataNavigationMessage(this.GetType().Name, typeof(DynamicListViewScrollingViewModel).Name, true);
            this.MessengerService.Send<NavigationMessage, DynamicListViewScrollingViewModel>(message);

            this.NavigationService.NavigateTo(AppConstants.NavigationPages.InfiniteScrollingPage);
        }

        private void GoToPickersView()
        {
            var message = new LoadDataNavigationMessage(this.GetType().Name, nameof(PickersViewModel), true);
            this.MessengerService.Send<NavigationMessage, PickersViewModel>(message);
            this.NavigationService.NavigateTo(AppConstants.NavigationPages.ObjectBindablePickerPage);
        }

        private void GoToToolbarWithPickerView() => this.NavigationService.NavigateTo(AppConstants.NavigationPages.ToolbarWithPickerPage);

        private async Task OnAppearing()
        {
            Debug.WriteLine("¡¡¡¡¡¡ OnAppearing");
            await Task.Delay(1);

            // await this.DialogService.ShowMessage("OnAppearing", string.Empty);
        }
    }
}