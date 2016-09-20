/*--------------------------------------------------------------------------------------------------------------------
 <copyright file="ObjectBindableCollection" company="CodigoEdulis">
   Código Edulis 2016
   http://www.codigoedulis.es
 </copyright>
 <summary>
    This implementation is a group of the offers of several persons along the network;
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

namespace XamarinFormsDemo.CustomControls
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public class InfiniteListView<T> : ListView where T : class
    {
        private readonly ObservableCollection<T> observableList;

        /// <summary>
        /// Initializes a new instance of the <see cref="InfiniteListView{T}"/> class.
        /// </summary>
        public InfiniteListView()
        {
            this.observableList = new ObservableCollection<T>();
            this.ItemsSource = this.observableList;
            this.ItemAppearing += this.OnItemAppearing;
        }

        /// <summary>
        /// Gets or sets the full items source.
        /// </summary>
        /// <value>
        /// The full items source.
        /// </value>
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
        /// Gets or sets the size of the page, i.e the number of items per load.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public string PageSize
        {
            get
            {
                return (string)this.GetValue(PageSizeProperty);
            }
            set
            {
                this.SetValue(PageSizeProperty, value);
            }
        }

        /// <summary>
        /// Called when [full items source changed].
        /// </summary>
        /// <param name="bindableElement">The bindable element.</param>
        /// <param name="oldvalue">The oldvalue.</param>
        /// <param name="newvalue">The newvalue.</param>
        private static void OnFullItemsSourceChanged(BindableObject bindableElement, object oldvalue, object newvalue)
        {
            var listView = bindableElement as InfiniteListView<T>;
            listView?.OnFullItemsSourceChanged(listView);
        }

        /// <summary>
        /// Called when [full items source changed].
        /// </summary>
        /// <param name="listView">The list view.</param>
        private void OnFullItemsSourceChanged(InfiniteListView<T> listView)
        {
            var numberOfItems = int.Parse(listView.PageSize);
            if(numberOfItems <= 0)
            {
                this.observableList.Clear();
            }
            else
            {
                if (numberOfItems > listView.FullItemsSource.Count)
                {
                    numberOfItems = listView.FullItemsSource.Count;
                }
                var items = listView.FullItemsSource.ToList().GetRange(0, numberOfItems);
                foreach (var elem in items)
                {
                    observableList.Add(elem);
                }
            }
        }

        /// <summary>
        /// Called when [item appearing].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ItemVisibilityEventArgs"/> instance containing the event data.</param>
        private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var listView = sender as InfiniteListView<T>;
            if(listView?.FullItemsSource == null || (this.observableList.Count == listView.FullItemsSource.Count))
            {
                return;
            }

            var indexOfItem = this.observableList.IndexOf((T)e.Item);
            var numberOfItems = int.Parse(listView.PageSize);

            if((indexOfItem > -1) && (e.Item == this.observableList.LastOrDefault()))
            {
                if (numberOfItems > listView.FullItemsSource.Count)
                {
                    numberOfItems = listView.FullItemsSource.Count;
                }

                var observableItemsCount = this.observableList.Count;
                var fullItemsCount = listView.FullItemsSource.Count;
                
                List<T> items;
                try
                {
                    items = listView.FullItemsSource.ToList().GetRange(this.observableList.Count, numberOfItems);

                }
                catch (System.ArgumentOutOfRangeException)
                {
                    items = listView.FullItemsSource.ToList().GetRange(this.observableList.Count, fullItemsCount - observableItemsCount);
                }
                foreach (var elem in items)
                {
                    observableList.Add(elem);
                }
            }
        }

        /// <summary>
        /// The full items source property
        /// </summary>
        public static BindableProperty FullItemsSourceProperty = BindableProperty.Create(nameof(FullItemsSource),
             typeof(IList<T>),typeof(InfiniteListView<T>),default(IList<T>),BindingMode.OneWay,null,OnFullItemsSourceChanged);

        /// <summary>
        /// The page size property
        /// </summary>
        public static BindableProperty PageSizeProperty = BindableProperty.Create
            (nameof(PageSize), typeof(string), typeof(InfiniteListView<T>), string.Empty);
    }
}