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
    public partial class HomePage : ContentPage
    {
        CarouselPage carpage;
        public ShopInfo shopdata;
        public ObservableCollection<ObservableCollection<Orders>> Order;

        public HomePage(CarouselPage page, ShopInfo shopinfo)
        {
            InitializeComponent();
            carpage = page;
            shopdata = shopinfo;
            BindingContext = shopdata;
        }
        
        async void LoadOrder()
        {
			try
			{
                using (var cl = new HttpClient())
                {
                    var formcontent = new FormUrlEncodedContent(new[]
                    {
                new KeyValuePair<string,string>("shop_id", Application.Current.Properties["shop_id"].ToString()),
                });

                    var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                        "/odermeApp/shop/getorders.php?", formcontent);

                    request.EnsureSuccessStatusCode();
                    var response = await request.Content.ReadAsStringAsync();
                    Order = JsonConvert.DeserializeObject<ObservableCollection<ObservableCollection<Orders>>>(response);

                     if (Order.Count <= 0)
                     {
                         PayNumber.Text = "0";
                         OrderNumber.Text = "0";
                     }
                     else
                     {
                         PayNumber.Text = Order[0].Count.ToString();
                         OrderNumber.Text = Order[1].Count.ToString();
                         if (Order[0].Count <= 0 || Order[0].Count.ToString() == "")
                         {
                             PayNumber.Text = "0";
                         }
                         if (Order[1].Count <= 0 || Order[1].Count.ToString() == "")
                         {
                             OrderNumber.Text = "0";
                         }
                     }
                }
            }
			catch { }
        }
        async void LoadData()
		{
            try
            {
                using (var cl = new HttpClient())
                {
                    var formcontent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string,string>("shop_id", Application.Current.Properties["shop_id"].ToString()),
                    });

                    var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                        "/odermeApp/shop/shopinfo.php?", formcontent);

                    request.EnsureSuccessStatusCode();
                    var response = await request.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<ShopInfo>(response);

                    if (res != null)
                    {
                        if (res.MenuList.Count > shopdata.MenuList.Count)
                        {
                            for (int i = 0; i < res.MenuList.Count; i++)
                            {
                                var Isitem = (from itm in shopdata.MenuList
                                                where itm.Id == res.MenuList[i].Id
                                                select itm).FirstOrDefault<Models.Menu>();
                                if (Isitem == null)
                                {
                                    shopdata.MenuList.Add(res.MenuList[i]);
                                }
                            }
                        }
                        else if (res.MenuList.Count < shopdata.MenuList.Count)
                        {
                            for (int i = 0; i < shopdata.MenuList.Count; i++)
                            {
                                var Isitem = (from itm in res.MenuList
                                              where itm.Id == shopdata.MenuList[i].Id
                                              select itm).FirstOrDefault<Models.Menu>();
                                if (Isitem == null)
                                {
                                    shopdata.MenuList.Remove(shopdata.MenuList[i]);
                                }
                            }
                        }                   
                    }
                }
            }
            catch { }
        }
        protected override void OnAppearing()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(600), () =>
            {
                LoadData();
                LoadOrder();
                return true;
            });
        }
        private void BackButton(object sender, EventArgs e)
        {
            //carpage.CurrentPage = 
        }

        private void GoToCart(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private async void GoToOrder(object sender, EventArgs e)
        {
            if (OrderNumber.Text == "0")
            {
                string action = await DisplayActionSheet("", "ตกลง", "", "คุณยังไม่มีออเดอร์");
                if (action == "ตกลง")
                {
                }
            }
            else
            {
                await Navigation.PushAsync(new OrderPage());
            }
            
        }

        private void GoToInfoAndReview(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void RecommendMenu(object sender, EventArgs e)
        {

        }

        private void SelectMenu(object sender, EventArgs e)
        {

        }

        private void GotoProfileShop(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new ProfileShopPage(shopdata));
            carpage.CurrentPage = carpage.Children[0];
        }
       
        async void SelectClose(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;

            var ges = frame.GestureRecognizers;
            TapGestureRecognizer tap = (TapGestureRecognizer)ges[0];
            var items = (from itm in shopdata.MenuList
                             where itm.Id == int.Parse(tap.CommandParameter.ToString())
                             select itm).FirstOrDefault<Models.Menu>();
            BoxView item = (BoxView)frame.Content;

            int stat = 0;
            int j = 0;
            for(int i=0; i<shopdata.MenuList.Count; i++)
			{
                if(items.Id == shopdata.MenuList[i].Id)
				{
                    j = i;
                    if(shopdata.MenuList[i].Status == true)
					{
                        stat = 0;
					}
					else
					{
                        stat = 1;
					}
				}
			}
            
            using (var cl = new HttpClient())
            {
                var formcontent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("menu_id", items.Id.ToString()),
                new KeyValuePair<string,string>("status", stat.ToString()),
            });

                var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                    "/odermeApp/shop/switchmenu.php?", formcontent);

                request.EnsureSuccessStatusCode();
                var response = await request.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<Account>(response);
                if(res.Status == "success")
				{
                    if (shopdata.MenuList[j].Status == true)
                    {
                        frame.BackgroundColor = Color.FromHex("#AEAEAE");
                        frame.BorderColor = Color.FromHex("#AEAEAE");
                        item.BackgroundColor = Color.White;
                        item.HorizontalOptions = LayoutOptions.StartAndExpand;
                        stat = 0;
                        shopdata.MenuList[j].Status = false;
                    }
                    else
                    {
                        frame.BackgroundColor = Color.FromHex("#FF7A21");
                        frame.BorderColor = Color.FromHex("#FF7A21");
                        item.BackgroundColor = Color.White;
                        item.HorizontalOptions = LayoutOptions.EndAndExpand;
                        stat = 1;
                        shopdata.MenuList[j].Status = true;
                    }
                }
            }
        }

        private void GoToAddMenu(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddMenuPage());
        }

        private async void GoToChkPayment(object sender, EventArgs e)
        {
            if(PayNumber.Text == "0")
			{
                string action = await DisplayActionSheet("", "ตกลง", "", "คุณยังไม่มีรายการชำระเงิน");
                if (action == "ตกลง")
                {
                }
            }
            else
			{
                await Navigation.PushAsync(new CheckPaymentPage());
            }
        }

        private void GotoDetailMenu(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;

            var ges = frame.GestureRecognizers;
            TapGestureRecognizer tap = (TapGestureRecognizer)ges[0];
            var items = (from itm in shopdata.MenuList
                         where itm.Id == int.Parse(tap.CommandParameter.ToString())
                         select itm).FirstOrDefault<Models.Menu>();

            Navigation.PushAsync(new DetailMenuPage(shopdata,items));
        }
    }
}