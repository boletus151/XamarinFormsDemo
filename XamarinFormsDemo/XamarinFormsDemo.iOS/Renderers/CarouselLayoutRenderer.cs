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

using Xamarin.Forms;
using XamarinFormsDemo.CustomControls;
using XamarinFormsDemo.iOS.Renderers;

[assembly: ExportRenderer(typeof(SwipeCarousel), typeof(CarouselLayoutRenderer))]

namespace XamarinFormsDemo.iOS.Renderers
{
    using System;
    using System.ComponentModel;
    using UIKit;
    using Xamarin.Forms.Platform.iOS;
    using CustomControls;

    public class CarouselLayoutRenderer : ScrollViewRenderer
    {
        UIScrollView _native;

        public CarouselLayoutRenderer()
        {
            this.PagingEnabled = true;
            this.ShowsHorizontalScrollIndicator = false;
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null) return;

            this._native = (UIScrollView)this.NativeView;
            this._native.Scrolled += this.NativeScrolled;
            e.NewElement.PropertyChanged += ElementPropertyChanged;
        }

        void NativeScrolled(object sender, EventArgs e)
        {
            var center = this._native.ContentOffset.X + (this._native.Bounds.Width / 2);
            ((XamarinFormsDemo.CustomControls.SwipeCarousel)this.Element).SelectedIndex = ((int)center) / ((int)this._native.Bounds.Width);
        }

        void ElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == SwipeCarousel.SelectedIndexProperty.PropertyName && !this.Dragging)
            {
                this.ScrollToSelection(false);
            }
        }

        void ScrollToSelection(bool animate)
        {
            if (this.Element == null) return;

            this._native.SetContentOffset(new CoreGraphics.CGPoint
                (this._native.Bounds.Width *
                    Math.Max(0, ((SwipeCarousel)this.Element).SelectedIndex),
                    this._native.ContentOffset.Y),
                animate);
        }

        public override void Draw(CoreGraphics.CGRect rect)
        {
            base.Draw(rect);
            this.ScrollToSelection(false);
        }
    }
}