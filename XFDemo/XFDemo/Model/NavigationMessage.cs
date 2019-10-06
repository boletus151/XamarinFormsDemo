namespace XFDemo.Model
{
    using GalaSoft.MvvmLight.Messaging;

    public abstract class NavigationMessage : MessageBase
    {
        #region Protected Constructors

        protected NavigationMessage(object sender, object target)
            : base(sender, target)
        {
        }

        #endregion
    }

    public class MyColorNavigationMessage : NavigationMessage
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MyColorNavigationMessage"/> class.
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

        #endregion

        #region Public Properties

        /// <value>The MyColor.</value>
        public MyColor MyColor { get; set; }

        #endregion
    }

    public class LoadDataNavigationMessage : NavigationMessage
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MyColorNavigationMessage"/> class.
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

        #endregion

        #region Public Properties

        /// <value>The MyColor.</value>
        public bool LoadData { get; set; }

        #endregion
    }
}