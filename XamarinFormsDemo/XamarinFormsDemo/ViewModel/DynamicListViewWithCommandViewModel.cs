namespace XFDemo.ViewModel
{
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;
    using Model;
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using Xamarin.Forms;

    public class DynamicListViewWithCommandViewModel : ParentViewModel
    {
        #region Private Fields

        private ObservableCollection<MyColor> colorsList;

        #endregion

        #region Private Methods

        private void GetMoreItemsCommandExecute()
        {
            var random = new Random();
            var max = 16;
            for (var i = 0; i < max; i++)
            {
                var hexadecimalColor = random.Next(0, 200);
                var color = new MyColor
                {
                    HexadecimalValue = $"#{hexadecimalColor}",
                    Name = i.ToString()
                };
                this.colorsList.Add(color);
            }
        }

        private void ItemTappedCommandExecute()
        {
            Debug.WriteLine("TAPPED!!!!!!!!!!!!!!!");
        }

        #endregion

        #region Public Constructors

        public DynamicListViewWithCommandViewModel(IMessenger messenger, INavigationService navigationService, IDialogService dialogService)
                                    : base(messenger, navigationService, dialogService)
        {;
            this.ColorsList = new ObservableCollection<MyColor>();
            this.GetMoreItemsCommandExecute();

            this.ItemTappedCommand = new Command(this.ItemTappedCommandExecute);
            this.GetMoreElementsCommand = new Command(this.GetMoreItemsCommandExecute);
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

        public Command ItemTappedCommand { get; }

        public Command GetMoreElementsCommand { get; }

        #endregion
    }
}