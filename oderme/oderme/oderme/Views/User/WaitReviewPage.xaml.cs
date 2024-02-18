using oderme.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oderme.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WaitReviewPage : ContentPage
    {

        public ObservableCollection<Orders> Order { get; set; } = new ObservableCollection<Orders>();
        public WaitReviewPage(ObservableCollection<Orders> order)
        {
            InitializeComponent();
            //Order = order;
            BindingContext = this;
            foreach (var item in order)
                Order.Add(item);
        }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void GotoReview(object sender, EventArgs e)
        {
            var item = (Button)sender;
            Orders listitem = (from itm in Order
                             where itm.Id == (int)(item.CommandParameter)
                             select itm).FirstOrDefault<Orders>();
            Navigation.PushAsync(new ReviewPage(listitem));
        }
    }
}