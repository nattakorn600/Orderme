using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace oderme.Models
{
	public class ShopData
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string  Address { get; set; }
		public string Image { get; set; }
		public string Background { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public int Distance { get; set; }
		public string Unit { get; set; }
		public string Promptpay_Number { get; set; }
		public string Promptpay_Name { get; set; }
		public ObservableCollection<Review> Review { get; set; }
		public string PointCount { get; set; }
		public string Point { get; set; }
		public string Star1 { get; set; }
		public string Star2 { get; set; }
		public string Star3 { get; set; }
		public string Star4 { get; set; }
		public string Star5 { get; set; }
		public ObservableCollection<MenuGroup> MenuGroup { get; set; }
	}
	public class ShopInfo
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string Image { get; set; }
		public string Background { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public int Distance { get; set; }
		public string Unit { get; set; }
		public string Promptpay_Number { get; set; }
		public string Promptpay_Name { get; set; }
		public bool Status { get; set; }
		public ObservableCollection<Review> Review { get; set; }
		public string PointCount { get; set; }
		public string Point { get; set; }
		public string Star1 { get; set; }
		public string Star2 { get; set; }
		public string Star3 { get; set; }
		public string Star4 { get; set; }
		public string Star5 { get; set; }
		public ObservableCollection<Menu> MenuList { get; set; }
	}
	public class Review
	{
		public string Point { get; set; }
		public string Comment { get; set; }
		public string Star1 { get; set; }
		public string Star2 { get; set; }
		public string Star3 { get; set; }
		public string Star4 { get; set; }
		public string Star5 { get; set; }
	}
	public class MenuGroup
	{
		public string Type { get; set; }

		public ObservableCollection<Menu> MenuList { get; set; }
	}
	public class Menu
	{
		public int Id { get; set; }
		public int Row { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string Type { get; set; }
		public string MainType { get; set; }
		public ObservableCollection<Other> OtherGroup { get; set; }
		public string Image { get; set; }
		public string Number { get; set; }
		public string Price { get; set; }
		public bool Status { get; set; }
	}
	public class Other
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Price { get; set; }
	}
}
