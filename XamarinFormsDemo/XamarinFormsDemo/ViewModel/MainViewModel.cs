// -------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="CodigoEdulis">
//    Código Edulis 2017
//    http://www.codigoedulis.es
//  </copyright>
//  <summary>
//     This implementation is a group of the offers of several persons along the network;
//     because of this, it is under Creative Common By License:
//     
//     You are free to:
// 
//     Share — copy and redistribute the material in any medium or format
//     Adapt — remix, transform, and build upon the material for any purpose, even commercially.
//     
//     The licensor cannot revoke these freedoms as long as you follow the license terms.
//     
//     Under the following terms:
//     
//     Attribution — You must give appropriate credit, provide a link to the license, and indicate if changes were made. You may do so in any reasonable manner, but not in any way that suggests the licensor endorses you or your use.
//     No additional restrictions — You may not apply legal terms or technological measures that legally restrict others from doing anything the license permits.
//  
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace XamarinFormsDemo.ViewModel
{
    using Constants;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;
    using Model;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class MainViewModel : ParentViewModel
    {
        private ObservableCollection<MyColor> colorsList;

        private ICommand goToCarouselViewCommand;

        private ICommand goToControlTemplatePageCommand;

        private ICommand goToDynamicListViewScrollingCommand;

        private ICommand goToPickersViewCommand;

        private ICommand goToToolbarWithPickerViewCommand;

        private ICommand goToRadioButtonPageCommand;

        private ICommand onAppearingCommand;

        private MyColor selectedColor;

        private MyColor selectedDot;

        private string selectedValue;

        public MainViewModel(IMessenger messenger, INavigationService navigationService, IDialogService dialogService)
            : base(messenger, navigationService, dialogService)
        {
            var c1 = new MyColor("Pink", "#FF69B4", @"http://weknowyourdreams.com/images/pink-color/pink-color-05.jpg");
            var c2 = new MyColor("Black", "#000000", @"http://www.color-hex.com/palettes/7449.png");

            this.ColorsList = new ObservableCollection<MyColor>
            {
                c1, c2
            };
            this.SelectedColor = this.ColorsList.First();
        }

        public ObservableCollection<MyColor> ColorsList
        {
            get
            {
                return this.colorsList;
            }

            set
            {
                this.colorsList = value;
                this.RaisePropertyChanged();
            }
        }

        public ICommand GoToCarouselViewCommand => this.goToCarouselViewCommand ?? (this.goToCarouselViewCommand = new RelayCommand(this.GoToCarouselView));

        public ICommand GoToControlTemplatePageCommand => this.goToControlTemplatePageCommand ?? (this.goToControlTemplatePageCommand = new RelayCommand(this.GoToControlTemplatePage));

        public ICommand GoToDynamicListViewScrollingCommand => this.goToDynamicListViewScrollingCommand ?? (this.goToDynamicListViewScrollingCommand = new RelayCommand(this.GoToInfiniteScrollingView));

        public ICommand GoToPickersViewCommand => this.goToPickersViewCommand ?? (this.goToPickersViewCommand = new RelayCommand(this.GoToPickersView));

        public ICommand GoToToolbarWithPickerViewCommand => this.goToToolbarWithPickerViewCommand ?? (this.goToToolbarWithPickerViewCommand = new RelayCommand(this.GoToToolbarWithPickerView));

        public ICommand GoToRadioButtonPageCommand => this.goToRadioButtonPageCommand ?? (this.goToRadioButtonPageCommand = new RelayCommand(()=>this.NavigationService.NavigateTo(AppConstants.NavigationPages.RadioButtonPage)));

        public ICommand OnAppearingCommand => this.onAppearingCommand ?? (this.onAppearingCommand = new RelayCommand(async () => await this.OnAppearing()));

        public MyColor SelectedColor
        {
            get
            {
                return this.selectedColor;
            }

            set
            {
                this.selectedColor = value;
                this.RaisePropertyChanged();
            }
        }

        public MyColor SelectedDot
        {
            get
            {
                return this.selectedDot;
            }

            set
            {
                this.selectedDot = value;
                this.RaisePropertyChanged();
            }
        }

        public string SelectedValue
        {
            get
            {
                return this.selectedValue;
            }

            set
            {
                this.selectedValue = value;
                this.RaisePropertyChanged();
            }
        }

        private void GoToCarouselView() => this.NavigationService.NavigateTo(AppConstants.NavigationPages.CarouselPage);

        private void GoToControlTemplatePage() => this.NavigationService.NavigateTo(AppConstants.NavigationPages.ControlTemplatePage);

        private void GoToInfiniteScrollingView()
        {
            var message = new LoadDataNavigationMessage(this.GetType().Name, typeof(MainViewModel).Name, true);
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