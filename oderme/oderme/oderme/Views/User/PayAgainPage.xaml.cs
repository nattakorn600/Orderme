using Newtonsoft.Json;
using oderme.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oderme.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PayAgainPage : ContentPage
    {
        private int pagebutton = 0;
        private MediaFile _mediafile;
        Orders Order = new Orders();
        public PayAgainPage(Orders order)
        {
            InitializeComponent();
            Order = order;
            Loaddata(order.Shop_Id);
        }
        async void Finish(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new LoadingPop());
            WebClient cl = new WebClient();
            if (_mediafile != null)
            {
                cl.UploadFile(Application.Current.Properties["domain"] +
                    "/odermeApp/user/payagain/payupdate.php?order_id=" + Order.Id, _mediafile.Path);
            }

            await DisplayAlert("", "สั่งอาหารสำเร็จแล้ว", "ตกลง");
            await PopupNavigation.Instance.PopAsync();
            await Navigation.PopToRootAsync();
        }
        async void Loaddata(int shop_id)
        {
            using (var cl = new HttpClient())
            {
                var formcontent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("shop_id", shop_id.ToString()),
            });

                var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                    "/odermeApp/user/payagain/getpay.php?", formcontent);

                request.EnsureSuccessStatusCode();
                var response = await request.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ShopData>(response);

                PromptName.Text = res.Promptpay_Name;
                PromptNumber.Text = res.Promptpay_Number;
                Prompt.Source = Application.Current.Properties["domain"].ToString() + "/odermeApp/shop/genprompt.php?data=" + res.Promptpay_Number.Length
                    + res.Promptpay_Number + "0" + Order.Price.ToString().Length + Order.Price;
                TotalPrice.Text = Order.Price.ToString();
            }
        }
        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }


        async void SelectImage(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Pick Photo", ":(No Pick Photo available.", "ok");
                return;
            }

            _mediafile = await CrossMedia.Current.PickPhotoAsync();

            if (_mediafile == null)
                return;

            ImageUp.Source = ImageSource.FromStream(() =>
            {
                return _mediafile.GetStream();
            });

            if (ImageUp.Source != null)
            {
                FrameUpImage.IsVisible = false;
            }

        }
    }
}