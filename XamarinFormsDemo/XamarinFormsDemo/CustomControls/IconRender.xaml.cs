namespace XamarinFormsDemo.CustomControls
{
    using Xamarin.Forms;

    public partial class IconRender : Label
    {
        private const int asciiCharacter = 153;

        public string IconFont
        {
            get
            {
                return (string)GetValue(IconFontProperty);
            }
            set
            {
                SetValue(IconFontProperty, value);
            }
        }

        private static char defaultIcon => (char)asciiCharacter;

        public static readonly BindableProperty IconFontProperty = BindableProperty.Create
            (nameof(IconFont), typeof(string), typeof(IconRender), defaultIcon.ToString());
    }
}