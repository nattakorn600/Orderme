using oderme.Models;
using oderme.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace oderme.ViewModels
{
	public class MapPageViewModel : ContentPage
	{
        public ObservableCollection<CustomPin> pin = new ObservableCollection<CustomPin>();
        CustomMap map = new CustomMap();
        
        
        public MapPageViewModel(CarouselPage page,ObservableCollection<ShopData> item)
		{
			map.CustomPins = new List<CustomPin>();
			map.IsShowingUser = true;
			map.MapType = MapType.Street;
			map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(13.7543822883747, 100.500995479524), Distance.FromMiles(100)));
			PositionUser();
            if(item != null)
			{
                Pin(item);
            }
 
            ImageButton backbtn = new ImageButton {
                Padding = 10,
                BackgroundColor = Color.White,
                Source = "backbutton.png"
            };
            backbtn.Clicked += async (sender, args) => page.CurrentPage = page.Children[1];
            Frame frame = new Frame {
                Margin = new Thickness(10, 50, 0, 0),
                Padding = 0,
                BackgroundColor = Color.White,
                HasShadow = false,
                CornerRadius = 50,
                HeightRequest = 40,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Content = backbtn
            };
            
            Content = new Grid()
            {
                Children = { map, frame }
            };
		}
        
        void Pin(ObservableCollection<ShopData> mappin)
        {
            for (int i = 0; i < mappin.Count; i++)
            {
                pin.Add(new CustomPin
                {
                    Type = PinType.Place,
                    Position = new Position(mappin[i].Latitude, mappin[i].Longitude),
                    Label = mappin[i].Name,
                    Address = mappin[i].Address,
                    Shop_Data = mappin[i],
                    Image = mappin[i].Image,
                    Distance = mappin[i].Distance,
                    Unit = mappin[i].Unit
                });

                map.Pins.Add(pin[i]);
                map.CustomPins.Add(pin[i]);
            }
        }
        async void PositionUser()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var position = await Geolocation.GetLocationAsync(request);
            int discircle = Convert.ToInt32(Application.Current.Properties["distance"])*1000;
            Circle circle = new Circle
            {
                Center = new Position(position.Latitude, position.Longitude),
                Radius = new Distance(discircle),
                StrokeColor = Color.FromHex("#88FF7A21"),
                StrokeWidth = 5,
                FillColor = Color.FromHex("#11FF7A21")
            };
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMeters(200)));
            map.MapElements.Add(circle);
        }
    }
}
