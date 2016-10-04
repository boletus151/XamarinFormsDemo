namespace XamarinFormsDemo.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;
    using Model;

    public class InfiniteListViewViewModel : ParentViewModel
    {
        private ObservableCollection<MyColor> colorsList;
        private ObservableCollection<MyColor> colorsList2;

        public InfiniteListViewViewModel(IMessenger messenger, INavigationService navigationService, IDialogService dialogService)
            : base(messenger, navigationService, dialogService)
        {
            this.MessengerService.Register<NavigationMessage>(this, this.MessageRecieved);
            this.ColorsList = new ObservableCollection<MyColor>();
            this.ColorsList2 = new ObservableCollection<MyColor>();
            this.FillListWithColors(this.ColorsList);
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

        private void FillListWithColors(ICollection<MyColor> list)
        {
            var random = new Random();
            for(var i = 0; i < 500; i++)
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
            var message = (navigationMessage as LoadDataNavigationMessage);
            if(message != null)
            {
                if(message.LoadData)
                {
                    this.FillListWithColors(this.ColorsList2);
                }
            }
        }
    }
}