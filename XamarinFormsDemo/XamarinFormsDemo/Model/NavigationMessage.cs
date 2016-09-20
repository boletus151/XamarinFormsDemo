namespace XamarinFormsDemo.Model
{
    using GalaSoft.MvvmLight.Messaging;

    public abstract class NavigationMessage : MessageBase
    {
        protected NavigationMessage(object sender, object target)
            : base(sender, target)
        {
        }
    }

    public class MyColorNavigationMessage : NavigationMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MyColorNavigationMessage" /> class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="target">The target.</param>
        /// <param name="myColor">The MyColor.</param>
        public MyColorNavigationMessage(object sender, object target, MyColor myColor)
            : base(sender, target)
        {
            Sender = sender;
            Target = target;
            this.MyColor = myColor;
        }

        /// <value>
        ///     The MyColor.
        /// </value>
        public MyColor MyColor { get; set; }
    }

    public class LoadDataNavigationMessage : NavigationMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyColorNavigationMessage" /> class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="target">The target.</param>
        /// <param name="loadData">if set to <c>true</c> [load data].</param>
        public LoadDataNavigationMessage(object sender, object target, bool loadData)
            : base(sender, target)
        {
            Sender = sender;
            Target = target;
            this.LoadData = loadData;
        }

        /// <value>
        ///     The MyColor.
        /// </value>
        public bool LoadData { get; set; }
    }
}