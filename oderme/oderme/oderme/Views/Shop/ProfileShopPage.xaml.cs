using Newtonsoft.Json;
using oderme.Controls;
using oderme.Models;
using oderme.Views.User;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oderme.Views.Shop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileShopPage : ContentPage
    {
        public ShopInfo shopInfo;
        CarouselPage carpage;
        public ProfileShopPage(CarouselPage page, ShopInfo shopinfo)
        {
            InitializeComponent();
            shopInfo = shopinfo;
            carpage = page;
            BindingContext = shopInfo;
            //phoneText.Text = Application.Current.Properties["user_phone"].ToString();
            emailText.Text = Application.Current.Properties["user_email"].ToString();
        }

        private void OnBackBtn(object sender, EventArgs e)
        {
            //Navigation.PopAsync();
            carpage.CurrentPage = carpage.Children[1];
        }

        private void GotoOrderHistory(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OrderHistoryPage());
        }

        void OnLogout(object sender, EventArgs e)
        {
            string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "session.json");
            string _fileData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "data.json");
            switch (Application.Current.Properties["social_type"])
            {
                case "Facebook":
                    CrossFacebookClient.Current.Logout();
                    break;
                case "Google":
                    CrossGoogleClient.Current.Logout();
                    break;
            }
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
            if (File.Exists(_fileData))
            {
                File.Delete(_fileData);
            }

            Application.Current.Properties["user_id"] = null;
            Application.Current.Properties["user_email"] = null;
            Application.Current.Properties["user_name"] = null;
            Application.Current.Properties["user_phone"] = null;
            Application.Current.Properties["user_picture"] = null;
            Application.Current.Properties["social_id"] = null;
            Application.Current.Properties["shop_id"] = null;
            Application.Current.Properties["social_type"] = null;

            App.Current.MainPage = new NavigationPage(new LoginPage());
        }

        private async void OnOffShop(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            BoxView item = (BoxView)frame.Content;

            int stat = 0;
            
            if (shopInfo.Status == true)
            {
                stat = 0;
            }
            else
            {
                stat = 1;
            }

			try
			{
                using (var cl = new HttpClient())
                {
                    var formcontent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string,string>("shop_id", shopInfo.Id.ToString()),
                        new KeyValuePair<string,string>("status", stat.ToString()),
                    });

                    var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                        "/odermeApp/shop/shoponoff.php?", formcontent);

                    request.EnsureSuccessStatusCode();
                    var response = await request.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<Account>(response);
                    if (res.Status == "success")
                    {
                        if (shopInfo.Status == true)
                        {
                            frame.BackgroundColor = Color.FromHex("#AEAEAE");
                            frame.BorderColor = Color.FromHex("#AEAEAE");
                            item.BackgroundColor = Color.White;
                            item.HorizontalOptions = LayoutOptions.StartAndExpand;
                            stat = 0;
                            shopInfo.Status = false;
                        }
                        else
                        {
                            frame.BackgroundColor = Color.FromHex("#FF7A21");
                            frame.BorderColor = Color.FromHex("#FF7A21");
                            item.BackgroundColor = Color.White;
                            item.HorizontalOptions = LayoutOptions.EndAndExpand;
                            stat = 1;
                            shopInfo.Status = true;
                        }
                    }
                }
            }
			catch { }
        }

        private void GoToUser(object sender, EventArgs e)
        {
            Application.Current.MainPage = new TransitionNavigationPage(new SlidePageUser());
        }

		private void GotoAboutShop(object sender, EventArgs e)
		{
            Navigation.PushAsync(new GenQrPage(shopInfo));
		}
	}
}