using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace oderme.Models
{
	public class Orders
	{
		public int Id { get; set; }
		public string Order_Id { get; set; }
		public int Shop_Id { get; set; }
		public string Shop_Name { get; set; }
		public int Price { get; set; }
		public int Wait { get; set; }
		public string Payment { get; set; }
		public int Table { get; set; }
		public string Date { get; set; }
		public int Number { get; set; }
		public string Status { get; set; }
		public string MenusText { get; set; }
		public string TextBtn { get; set; }
		public string BgBtn { get; set; }
		public string EnableBtn { get; set; }
		public double Rotation { get; set; }
		public bool Visible { get; set; }
		public ObservableCollection<MenuOrders> Menus { get; set; }
	}
	public class MenuOrders
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Price { get; set; }
		public string Image { get; set; }
		public string Type { get; set; }
		public string Other { get; set; }
		public string Other_Price { get; set; }
		public int Number { get; set; }
		public int Total { get; set; }
		public ObservableCollection<Other> OtherGroup { get; set; }
		public bool VisibleOther { get; set; }
		public bool VisibleArrow { get; set; }
		public double Rotation { get; set; }
	}
	public class ShopOrderHistory
	{
		public string Date { get; set; }
		public ObservableCollection<Orders> Menus { get; set; }
	}
}
