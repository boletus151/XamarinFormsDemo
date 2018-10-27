namespace XamarinFormsDemo.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Constants;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Views;
    using Xamarin.Forms;

    public class RegexViewModel : ParentViewModel
    {

        #region Private Fields

        private string result;

        private string textValue;

        private string selectedItem;

        private ObservableCollection<string> patternList;

        #endregion

        #region Private Methods

        private void MatchCommandExecute()
        {
            if (string.IsNullOrEmpty(this.TextValue) || Regex.IsMatch(this.TextValue, this.SelectedItem))
            {
                this.Result = true.ToString();
            }
            else
            {
                this.Result = false.ToString();
            }
        }

        #endregion

        #region Public Constructors

        public RegexViewModel(IMessenger messenger, INavigationService navigationService, IDialogService dialogService)
                    : base(messenger, navigationService, dialogService)
        {
            this.MatchCommand = new Command(this.MatchCommandExecute);
            this.PatternList = new ObservableCollection<string>
            {
                @"^\d*$",
                @"\d",
                @"^[0-9]([.,][0-9]{1,3})?$",
                @"^\d*[.,]\d*$"
            };
            this.TextValue = "1.2";
            this.SelectedItem = this.PatternList.First();
        }

        #endregion

        #region Public Properties

        public ObservableCollection<string> PatternList
        {
            get
            {
                return this.patternList;
            }
            set
            {
                this.patternList = value;
                this.RaisePropertyChanged();
            }
        }

        public string SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set
            {
                this.selectedItem = value;
                this.RaisePropertyChanged();
            }
        }

        public Command MatchCommand { get; }

        public string Result
        {
            get
            {
                return this.result;
            }
            set
            {
                this.result = value;
                this.RaisePropertyChanged();
            }
        }

        public string TextValue
        {
            get
            {
                return this.textValue;
            }
            set
            {
                this.textValue = value;
                this.RaisePropertyChanged();
            }
        }

        #endregion
    }
}