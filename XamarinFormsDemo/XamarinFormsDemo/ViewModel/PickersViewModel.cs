// -------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectBindablePicker" company="CodigoEdulis">
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
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;

    using XamarinFormsDemo.Model;

    public class PickersViewModel : ParentViewModel
    {
        private ObservableCollection<MyColor> colorsList;

        private ObservableCollection<MyColor> colorsListAsync;

        private MyColor selectedColor;

        private MyColor selectedColorAsync;

        private string selectedValue;

        public PickersViewModel(IMessenger messenger, INavigationService navigationService, IDialogService dialogService)
            : base(messenger, navigationService, dialogService)
        {
            this.MessengerService.Register<NavigationMessage>(this, async message => await this.OnNavigationMessageReceivedAsync(message));

            var c1 = new MyColor("Pink", "#FF69B4", @"http://weknowyourdreams.com/images/pink-color/pink-color-05.jpg");
            var c2 = new MyColor("Black", "#000000", @"http://www.color-hex.com/palettes/7449.png");

            this.ColorsList = new ObservableCollection<MyColor>
            {
                c1, c2
            };
            this.SelectedColor = this.ColorsList.First();

            this.ColorsListAsync = new ObservableCollection<MyColor>();
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

        public ObservableCollection<MyColor> ColorsListAsync
        {
            get
            {
                return this.colorsListAsync;
            }

            set
            {
                this.colorsListAsync = value;
                this.RaisePropertyChanged();
            }
        }

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

        public MyColor SelectedColorAsync
        {
            get
            {
                return this.selectedColorAsync;
            }

            set
            {
                this.selectedColorAsync = value;
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

        private async Task OnNavigationMessageReceivedAsync(NavigationMessage message)
        {
            if(message != null)
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                this.ColorsListAsync.Add(this.ColorsList.First());
                this.ColorsListAsync.Add(this.ColorsList.Last());
                this.SelectedColorAsync = this.ColorsListAsync.Last();
            }
        }

        public void CleanupViewModel()
        {
            this.ColorsListAsync.Clear();
            this.ColorsList.Clear();
            this.SelectedColorAsync = null;
            this.SelectedColor = null;
            base.Cleanup();
        }
    }
}