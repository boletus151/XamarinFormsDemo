// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadioButtonViewModel.cs" company="Pernod Ricard">
//    Pernod Ricard 2017 - Fase 2.0
//  </copyright>
//  <summary>
//    The definition of  RadioButtonViewModel.cs
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace XamarinFormsDemo.ViewModel
{
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;

    public class RadioButtonViewModel : ParentViewModel
    {
        private bool isChecked;

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
    }
}