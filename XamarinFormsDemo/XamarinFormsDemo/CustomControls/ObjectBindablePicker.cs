/*--------------------------------------------------------------------------------------------------------------------
<copyright file="ObjectBindablePicker" company="CodigoEdulis">
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
    using System.Linq;
    using System.Reflection;
    using Xamarin.Forms;

    public class ObjectBindablePicker : Picker
    {
        public static BindableProperty ItemsSourceProperty =
            BindableProperty.Create<ObjectBindablePicker, IList>(o => o.ItemsSource, default(IList),
                propertyChanged: OnItemsSourceChanged);

        public static BindableProperty SelectedItemProperty =
            BindableProperty.Create<ObjectBindablePicker, object>(o => o.SelectedItem, default(object),
                propertyChanged: OnSelectedItemChanged);

        public static BindableProperty SelectedValueProperty =
            BindableProperty.Create<ObjectBindablePicker, object>(x => x.SelectedValue, default(object),
                BindingMode.OneWayToSource);

        public static BindableProperty SelectedValuePathProperty =
            BindableProperty.Create<ObjectBindablePicker, string>(x => x.SelectedValuePath, string.Empty);

        public ObjectBindablePicker()
        {
            this.SelectedIndexChanged += this.OnSelectedIndexChanged;
        }

        /// <summary>
        /// Gets or sets the display member. The title that user is going to see in the list
        /// </summary>
        /// <value>
        /// The display member.
        /// </value>
        public string DisplayMember { get; set; }

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>
        /// The items source.
        /// </value>
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>
        /// The selected item.
        /// </value>
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected value. The value of the property of your model object you want to save i.e in the View Model.
        /// </summary>
        /// <value>
        /// The selected value.
        /// </value>
        public object SelectedValue
        {
            get { return GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected value path. The Property´s Name of uour model object 
        /// </summary>
        /// <value>
        /// The selected value path.
        /// </value>
        public string SelectedValuePath
        {
            get { return (string)GetValue(SelectedValuePathProperty); }
            set { SetValue(SelectedValuePathProperty, value); }
        }

        /// <summary>
        /// Called when [items source changed].
        /// </summary>
        /// <param name="bindableObject">The bindableObject.</param>
        /// <param name="oldvalue">The old value.</param>
        /// <param name="newvalue">The new value.</param>
        private static void OnItemsSourceChanged(BindableObject bindableObject, IList oldvalue, IList newvalue)
        {
            var picker = bindableObject as ObjectBindablePicker;

            if (picker == null)
            {
                return;
            }

            picker.Items.Clear();

            if (newvalue == null)
            {
                return;
            }

            //now it works like "subscribe once" but you can improve
            foreach (var item in newvalue)
            {
                if (string.IsNullOrEmpty(picker.DisplayMember))
                {
                    picker.Items.Add(item.ToString());
                }
                else
                {
                    // for PCL
                    /*var type = item.GetType();
                    var prop = type.GetProperty(picker.DisplayMember);
                    picker.Items.Add(prop.GetValue(item).ToString());*/

                    var prop =
                        item.GetType()
                            .GetRuntimeProperties()
                            .FirstOrDefault(
                                p => string.Equals(p.Name, picker.DisplayMember, StringComparison.OrdinalIgnoreCase));
                    if (prop != null)
                    {
                        picker.Items.Add(prop.GetValue(item).ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Called when [selected index changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
            {
                SelectedItem = null;
            }
            else
            {
                var picker = sender as ObjectBindablePicker;
                if (picker == null)
                {
                    return;
                }

                SelectedItem = ItemsSource[SelectedIndex];

                if (string.IsNullOrEmpty(SelectedValuePath))
                {
                    return;
                }

                var prop = SelectedItem.GetType().GetRuntimeProperties()
                    .FirstOrDefault(p => string.Equals(p.Name, picker.SelectedValuePath, StringComparison.OrdinalIgnoreCase));
                if (prop != null)
                {
                    SelectedValue = prop.GetValue(SelectedItem);
                }
            }
        }

        /// <summary>
        /// Called when [selected item changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldvalue">The oldvalue.</param>
        /// <param name="newvalue">The newvalue.</param>
        private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var picker = bindable as ObjectBindablePicker;

            if (picker?.ItemsSource == null)
            {
                return;
            }
            if (picker.ItemsSource.Contains(picker.SelectedItem))
            {
                picker.SelectedIndex = picker.ItemsSource.IndexOf(picker.SelectedItem);
            }
        }
    }
}