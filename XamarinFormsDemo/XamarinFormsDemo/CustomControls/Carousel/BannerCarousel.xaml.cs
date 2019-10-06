namespace XamarinFormsDemo.CustomControls.Carousel
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using Xamarin.Forms;

    public partial class BannerCarousel : CarouselView
    {
        /*private int _currentIndex;

        private ICommand _scrollToNextCommand;

        private ICommand _scrollToPreviousCommand;

        public BannerCarousel()
        {
            InitializeComponent();
            Items = new List<ICarouselItem>();
        }

        public bool ManyItems
        {
            get
            {
                return (bool)GetValue(ManyItemsProperty);
            }
            set
            {
                SetValue(ManyItemsProperty, value);
            }
        }

        public ICommand ScrollToNextCommand => this._scrollToNextCommand ?? (this._scrollToNextCommand = new GalaSoft.MvvmLight.Command.RelayCommand(this.ScrollNext));

        public ICommand ScrollToPreviousCommand => this._scrollToPreviousCommand ?? (this._scrollToPreviousCommand = new GalaSoft.MvvmLight.Command.RelayCommand(this.ScrollPrevious));

        private void ScrollNext()
        {
            if (Items == null || _currentIndex == Items.Count() - 1)
            {
                return;
            }
            _currentIndex++;
            MoveToItem();
        }

        private void ScrollPrevious()
        {
            if (Items == null || _currentIndex == 0)
            {
                return;
            }
            _currentIndex--;
            MoveToItem();
        }

        private void MoveToItem()
        {
            var item = Items.ElementAt(_currentIndex);
            if (item != null)
            {
                ItemContainer.Content = GetImage(item);
                ToggleIndicator(item);
            }
        }

        public static readonly BindableProperty ManyItemsProperty = BindableProperty.Create
            (nameof(ManyItems), typeof(bool), typeof(SingleItemCarouselView), false, BindingMode.Default);*/
    }

}
