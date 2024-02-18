using Newtonsoft.Json;
using oderme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oderme.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReviewPage : ContentPage
    {
        Orders Reorder = new Orders();
        public ReviewPage(Orders order)
        {
            InitializeComponent();
            Reorder = order;
        }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void Finish(object sender, EventArgs e)
        {
            using (var cl = new HttpClient())
            {
                var formcontent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("user_id", Application.Current.Properties["user_id"].ToString()),
                new KeyValuePair<string,string>("order_id", Reorder.Id.ToString()),
                new KeyValuePair<string,string>("shop_id", Reorder.Shop_Id.ToString()),
                new KeyValuePair<string,string>("comment", Comment.Text),
                new KeyValuePair<string,string>("value", value.Text),
            });

                var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                    "/odermeApp/user/review.php?", formcontent);

                request.EnsureSuccessStatusCode();
                var response = await request.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<Account>(response);
                if(res.Status == "success")
				{
                    await Navigation.PopToRootAsync();
                }
            }
        }

        private void ChangeStar1(object sender, EventArgs e)
        {
            star1.Source = "stared.png";
            star2.Source = "star.png";
            star3.Source = "star.png";
            star4.Source = "star.png";
            star5.Source = "star.png";
            value.Text = "1";
        }
        private void ChangeStar2(object sender, EventArgs e)
        {
            star1.Source = "stared.png";
            star2.Source = "stared.png";
            star3.Source = "star.png";
            star4.Source = "star.png";
            star5.Source = "star.png";
            value.Text = "2";
        }
        private void ChangeStar3(object sender, EventArgs e)
        {
            star1.Source = "stared.png";
            star2.Source = "stared.png";
            star3.Source = "stared.png";
            star4.Source = "star.png";
            star5.Source = "star.png";
            value.Text = "3";
        }
        private void ChangeStar4(object sender, EventArgs e)
        {
            star1.Source = "stared.png";
            star2.Source = "stared.png";
            star3.Source = "stared.png";
            star4.Source = "stared.png";
            star5.Source = "star.png";
            value.Text = "4";
        }
        private void ChangeStar5(object sender, EventArgs e)
        {
            star1.Source = "stared.png";
            star2.Source = "stared.png";
            star3.Source = "stared.png";
            star4.Source = "stared.png";
            star5.Source = "stared.png";
            value.Text = "5";
        }
    }
}