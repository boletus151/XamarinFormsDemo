using System;

namespace XamarinFormsDemo.Model
{
    public class MyColor : IEquatable<MyColor>
    {
        public MyColor(string name, string hexadecimalValue)
        {
            this.Name = name;
            this.HexadecimalValue = hexadecimalValue;
        }

        public string Name { get; set; }

        public string HexadecimalValue { get; set; }

        public bool Equals(MyColor other)
        {
            return string.Equals(this.Name, other.Name) && string.Equals(this.HexadecimalValue, other.HexadecimalValue);
        }
    }
}
