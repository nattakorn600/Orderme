using oderme.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oderme.Views.Shop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMenuPage : ContentPage
    {
        public ObservableCollection<Other> Choice { get; set; } = new ObservableCollection<Other>()
        {
        };
        private MediaFile _mediafile;

        public AddMenuPage()
        {
            InitializeComponent();
            BindingContext = this;
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

        private void AddChoice(object sender, EventArgs e)
        {
            MyChoice.IsVisible = false;
            Addchoice.IsVisible = true;
            MyChoice.Margin = new Thickness(0, 20, 0, 0);
        }

        private void Additem(object sender, EventArgs e)
        {
            var CountId = Choice.Count;
            if (EntryChoice.Text != null && EntryPrice.Text != null && EntryChoice.Text != "" && EntryPrice.Text != "")
                {
                    Choice.Add(new Other
                    {
                        Id = CountId+1,
                        Name = EntryChoice.Text,
                        Price = EntryPrice.Text
                    });
                    MyChoice.IsVisible = true;
                    Addchoice.IsVisible = false;
                    EntryChoice.Text = null;
                    EntryPrice.Text = null;
            } else { 
               DisplayAlert("", "กรุณากรอกข้อมูลให้ครบ", "OK");
            }
        }

        private void Close(object sender, EventArgs e)
        {
            MyChoice.IsVisible = true;
            Addchoice.IsVisible = false;
        }

        private async void CancleChoice(object sender, EventArgs e)
        {
            var item = (ImageButton)sender;
            Other itemchoice = (from itm in Choice
                              where itm.Id == (int)(item.CommandParameter)
                              select itm).FirstOrDefault<Other>();

            string action = await DisplayActionSheet("", "ยกเลิก", "ลบ", "คุณต้องการจะลบ  " + itemchoice.Name + "  หรือไม่");
            if(action == "ลบ")
            {
                Choice.Remove(itemchoice);
            } else if(action == "ยกเลิก") {
                
            }
        }
            
        private async void Finish(object sender, EventArgs e)
        {
            string other = "";
            string otherprice = "";
            if (ImageUp.Source == null)
            {
                await DisplayAlert("", "กรุณาใส่รูปเมนูอาหาร", "OK");
            } else if (NameMenu.Text == null || NameMenu.Text == "")
            {
                await DisplayAlert("", "กรุณาใส่ชื่อเมนูอาหาร", "OK");
            }else if (TypeMenu.Text == null || TypeMenu.Text == "")
            {
                await DisplayAlert ("", "กรุณาใส่ประเภทอาหาร", "OK");
            }else if (PriceMenu.Text == null || PriceMenu.Text == "")
            {
                await DisplayAlert ("", "กรุณาใส่ราคาอาหาร", "OK");
            } else {
                await PopupNavigation.Instance.PushAsync(new LoadingPop());
                foreach (var item in Choice)
				{
                    if(other == "")
					{
                        other = item.Name;
                        otherprice = item.Price;
					}
					else
					{
                        other = other + "," + item.Name;
                        otherprice = otherprice + "," + item.Price;
                    }
				}

                WebClient cl = new WebClient();
                if (_mediafile != null)
                {
                    cl.UploadFile(Application.Current.Properties["domain"] +
                        "/odermeApp/shop/createmenu.php?shop_id=" + Application.Current.Properties["shop_id"].ToString()
                        + "&name=" + NameMenu.Text + "&price=" + PriceMenu.Text + "&type=" + TypeMenu.Text
                        + "&other=" + other + "&otherprice=" + otherprice, _mediafile.Path);
                }
                await PopupNavigation.Instance.PopAsync();
                await Navigation.PopAsync();
            }
        }
    }
}