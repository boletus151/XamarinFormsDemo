using Xamarin.Forms;

namespace XFDemo.CustomControls
{
    public class ColoreableCell : ViewCell
    {
        public BindableProperty CellBackgroundColorProperty = BindableProperty.Create(nameof(CellBackgroundColor), typeof(Color), typeof(ColoreableCell), Color.Default);

        public ColoreableCell()
        {
        }

        public Color CellBackgroundColor
        {
            get => (Color)this.GetValue(this.CellBackgroundColorProperty);
            set => this.SetValue(this.CellBackgroundColorProperty, value);
        }
    }
}
