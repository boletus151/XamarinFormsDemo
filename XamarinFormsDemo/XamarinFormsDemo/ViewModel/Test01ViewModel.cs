using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace XamarinFormsDemo.ViewModel
{
    public class Test01ViewModel : ViewModelBase
    {
        private IDialogService dialogService;
        private string reverseString;

        public string ReverseString
        {
            get { return reverseString; }
            set
            {
                reverseString = value;
                this.RaisePropertyChanged();
            }
        }


        private string myText;

        public string MyText
        {
            get { return myText; }
            set
            {
                this.myText = value;
                RaisePropertyChanged();
            }
        }

        public ICommand ReverseCommand { get; }

        public Test01ViewModel(IDialogService dialogService)
        {
            this.ReverseCommand = new Command(async()=> await this.ReverseCommandExectue());
            this.dialogService = dialogService;
        }

        private async Task ReverseCommandExectue()
        {
            if (string.IsNullOrEmpty(this.MyText))
            {
                await this.dialogService.ShowMessage("The input text is empty", "Reverse String Demo");

            }
            else
            {
                Array myArray = Array.CreateInstance(typeof(String), this.MyText.Length);
                myArray.SetValue(this.MyText, 0, this.myText.Length);
                this.ReverseString = myArray.ToString();

                await this.dialogService.ShowMessage($"The input text in reversed order is: {this.ReverseString}", "Reverse String Demo");
            }
        }
    }
}
