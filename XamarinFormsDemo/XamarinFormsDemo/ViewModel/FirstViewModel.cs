// -------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="CodigoEdulis">
//     Código Edulis 2017 http://www.codigoedulis.es
// </copyright>
// <summary>
// This implementation is a group of the offers of several persons along the network; because of
// this, it is under Creative Common By License:
//
// You are free to:
//
// Share — copy and redistribute the material in any medium or format Adapt — remix, transform, and
// build upon the material for any purpose, even commercially.
//
// The licensor cannot revoke these freedoms as long as you follow the license terms.
//
// Under the following terms:
//
// Attribution — You must give appropriate credit, provide a link to the license, and indicate if
// changes were made. You may do so in any reasonable manner, but not in any way that suggests the
// licensor endorses you or your use. No additional restrictions — You may not apply legal terms or
// technological measures that legally restrict others from doing anything the license permits.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XamarinFormsDemo.ViewModel
{
    using Constants;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;
    using Model;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class FirstViewModel : ParentViewModel
    {
        #region Private Methods

        private void GoToCarouselPage() => this.NavigationService.NavigateTo(AppConstants.NavigationPages.CarouselPage);

        private void GoToHorizontalListViewPage()
        {
            var message = new LoadDataNavigationMessage(this.GetType().Name, typeof(DynamicListViewScrollingViewModel).Name, true);
            this.MessengerService.Send<NavigationMessage, DynamicListViewScrollingViewModel>(message);

            this.NavigationService.NavigateTo(AppConstants.NavigationPages.HorizontalListViewPage);
        }

        private void GoToDynamicListViewScrollingPage()
        {
            var message = new LoadDataNavigationMessage(this.GetType().Name, typeof(DynamicListViewScrollingViewModel).Name, true);
            this.MessengerService.Send<NavigationMessage, DynamicListViewScrollingViewModel>(message);

            this.NavigationService.NavigateTo(AppConstants.NavigationPages.DynamicListViewScrollingPage);
        }

        private void GoToPickersPage()
        {
            var message = new LoadDataNavigationMessage(this.GetType().Name, nameof(PickersViewModel), true);
            this.MessengerService.Send<NavigationMessage, PickersViewModel>(message);
            this.NavigationService.NavigateTo(AppConstants.NavigationPages.ObjectBindablePickerPage);
        }

        private void GoToToolbarWithPickerPage() => this.NavigationService.NavigateTo(AppConstants.NavigationPages.ToolbarWithPickerPage);

        private async Task OnAppearing()
        {
            Debug.WriteLine("¡¡¡¡¡¡ OnAppearing");
            await Task.Delay(1);

            // await this.DialogService.ShowMessage("OnAppearing", string.Empty);
        }

        #endregion

        #region Public Constructors

        public FirstViewModel(IMessenger messenger, INavigationService navigationService, IDialogService dialogService)
                                                            : base(messenger, navigationService, dialogService)
        {
            this.OnAppearingCommand = new Command(async () => await this.OnAppearing());
            this.GoToCarouselPageCommand = new Command(this.GoToCarouselPage);
            this.GoToDynamicListViewScrollingPageCommand = new Command(this.GoToDynamicListViewScrollingPage);
            this.GoToHorizontalListViewPageCommand = new Command(this.GoToHorizontalListViewPage);
            this.GoToPickersPageCommand = new Command(this.GoToPickersPage);
            this.GoToRadioButtonPageCommand = new Command(() => this.NavigationService.NavigateTo(AppConstants.NavigationPages.RadioButtonPage));
            this.GoToToolbarWithPickerPageCommand = new Command(this.GoToToolbarWithPickerPage);
            this.GoToRegexPageCommand = new Command(() => this.NavigationService.NavigateTo(AppConstants.NavigationPages.RegexPage));
            this.GoToReverseStringPageCommand = new Command(() => this.NavigationService.NavigateTo(AppConstants.NavigationPages.ReverseStringPage));
        }

        #endregion

        #region Public Properties

        public ICommand GoToCarouselPageCommand { get; }

        public ICommand GoToDynamicListViewScrollingPageCommand { get; }

        public ICommand GoToHorizontalListViewPageCommand { get; }

        public ICommand GoToPickersPageCommand { get; }

        public ICommand GoToRadioButtonPageCommand { get; }

        public ICommand GoToToolbarWithPickerPageCommand { get; }

        public ICommand GoToRegexPageCommand { get; }

        public ICommand GoToReverseStringPageCommand { get; }

        public ICommand OnAppearingCommand { get; }

        #endregion
    }
}