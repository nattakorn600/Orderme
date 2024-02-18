using Android.Content;
using Android.Widget;
using oderme.Controls;
using oderme.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SearchPageSearchBar), typeof(SearchPageSearchBarRenderer))]

namespace oderme.Droid.Renderers
{
    public class SearchPageSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var plateId = Resources.GetIdentifier("android:id/search_plate", null, null);
                var plate = Control.FindViewById(plateId);
                plate.SetBackgroundColor(Android.Graphics.Color.Transparent);

                var searchView = base.Control as SearchView;
                int searchIconId = Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                ImageView searchViewIcon = (ImageView)searchView.FindViewById<ImageView>(searchIconId);
                searchViewIcon.SetColorFilter(Android.Graphics.Color.ParseColor("#C4C4C4"));
            }
        }
    }
}