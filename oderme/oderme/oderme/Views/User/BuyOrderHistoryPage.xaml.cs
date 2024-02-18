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

namespace oderme.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuyOrderHistoryPage : ContentPage
    {
        public ObservableCollection<Orders> OrderHis { get; set; } = new ObservableCollection<Orders>();
        public BuyOrderHistoryPage()
        {
            InitializeComponent();
            LoadData();
            
        }
        async void LoadData()
        {
            using (var cl = new HttpClient())
            {
                var formcontent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("user_id", Application.Current.Properties["user_id"].ToString()),
            });

                var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                    "/odermeApp/user/gethistory.php?", formcontent);

                request.EnsureSuccessStatusCode();
                var response = await request.Content.ReadAsStringAsync();
                OrderHis = JsonConvert.DeserializeObject<ObservableCollection<Orders>>(response);
                BindingContext = this;
            }
        }
        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        async void GetDetail (object sender, EventArgs e)
        {
            var item = (Button)sender;
            Orders listitem = (from itm in OrderHis
                             where itm.Id == (int)(item.CommandParameter)
                                   select itm).FirstOrDefault<Orders>();
            await Navigation.PushAsync(new DetailBuyOrderHistoryPage(listitem));
        }
    }
}