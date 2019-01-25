using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinFormsDemo.CustomControls
{
    public class DynamicListViewWithCommand<T>: ListView where T : class
    {
        private readonly ObservableCollection<T> observableList;

        /// <summary>
        /// The full items source property
        /// </summary>
        public static BindableProperty FullItemsSourceProperty = BindableProperty.Create
            (nameof(FullItemsSource), typeof(IList<T>), typeof(DynamicListViewWithCommand<T>), default(IList<T>), BindingMode.OneWay, null,
                OnFullItemsSourceChanged);

        public static readonly BindableProperty GetMoreElementsCommandProperty = BindableProperty.Create(
            nameof(GetMoreElementsCommand),
            typeof(ICommand),
            typeof(DynamicListViewWithCommand<T>),
            null,
            BindingMode.OneWay);

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicListViewWithCommand{T}"/> class.
        /// </summary>
        public DynamicListViewWithCommand()
        {
            this.observableList = new ObservableCollection<T>();
            this.ItemsSource = this.observableList;
            this.ItemAppearing += this.OnItemAppearing;
            this.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Gets or sets the full items source.
        /// </summary>
        /// <value>The full items source.</value>
        public IList<T> FullItemsSource
        {
            get
            {
                return (IList<T>)this.GetValue(FullItemsSourceProperty);
            }
            set
            {
                this.SetValue(FullItemsSourceProperty, value);
            }
        }

        /// <summary>
        /// Execute a Command that brings more results to the list
        /// </summary>
        /// <value>The get more elements command.</value>
        public ICommand GetMoreElementsCommand
        {
            get => (ICommand)GetValue(GetMoreElementsCommandProperty);
            set => SetValue(GetMoreElementsCommandProperty, value);
        }

        /// <summary>
        /// Called when [full items source changed].
        /// </summary>
        /// <param name="bindableElement">The bindable element.</param>
        /// <param name="oldvalue">The oldvalue.</param>
        /// <param name="newvalue">The newvalue.</param>
        private static void OnFullItemsSourceChanged(BindableObject bindableElement, object oldvalue, object newvalue)
        {
            var listView = bindableElement as DynamicListViewWithCommand<T>;
            listView?.OnFullItemsSourceChanged(listView);
        }

        /// <summary>
        /// Called when [full items source changed].
        /// </summary>
        /// <param name="listView">The list view.</param>
        private void OnFullItemsSourceChanged(DynamicListViewWithCommand<T> listView)
        {
            if (listView.FullItemsSource != null)
            {
                this.observableList.Clear();

                var items = listView.FullItemsSource.ToList();

                foreach (var elem in items)
                {
                    this.observableList.Add(elem);
                }
            }
        }

        /// <summary>
        /// Called when [item appearing].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        /// The <see cref="ItemVisibilityEventArgs"/> instance containing the event data.
        /// </param>
        private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var listView = sender as DynamicListViewWithCommand<T>;
            if (listView?.FullItemsSource == null || !listView.FullItemsSource.Any())
            {
                return;
            }

            var indexOfAppearedItem = listView.FullItemsSource.IndexOf((T)e.Item);
            var indexOfLatestItemInList = this.observableList.IndexOf((T)this.observableList.LastOrDefault());

            if (indexOfAppearedItem == indexOfLatestItemInList)
            {
                listView.GetMoreElementsCommand?.Execute(null);
            }
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var listView = sender as DynamicListViewWithCommand<T>;
            if (listView?.FullItemsSource == null || !listView.FullItemsSource.Any())
            {
                return;
            }

            this.observableList.Clear();
            foreach (var item in listView.FullItemsSource)
            {
                this.observableList.Add(item);
            }
        }

    }
}
