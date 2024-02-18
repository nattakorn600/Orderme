using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace oderme.Models
{
	public class MapPin
	{
		public Position Position { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string Image { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string Shop_id { get; set; }
	}
}
