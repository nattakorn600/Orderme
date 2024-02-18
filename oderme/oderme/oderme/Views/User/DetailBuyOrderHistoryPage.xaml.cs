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
    public partial class DetailBuyOrderHistoryPage : ContentPage
    {
        public DetailBuyOrderHistoryPage(Orders order)
        {
            InitializeComponent();
            BindingContext = order;
        }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        
    }
}