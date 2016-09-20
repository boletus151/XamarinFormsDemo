namespace XamarinFormsDemo.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;

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
    public class ParentViewModel : ViewModelBase
    {
        protected IDialogService DialogService;

        protected INavigationService NavigationService;

        protected IMessenger MessengerService;

        public ParentViewModel(IMessenger messenger, INavigationService navigationService, IDialogService dialogService)
            : base(messenger)
        {
            this.NavigationService = navigationService;
            this.DialogService = dialogService;
            this.MessengerService = messenger;
        }
    }
}