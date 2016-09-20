namespace XamarinFormsDemo.Services
{
    using System;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight.Views;
    using Xamarin.Forms;

    public class DialogService : IDialogService
    {
        private Page _dialogPage;

        public async Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            await this._dialogPage.DisplayAlert(title, message, buttonText);

            if(afterHideCallback != null)
            {
                afterHideCallback();
            }
        }

        public async Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            await this._dialogPage.DisplayAlert(title, error.Message, buttonText);

            if(afterHideCallback != null)
            {
                afterHideCallback();
            }
        }

        public async Task ShowMessage(string message, string title)
        {
            await this._dialogPage.DisplayAlert(title, message, "OK");
        }

        public async Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            await this._dialogPage.DisplayAlert(title, message, buttonText);

            if(afterHideCallback != null)
            {
                afterHideCallback();
            }
        }

        public async Task<bool> ShowMessage
            (string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            var result = await this._dialogPage.DisplayAlert(title, message, buttonConfirmText, buttonCancelText);

            if(afterHideCallback != null)
            {
                afterHideCallback(result);
            }

            return result;
        }

        public async Task ShowMessageBox(string message, string title)
        {
            await this._dialogPage.DisplayAlert(title, message, "OK");
        }

        public void Initialize(Page dialogPage)
        {
            this._dialogPage = dialogPage;
        }
    }
}