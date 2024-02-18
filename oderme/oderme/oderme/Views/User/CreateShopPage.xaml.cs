using System;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using oderme.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using Rg.Plugins.Popup.Services;
using System.IO;

namespace oderme.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateShopPage : ContentPage
    {
        MediaFile _mediafileimg;
        MediaFile _mediafileback;
        ShopData data = new ShopData();
        Position PinPosition;
        Geocoder geoCoder;
        private bool SelectOnMap = true;
        public CreateShopPage()
        {
            InitializeComponent();
            BindingContext = data;
            geoCoder = new Geocoder();
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(13.7543822883747, 100.500995479524), Distance.FromMiles(450)));
            PositionUser();
            btnRadio.IsVisible = false;
        }
        async void PositionUser()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                var position = await Geolocation.GetLocationAsync(request);
                PinPosition = new Position(position.Latitude, position.Longitude);
                IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(PinPosition);
                string address = possibleAddresses.FirstOrDefault();
                string[] addres = address.Split(",".ToCharArray());
                string[] addres1 = addres[addres.Length - 1].Split(" ".ToCharArray());
                string adrs = "";
                try
                {
                    adrs = int.Parse(addres1[addres1.Length - 2]).ToString();
                    adrs = addres1[addres1.Length - 3];
                }
                catch
                {
                    adrs = addres1[addres1.Length - 2];
                }
                Proven.Text = adrs;
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMeters(200)));
            }
            catch
            {
                await DisplayAlert("ข้อผิดพลาด!!", "กรุณาเปิดการเข้าถึงตำแหน่งของคุณ", "ตกลง");
                await Navigation.PopAsync();
            }
        }

        async void CreateShop(object sender, EventArgs e)
        {
            if (ImageCover.Source == null )
            {
                await DisplayAlert("", "กรุณาใส่รูปปกร้านค้า", "ตกลง");
            }
            else if (ImageShop.Source == null) 
            {
                await DisplayAlert("", "กรุณาใส่รูปโปรไฟล์ร้านค้า", "ตกลง");
            }

            else if(NameShop.Text == null || NameShop.Text == "")
            {
                await DisplayAlert("", "กรุณาใส่ชื่อร้านค้า", "ตกลง");
            }

            else if(NamePromptpay.Text == null || NamePromptpay.Text == "") 
            {
                await DisplayAlert("", "กรุณาใส่ชื่อพร้อมเพย์", "ตกลง");
            }

            else if(NumPromptpay.Text == null || NumPromptpay.Text == "") 
            {
                await DisplayAlert("", "กรุณาใส่หมายเลขพร้อมเพย์", "ตกลง");
            }

            else if (NameAddress.Text == null || NameAddress.Text == "")
            {
                await DisplayAlert("", "กรุณาใส่ที่อยู่ร้านค้า", "ตกลง");
            }
            else if (map.IsShowingUser == false)
            {
                await DisplayAlert("", "กรุณาเลือกตำแหน่งร้านค้า", "ตกลง");
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new LoadingPop());
                using (var cla = new HttpClient())
                {
                    var formcontent = new FormUrlEncodedContent(new[]
                {

                        new KeyValuePair<string,string>("name", data.Name),
                        new KeyValuePair<string, string>("address", data.Address),
                        new KeyValuePair<string, string>("latitude", data.Latitude.ToString()),
                        new KeyValuePair<string, string>("longitude", data.Longitude.ToString()),
                        new KeyValuePair<string, string>("prompt_number", data.Promptpay_Number.ToString()),
                        new KeyValuePair<string, string>("prompt_name", data.Promptpay_Name.ToString()),
                        new KeyValuePair<string, string>("user_id", Application.Current.Properties["user_id"].ToString()),
                    });

                    var request = await cla.PostAsync(Application.Current.Properties["domain"] +
                    "/odermeApp/shop/shopcreate.php", formcontent);

                    request.EnsureSuccessStatusCode();
                    var response = await request.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<Account>(response);

                    if(res.Status == "success")
				    {
                        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "session.json");
                        Application.Current.Properties["shop_id"] = res.Shop_Id;
                        WebClient cl = new WebClient();

                        if (_mediafileimg != null)
                        {
                            cl.UploadFile(Application.Current.Properties["domain"] +
                                "/odermeApp/shop/shopimage.php?user_id=" + Application.Current.Properties["user_id"].ToString(), _mediafileimg.Path);
                        }
                        if (_mediafileback != null)
                        {
                            cl.UploadFile(Application.Current.Properties["domain"] +
                                "/odermeApp/shop/shopbackground.php?user_id=" + Application.Current.Properties["user_id"].ToString(), _mediafileback.Path);
                        }

                        Account ress = new Account();
                        string jsondata = File.ReadAllText(_fileName);
                        ress = JsonConvert.DeserializeObject<Account>(jsondata);
                        ress.Shop_Id = res.Shop_Id;

                        if (File.Exists(_fileName))
                        {
                            File.Delete(_fileName);
                        }

                        string json = JsonConvert.SerializeObject(ress, Formatting.Indented);
                        File.WriteAllText(_fileName, json);

                        await DisplayAlert("", "ลงทะเบียนสำเร็จ", "ตกลง");
                        await PopupNavigation.Instance.PopAsync();
                        await Navigation.PopAsync();
                    }
                    else
				    {
                        await DisplayAlert("","ลงทะเบียนไม่สำเร็จ กรุณาลองใหม่อีกครั้ง","ตกลง");
                        await PopupNavigation.Instance.PopAsync();
                        await Navigation.PopAsync();
				    }
                }
            }
            
        }
        

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        async void SelCover(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Pick Photo", ":(No Pick Photo available.", "ok");
                return;
            }

            _mediafileback = await CrossMedia.Current.PickPhotoAsync();

            if (_mediafileback == null)
                return;

            ImageCover.Source = ImageSource.FromStream(() =>
            {
                return _mediafileback.GetStream();
            });

            if (ImageCover.Source != null)
            {
                ImageCover.Padding = 0;
            }
        }

        async void SelProShop(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Pick Photo", ":(No Pick Photo available.", "ok");
                return;
            }

            _mediafileimg = await CrossMedia.Current.PickPhotoAsync();

            if (_mediafileimg == null)
                return;

            ImageShop.Source = ImageSource.FromStream(() =>
            {
                return _mediafileimg.GetStream();
            });

            if (ImageShop.Source != null)
            {
                ImageShop.Margin = 0;
            }
        }
        async void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            if (SelectOnMap == true)
            {
                map.Pins.Clear();
                Position PinPuk = new Position(e.Position.Latitude, e.Position.Longitude);
                IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(PinPuk);
                string address = possibleAddresses.FirstOrDefault();
                data.Latitude = e.Position.Latitude;
                data.Longitude = e.Position.Longitude;

                Pin pin = new Pin
                {
                    Label = "Your location",
                    Address = address,
                    Type = PinType.Place,
                    Position = new Position(PinPuk.Latitude, PinPuk.Longitude)
                };
                map.Pins.Add(pin);
                //Launcher.OpenAsync("geo:15.8700,100.9925");
            }
        }

        async void SelectLocation(object sender, EventArgs e)
        {
            if (SelectOnMap == true)
            {
                map.Pins.Clear();
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(PinPosition.Latitude, PinPosition.Longitude), Distance.FromMeters(200)));
                IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(PinPosition);
                string address = possibleAddresses.FirstOrDefault();
                data.Latitude = PinPosition.Latitude;
                data.Longitude = PinPosition.Longitude;

                Pin pin = new Pin
                {
                    Label = "Your location",
                    Address = address,
                    Type = PinType.Place,
                    Position = new Position(PinPosition.Latitude, PinPosition.Longitude)
                };
                map.Pins.Add(pin);
                btnRadio.IsVisible = true;
                SelectOnMap = false;
            }
            else
            {
                SelectOnMap = true;
                btnRadio.IsVisible = false;
                map.Pins.Clear();
            }
        }
    }
}