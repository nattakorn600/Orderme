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
    public partial class DetialCheckPaymentPage : ContentPage
    {
        Orders ord = new Orders();
        public DetialCheckPaymentPage(Orders item)
        {
            InitializeComponent();
            BindingContext = item;
            ord = item;
        }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        async void updateorder(string id, string stat)
		{
            using (var cl = new HttpClient())
            {
                var formcontent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string,string>("id", id),
                    new KeyValuePair<string,string>("status", stat),
                });

                var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                    "/odermeApp/shop/checkpay.php?", formcontent);

                request.EnsureSuccessStatusCode();
                var response = await request.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<Account>(response);
                if (res.Status == "success")
                {
                    await Navigation.PopAsync();
				}
				else
				{
                    await DisplayAlert("","การอัพเดทล้มเหลวโปรดลองใหม่อีกครั้ง","ok");
				}
            }
        }
        private async void CancelBill(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("", "ยกเลิก", "ชำระเงินไม่สำเร็จ", "ยืนยันการทำรายการ");
            if (action == "ชำระเงินไม่สำเร็จ")
            {
                updateorder(ord.Id.ToString(), "0");
            }
            else if (action == "ยกเลิก")
            {

            }
        }

        private async void CheckBill(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("", "ยกเลิก", "ชำระเงินเสร็จสิ้น", "ยืนยันการทำรายการ");
            if (action == "ชำระเงินเสร็จสิ้น")
            {
                updateorder(ord.Id.ToString(), "2");
            }
            else if (action == "ยกเลิก")
            {

            }
        }
    }
}