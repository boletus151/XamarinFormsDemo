/*--------------------------------------------------------------------------------------------------------------------
<copyright file="ObjectBindableNotifierPicker" company="CodigoEdulis">
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
    using System.Linq;
    using System.Reflection;
    using Xamarin.Forms;

    public class ObjectBindableNotifierPicker : Picker
    {
        public ObjectBindableNotifierPicker()
        {
            this.SelectedIndexChanged += this.OnSelectedIndexChanged;
        }

        /// <summary>
        ///     Gets or sets the display member. The title that user is going to see in the list
        /// </summary>
        /// <value>
        ///     The display member.
        /// </value>
        public string DisplayMember
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the items source.
        /// </summary>
        /// <value>
        ///     The items source.
        /// </value>
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

        /// <summary>
        ///     Gets or sets the selected item.
        /// </summary>
        /// <value>
        ///     The selected item.
        /// </value>
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

        /// <summary>
        ///     Gets or sets the selected value. The value of the property of your model object you want to save i.e in the View
        ///     Model.
        /// </summary>
        /// <value>
        ///     The selected value.
        /// </value>
        public object SelectedValue
        {
            get
            {
                return this.GetValue(SelectedValueProperty);
            }
            set
            {
                this.SetValue(SelectedValueProperty, value);
            }
        }

        /// <summary>
        ///     Gets or sets the selected value path. The Property´s Name of uour model object
        /// </summary>
        /// <value>
        ///     The selected value path.
        /// </value>
        public string SelectedValuePath
        {
            get
            {
                return (string)this.GetValue(SelectedValuePathProperty);
            }
            set
            {
                this.SetValue(SelectedValuePathProperty, value);
            }
        }

        private static NotifyCollectionChangedEventHandler NotifyCollectionOnCollectionChanged(ObjectBindableNotifierPicker picker)
        {
            return (sender, args) =>
            {
                if(args.Action == NotifyCollectionChangedAction.Reset)
                {
                    picker.Items.Clear();
                    return;
                }

                if(args.NewItems == null)
                {
                    return;
                }

                if(args.OldItems != null)
                {
                    foreach(var oldItem in args.OldItems)
                    {
                        picker.Items.Remove((oldItem ?? "").ToString());
                    }
                }
                picker.RefreshItems(args.NewItems);
            };
        }

        /// <summary>
        ///     Called when [items source changed].
        /// </summary>
        /// <param name="bindableObject">The bindableObject.</param>
        /// <param name="oldvalue">The old value.</param>
        /// <param name="newvalue">The new value.</param>
        private static void OnItemsSourceChanged(BindableObject bindableObject, object oldvalue, object newvalue)
        {
            var picker = bindableObject as ObjectBindableNotifierPicker;

            if(picker == null)
            {
                return;
            }

            picker.Items.Clear();

            if(newvalue == null)
            {
                return;
            }

            var notifyCollection = newvalue as INotifyCollectionChanged;
            if(notifyCollection != null)
            {
                notifyCollection.CollectionChanged += NotifyCollectionOnCollectionChanged(picker);
            }
            picker.RefreshItems(newvalue as IList);
        }

        /// <summary>
        ///     Called when [selected item changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldvalue">The oldvalue.</param>
        /// <param name="newvalue">The newvalue.</param>
        private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var picker = bindable as ObjectBindableNotifierPicker;

            if(picker?.ItemsSource == null)
            {
                return;
            }
            if(picker.ItemsSource.Contains(picker.SelectedItem))
            {
                picker.SelectedIndex = picker.ItemsSource.IndexOf(picker.SelectedItem);
            }
        }

        /// <summary>
        ///     Called when [selected index changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if(this.SelectedIndex < 0 || this.SelectedIndex > this.Items.Count - 1)
            {
                this.SelectedItem = null;
            }
            else
            {
                var picker = sender as ObjectBindableNotifierPicker;
                if(picker == null)
                {
                    return;
                }

                this.SelectedItem = this.ItemsSource[this.SelectedIndex];

                if(string.IsNullOrEmpty(this.SelectedValuePath))
                {
                    return;
                }

                var prop = this.SelectedItem.GetType().GetRuntimeProperties().FirstOrDefault
                    (p => string.Equals(p.Name, picker.SelectedValuePath, StringComparison.OrdinalIgnoreCase));
                if(prop != null)
                {
                    this.SelectedValue = prop.GetValue(this.SelectedItem);
                }
            }
        }

        private void RefreshItems(IList newvalue)
        {
            foreach(var item in newvalue)
            {
                if(string.IsNullOrEmpty(this.DisplayMember))
                {
                    this.Items.Add(item.ToString());
                }
                else
                {
                    // for PCL
                    /*var type = item.GetType();
                    var prop = type.GetProperty(picker.DisplayMember);
                    picker.Items.Add(prop.GetValue(item).ToString());*/

                    var prop = item.GetType().GetRuntimeProperties().FirstOrDefault
                        (p => string.Equals(p.Name, this.DisplayMember, StringComparison.OrdinalIgnoreCase));
                    if(prop != null)
                    {
                        this.Items.Add(prop.GetValue(item).ToString());
                    }
                }
            }
        }

        public static BindableProperty ItemsSourceProperty = BindableProperty.Create
            (nameof(ItemsSource), typeof(IList), typeof(ObjectBindableNotifierPicker), default(IList), BindingMode.OneWay, null, OnItemsSourceChanged);

        public static BindableProperty SelectedItemProperty = BindableProperty.Create
            (nameof(SelectedItem), typeof(object), typeof(ObjectBindableNotifierPicker), default(object), BindingMode.TwoWay, null, OnSelectedItemChanged);

        public static BindableProperty SelectedValueProperty = BindableProperty.Create
            (nameof(SelectedValueProperty), typeof(object), typeof(ObjectBindableNotifierPicker), default(object), BindingMode.TwoWay);

        public static BindableProperty SelectedValuePathProperty = BindableProperty.Create
            (nameof(SelectedValuePathProperty), typeof(string), typeof(ObjectBindableNotifierPicker), string.Empty);
    }
}