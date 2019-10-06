using System;
using Xamarin.Forms;

namespace XFDemo.CustomControls
{
    public class ColoreableCell : ViewCell
    {
        public ColoreableCell()
        {
            this.Tapped += this.ColoredCell_Tapped;
        }

        public BindableProperty CellBackgroundColorProperty = BindableProperty.Create(nameof(CellBackgroundColor), typeof(string), typeof(ColoreableCell), Color.White, BindingMode.Default);

        public string CellBackgroundColor
        {
            get => (string)this.GetValue(this.CellBackgroundColorProperty);
            set => this.SetValue(this.CellBackgroundColorProperty, value);
        }

        private void ColoredCell_Tapped(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.CellBackgroundColor))
            {
                var color = Color.FromHex(this.CellBackgroundColor);
                this.View.BackgroundColor = color;
            }
        }
    }
}
