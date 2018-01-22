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
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;
    using Model;
    using Xamarin.Forms;

    public class HorizontalListViewModel : ParentViewModel
    {
        private ObservableCollection<MyColor> colorsList;

        private readonly List<MyColor> colorsListCopy;

        private string filterText;

        public HorizontalListViewModel(IMessenger messenger, INavigationService navigationService, IDialogService dialogService)
            : base(messenger, navigationService, dialogService)
        {
            this.MessengerService.Register<NavigationMessage>(this, this.MessageRecieved);
            this.ColorsList = new ObservableCollection<MyColor>();
            var list = this.GetColorList(12);
            this.FillListWithColors(list);
            this.colorsListCopy = new List<MyColor>(this.ColorsList);
            this.FilterByTextCommand = new Command(this.FiterByTextCommandExecute);
        }

        private void FillListWithColors(List<MyColor> list)
        {
            foreach (var color in list)
            {
                this.ColorsList.Add(color);
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

        private List<MyColor> GetColorList(int max = 200)
        {
            var random = new Random();
            var list = new List<MyColor>();
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
            return list.OrderBy(e => e.HexadecimalValue, new HexadecimalStringComarator()).ToList();
        }

        private class HexadecimalStringComarator : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                var numX = x?.Replace("#", "");
                var numY = y?.Replace("#", "");
                if (numY != null && numX != null)
                {
                    var intX = int.Parse(numX);
                    var intY = int.Parse(numY);
                    if(intX == intY)
                    {
                        return 0;
                    }
                    return intX > intY ? 1 : -1;
                }
                return -1;
            }
        }

        private void FiterByTextCommandExecute()
        {
            this.ColorsList.Clear();
            if (string.IsNullOrEmpty(this.FilterText))
            {
                this.FillListWithColors(this.colorsListCopy);
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