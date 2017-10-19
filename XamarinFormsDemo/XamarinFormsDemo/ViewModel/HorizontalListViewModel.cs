// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HorizontalListViewModel.cs" company="Pernod Ricard">
//    Pernod Ricard 2017 - Fase 2.0
//  </copyright>
//  <summary>
//    The definition of  HorizontalListViewModel.cs
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace XamarinFormsDemo.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;
    using Model;
    using Xamarin.Forms;

    public class HorizontalListViewModel : ParentViewModel
    {
        private ObservableCollection<MyColor> colorsList;

        private ObservableCollection<MyColor> colorsListCopy;

        private string filterText;

        public HorizontalListViewModel(IMessenger messenger, INavigationService navigationService, IDialogService dialogService)
            : base(messenger, navigationService, dialogService)
        {
            this.MessengerService.Register<NavigationMessage>(this, this.MessageRecieved);
            this.ColorsList = new ObservableCollection<MyColor>();
            this.FillListWithColors(20);
            this.colorsListCopy = new ObservableCollection<MyColor>(this.ColorsList);
            this.FilterByTextCommand = new Command(this.FiterByTextCommandExecute);
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

        public Command FilterByTextCommand { get; }

        public string FilterText
        {
            get
            {
                return this.filterText;
            }
            set
            {
                this.filterText = value;
                this.RaisePropertyChanged();
            }
        }

        private void FillListWithColors(int max = 200)
        {
            var random = new Random();
            for(var i = 0; i < max; i++)
            {
                var hexadecimalColor = random.Next(100000, 999999);
                var color = new MyColor
                {
                    HexadecimalValue = $"#{hexadecimalColor}", Name = i.ToString()
                };
                this.ColorsList.Add(color);
            }
        }

        private void FiterByTextCommandExecute()
        {
            this.ColorsList.Clear();
            if (string.IsNullOrEmpty(this.FilterText))
            {
                this.FillListWithColors(20);
            }
            else
            {
                var filtered = this.colorsListCopy.Where(e => e.HexadecimalValue.Contains(this.FilterText));
                foreach (var color in filtered)
                {
                    this.ColorsList.Add(color);
                }
            }
        }

        private void MessageRecieved(NavigationMessage navigationMessage)
        {
        }
    }
}