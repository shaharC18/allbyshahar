using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllByShahar.Models
{
	public class MenuItem
	{ //מחלקה לייצוג מוצר
		[Key]
		public int Id { get; set; }//קוד מוצר
		 //-----------------------------------
		 //שם מוצר
		 [Required(ErrorMessage = "הכנס שם ")]
		[Display(Name = " שם")]
		public string Name { get; set; }
		//-----------------------------------
		//תיאור מוצר
		[Display(Name = " תיאור")]
		public string Description { get; set; }
		//-----------------------------------
		//תמונת הפריט
		[Display(Name = " תמונה")]
		public string Image { get; set; }
		//-----------------------------------
		//מחיר הפריט
		[Range(1, 1000, ErrorMessage = "מחיר צריך לעלות בין 1 שקל ל1,000 שקל")]
        [Display(Name = " מחיר")]

        public double Price { get; set; }
		//-----------------------------------
		
		//דרך 1
		[Display(Name = "סוג ")]
		public int TypeId { get; set; }

		[ForeignKey("TypeId")]
		public TypeItem TypeItem { get; set; }
		
		
		//דרך 2
		[Display(Name = "קטגוריה")]
        public int CategoryId { get; set; }
		public Category Category { get; set; }

	
	}
}
