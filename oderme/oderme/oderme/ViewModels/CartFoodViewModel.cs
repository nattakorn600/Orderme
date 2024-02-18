using Newtonsoft.Json;
using oderme.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace oderme.ViewModels
{
    public class CartFoodViewModel : INotifyPropertyChanged
    {
        public CartFoodViewModel()
        {
            CartsData();
        }

        private void CartsData()
        {
            string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "menu.json");
            Carts = new ObservableCollection<Models.Menu>();
            if (File.Exists(_fileName))
            {
                string jsondata = File.ReadAllText(_fileName);
                var res = JsonConvert.DeserializeObject<ObservableCollection<Models.Menu>>(jsondata);
                foreach(var item in res)
				{
                    TotalPrice += int.Parse(item.Price);
                    Carts.Add(item);
				}
            }
            
            /*Carts.Add(new test { Id = 1, t1="imgtest1.jpg", t2=" ต้มยำกุ้ง", t3=" อาหารคาว,ยำ", t4="1", t5="112" });
            Carts.Add(new test { Id = 2, t1 ="imgtest2.jpg", t2=" ยำปลาหมึก", t3=" อาหารคาว,ยำ", t4="1", t5="79" });
            Carts.Add(new test { Id = 3, t1 ="imgtest3.jpg", t2=" ไข่ดาวไส้กรอก", t3=" อาหารคาว,ยำ", t4="1", t5="69" });
            Carts.Add(new test { Id = 4, t1 ="imgtest4.jpg", t2=" ปลาทอด", t3=" อาหารคาว,ยำ", t4="1", t5="149" });
            Carts.Add(new test { Id = 5, t1 ="imgtest1.jpg", t2=" ต้มยำกุ้ง", t3=" อาหารคาว,ยำ", t4="1", t5="112" });
            Carts.Add(new test { Id = 6, t1 ="imgtest2.jpg", t2=" ยำปลาหมึก", t3=" อาหารคาว,ยำ", t4="1", t5="79" });
            Carts.Add(new test { Id = 7, t1 ="imgtest3.jpg", t2=" ไข่ดาวไส้กรอก", t3=" อาหารคาว,ยำ", t4="1", t5="69" });
            Carts.Add(new test { Id = 8, t1 ="imgtest4.jpg", t2=" ปลาทอด", t3=" อาหารคาว,ยำ", t4="1", t5="149" });
            Carts.Add(new test { Id = 9, t1 ="imgtest1.jpg", t2=" ต้มยำกุ้ง", t3=" อาหารคาว,ยำ", t4="1", t5="112" });
            Carts.Add(new test { Id = 10, t1 ="imgtest2.jpg", t2=" ยำปลาหมึก", t3=" อาหารคาว,ยำ", t4="1", t5="79" });
            Carts.Add(new test { Id = 11, t1 ="imgtest3.jpg", t2=" ไข่ดาวไส้กรอก", t3=" อาหารคาว,ยำ", t4="1", t5="69" });
            Carts.Add(new test { Id = 12, t1 ="imgtest4.jpg", t2=" ปลาทอด", t3=" อาหารคาว,ยำ", t4="1", t5="149" });*/
        }

        ObservableCollection<Models.Menu> _Carts;
        public ObservableCollection<Models.Menu> Carts 
        {
            get{ return _Carts; }
            set
            {
                if(value != null)
                {
                    _Carts = value;
                    OnPropertyChanged();
                }
            } 
        }
        private int _totalPrice;
        public int TotalPrice 
        {
			get { return _totalPrice; }
            set
			{
                if (value != null)
				{
                    _totalPrice = value;
                    OnPropertyChanged();
                }
			}
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
