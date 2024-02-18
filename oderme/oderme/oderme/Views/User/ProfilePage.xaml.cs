using Newtonsoft.Json;
using oderme.Controls;
using oderme.Models;
using oderme.ViewModels;
using oderme.Views.Shop;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oderme.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        CarouselPage carpage;
        public ObservableCollection<ObservableCollection<Orders>> Order { get; set; } = new ObservableCollection<ObservableCollection<Orders>>();
        public ProfilePage(CarouselPage page)
        {
            InitializeComponent();
            Name.Text = Application.Current.Properties["user_name"].ToString();
            Picture.Source = Application.Current.Properties["user_picture"].ToString();
            emailText.Text = Application.Current.Properties["user_email"].ToString();
            carpage = page;
            distance.Text = Application.Current.Properties["distance"].ToString();
            Sliderr.Value = double.Parse(Application.Current.Properties["distance"].ToString());
            Sliderr.Value = Preferences.Get("SliderValue", double.NaN);
        }
		protected override void OnAppearing()
		{
			base.OnAppearing();
            if (Application.Current.Properties["shop_id"] != null)
            {
                SelStart.Text = "ร้านค้าของคุณ";
            }
            Device.StartTimer(TimeSpan.FromMilliseconds(600), () =>
            {
                LoadData();
                return true;
            });
        }
        async void LoadData()
        {
			try
			{
                using (var cl = new HttpClient())
                {
                    var formcontent = new FormUrlEncodedContent(new[]
                    {
                new KeyValuePair<string,string>("user_id", Application.Current.Properties["user_id"].ToString()),
                });

                    var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                        "/odermeApp/user/getorders.php?", formcontent);

                    request.EnsureSuccessStatusCode();
                    var response = await request.Content.ReadAsStringAsync();
                    Order = JsonConvert.DeserializeObject<ObservableCollection<ObservableCollection<Orders>>>(response);
                    if (Order.Count <= 0)
                    {
                        WaitPayStatus.Text = "0";
                        WaitCookStatus.Text = "0";
                        WaitCookingStatus.Text = "0";
                        ReviewStatus.Text = "0";
                    }
                    else
                    {
                        WaitPayStatus.Text = Order[0].Count.ToString();
                        WaitCookStatus.Text = Order[1].Count.ToString();
                        WaitCookingStatus.Text = Order[2].Count.ToString();
                        ReviewStatus.Text = Order[3].Count.ToString();
                    }
                }
			}
			catch { }
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
        void OnBackBtn(object sender, EventArgs e)
		{
            carpage.CurrentPage = carpage.Children[1];
        }
        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            int value = (int)args.NewValue;
            if (value == 0 )
            {
                value = 1;
                distance.Text = value.ToString();
                Preferences.Set("SliderValue", Sliderr.Value);
            }
            distance.Text = value.ToString();
            Preferences.Set("SliderValue", Sliderr.Value);
            Application.Current.Properties["distance"] = value.ToString();
        }

        private async void GotoCreateShop(object sender, EventArgs e)
        {
            if(SelStart.Text == "เริ่มการขาย")
			{
                await Navigation.PushAsync(new CreateShopPage());
            }

            if (SelStart.Text == "ร้านค้าของคุณ")
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
                            Application.Current.MainPage = new TransitionNavigationPage(new SlideShopPage(res));
                        }
                    }
                }
				catch { }
            }
        }

        private void GoToWaitPay(object sender, EventArgs e)
        {
            if(WaitPayStatus.Text != "0")
            Navigation.PushAsync(new WaitForPayPage(Order[0]));
        }

        private void GoToWaitForCooking(object sender, EventArgs e)
        {
            if (WaitCookStatus.Text != "0")
                Navigation.PushAsync(new WaitForCookingPage(Order[1]));
        }

        private void GoToCooking(object sender, EventArgs e)
        {

        }

        private void GoToReview(object sender, EventArgs e)
        {
            if (ReviewStatus.Text != "0")
                Navigation.PushAsync(new WaitReviewPage(Order[3]));
        }

        private void GotoOrderHistory(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BuyOrderHistoryPage());
        }
    }
}