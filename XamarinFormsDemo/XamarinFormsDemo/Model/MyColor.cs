namespace XFDemo.Model
{
    using System;

    public class MyColor : IEquatable<MyColor>
    {
        #region Public Constructors

        public MyColor(string name, string hexadecimalValue)
        {
            this.Name = name;
            this.HexadecimalValue = hexadecimalValue;
        }

        public MyColor()
        {
        }

        public MyColor(string name, string hexadecimalValue, string url) : this(name, hexadecimalValue)
        {
            this.ImageUrl = url;
        }

        #endregion

        #region Public Properties

        public string HexadecimalValue
        {
            get;
            set;
        }

        public string ImageUrl
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        public bool Equals(MyColor other)
        {
            return string.Equals(this.Name, other.Name) && string.Equals(this.HexadecimalValue, other.HexadecimalValue);
        }

        #endregion
    }
}