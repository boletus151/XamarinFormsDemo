namespace XamarinFormsDemo.ViewModel
{
    using System;
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

        private ICommand goToControlTemplatePageCommand;

        private ICommand goToInfiniteScrollingViewCommand;

        private ObservableCollection<MyColor> infiniteColorsList;

        private ICommand onAppearingCommand;

        private MyColor selectedColor;

        private string selectedValue;

        private ICommand tryDebugCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="messenger">The messenger.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="dialogService">The dialog service.</param>
        public MainViewModel(IMessenger messenger, INavigationService navigationService, IDialogService dialogService)
            : base(messenger, navigationService, dialogService)
        {
            var c1 = new MyColor("White", "#FFFFFF");
            var c2 = new MyColor("Black", "#000000");

            this.ColorsList = new ObservableCollection<MyColor>
            {
                c1,
                c2
            };
            this.SelectedColor = this.ColorsList.First();

            this.InfiniteColorsList = new ObservableCollection<MyColor>();
            var random = new Random();
            for(var i = 0; i < 500; i++)
            {
                var hexadecimalColor = random.Next(100000, 999999);
                var color = new MyColor
                {
                    HexadecimalValue = $"#{hexadecimalColor}",
                    Name = i.ToString()
                };
                InfiniteColorsList.Add(color);
            }
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

        public ICommand GoToControlTemplatePageCommand
            => this.goToControlTemplatePageCommand ?? (this.goToControlTemplatePageCommand = new RelayCommand(this.GoToControlTemplatePage));

        public ICommand GoToInfiniteScrollingViewCommand
            => this.goToInfiniteScrollingViewCommand ?? (this.goToInfiniteScrollingViewCommand = new RelayCommand(this.GoToInfiniteScrollingView));

        public ObservableCollection<MyColor> InfiniteColorsList
        {
            get
            {
                return this.infiniteColorsList;
            }
            set
            {
                this.infiniteColorsList = value;
                this.RaisePropertyChanged();
            }
        }

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

        public ICommand TryDebugCommand => this.tryDebugCommand ?? (this.tryDebugCommand = new RelayCommand(this.TryDebug));

        private void GoToControlTemplatePage()
        {
            this.NavigationService.NavigateTo(AppConstants.NavigationPages.ControlTemplatePage);
        }

        private void GoToInfiniteScrollingView()
        {
            this.NavigationService.NavigateTo(AppConstants.NavigationPages.InfiniteScrollingPage);
        }

#pragma warning disable 1998
        private async Task OnAppearing()
#pragma warning restore 1998
        {
            Debug.WriteLine($"　　　 OnAppearing");

            //await this.DialogService.ShowMessage("OnAppearing", string.Empty);
        }

        private void TryDebug()
        {
            Debug.WriteLine($"　　　The Selected Item is: {this.SelectedColor}");
        }
    }
}