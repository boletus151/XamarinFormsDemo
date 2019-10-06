namespace XFDemo.ViewModel
{
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using Xamarin.Forms;

    public class DynamicListViewScrollingViewModel : ParentViewModel
    {
        #region Private Fields

        private ObservableCollection<MyColor> colorsList;

        private ObservableCollection<MyColor> colorsList2;

        #endregion

        #region Private Methods

        private void FillListWithColors(ICollection<MyColor> list, int max = 200)
        {
            var random = new Random();
            for (var i = 0; i < max; i++)
            {
                var hexadecimalColor = random.Next(100000, 999999);
                var color = new MyColor
                {
                    HexadecimalValue = $"#{hexadecimalColor}",
                    Name = i.ToString()
                };
                list.Add(color);
            }
        }

        private void MessageRecieved(NavigationMessage navigationMessage)
        {
            var message = (LoadDataNavigationMessage)navigationMessage;
            if (message != null)
            {
                if (message.LoadData)
                {
                    this.FillListWithColors(this.ColorsList2);
                }
            }
        }

        private void ItemTappedCommandExecute()
        {
            Debug.WriteLine("TAPPED!!!!!!!!!!!!!!!");
        }

        #endregion

        #region Public Constructors

        public DynamicListViewScrollingViewModel(IMessenger messenger, INavigationService navigationService, IDialogService dialogService)
                                    : base(messenger, navigationService, dialogService)
        {
            this.MessengerService.Register<NavigationMessage>(this, this.MessageRecieved);
            this.ColorsList = new ObservableCollection<MyColor>();
            this.ColorsList2 = new ObservableCollection<MyColor>();
            this.FillListWithColors(this.ColorsList);

            this.ItemTappedCommand = new Command(this.ItemTappedCommandExecute);
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

        public ObservableCollection<MyColor> ColorsList2
        {
            get
            {
                return this.colorsList2;
            }
            set
            {
                this.colorsList2 = value;
                this.RaisePropertyChanged();
            }
        }

        public Command ItemTappedCommand { get; }

        #endregion
    }
}