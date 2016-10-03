/*--------------------------------------------------------------------------------------------------------------------
 <copyright file="CarouselLayoutRenderer" company="CodigoEdulis">
   Código Edulis 2016
   http://www.codigoedulis.es
 </copyright>
 <summary>
    This implementation is based on: http://chrisriesgo.com/xamarin-forms-carousel-view-recipe/;
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
using CustomLayouts.iOS.Renderers;
using Xamarin.Forms;
using XamarinFormsDemo.CustomControls;

[assembly: ExportRenderer(typeof(CarouselLayout), typeof(CarouselLayoutRenderer))]

namespace CustomLayouts.iOS.Renderers
{
    using System;
    using System.ComponentModel;
    using UIKit;
    using Xamarin.Forms.Platform.iOS;

    public class CarouselLayoutRenderer : ScrollViewRenderer
    {
        private UIScrollView _native;

        public CarouselLayoutRenderer()
        {
            PagingEnabled = true;
            ShowsHorizontalScrollIndicator = false;
        }

        public override void Draw(CoreGraphics.CGRect rect)
        {
            base.Draw(rect);
            ScrollToSelection(false);
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if(e.OldElement != null)
                return;

            _native = (UIScrollView)NativeView;
            _native.Scrolled += NativeScrolled;
            e.NewElement.PropertyChanged += ElementPropertyChanged;
        }

        private void ElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == CarouselLayout.SelectedIndexProperty.PropertyName && !Dragging)
            {
                ScrollToSelection(false);
            }
        }

        private void NativeScrolled(object sender, EventArgs e)
        {
            var center = _native.ContentOffset.X + (_native.Bounds.Width / 2);
            ((CarouselLayout)Element).SelectedIndex = ((int)center) / ((int)_native.Bounds.Width);
        }

        private void ScrollToSelection(bool animate)
        {
            if(Element == null)
                return;

            _native.SetContentOffset
                (
                    new CoreGraphics.CGPoint(_native.Bounds.Width * Math.Max(0, ((CarouselLayout)Element).SelectedIndex), _native.ContentOffset.Y),
                    animate);
        }
    }
}