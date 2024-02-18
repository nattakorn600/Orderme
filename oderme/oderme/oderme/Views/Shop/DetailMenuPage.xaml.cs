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
    public partial class DetailMenuPage : ContentPage
    {
        Models.Menu Menus = new Models.Menu();
        public DetailMenuPage(ShopInfo shopdata, Models.Menu items)
        {
            InitializeComponent();
            Menus = items;
            Bg.Source = shopdata.Background;
            BindingContext = Menus;
            //BindingContext = this;
        }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void CancelOther(object sender, EventArgs e)
        {
            var item = (ImageButton)sender;
            Other itemchoice = (from itm in Menus.OtherGroup
                                where itm.Id == (int)(item.CommandParameter)
                                select itm).FirstOrDefault<Other>();

            string action = await DisplayActionSheet("Oderme", "ยกเลิก", "ลบ", "คุณต้องการจะลบ  " + itemchoice.Name + "  หรือไม่");
            if (action == "ลบ")
            {
                Menus.OtherGroup.Remove(itemchoice);
            }
            else if (action == "ยกเลิก")
            {

            }
        }

        private void AddChoice(object sender, EventArgs e)
        {
            MyChoice.IsVisible = false;
            Addchoice.IsVisible = true;
            MyChoice.Margin = new Thickness(0, 20, 0, 0);
        }

        private void AddOther(object sender, EventArgs e)
        {
            var CountId = Menus.OtherGroup.Count;
            if (EntryChoice.Text != null && EntryPrice.Text != null && EntryChoice.Text != "" && EntryPrice.Text != "")
            {
                Menus.OtherGroup.Add(new Other
                {
                    Id = CountId + 1,
                    Name = EntryChoice.Text,
                    Price = EntryPrice.Text
                });
                MyChoice.IsVisible = true;
                Addchoice.IsVisible = false;
                EntryChoice.Text = null;
                EntryPrice.Text = null;
                MyChoice.Margin = new Thickness(20, 0, 20, 0);
            }
            else
            {
                DisplayAlert("Oderme", "กรุณากรอกข้อมูลให้ครบ", "OK");
            }
        }

        private void Close(object sender, EventArgs e)
        {
            MyChoice.IsVisible = true;
            Addchoice.IsVisible = false;
            MyChoice.Margin = new Thickness(20, 0, 20, 0);
        }
        private async void FinishBtn(object sender, EventArgs e)
		{
            string other = "";
            string otherprice = "";
            foreach (var item in Menus.OtherGroup)
            {
                if (other == "")
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

            using (var cl = new HttpClient())
            {
                var formcontent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("menu_id", Menus.Id.ToString()),
                new KeyValuePair<string,string>("other", other),
                new KeyValuePair<string,string>("otherprice", otherprice),
            });

                var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                    "/odermeApp/shop/editmenu.php?", formcontent);

                request.EnsureSuccessStatusCode();
                var response = await request.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<Account>(response);

                await Navigation.PopAsync();
            }
        }
        private async void DeleteBtn(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("", "ยกเลิก", "ลบ", "คุณต้องการจะลบ  " + Menus.Name + "  หรือไม่");
            if (action == "ลบ")
            {
                using (var cl = new HttpClient())
            {
                var formcontent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("menu_id", Menus.Id.ToString()),
            });

                var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                    "/odermeApp/shop/delmenu.php?", formcontent);

                request.EnsureSuccessStatusCode();
                var response = await request.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<Account>(response);
            }
            await Navigation.PopAsync();
            }
            else if (action == "ยกเลิก")
            {

            }
            
        }
    }
}