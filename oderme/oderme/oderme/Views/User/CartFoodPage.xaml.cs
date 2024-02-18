using Newtonsoft.Json;
using oderme.Models;
using oderme.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace oderme.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartFoodPage : ContentPage
    {
        public ObservableCollection<Models.Menu> Carts { get; set; }
        public ObservableCollection<Models.Menu> DelStack = new ObservableCollection<Models.Menu>();
        public int TotalPrice { get; set; }
        bool del = false;
        ShopData Shopdata = new ShopData();
        public CartFoodPage(ShopData shopdata)
        {
            InitializeComponent();
            CartsData();
            Table.Text = Application.Current.Properties["table"].ToString();
            Shopdata = shopdata;
        }
		private void CartsData()
        {
            string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "menu.json");
            Carts = new ObservableCollection<Models.Menu>();
            if (File.Exists(_fileName))
            {
                string jsondata = File.ReadAllText(_fileName);
                var res = JsonConvert.DeserializeObject<ObservableCollection<Models.Menu>>(jsondata);
                for(int i=0; i<res.Count; i++)
                {
                    TotalPrice += int.Parse(res[i].Price);
                    res[i].Row = i;
                    Carts.Add(res[i]);
                }
                BindingContext = this;
            }
        }
        private void SelectDelete(object sender, EventArgs e)
		{
            Frame frame = (Frame)sender;
            var ges = frame.GestureRecognizers;
            TapGestureRecognizer tap = (TapGestureRecognizer)ges[0];
            BoxView boxview = (BoxView)((Frame)((Frame)sender).Content).Content;
            if (boxview.BackgroundColor == Color.FromHex("#FF7A21"))
            {
                boxview.BackgroundColor = Color.White;
                var otheritem = (from itm in Carts
                                 where itm.Row == (int)(tap.CommandParameter)
                                 select itm).FirstOrDefault<Models.Menu>();
                DelStack.Remove(otheritem);
            }
			else
			{
                boxview.BackgroundColor = Color.FromHex("#FF7A21");
                var otheritem = (from itm in Carts
                                    where itm.Row == (int)(tap.CommandParameter)
                                 select itm).FirstOrDefault<Models.Menu>();
                DelStack.Add(otheritem);
            }
        }
        private void DeleteClick(object sender, EventArgs e)
		{
            ImageButton btn = (ImageButton)sender;
            BindingContext = "";
            var subcart = Carts;
            Carts = new ObservableCollection<Models.Menu>();
            if(del == false)
			{
                DelStack = new ObservableCollection<Models.Menu>();
                for (int i = 0; i < subcart.Count; i++)
                {
                    subcart[i].Status = true;
                    subcart[i].Row = i;
                    Carts.Add(subcart[i]);
                }
                btn.Source = "check.png";
                del = true;
			}
			else
			{
                TotalPrice = 0;
                for (int i = 0; i < subcart.Count; i++)
                {
                    Models.Menu Isitem = null;
                    if (DelStack.Count > 0)
					{
                        Isitem = (from itm in DelStack
                                      where itm.Row == subcart[i].Row
                                      select itm).FirstOrDefault<Models.Menu>();
                    }
                    subcart[i].Status = false;
                    subcart[i].Row = i;
                    if(Isitem == null)
					{
                        TotalPrice += int.Parse(subcart[i].Price);
                        Carts.Add(subcart[i]);
                    }
                }

                string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "menu.json");

                ObservableCollection<Models.Menu> res = new ObservableCollection<Models.Menu>();
                if (File.Exists(_fileName))
                {
                    File.Delete(_fileName);
                }
                string json = JsonConvert.SerializeObject(Carts, Formatting.Indented);
                File.WriteAllText(_fileName, json);

                btn.Source = "delete.png";
                del = false;
            }
            BindingContext = this;
        }
        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void GoToPayment(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PaymentPage(Shopdata));
        }
    }
}