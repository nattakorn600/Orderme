using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace oderme.Models
{
    public class CustomPin : Pin
    {
        public ShopData Shop_Data { get; set; }
        public string Image { get; set; }
        public int Distance { get; set; }
        public string Unit { get; set; }
    }
}
