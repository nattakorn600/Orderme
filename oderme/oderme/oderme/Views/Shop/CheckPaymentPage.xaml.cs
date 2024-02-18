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
    public partial class CheckPaymentPage : ContentPage
    {
        public ObservableCollection<Orders> ChkPayment { get; set; }
        public CheckPaymentPage()
        {
            InitializeComponent();
        }

        async void Loaddata()
		{
            BindingContext = null;
            using (var cl = new HttpClient())
            {
                var formcontent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("shop_id", Application.Current.Properties["shop_id"].ToString()),
            });

                var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                    "/odermeApp/shop/getpay.php?", formcontent);

                request.EnsureSuccessStatusCode();
                var response = await request.Content.ReadAsStringAsync();
                ChkPayment = new ObservableCollection<Orders>();
                var res = JsonConvert.DeserializeObject<ObservableCollection<Orders>>(response);
                foreach(var item in res)
				{
                    ChkPayment.Add(item);
				}
                BindingContext = this;
            }
        }
		protected override void OnAppearing()
		{
			base.OnAppearing();
            Loaddata();
        }
		private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void GetDetailChk(object sender, EventArgs e)
        {
            var item = (Button)sender;
            Orders listitem = (from itm in ChkPayment
                               where itm.Id == (int)(item.CommandParameter)
                               select itm).FirstOrDefault<Orders>();

            Navigation.PushAsync(new DetialCheckPaymentPage(listitem));
        }
    }
}