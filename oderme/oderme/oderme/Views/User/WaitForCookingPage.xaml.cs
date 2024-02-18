using oderme.Models;
using oderme.ViewModels;
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
    public partial class WaitForCookingPage : ContentPage
    {
        public ObservableCollection<Orders> Order { get; set; } = new ObservableCollection<Orders>();
        /*public ObservableCollection<test> OrderCooking { get; set; } = new ObservableCollection<test>()
        {
            new test (){ Id=1, t1="122021092035"},
            new test (){ Id=2, t1="122021092034"},
            new test (){ Id=3, t1="122021092033"},
            new test (){ Id=4, t1="122021092032"},
            new test (){ Id=5, t1="122021092031"},
            new test (){ Id=6, t1="122021092030"},
            new test (){ Id=7, t1="122021092029"},
            new test (){ Id=8, t1="122021092028"},
            new test (){ Id=9, t1="122021092027"},
            new test (){ Id=10, t1="122021092026"},
            new test (){ Id=11, t1="122021092025"},
            new test (){ Id=12, t1="122021092024"},
            new test (){ Id=13, t1="122021092023"},
            new test (){ Id=14, t1="122021092022"},
            new test (){ Id=15, t1="122021092021"},
        };

        test ItemList = new test();*/
        public WaitForCookingPage(ObservableCollection<Orders> order)
        {
            InitializeComponent();
            BindingContext = this;
            
            foreach(var item in order)
                Order.Add(item);
        }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        async void GetDetailWait(object sender, EventArgs e)
        {
            var item = (Button)sender;
            Orders listitem = (from itm in Order
                                 where itm.Id == (int)(item.CommandParameter)
                                 select itm).FirstOrDefault<Orders>();
            Navigation.PushAsync(new DetailWaitForCookingPage(listitem));

        }
    }

}
