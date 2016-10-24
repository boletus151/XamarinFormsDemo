namespace XamarinFormsDemo.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Constants;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;
    using Model;

    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    ///     </para>
    ///     <para>
    ///         You can also use Blend to data bind with the tool's support.
    ///     </para>
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MainViewModel : ParentViewModel
    {
        private ObservableCollection<MyColor> colorsList;

        private ICommand goToCarouselViewCommand;

        private ICommand goToControlTemplatePageCommand;

        private ICommand goToDynamicListViewScrollingCommand;

        private ICommand goToObjectBindablePickerViewCommand;

        private ICommand goToToolbarWithPickerViewCommand;

        private ICommand onAppearingCommand;

        private MyColor selectedColor;

        private MyColor selectedDot;

        private string selectedValue;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        /// <param name="messenger">The messenger.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="dialogService">The dialog service.</param>
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

        public ICommand GoToCarouselViewCommand
            => this.goToCarouselViewCommand ?? (this.goToCarouselViewCommand = new RelayCommand(this.GoToCarouselView));

        public ICommand GoToControlTemplatePageCommand
            => this.goToControlTemplatePageCommand ?? (this.goToControlTemplatePageCommand = new RelayCommand(this.GoToControlTemplatePage));

        public ICommand GoToDynamicListViewScrollingCommand
            => this.goToDynamicListViewScrollingCommand ?? (this.goToDynamicListViewScrollingCommand = new RelayCommand(this.GoToInfiniteScrollingView));

        public object GoToObjectBindablePickerViewCommand
            =>
                this.goToObjectBindablePickerViewCommand ??
                    (this.goToObjectBindablePickerViewCommand = new RelayCommand(this.GoToObjectBindablePickerView));

        public ICommand GoToToolbarWithPickerViewCommand
            => this.goToToolbarWithPickerViewCommand ?? (this.goToToolbarWithPickerViewCommand = new RelayCommand(this.GoToToolbarWithPickerView));

        public ICommand OnAppearingCommand
            => this.onAppearingCommand ?? (this.onAppearingCommand = new RelayCommand(async () => await this.OnAppearing()));

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
            MessengerService.Send<NavigationMessage, DynamicListViewScrollingViewModel>(message);

            this.NavigationService.NavigateTo(AppConstants.NavigationPages.InfiniteScrollingPage);
        }

        private void GoToObjectBindablePickerView() => this.NavigationService.NavigateTo(AppConstants.NavigationPages.ObjectBindablePickerPage);

        private void GoToToolbarWithPickerView() => this.NavigationService.NavigateTo(AppConstants.NavigationPages.ToolbarWithPickerPage);

        private async Task OnAppearing()
        {
            Debug.WriteLine("¡¡¡¡¡¡ OnAppearing");
            await Task.Delay(1);
            //await this.DialogService.ShowMessage("OnAppearing", string.Empty);
        }
    }
}