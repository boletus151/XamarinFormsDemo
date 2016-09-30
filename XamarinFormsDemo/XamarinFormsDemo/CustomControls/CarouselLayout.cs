namespace XamarinFormsDemo.CustomControls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class CarouselLayout : ScrollView
    {
        public enum IndicatorStyleEnum
        {
            None,

            Dots,

            Tabs
        }

        private readonly StackLayout stackLayoutContainer;

        private bool _layingOutChildren;

        private int selectedIndex;

        public CarouselLayout()
        {
            Orientation = ScrollOrientation.Horizontal;

            this.stackLayoutContainer = new StackLayout
            {
                Orientation = StackOrientation.Horizontal, Spacing = 0
            };
            Content = this.stackLayoutContainer;
        }

        public IList<View> Children => this.stackLayoutContainer.Children;

        public IndicatorStyleEnum IndicatorStyle
        {
            get;
            set;
        }

        public IList ItemsSource
        {
            get
            {
                return (IList)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public DataTemplate ItemTemplate
        {
            get;
            set;
        }

        public int SelectedIndex
        {
            get
            {
                return (int)GetValue(SelectedIndexProperty);
            }
            set
            {
                SetValue(SelectedIndexProperty, value);
            }
        }

        public object SelectedItem
        {
            get
            {
                return GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);
            if(_layingOutChildren)
            {
                return;
            }

            _layingOutChildren = true;
            foreach(var child in Children)
            {
                child.WidthRequest = width;
            }
            _layingOutChildren = false;
        }

        private static void OnItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CarouselLayout)bindable).ItemsSourceChanged();
        }

        private static void OnSelectedItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CarouselLayout)bindable).UpdateSelectedIndex();
        }

        private void ItemsSourceChanged()
        {
            this.stackLayoutContainer.Children.Clear();
            foreach(var item in ItemsSource)
            {
                var view = (View)ItemTemplate.CreateContent();
                var bindableObject = (BindableObject)view;
                if(bindableObject != null)
                {
                    bindableObject.BindingContext = item;
                }
                this.stackLayoutContainer.Children.Add(view);
            }

            if(this.selectedIndex >= 0)
            {
                SelectedIndex = this.selectedIndex;
                this.UpdateSelectedItem();
            }
        }

        private void ItemsSourceChanging()
        {
            if(ItemsSource == null)
                return;
            this.selectedIndex = ItemsSource.IndexOf(SelectedItem);
        }

        private void UpdateSelectedIndex()
        {
            if(SelectedItem == BindingContext)
            {
                return;
            }

            SelectedIndex = Children.Select(c => c.BindingContext).ToList().IndexOf(SelectedItem);
        }

        private void UpdateSelectedItem()
        {
            SelectedItem = SelectedIndex > -1 ? Children[SelectedIndex].BindingContext : null;
        }

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create
            (nameof(SelectedIndex), typeof(int), typeof(CarouselLayout), 0, BindingMode.TwoWay, null, SelectedIndexPropertyChanged);

        private static void SelectedIndexPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CarouselLayout)bindable).UpdateSelectedItem();
        }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create
            (nameof(ItemsSource), typeof(IList), typeof(CarouselLayout), null, BindingMode.Default, null, OnItemsSourcePropertyChanged);

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create
            (nameof(SelectedItem), typeof(object), typeof(CarouselLayout), null, BindingMode.TwoWay, null, OnSelectedItemPropertyChanged);
    }
}