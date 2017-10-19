// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HorizontalListView.cs" company="Pernod Ricard">
//    Pernod Ricard 2017 - Fase 2.0
//  </copyright>
//  <summary>
//    The definition of  HorizontalListView.cs
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace XamarinFormsDemo.CustomControls
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using Xamarin.Forms;

    public delegate void HorizontalListViewItemAddedEventHandler(object sender, HorizontalListViewItemAddedEventArgs args);

    public class HorizontalListView : Grid
    {
        private int lastRow;

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
            control.RowDefinitions.Clear();
            control.ColumnDefinitions.Clear();
            var columns = -1;
            var rows = 0;

            var requiredRows = control.CheckIfItIsInt();
            for(var i = 0; i < requiredRows; i++)
            {
                control.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Auto)
                });
            }

            foreach(var item in newValueAsEnumerable)
            {
                var view = control.CreateChildViewFor(item);

                if(rows % requiredRows == 0)
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

        private void AddNewItem(NotifyCollectionChangedEventArgs e)
        {
            var columns = this.ColumnDefinitions.Count;
            var rows = this.RowDefinitions.Count;

            var requiredRows = this.CheckIfItIsInt();

            if(rows != requiredRows)
            {
                for(var i = 0; i < requiredRows; i++)
                {
                    this.RowDefinitions.Add(new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Auto)
                    });
                }
            }
            rows = this.lastRow;
            foreach(var item in e.NewItems)
            {
                var view = this.CreateChildViewFor(item);

                if(rows % requiredRows == 0)
                {
                    this.ColumnDefinitions.Add(new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Auto)
                    });
                    columns++;
                    rows = 0;
                }
                this.Children.Add(view, columns, rows);
                rows++;
                this.OnItemCreated(view);
            }
            this.lastRow = rows;
            this.UpdateChildrenLayout();
            this.InvalidateLayout();
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
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    this.ResetControl(e);
                    return;
                case NotifyCollectionChangedAction.Add:
                    this.AddNewItem(e);
                    break;
            }
        }

        private void ResetControl(NotifyCollectionChangedEventArgs e)
        {
            this.lastRow = 0;
            var oldObservableCollection = e.OldItems as INotifyCollectionChanged;
            if(oldObservableCollection != null)
            {
                oldObservableCollection.CollectionChanged -= this.OnItemsSourceCollectionChanged;
            }
            this.Children.Clear();
            this.RowDefinitions.Clear();
            this.ColumnDefinitions.Clear();
            this.UpdateChildrenLayout();
            this.InvalidateLayout();
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