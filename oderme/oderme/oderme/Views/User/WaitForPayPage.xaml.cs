using oderme.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using oderme.ViewModels;

namespace oderme.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WaitForPayPage : ContentPage
    {
        public ObservableCollection<Orders> Order { get; set; } = new ObservableCollection<Orders>();
        /*public ObservableCollection<test> Order { get; set; } = new ObservableCollection<test>()
        {
            new test (){ Id=1, t1="122021092035"},
            new test (){ Id=2, t1="122021092034"},
            new test (){ Id=3, t1="122021092033"},
        };

        public ObservableCollection<test> Menu { get; set; } = new ObservableCollection<test>()
        {
            new test (){ Id=1, t1="122021092035", t2="ยำปลาหมึก", t3="อาหารคาวม ยำ", t4="1", t5="79.00"},
            new test (){ Id=2, t1="122021092034", t2="ยำปลาหมึก", t3="อาหารคาวม ยำ", t4="1", t5="79.00"},
            new test (){ Id=3, t1="122021092034", t2="ยำปลาหมึก", t3="อาหารคาวม ยำ", t4="1", t5="79.00"},
            new test (){ Id=4, t1="122021092033", t2="ยำปลาหมึก", t3="อาหารคาวม ยำ", t4="1", t5="79.00"},
            new test (){ Id=5, t1="122021092033", t2="ยำปลาหมึก", t3="อาหารคาวม ยำ", t4="1", t5="79.00"},
            new test (){ Id=6, t1="122021092033", t2="ยำปลาหมึก", t3="อาหารคาวม ยำ", t4="1", t5="79.00"},
        };*/

        public WaitForPayPage(ObservableCollection<Orders> order)
        {
            InitializeComponent();
            BindingContext = this;
            foreach(var item in order)
			{
                if(item.Status == "0"){
                    item.TextBtn = "ชำระเงิน";
                    item.BgBtn = "#FF7A21";
                    item.EnableBtn = "True";
                }
                else{
                    item.TextBtn = "กำลังตรวจสอบการชำระ";
                    item.BgBtn = "#E0E0E0";
                    item.EnableBtn = "False";
                }
                Order.Add(item);
            }
                
            // BindingContext = new OrderViewModel(1);
        }
        void GoToPay(object sender, EventArgs e)
		{
            var item = (Button)sender;
            Orders listitem = (from itm in Order
                               where itm.Id == (int)(item.CommandParameter)
                               select itm).FirstOrDefault<Orders>();
            Navigation.PushAsync(new PayAgainPage(listitem));
        }
        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
             
        }

    }
}