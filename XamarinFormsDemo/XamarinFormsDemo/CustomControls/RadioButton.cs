// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadioButton.cs" company="Pernod Ricard">
//    Pernod Ricard 2017 - Fase 2.0
//  </copyright>
//  <summary>
//    The definition of  RadioButton.cs
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace XamarinFormsDemo.CustomControls
{
    using System;
    using Xamarin.Forms;

    public class RadioButton : Button
    {
        #region Constructors

        public RadioButton()
        {
            this.Clicked += this.OnClicked;
        }

        #endregion

        #region Public Properties

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
                this.RaiseCheckedChanged();
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

        #endregion

        #region Public Methods

        public void OnClicked(object sender, EventArgs e)
        {
            this.Checked = !this.Checked;
        }

        #endregion

        #region Private Static Methods

        private static void OnCheckedChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var radioButton = (RadioButton)bindable;
            if(radioButton == null)
            {
                return;
            }

            if (newvalue != null && (Boolean)newvalue)
                radioButton.Image = radioButton.CheckedImage;
            else
                radioButton.Image = radioButton.UncheckedImage;
        }

        #endregion

        #region Private Methods

        private void RaiseCheckedChanged()
        {
            this.CheckedChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        public static BindableProperty CheckedProperty = BindableProperty.Create(nameof(Checked), typeof(bool), typeof(RadioButton), false, BindingMode.OneWay, null, OnCheckedChanged);
        public static BindableProperty CheckedImageProperty = BindableProperty.Create(nameof(CheckedImage), typeof(string), typeof(RadioButton), string.Empty);
        public static BindableProperty UncheckedImageProperty = BindableProperty.Create(nameof(UncheckedImage), typeof(string), typeof(RadioButton), string.Empty);

        public event EventHandler CheckedChanged;
    }
}