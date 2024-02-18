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
	public partial class OrderHistoryPage : ContentPage
	{
		public ObservableCollection<ShopOrderHistory> Data { get; set; }
		/*public ObservableCollection<ShopOrderHistory> Data { get; set; } = new ObservableCollection<ShopOrderHistory>()
		{
			new ShopOrderHistory
			{
				Date = "05 ต.ค. 2564",
				Menus = new ObservableCollection<Orders>
				{
					new Orders
					{
						Order_Id = "156598958898",
						Date = "05 ต.ค. 2564 - 06.00น.",
						Price = 100,
						Table = 3,
						Number = 5,
						Payment = "Promptpay",
						Rotation = 90,
						Visible = false
					},
					new Orders
					{
						Order_Id = "565598958568",
						Date = "05 ต.ค. 2564 - 10.00น.",
						Price = 150,
						Table = 1,
						Number = 3,
						Payment = "Promptpay",
						Rotation = 90,
						Visible = false
					},
					new Orders
					{
						Order_Id = "9895684651155",
						Date = "05 ต.ค. 2564 - 12.00น.",
						Price = 200,
						Table = 2,
						Number = 4,
						Payment = "Promptpay",
						Rotation = 90,
						Visible = false
					}
				}
			},
			new ShopOrderHistory
			{
				Date = "04 ต.ค. 2564",
				Menus = new ObservableCollection<Orders>
				{
					new Orders
					{
						Order_Id = "156598958894",
						Date = "04 ต.ค. 2564 - 06.00น.",
						Price = 100,
						Table = 3,
						Number = 5,
						Payment = "Promptpay",
						Rotation = 90,
						Visible = false
					},
					new Orders
					{
						Order_Id = "565598958564",
						Date = "04 ต.ค. 2564 - 10.00น.",
						Price = 150,
						Table = 1,
						Number = 3,
						Payment = "Promptpay",
						Rotation = 90,
						Visible = false
					},
					new Orders
					{
						Order_Id = "9895684651154",
						Date = "04 ต.ค. 2564 - 12.00น.",
						Price = 200,
						Table = 2,
						Number = 4,
						Payment = "Promptpay",
						Rotation = 90,
						Visible = false
					}
				}
			},
			new ShopOrderHistory
			{
				Date = "03 ต.ค. 2564",
				Menus = new ObservableCollection<Orders>
				{
					new Orders
					{
						Order_Id = "156598958893",
						Date = "03 ต.ค. 2564 - 06.00น.",
						Price = 100,
						Table = 3,
						Number = 5,
						Payment = "Promptpay",
						Rotation = 90,
						Visible = false
					},
					new Orders
					{
						Order_Id = "565598958563",
						Date = "03 ต.ค. 2564 - 10.00น.",
						Price = 150,
						Table = 1,
						Number = 3,
						Payment = "Promptpay",
						Rotation = 90,
						Visible = false
					},
					new Orders
					{
						Order_Id = "9895684651153",
						Date = "03 ต.ค. 2564 - 12.00น.",
						Price = 200,
						Table = 2,
						Number = 4,
						Payment = "Promptpay",
						Rotation = 90,
						Visible = false
					}
				}
			}
		};*/
		public OrderHistoryPage()
		{
			InitializeComponent();
			Loaddata();
		}
		async void Loaddata()
		{
			using (var cl = new HttpClient())
			{
				var formcontent = new FormUrlEncodedContent(new[]
				{
				new KeyValuePair<string,string>("shop_id", Application.Current.Properties["shop_id"].ToString()),
				});

				var request = await cl.PostAsync(Application.Current.Properties["domain"] +
					"/odermeApp/shop/gethistory.php?", formcontent);

				request.EnsureSuccessStatusCode();
				var response = await request.Content.ReadAsStringAsync();
				Data = new ObservableCollection<ShopOrderHistory>();
				Data = JsonConvert.DeserializeObject<ObservableCollection<ShopOrderHistory>>(response);
				BindingContext = this;
			}
		}
		private void BackButton(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}
		private void ArrowClick(object sender, EventArgs e)
		{
			ImageButton btn = (ImageButton)sender;

			for(int i=0; i<Data.Count; i++)
			{
				var listitem = (from itm in Data[i].Menus
								where itm.Order_Id == btn.CommandParameter.ToString()
								select itm).FirstOrDefault<Orders>();
				if(listitem != null)
				{
					int ind = Data[i].Menus.IndexOf(listitem);
					if (listitem.Visible == false)
					{
						listitem.Visible = true;
						listitem.Rotation = 270;
					}
					else
					{
						listitem.Visible = false;
						listitem.Rotation = 90;
					}
					Data[i].Menus[ind] = listitem;
					return;
				}
			}
		}
	}
}