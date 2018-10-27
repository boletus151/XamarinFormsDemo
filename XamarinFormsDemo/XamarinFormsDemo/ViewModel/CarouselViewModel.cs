namespace XamarinFormsDemo.ViewModel
{
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;
    using Model;
    using System.Collections.ObjectModel;

    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.</para>
    /// <para>You can also use Blend to data bind with the tool's support.</para>
    /// <para>See http://www.galasoft.ch/mvvm</para>
    /// </summary>
    public class CarouselViewModel : ParentViewModel
    {
        #region Private Fields

        private ObservableCollection<MyColor> colorsList;

        private MyColor selectedDot;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CarouselViewModel"/> class.
        /// </summary>
        /// <param name="messenger">The messenger.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="dialogService">The dialog service.</param>
        public CarouselViewModel(IMessenger messenger, INavigationService navigationService, IDialogService dialogService)
            : base(messenger, navigationService, dialogService)
        {
            var c1 = new MyColor("Pink", "#FF69B4", @"http://weknowyourdreams.com/images/pink-color/pink-color-05.jpg");
            var c2 = new MyColor("Black", "#000000", @"http://www.color-hex.com/palettes/7449.png");

            this.ColorsList = new ObservableCollection<MyColor>
            {
                c1, c2
            };
        }

        #endregion

        #region Public Properties

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

        #endregion
    }
}