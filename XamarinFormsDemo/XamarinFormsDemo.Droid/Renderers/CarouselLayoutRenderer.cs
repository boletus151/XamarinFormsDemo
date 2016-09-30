using XamarinFormsDemo.CustomControls;
using XamarinFormsDemo.Droid.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(CarouselLayout), typeof(CarouselLayoutRenderer))]

namespace XamarinFormsDemo.Droid.Renderers
{
    using System.ComponentModel;
    using System.Reflection;
    using System.Timers;
    using Android.Graphics;
    using Android.Views;
    using Android.Widget;
    using Java.Lang;
    using Xamarin.Forms.Platform.Android;

    public class CarouselLayoutRenderer : ScrollViewRenderer
    {
        private int _deltaX;

        private Timer _deltaXResetTimer;

        private bool _initialized;

        private bool _motionDown;

        private int _prevScrollX;

        private Timer _scrollStopTimer;

        private HorizontalScrollView _scrollView;

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            if(this._initialized)
                return;
            this._initialized = true;
            var carouselLayout = (CarouselLayout)this.Element;
            this._scrollView.ScrollTo(carouselLayout.SelectedIndex * this.Width, 0);
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if(e.NewElement == null)
                return;

            this._deltaXResetTimer = new Timer(100)
            {
                AutoReset = false
            };
            this._deltaXResetTimer.Elapsed += (object sender, ElapsedEventArgs args) => this._deltaX = 0;

            this._scrollStopTimer = new Timer(200)
            {
                AutoReset = false
            };
            this._scrollStopTimer.Elapsed += (object sender, ElapsedEventArgs args2) => this.UpdateSelectedIndex();

            e.NewElement.PropertyChanged += ElementPropertyChanged;
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            if(this._initialized && (w != oldw))
            {
                this._initialized = false;
            }
            base.OnSizeChanged(w, h, oldw, oldh);
        }

        private void ElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Renderer")
            {
                this._scrollView = (HorizontalScrollView)typeof(ScrollViewRenderer).GetField("_hScrollView", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);

                this._scrollView.HorizontalScrollBarEnabled = false;
                this._scrollView.Touch += this.HScrollViewTouch;
            }
            if(e.PropertyName == CarouselLayout.SelectedIndexProperty.PropertyName && !this._motionDown)
            {
                this.ScrollToIndex(((CarouselLayout)this.Element).SelectedIndex);
            }
        }

        private void HScrollViewTouch(object sender, TouchEventArgs e)
        {
            e.Handled = false;

            switch(e.Event.Action)
            {
                case MotionEventActions.Move:
                    this._deltaXResetTimer.Stop();
                    this._deltaX = this._scrollView.ScrollX - this._prevScrollX;
                    this._prevScrollX = this._scrollView.ScrollX;

                    this.UpdateSelectedIndex();

                    this._deltaXResetTimer.Start();
                    break;
                case MotionEventActions.Down:
                    this._motionDown = true;
                    this._scrollStopTimer.Stop();
                    break;
                case MotionEventActions.Up:
                    this._motionDown = false;
                    this.SnapScroll();
                    this._scrollStopTimer.Start();
                    break;
            }
        }

        private void ScrollToIndex(int targetIndex)
        {
            var targetX = targetIndex * this._scrollView.Width;
            this._scrollView.Post(new Runnable(() =>
            {
                this._scrollView.SmoothScrollTo(targetX, 0);
            }));
        }

        private void SnapScroll()
        {
            var roughIndex = (float)this._scrollView.ScrollX / this._scrollView.Width;

            var targetIndex = this._deltaX < 0 ? Math.Floor(roughIndex) : this._deltaX > 0 ? Math.Ceil(roughIndex) : Math.Round(roughIndex);

            this.ScrollToIndex((int)targetIndex);
        }

        private void UpdateSelectedIndex()
        {
            var center = this._scrollView.ScrollX + (this._scrollView.Width / 2);
            var carouselLayout = (CarouselLayout)this.Element;
            carouselLayout.SelectedIndex = (center / this._scrollView.Width);
        }
    }
}