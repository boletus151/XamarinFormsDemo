/*--------------------------------------------------------------------------------------------------------------------
 <copyright file="DynamicListView" company="CodigoEdulis">
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

using System;
using Xamarin.Forms;

namespace XFDemo.CustomControls
{
    public class ColoreableCell : ViewCell
    {
        public ColoreableCell()
        {
            this.Tapped += this.ColoredCell_Tapped;
        }

        public BindableProperty CellBackgroundColorProperty = BindableProperty.Create(nameof(CellBackgroundColor), typeof(string), typeof(ColoreableCell), Color.White, BindingMode.Default);

        public string CellBackgroundColor
        {
            get => (string)this.GetValue(this.CellBackgroundColorProperty);
            set => this.SetValue(this.CellBackgroundColorProperty, value);
        }

        private void ColoredCell_Tapped(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.CellBackgroundColor))
            {
                var color = Color.FromHex(this.CellBackgroundColor);
                this.View.BackgroundColor = color;
            }
        }
    }
}
