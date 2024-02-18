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
    public partial class PaymentPage : ContentPage
    {
        private int pagebutton = 0;
        private int pixels = 50;
        private MediaFile _mediafile;
        int _TotalPrice = 0;
        ShopData Shopdata = new ShopData();
        ObservableCollection<Models.Menu> Carts = new ObservableCollection<Models.Menu>();
        public PaymentPage(ShopData shopdata)
        {
            InitializeComponent();
            Table.Text = Application.Current.Properties["table"].ToString();
            Shopdata = shopdata;
            Loaddata();
        }
        async void Finish(object sender, EventArgs e)
		{
            await PopupNavigation.Instance.PushAsync(new LoadingPop());
            string json = JsonConvert.SerializeObject(Carts, Formatting.Indented);

            WebClient cl = new WebClient();
            if (_mediafile != null)
            {
                cl.UploadFile(Application.Current.Properties["domain"] +
                    "/odermeApp/user/order.php?user_id=" + Application.Current.Properties["user_id"].ToString() + "&shop_id="
                    + Shopdata.Id.ToString() + "&menu=" + json + "&price=" + _TotalPrice.ToString() + "&table=" + Table.Text
                    , _mediafile.Path);
            }

            await DisplayAlert("", "สั่งอาหารสำเร็จแล้ว", "ตกลง");
            string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "menu.json");
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
            await PopupNavigation.Instance.PopAsync();
            await Navigation.PopToRootAsync();
        }
        void Loaddata()
		{
            string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "menu.json");
            Carts = new ObservableCollection<Models.Menu>();
            if (File.Exists(_fileName))
            {
                string jsondata = File.ReadAllText(_fileName);
                var res = JsonConvert.DeserializeObject<ObservableCollection<Models.Menu>>(jsondata);
                foreach (var item in res)
                {
                    _TotalPrice += int.Parse(item.Price);
                    Carts.Add(item);
                }
                ShopName.Text = Shopdata.Name;
                PromptName.Text = Shopdata.Promptpay_Name;
                PromptNumber.Text = Shopdata.Promptpay_Number;
                Prompt.Source = Application.Current.Properties["domain"].ToString() + "/odermeApp/shop/genprompt.php?data=" + Shopdata.Promptpay_Number.Length 
                    + Shopdata.Promptpay_Number + "0" + _TotalPrice.ToString().Length + _TotalPrice;
                TotalPrice.Text = _TotalPrice.ToString();
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