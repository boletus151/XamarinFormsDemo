using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace XFDemo.CustomControls
{
    public class DynamicListViewWithCommand: ListView
    {
        public static readonly BindableProperty GetMoreElementsCommandProperty = BindableProperty.Create(
            nameof(GetMoreElementsCommand),
            typeof(ICommand),
            typeof(DynamicListViewWithCommand),
            null,
            BindingMode.OneWay);

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicListViewWithCommand"/> class.
        /// </summary>
        public DynamicListViewWithCommand()
        {
            this.ItemAppearing += this.OnItemAppearing;
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
        /// Called when [item appearing].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        /// The <see cref="ItemVisibilityEventArgs"/> instance containing the event data.
        /// </param>
        private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var listView = sender as DynamicListViewWithCommand;
            if (listView?.ItemsSource == null)
            {
                return;
            }

            var castedList = new List<object>();
            foreach (var item in listView.ItemsSource)
            {
                castedList.Add(item as object);
            }

            if (castedList != null)
            {
                var itemApearingIndex = castedList.IndexOf(e.Item);
                var listLastItemIndex = castedList.IndexOf(castedList.LastOrDefault());


                if (itemApearingIndex == listLastItemIndex)
                {
                    listView.GetMoreElementsCommand.Execute(null);
                }

            }
        }

    }
}
