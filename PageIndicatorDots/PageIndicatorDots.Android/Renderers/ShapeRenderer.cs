using System.ComponentModel;
using PageIndicatorDots.Controls;
using PageIndicatorDots.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer (typeof(ShapeView), typeof(ShapeRenderer))]
namespace PageIndicatorDots.Droid.Renderers
{
	public class ShapeRenderer : ViewRenderer<ShapeView, Shape>
	{
		public ShapeRenderer ()
		{
		}

	    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
	    {
	        base.OnElementPropertyChanged(sender, e);

	        if (Element == null)
	            return;

	        SetNativeControl(new Shape(Resources.DisplayMetrics.Density, Context)
	        {
	            ShapeView = Element
	        });
        }

	    protected override void OnElementChanged (ElementChangedEventArgs<ShapeView> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null || this.Element == null)
				return;

			SetNativeControl (new Shape (Resources.DisplayMetrics.Density, Context) {
				ShapeView = Element
			});
		}
	}
}