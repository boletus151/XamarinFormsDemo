/*--------------------------------------------------------------------------------------------------------------------
 <copyright file="LabelCustomRenderer" company="CodigoEdulis">
   Código Edulis 2016
   http://www.codigoedulis.es
 </copyright>
 <summary>
    This implementation is a group of the offers of several persons along the network; because of
    this, it is under Creative Common By License:
    
    You are free to:

    Share — copy and redistribute the material in any medium or format
    Adapt — remix, transform, and build upon the material for any purpose, even commercially.
    
    The licensor cannot revoke these freedoms as long as you follow the license terms.
    
    Under the following terms:
    
    Attribution — You must give appropriate credit, provide a link to the license, and indicate if changes were made. You may do so in any reasonable manner, but not in any way that suggests the licensor endorses you or your use.
    No additional restrictions — You may not apply legal terms or technological measures that legally restrict others from doing anything the license permits.
 
 </summary>
--------------------------------------------------------------------------------------------------------------------*/

using Xamarin.Forms;
using XamarinFormsDemo.Droid.Renderers;
using Android.Graphics;
using Java.Lang;

[assembly: ExportRenderer(typeof(Label), typeof(LabelCustomRenderer))]

namespace XamarinFormsDemo.Droid.Renderers
{
    using Android.Content;
    using Android.Widget;
    using Xamarin.Forms.Platform.Android;

    public class LabelCustomRenderer : ViewRenderer<Label, TextView>
    {
        TextView textView;

        public LabelCustomRenderer(Context context) : base(context)
        {

        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            SetLabelSelectable();

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            SetLabelSelectable();
        }

        private void SetLabelSelectable()
        {
            var label = (Label)Element;
            if (label == null)
            {
                return;
            }

            if (Control == null)
            {
                textView = new TextView(this.Context);
            }

            textView.Enabled = true;
            textView.Focusable = true;
            textView.LongClickable = true;
            textView.SetTextIsSelectable(true);

            // Initial properties Set
            textView.Text = label.Text;

            switch (label.FontAttributes)
            {
                case FontAttributes.None:
                    textView.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    break;
                case FontAttributes.Bold:
                    textView.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    break;
                case FontAttributes.Italic:
                    textView.SetTypeface(null, Android.Graphics.TypefaceStyle.Italic);
                    break;
                default:
                    textView.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    break;
            }

            textView.TextSize = (float)label.FontSize;

            var androidC1 = label.TextColor.ToAndroid();
            var textColor1 = new Android.Graphics.Color(androidC1.R, androidC1.G, androidC1.B);
            textView.SetTextColor(textColor1);

            var androidC2 = label.BackgroundColor.ToAndroid();
            var textColor2 = new Android.Graphics.Color(androidC2.R, androidC2.G, androidC2.B);
            textView.SetTextColor(textColor2);

            SetNativeControl(textView);
        }
    }
}