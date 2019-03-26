/*--------------------------------------------------------------------------------------------------------------------
 <copyright file="LabelRenderer" company="CodigoEdulis">
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
using XamarinFormsDemo.iOS.Renderers;
using CoreGraphics;

[assembly: ExportRenderer(typeof(Label), typeof(LabelCustomRenderer))]

namespace XamarinFormsDemo.iOS.Renderers
{
    using System;
    using UIKit;
    using Xamarin.Forms.Platform.iOS;

    public class LabelCustomRenderer : ViewRenderer<Label, UITextView>
    {
        public LabelCustomRenderer()
        {
        }
        UITextView uiTextView;

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var label = (Label)Element;
            if (label == null)
                return;

            if (Control == null)
            {
                uiTextView = new UITextView();
            }

            uiTextView.Selectable = true;
            uiTextView.Editable = false;
            uiTextView.ScrollEnabled = false;
            uiTextView.TextContainerInset = UIEdgeInsets.Zero;
            uiTextView.TextContainer.LineFragmentPadding = 0;

            // Initial properties Set
            uiTextView.Text = label.Text;
            switch (label.FontAttributes)
            {
                case FontAttributes.None:
                    uiTextView.Font = UIFont.SystemFontOfSize(new nfloat(label.FontSize));
                    break;
                case FontAttributes.Bold:
                    uiTextView.Font = UIFont.BoldSystemFontOfSize(new nfloat(label.FontSize));
                    break;
                case FontAttributes.Italic:
                    uiTextView.Font = UIFont.ItalicSystemFontOfSize(new nfloat(label.FontSize));
                    break;
                default:
                    uiTextView.Font = UIFont.BoldSystemFontOfSize(new nfloat(label.FontSize));
                    break;
            }


            var textColor = new Color(label.TextColor.R, label.TextColor.G, label.TextColor.B);
            uiTextView.TextColor = textColor.ToUIColor();

            var backgroundColor = new Color(label.BackgroundColor.R, label.BackgroundColor.G, label.BackgroundColor.B);
            uiTextView.BackgroundColor = label.BackgroundColor.ToUIColor();

            SetNativeControl(uiTextView);
        }
    }
}