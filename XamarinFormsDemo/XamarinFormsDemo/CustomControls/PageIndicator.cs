/*--------------------------------------------------------------------------------------------------------------------
 <copyright file="PageIndicatorDots" company="CodigoEdulis">
   Código Edulis 2016
   http://www.codigoedulis.es
 </copyright>
 <summary>
    This implementation is based on: http://chrisriesgo.com/xamarin-forms-carousel-view-recipe/ and https://github.com/chrisriesgo;
    because of this, it is under Creative Common By License:

    You are free to:

    Share — copy and redistribute the material in any medium or format
    Adapt — remix, transform, and build upon the material for any purpose, even commercially.

    The licensor cannot revoke these freedoms as long as you follow the license terms.

    Under the following terms:

    Attribution — You must give appropriate credit, provide a link to the license, and indicate if changes were made. You may do so in any reasonable manner, but not in any way that suggests the licensor endorses you or your use.
    No additional restrictions — You may not apply legal terms or technological measures that legally restrict others from doing anything the license permits.

 </summary>
--------------------------------------------------------------------------------------------------------------------*/

namespace XamarinFormsDemo.CustomControls
{
    using System;
    using System.Collections;
    using System.Linq;
    using Xamarin.Forms;

    public class PageIndicator : StackLayout
    {
        #region Private Methods

        private static void ItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((PageIndicator)bindable).ItemsSourceChanged();
        }

        private static void SelectDot(Button dot)
        {
            dot.Opacity = 1.0;
        }

        private static void SelectedItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((PageIndicator)bindable).SelectedItemChanged();
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
                BorderRadius = Convert.ToInt32(DotSize / 2),
                HeightRequest = DotSize,
                WidthRequest = DotSize,
                BackgroundColor = DotColor
            };
            this.Children.Add(dot);
        }

        private void ItemsSourceChanged()
        {
            if (this.ItemsSource == null)
            {
                return;
            }

            var countDelta = this.ItemsSource.Count - this.Children.Count;

            if (countDelta > 0)
            {
                for (var i = 0; i < countDelta; i++)
                {
                    this.CreateDot();
                }
            }
            else if (countDelta < 0)
            {
                for (var i = 0; i < -countDelta; i++)
                {
                    this.Children.RemoveAt(0);
                }
            }
        }

        private void SelectedItemChanged()
        {
            var selectedIndex = this.ItemsSource.IndexOf(this.SelectedItem);
            var pagerIndicators = this.Children.Cast<Button>().ToList();

            foreach (var pi in pagerIndicators)
            {
                UnselectDot(pi);
            }

            if (selectedIndex > -1)
            {
                SelectDot(pagerIndicators[selectedIndex]);
            }
        }

        #endregion

        #region Public Fields

        public static BindableProperty ItemsSourceProperty = BindableProperty.Create
            (nameof(ItemsSource), typeof(IList), typeof(PageIndicator), null, BindingMode.OneWay, null, ItemsSourcePropertyChanged);

        public static BindableProperty SelectedItemProperty = BindableProperty.Create
            (nameof(SelectedItem), typeof(object), typeof(PageIndicator), null, BindingMode.TwoWay, null, SelectedItemPropertyChanged);

        #endregion

        #region Public Constructors

        public PageIndicator()
        {
            this.HorizontalOptions = LayoutOptions.CenterAndExpand;
            this.VerticalOptions = LayoutOptions.End;
            this.Orientation = StackOrientation.Horizontal;
            this.DotColor = Color.Black;
        }

        #endregion

        #region Public Properties

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

        #endregion
    }
}