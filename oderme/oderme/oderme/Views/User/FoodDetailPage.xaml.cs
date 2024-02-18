using Newtonsoft.Json;
using oderme.Models;
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
    public partial class FoodDetailPage : ContentPage
    {
        int total = 0;
        string menusother = "";
        ObservableCollection<Other> other = new ObservableCollection<Other>();
        Models.Menu menus = new Models.Menu();
        Models.Menu menu = new Models.Menu();
        public FoodDetailPage(ShopData shopdata, Models.Menu menuitem)
        {
            InitializeComponent();
            Bg.Source = shopdata.Background;
            BindingContext = menuitem;
            menus = menuitem;
            total = int.Parse(menuitem.Price);
            LastPrice.Text = menuitem.Price;
        }
        void SelectOther(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            var ges = frame.GestureRecognizers;
            TapGestureRecognizer tap = (TapGestureRecognizer)ges[0];
            BoxView item = (BoxView)((Frame)frame.Content).Content;
            if (item.BackgroundColor == Color.FromHex("#FF7A21"))
            {
                item.BackgroundColor = Color.Transparent;
                var otheritem = (from itm in menus.OtherGroup
                                 where itm.Name == tap.CommandParameter.ToString()
                                 select itm).FirstOrDefault<Other>();
                other.Remove(otheritem);
            }
            else
            {
                item.BackgroundColor = Color.FromHex("#FF7A21");
                var otheritem = (from itm in menus.OtherGroup
                                 where itm.Name == tap.CommandParameter.ToString()
                                 select itm).FirstOrDefault<Other>();
                other.Add(otheritem);
            }
            int totalother = 0;
            string menuother = "";
            if (other.Count > 0)
            {
                for (int i = 0; i < other.Count; i++)
                {
                    totalother += int.Parse(other[i].Price);
                    if (menuother == "")
                    {
                        menuother = other[i].Name;
                    }
                    else
                    {
                        menuother = menuother + "," + other[i].Name;
                    }
                }
            }
            menusother = menuother;
            total = int.Parse(menus.Price) + totalother;
            LastPrice.Text = (int.Parse(CountMenu.Text) * total).ToString();
        }
        void AddToCard(object sender, EventArgs e)
        {
            string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "menu.json");
            menu.Id = menus.Id;
            menu.Type = menus.Type;
            menu.Image = menus.Image;
            menu.Name = menus.Name;
            menu.MainType = menusother;
            menu.OtherGroup = other;
            menu.Number = CountMenu.Text;
            menu.Price = LastPrice.Text;
            menu.Status = false;

            ObservableCollection<Models.Menu> res = new ObservableCollection<Models.Menu>();
            if (File.Exists(_fileName))
            {
                string jsondata = File.ReadAllText(_fileName);
                res = JsonConvert.DeserializeObject<ObservableCollection<Models.Menu>>(jsondata);
                File.Delete(_fileName);
            }
            res.Add(menu);
            string json = JsonConvert.SerializeObject(res, Formatting.Indented);
            File.WriteAllText(_fileName, json);

            Navigation.PopAsync();
        }
        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void BtnPlus(object sender, EventArgs e)
        {
            CountMenu.Text = (int.Parse(CountMenu.Text) + 1).ToString();
            LastPrice.Text = (int.Parse(CountMenu.Text) * total).ToString();
        }

        private void BtnNegative(object sender, EventArgs e)
        {
            if (int.Parse(CountMenu.Text) > 1)
            {
                CountMenu.Text = (int.Parse(CountMenu.Text) - 1).ToString();
                LastPrice.Text = (int.Parse(CountMenu.Text) * total).ToString();
            }
        }
    }
}