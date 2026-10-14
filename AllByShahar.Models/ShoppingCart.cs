using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AllByShahar.Models
{
    public class ShoppingCart
    {// מחלקה לייצוג עגלת קניות
		public ShoppingCart()
		{
			Count = 1;
		}
		public int Id { get; set; }//קוד 
        //-----------------------------------
		
        public int MenuItemId { get; set; }
        [ForeignKey("MenuItemId")]
        [ValidateNever]
        public MenuItem MenuItem { get; set; }
		//-----------------------------------
		//כמות לכל פריט
		[Range(1, 100, ErrorMessage = "בבקשה לבחור מספר בין 1 ל-100")]
		[Display(Name ="כמות")]
        public int Count { get; set; }
		//-----------------------------------
		//פרטי הקונה
		public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
