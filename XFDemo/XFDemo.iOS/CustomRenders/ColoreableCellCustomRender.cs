using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFDemo.CustomControls;
using XFDemo.iOS.CustomRenders;

[assembly: ExportRenderer(typeof(ColoreableCell), typeof(ColoreableCellCustomRender))]
namespace XFDemo.iOS.CustomRenders
{
    public class ColoreableCellCustomRender : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            var view = item as ColoreableCell;

            cell.SelectedBackgroundView = new UIView()
            {
                BackgroundColor = view.CellBackgroundColor.ToUIColor()
            };

            return cell;
        }
    }
}
