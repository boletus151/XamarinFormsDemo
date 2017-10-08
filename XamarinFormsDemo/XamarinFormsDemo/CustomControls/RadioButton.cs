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
    using Xamarin.Forms;

    public class RadioButton : Button
    {
        public RadioButton()
        {
            this.Clicked += this.OnClicked;
        }

        public bool Checked
        {
            get
            {
                return (bool)this.GetValue(CheckedProperty);
            }
            set
            {
                this.SetValue(CheckedProperty, value);
                this.OnPropertyChanged();
            }
        }

        public string CheckedImage
        {
            get
            {
                return (string)this.GetValue(CheckedImageProperty);
            }
            set
            {
                this.SetValue(CheckedImageProperty, value);
            }
        }

        public double OpacityWhenIsDisabled
        {
            get
            {
                return (double)this.GetValue(OpacityWhenIsDisabledProperty);
            }
            set
            {
                this.SetValue(OpacityWhenIsDisabledProperty, value);
            }
        }

        public string UncheckedImage
        {
            get
            {
                return (string)this.GetValue(UncheckedImageProperty);
            }
            set
            {
                this.SetValue(UncheckedImageProperty, value);
            }
        }

        public void OnClicked(object sender, EventArgs e)
        {
            this.Checked = !this.Checked;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            OnCheckedChanged(this, this.Checked, this.Checked);
        }

        private static void OnCheckedChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var radioButton = (RadioButton)bindable;
            if(radioButton == null)
            {
                return;
            }

            if(newvalue != null && (bool)newvalue)
                radioButton.Image = radioButton.CheckedImage;
            else
                radioButton.Image = radioButton.UncheckedImage;
        }

        private static void OnPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var radioButton = (RadioButton)bindable;
            if(radioButton == null)
            {
                return;
            }
            if(newvalue.Equals(1.0))
            {
                return;
            }
            radioButton.Style = OpacityStyleDataTrigger(radioButton);
        }

        private static Style OpacityStyleDataTrigger(RadioButton radioButton)
        {
            var trigger = new Trigger(typeof(Button))
            {
                Property = IsEnabledProperty, Value = false
            };
            var setter = new Setter
            {
                Property = OpacityProperty, Value = radioButton.OpacityWhenIsDisabled
            };
            trigger.Setters.Add(setter);

            var style = new Style(typeof(Button));

            style.Triggers.Add(trigger);

            return style;
        }

        private static bool OpacityValidateValue(BindableObject bindable, object value)
        {
            var opacity = (double?)value;
            return !(opacity < 0);
        }

        public static BindableProperty CheckedProperty = BindableProperty.Create(nameof(Checked), typeof(bool), typeof(RadioButton), false, BindingMode.TwoWay, null, OnCheckedChanged);

        public static BindableProperty CheckedImageProperty = BindableProperty.Create(nameof(CheckedImage), typeof(string), typeof(RadioButton), string.Empty);

        public static BindableProperty UncheckedImageProperty = BindableProperty.Create(nameof(UncheckedImage), typeof(string), typeof(RadioButton), string.Empty);

        public static BindableProperty OpacityWhenIsDisabledProperty = BindableProperty.Create(nameof(OpacityWhenIsDisabled), typeof(double), typeof(RadioButton), 1.0, BindingMode.OneWayToSource, OpacityValidateValue, OnPropertyChanged);
    }
}