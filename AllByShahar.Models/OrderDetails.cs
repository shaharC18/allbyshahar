using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllByShahar.Models
{
	public class OrderDetails
	{// מחלקה לייצוג פרטי ההזמנה
		public int Id { get; set; }//קוד 
		//-----------------------------------
		//מספר הזמנה
		[Required(ErrorMessage = "הכנס שם ")]
		[Display(Name = " מספר הזמנה")]
		public int OrderId { get; set; }
		[ForeignKey("OrderId")]

		public OrderHeader OrderHeader { get; set; }
		//-----------------------------------
		

		[Required(ErrorMessage = "חייב ת.ז למוצר ")]
		public int MenuItemId { get; set; }
		[ForeignKey("MenuItemId")]
		public virtual MenuItem MenuItem { get; set; }
		//-----------------------------------
		//מספר
		[Display(Name = " מספר")]
		public int Count { get; set; }
		//-----------------------------------
		//מחיר
		[Required(ErrorMessage = "הכנס מחיר ")]
		[Display(Name = " מחיר")]
		public double Price { get; set; }
		[Display(Name = " שם")]
		//-----------------------------------
		//שם
		public string Name { get; set; }
	}
}
