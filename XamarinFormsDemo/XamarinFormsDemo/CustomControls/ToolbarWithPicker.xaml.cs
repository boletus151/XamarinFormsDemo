using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XFDemo.CustomControls
{
    using System.Collections;

    public partial class ToolbarWithPicker : ContentView
    {
        public ToolbarWithPicker()
        {
            InitializeComponent();
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

        public string DisplayMember
        {
            get
            {
                return (string)this.GetValue(DisplayMemberProperty);
            }
            set
            {
                this.SetValue(DisplayMemberProperty, value);
            }
        }

        public string Icon
        {
            get
            {
                return (string)this.GetValue(IconProperty);
            }
            set
            {
                this.SetValue(IconProperty, value);
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

        public static BindableProperty DisplayMemberProperty = BindableProperty.Create
            (nameof(DisplayMember), typeof(string), typeof(ToolbarWithPicker), string.Empty);

        public static BindableProperty ItemsSourceProperty = BindableProperty.Create
            (nameof(ItemsSource), typeof(IList), typeof(ToolbarWithPicker), default(IList));

        public static BindableProperty IconProperty = BindableProperty.Create
            (nameof(Icon), typeof(string), typeof(ToolbarWithPicker), string.Empty);

        public static BindableProperty SelectedItemProperty = BindableProperty.Create
            (nameof(SelectedItem), typeof(object), typeof(ToolbarWithPicker), default(object), BindingMode.TwoWay);


    }
}
