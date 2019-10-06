namespace XFDemo.CustomControls.Carousel
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public abstract class CarouselView : StackLayout
    {
        /*public ICommand Command
        {
            get
            {
                return (ICommand)this.GetValue(CommandProperty);
            }
            set
            {
                this.SetValue(CommandProperty, value);
            }
        }

        public object CommandParameter
        {
            get
            {
                return this.GetValue(CommandParameterProperty);
            }
            set
            {
                this.SetValue(CommandParameterProperty, value);
            }
        }
        
        public bool IsBusy
        {
            get
            {
                return (bool)this.GetValue(IsBusyProperty);
            }
            set
            {
                this.SetValue(IsBusyProperty, value);
            }
        }
        
        public IEnumerable<object> Items
        {
            get
            {
                return (IEnumerable<object>)this.GetValue(ItemsProperty);
            }
            set
            {
                this.SetValue(ItemsProperty, value);
            }
        }
        
        public DataTemplate Template
        {
            get
            {
                return (DataTemplate)this.GetValue(TemplateProperty);
            }
            set
            {
                this.SetValue(TemplateProperty, value);
            }
        }
        
        public void SelectOneItem(ICarouselIconItem item)
        {
            foreach(var i in this.Items.OfType<ICarouselIconItem>())
            {
                i.IsSelected = (item == i);
            }
        }

        protected static void CommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var carousel = bindable as CarouselView;
            carousel?.RefreshItemContainer();
        }

        protected static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var carousel = bindable as CarouselView;
            if(carousel == null)
                return;

            carousel.RefreshItemContainer();

            var items = newValue as INotifyCollectionChanged;
            if(items == null)
                return;

            items.CollectionChanged += (s, e) =>
            {
                carousel.RefreshItemContainer();
            };
        }

        protected virtual void RefreshItemContainer()
        {
        }

        protected static void TemplatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var carousel = bindable as CarouselView;
            carousel?.RefreshItemContainer();
        }

        private static void CommandParameterChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var carousel = bindable as CarouselView;
            carousel?.RefreshItemContainer();
        }

        public static readonly BindableProperty ItemsProperty = BindableProperty.Create
            (nameof(Items), typeof(IEnumerable<object>), typeof(CarouselView), null, BindingMode.Default, null, ItemsSourceChanged);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create
            (nameof(Command), typeof(ICommand), typeof(CarouselView), null, BindingMode.Default, null, CommandPropertyChanged);

        public static readonly BindableProperty TemplateProperty = BindableProperty.Create
            (nameof(Template), typeof(DataTemplate), typeof(CarouselView), null, BindingMode.Default, null, TemplatePropertyChanged);

        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create
            (nameof(IsBusy), typeof(bool), typeof(CarouselView), false, BindingMode.Default);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create
            (nameof(CommandParameter), typeof(object), typeof(CarouselView), null, BindingMode.Default, null, CommandParameterChanged);
    */}
}