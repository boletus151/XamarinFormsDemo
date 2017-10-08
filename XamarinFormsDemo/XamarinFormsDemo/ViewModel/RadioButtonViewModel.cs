// -------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectBindablePicker.cs" company="CodigoEdulis">
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
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;

    public class RadioButtonViewModel : ParentViewModel
    {
        private bool isChecked;

        private bool isCheckedDisabled;

        public RadioButtonViewModel(IMessenger messenger, INavigationService navigationService, IDialogService dialogService)
            : base(messenger, navigationService, dialogService)
        {
            this.Checked = false;
        }

        public bool Checked
        {
            get
            {
                return this.isChecked;
            }
            set
            {
                this.isChecked = value;
                this.RaisePropertyChanged();
            }
        }
        public bool CheckedDisabled
        {
            get
            {
                return this.isCheckedDisabled;
            }
            set
            {
                this.isCheckedDisabled = value;
                this.RaisePropertyChanged();
            }
        }
    }
}