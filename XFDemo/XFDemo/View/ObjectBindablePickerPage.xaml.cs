// -------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectBindablePickerPage.cs" company="CodigoEdulis">
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

namespace XFDemo.View
{
    using Xamarin.Forms;

    using XFDemo.ViewModel;

    public partial class ObjectBindablePickerPage : ContentPage
    {
        public ObjectBindablePickerPage()
        {
            this.InitializeComponent();
        }

        /*protected override void OnDisappearing()
        {
            base.OnDisappearing();
            var vm = (PickersViewModel)this.BindingContext;
            vm?.CleanupViewModel();
        }*/
    }
}