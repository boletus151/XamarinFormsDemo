/*--------------------------------------------------------------------------------------------------------------------
 <copyright file="HorizontalListView.cs" company="CodigoEdulis">
   Código Edulis 2016
   http://www.codigoedulis.es
 </copyright>
 <summary>
    This implementation is a group of the offers of several persons along the network;
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
    using System.Collections.Specialized;
    using Xamarin.Forms;

    public delegate void HorizontalListViewItemAddedEventHandler(object sender, HorizontalListViewItemAddedEventArgs args);

    public class HorizontalListView : Grid
    {
        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)this.GetValue(ItemsSourceProperty);
            }
            set
            {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        public DataTemplate ItemTemplate
        {
            get
            {
                return (DataTemplate)this.GetValue(ItemTemplateProperty);
            }
            set
            {
                this.SetValue(ItemTemplateProperty, value);
            }
        }

        public string MaxNumberOfRows
        {
            get
            {
                return (string)this.GetValue(MaxNumberOfRowsProperty);
            }
            set
            {
                this.SetValue(MaxNumberOfRowsProperty, value);
            }
        }

        protected virtual void OnItemCreated(View view) => this.ItemCreated?.Invoke(this, new HorizontalListViewItemAddedEventArgs(view, view.BindingContext));

        private static void ItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newValueAsEnumerable = (IEnumerable)newValue;
            if(newValueAsEnumerable == null)
            {
                return;
            }

            var control = (HorizontalListView)bindable;
            var oldObservableCollection = oldValue as INotifyCollectionChanged;

            if(oldObservableCollection != null)
            {
                oldObservableCollection.CollectionChanged -= control.OnItemsSourceCollectionChanged;
            }

            var newObservableCollection = newValue as INotifyCollectionChanged;

            if(newObservableCollection != null)
            {
                newObservableCollection.CollectionChanged += control.OnItemsSourceCollectionChanged;
            }

            control.Children.Clear();
            var columns = -1;
            var rows = 0;

            var requiredRows = control.CheckIfItIsInt();
            for (var i = 0; i < requiredRows; i++)
            {
                control.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Star)
                });
            }

            foreach (var item in newValueAsEnumerable)
            {
                var view = control.CreateChildViewFor(item);

                if (rows % requiredRows == 0)
                {
                    control.ColumnDefinitions.Add(new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Auto)
                    });
                    columns++;
                    rows = 0;
                }
                control.Children.Add(view, columns, rows);
                rows++;
                control.OnItemCreated(view);
            }

            control.UpdateChildrenLayout();
            control.InvalidateLayout();
        }

        private int CheckIfItIsInt()
        {
            int requiredColumns;
            var isInt = int.TryParse(this.MaxNumberOfRows, out requiredColumns);
            if(!isInt)
                return 1;
            return requiredColumns;
        }

        private View CreateChildViewFor(object item)
        {
            this.ItemTemplate.SetValue(BindingContextProperty, item);
            return (View)this.ItemTemplate.CreateContent();
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var invalidate = false;

            if(e.OldItems != null)
            {
                this.Children.RemoveAt(e.OldStartingIndex);
                invalidate = true;
            }

            if(e.NewItems != null)
            {
                for(var i = 0; i < e.NewItems.Count; ++i)
                {
                    var item = e.NewItems[i];
                    var view = this.CreateChildViewFor(item);

                    this.Children.Insert(i + e.NewStartingIndex, view);
                    this.OnItemCreated(view);
                }

                invalidate = true;
            }

            if(invalidate)
            {
                this.UpdateChildrenLayout();
                this.InvalidateLayout();
            }
        }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(HorizontalListView), null, BindingMode.OneWay, propertyChanged: ItemsChanged);

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(HorizontalListView), default(DataTemplate));

        public static readonly BindableProperty MaxNumberOfRowsProperty = BindableProperty.Create(nameof(MaxNumberOfRows), typeof(string), typeof(HorizontalListView), "2", BindingMode.OneWayToSource);

        public event HorizontalListViewItemAddedEventHandler ItemCreated;
    }

    public class HorizontalListViewItemAddedEventArgs : EventArgs
    {
        public HorizontalListViewItemAddedEventArgs(View view, object model)
        {
            this.View = view;
            this.Model = model;
        }

        public object Model { get; }

        public View View { get; }
    }
}