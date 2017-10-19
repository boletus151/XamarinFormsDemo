﻿// -------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="CodigoEdulis">
//    Código Edulis 2017
//    http://www.codigoedulis.es
//  </copyright>
//  <summary>
//     This implementation is a group of the offers of several persons along the network;
//     because of this, it is under Creative Common By License:
//     
//     You are free to:
// 
//     Share — copy and redistribute the material in any medium or format
//     Adapt — remix, transform, and build upon the material for any purpose, even commercially.
//     
//     The licensor cannot revoke these freedoms as long as you follow the license terms.
//     
//     Under the following terms:
//     
//     Attribution — You must give appropriate credit, provide a link to the license, and indicate if changes were made. You may do so in any reasonable manner, but not in any way that suggests the licensor endorses you or your use.
//     No additional restrictions — You may not apply legal terms or technological measures that legally restrict others from doing anything the license permits.
//  
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
        private readonly int lastColumn = 0;

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

        public int MaxColumnWidth
        {
            get
            {
                return (int)this.GetValue(MaxColumnWidthProperty);
            }
            set
            {
                this.SetValue(MaxColumnWidthProperty, value);
            }
        }

        public int MaxNumberOfRows
        {
            get
            {
                return (int)this.GetValue(MaxNumberOfRowsProperty);
            }
            set
            {
                this.SetValue(MaxNumberOfRowsProperty, value);
            }
        }

        public int MaxRowHeight
        {
            get
            {
                return (int)this.GetValue(MaxRowHeightProperty);
            }
            set
            {
                this.SetValue(MaxRowHeightProperty, value);
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

            if(oldValue is INotifyCollectionChanged oldObservableCollection)
            {
                oldObservableCollection.CollectionChanged -= control.OnItemsSourceCollectionChanged;
            }

            if(newValue is INotifyCollectionChanged newObservableCollection)
            {
                newObservableCollection.CollectionChanged += control.OnItemsSourceCollectionChanged;
            }

            control.Children.Clear();
            control.RowDefinitions.Clear();
            control.ColumnDefinitions.Clear();
            var columns = -1;
            var rows = 0;

            for(var i = 0; i < control.MaxNumberOfRows; i++)
            {
                if(control.MaxRowHeight > 0)
                {
                    control.RowDefinitions.Add(new RowDefinition
                    {
                        Height = new GridLength(control.MaxRowHeight, GridUnitType.Auto)
                    });
                }
                else
                {
                    control.RowDefinitions.Add(new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Auto)
                    });
                }
            }

            foreach(var item in newValueAsEnumerable)
            {
                var view = control.CreateChildViewFor(item);

                if(rows % control.MaxNumberOfRows == 0)
                {
                    if(control.MaxColumnWidth > 0)
                    {
                        control.ColumnDefinitions.Add(new ColumnDefinition
                        {
                            Width = new GridLength(control.MaxColumnWidth, GridUnitType.Auto)
                        });
                    }
                    else
                    {
                        control.ColumnDefinitions.Add(new ColumnDefinition
                        {
                            Width = new GridLength(1, GridUnitType.Auto)
                        });
                    }
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
            var rows = this.RowDefinitions.Count;

            if(rows != this.MaxNumberOfRows)
            {
                for(var i = 0; i < this.MaxNumberOfRows; i++)
                {
                    //if (this.MaxRowHeight > 0)
                    //{
                    //    this.RowDefinitions.Add(new RowDefinition
                    //    {
                    //        Height = new GridLength(this.MaxRowHeight)
                    //    });
                    //}
                    //else
                    {
                        this.RowDefinitions.Add(new RowDefinition
                        {
                            Height = new GridLength(1, GridUnitType.Auto)
                        });
                    }
                }
            }
            var columns = this.lastColumn;
            rows = this.lastRow;
            foreach(var item in e.NewItems)
            {
                var view = this.CreateChildViewFor(item);

                if(rows % this.MaxNumberOfRows == 0)
                {
                    //if (this.MaxColumnWidth > 0)
                    //{
                    //    this.ColumnDefinitions.Add(new ColumnDefinition
                    //    {
                    //        Width = new GridLength(this.MaxColumnWidth)
                    //    }); 
                    //}
                    //else
                    {
                        this.ColumnDefinitions.Add(new ColumnDefinition
                        {
                            Width = new GridLength(1, GridUnitType.Auto)
                        });
                    }
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

        public static readonly BindableProperty MaxNumberOfRowsProperty = BindableProperty.Create(nameof(MaxNumberOfRows), typeof(int), typeof(HorizontalListView), 2, BindingMode.OneWayToSource);

        public static readonly BindableProperty MaxRowHeightProperty = BindableProperty.Create(nameof(MaxRowHeight), typeof(int), typeof(HorizontalListView), 0, BindingMode.OneWayToSource);

        public static readonly BindableProperty MaxColumnWidthProperty = BindableProperty.Create(nameof(MaxColumnWidth), typeof(int), typeof(HorizontalListView), 0, BindingMode.OneWayToSource);

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