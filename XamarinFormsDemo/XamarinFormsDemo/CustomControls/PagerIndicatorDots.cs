namespace XamarinFormsDemo.CustomControls
{
    using System;
    using System.Collections;
    using System.Linq;
    using Xamarin.Forms;

    public interface ITabProvider
    {
        string ImageSource
        {
            get;
            set;
        }
    }

    public class PagerIndicatorDots : StackLayout
    {
        public PagerIndicatorDots()
        {
            this.HorizontalOptions = LayoutOptions.CenterAndExpand;
            this.VerticalOptions = LayoutOptions.End;
            this.Orientation = StackOrientation.Horizontal;
            this.DotColor = Color.Black;
        }

        public Color DotColor
        {
            get;
            set;
        }

        public double DotSize
        {
            get;
            set;
        }

        public IList ItemsSource
        {
            get
            {
                return (IList)this.GetValue(ItemsSourceProperty);
            }
            set
            {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        public object SelectedItem
        {
            get
            {
                return this.GetValue(SelectedItemProperty);
            }
            set
            {
                this.SetValue(SelectedItemProperty, value);
            }
        }

        private static void ItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((PagerIndicatorDots)bindable).ItemsSourceChanged();
        }

        private static void SelectDot(Button dot)
        {
            dot.Opacity = 1.0;
        }

        private static void SelectedItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((PagerIndicatorDots)bindable).SelectedItemChanged();
        }

        private static void UnselectDot(Button dot)
        {
            dot.Opacity = 0.5;
        }

        private void CreateDot()
        {
            //Make one button and add it to the dotLayout
            var dot = new Button
            {
                BorderRadius = Convert.ToInt32(DotSize),
                HeightRequest = DotSize,
                WidthRequest = DotSize,
                BackgroundColor = DotColor
            };
            this.Children.Add(dot);
        }

        private void ItemsSourceChanged()
        {
            if(this.ItemsSource == null)
            {
                return;
            }

            var countDelta = this.ItemsSource.Count - this.Children.Count;

            if(countDelta > 0)
            {
                for(var i = 0; i < countDelta; i++)
                {
                    this.CreateDot();
                }
            }
            else if(countDelta < 0)
            {
                for(var i = 0; i < -countDelta; i++)
                {
                    this.Children.RemoveAt(0);
                }
            }
        }
        
        private void SelectedItemChanged()
        {
            var selectedIndex = this.ItemsSource.IndexOf(this.SelectedItem);
            var pagerIndicators = this.Children.Cast<Button>().ToList();

            foreach(var pi in pagerIndicators)
            {
                UnselectDot(pi);
            }

            if(selectedIndex > -1)
            {
                SelectDot(pagerIndicators[selectedIndex]);
            }
        }

        public static BindableProperty ItemsSourceProperty = BindableProperty.Create
            (nameof(ItemsSource), typeof(IList), typeof(PagerIndicatorDots), null, BindingMode.OneWay, null, ItemsSourcePropertyChanged);

        public static BindableProperty SelectedItemProperty = BindableProperty.Create
            (nameof(SelectedItem), typeof(object), typeof(PagerIndicatorDots), null, BindingMode.TwoWay, null, SelectedItemPropertyChanged);
    }
}