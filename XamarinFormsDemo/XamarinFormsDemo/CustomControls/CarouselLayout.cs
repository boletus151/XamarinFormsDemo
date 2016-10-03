/*--------------------------------------------------------------------------------------------------------------------
 <copyright file="CarouselLayout" company="CodigoEdulis">
   Código Edulis 2016
   http://www.codigoedulis.es
 </copyright>
 <summary>
    This implementation is based on: http://chrisriesgo.com/xamarin-forms-carousel-view-recipe/;
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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
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

        private static void SelectedIndexPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CarouselLayout)bindable).UpdateSelectedItem();
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

        private void UpdateSelectedIndex()
        {
            if(this.SelectedIndex > -1)
            {
                if(this.Children != null && this.Children.Any())
                {
                    try
                    {
                        this.SelectedItem = this.Children[this.SelectedIndex].BindingContext;
                    }
                    catch(IndexOutOfRangeException exception)
                    {
                        Debug.WriteLine($"Carousel Layout Exception: Index out of range: {exception.Message}.");
                        this.SelectedItem = null;
                    }
                }
                else
                {
                    this.SelectedItem = null;
                }
            }
            else
            {
                this.SelectedItem = null;
            }
        }

        private void UpdateSelectedItem()
        {
            SelectedItem = SelectedIndex > -1 ? Children[SelectedIndex].BindingContext : null;
        }

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create
            (nameof(SelectedIndex), typeof(int), typeof(CarouselLayout), 0, BindingMode.TwoWay, null, SelectedIndexPropertyChanged);

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create
            (nameof(ItemsSource), typeof(IList), typeof(CarouselLayout), null, BindingMode.Default, null, OnItemsSourcePropertyChanged);

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create
            (nameof(SelectedItem), typeof(object), typeof(CarouselLayout), null, BindingMode.TwoWay, null, OnSelectedItemPropertyChanged);
    }
}