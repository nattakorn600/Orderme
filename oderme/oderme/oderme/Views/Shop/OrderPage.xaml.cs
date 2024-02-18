using Newtonsoft.Json;
using oderme.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oderme.Views.Shop
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrderPage : ContentPage
	{
		Orders ord;
		public OrderPage()
		{
			InitializeComponent();
			Loaddata("");
		}
		async void Loaddata(string id)
		{
			try
			{
				BindingContext = "";
				using (var cl = new HttpClient())
				{
					var formcontent = new FormUrlEncodedContent(new[]
					{
				new KeyValuePair<string,string>("shop_id", Application.Current.Properties["shop_id"].ToString()),
				new KeyValuePair<string,string>("order_id", id),
				});

					var request = await cl.PostAsync(Application.Current.Properties["domain"] +
						"/odermeApp/shop/doorder.php?", formcontent);

					request.EnsureSuccessStatusCode();
					var response = await request.Content.ReadAsStringAsync();
					ord = new Orders();
					ord = JsonConvert.DeserializeObject<Orders>(response);
					BindingContext = ord;
				}
			}
			catch 
			{
				//await Navigation.PopAsync();
			}
		}
		private void ArrowClick(object sender, EventArgs e)
		{
			var btn = (ImageButton)sender;
			MenuOrders listitem = (from itm in ord.Menus
							   where itm.Id == (int)(btn.CommandParameter)
							   select itm).FirstOrDefault<MenuOrders>();
			int ind = ord.Menus.IndexOf(listitem);
			if(listitem.VisibleOther == true)
			{
				listitem.Rotation = 90;
				listitem.VisibleOther = false;
			}
			else
			{
				listitem.Rotation = 270;
				listitem.VisibleOther = true;
			}
			ord.Menus[ind] = listitem;
		}
		private async void NextOrder(object sender, EventArgs e)
		{
            try
            {
				Loaddata(ord.Order_Id.ToString());
            }
			catch
            {
				string action = await DisplayActionSheet("", "ตกลง", "", "คุณยังไม่มีออเดอร์");
				if (action == "ตกลง")
				{
					Navigation.PopAsync();
				}
			}
			
		}
		private void BackButton(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}
	}
}