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
        private ObservableCollection<MyColor> infiniteColorsList;
        private ObservableCollection<MyColor> infiniteColorsList2;

        public InfiniteListViewViewModel(IMessenger messenger, INavigationService navigationService, IDialogService dialogService)
            : base(messenger, navigationService, dialogService)
        {
            this.MessengerService.Register<NavigationMessage>(this, this.MessageRecieved);
            this.InfiniteColorsList = new ObservableCollection<MyColor>();
            this.InfiniteColorsList2 = new ObservableCollection<MyColor>();
            this.FillListWithColors(this.InfiniteColorsList);
        }

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

        public ObservableCollection<MyColor> InfiniteColorsList2
        {
            get
            {
                return this.infiniteColorsList2;
            }
            set
            {
                this.infiniteColorsList2 = value;
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
                    this.FillListWithColors(this.InfiniteColorsList2);
                }
            }
        }
    }
}