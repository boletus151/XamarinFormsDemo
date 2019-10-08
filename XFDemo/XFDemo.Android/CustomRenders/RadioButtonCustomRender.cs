using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFDemo.CustomControls;
using XFDemo.Droid.CustomRenders;

[assembly: ExportRenderer(typeof(RadioButton), typeof(RadioButtonCustomRender))]
namespace XFDemo.Droid.CustomRenders
{
    public class RadioButtonCustomRender : ImageButtonRenderer
    {
        public RadioButtonCustomRender(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<ImageButton> e)
        {
            var xfControl = e.NewElement as RadioButton;
            if (xfControl != null)
            {
                base.OnElementChanged(e);
                Element.HeightRequest = xfControl.ImageHeight;
                Element.WidthRequest = xfControl.ImageWidth;
            }
        }
    }
}
