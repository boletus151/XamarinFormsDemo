using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using XamarinFormsDemo.Model;

namespace XamarinFormsDemo.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<MyColor> _colorsList;
        private MyColor _selectedColor;
        private ICommand _tryDebugCommand;
        private string _selectedValue;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            var c1 = new MyColor("White", "#FFFFFF");
            var c2 = new MyColor("Black", "#000000");

            this.ColorsList = new ObservableCollection<MyColor> { c1, c2 };
            this.SelectedColor = this.ColorsList.First();
        }

        public string SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                _selectedValue = value;
                RaisePropertyChanged();
            }
        }

        public MyColor SelectedColor
        {
            get { return _selectedColor; }
            set { _selectedColor = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<MyColor> ColorsList
        {
            get { return _colorsList; }
            set { _colorsList = value; RaisePropertyChanged(); }
        }

        public ICommand TryDebugCommand => _tryDebugCommand ?? (_tryDebugCommand = new RelayCommand(this.TryDebug));

        private void TryDebug()
        {
            Debug.WriteLine($"¡¡¡¡¡¡The Selected Item is: {SelectedColor}");
        }
    }
}