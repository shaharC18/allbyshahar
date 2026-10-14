using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllByShahar.Models
{
    public class OrderHeader
	{//מחלקה לייצוג לסיכום הזמנה
		[Key]
		public int Id { get; set; }// קוד 
		//-----------------------------------
		//פרטי הקונה
		[Required]
		public string UserId { get; set; }
		[ForeignKey("UserId")]
		[ValidateNever]
		public ApplicationUser ApplicationUser { get; set; }
		//-----------------------------------
		//תאריך לקבלת ההזמנה
		[Required]
		[Display(Name = " תאריך הזנת ההזמנה")]

		public DateTime OrderDate { get; set; }
		[Required]
		[DisplayFormat(DataFormatString = "{0:C}")]
		//-----------------------------------
		//מחיר סופי
		[Display(Name = "מחיר כולל")]
		public double OrderTotal { get; set; }
		//-----------------------------------
		//שעת איסוף ההזמנה
		[Required(ErrorMessage = "הכנס שעת איסוף הזמנה ")]
		[Display(Name = "שעת איסוף הזמנה")]
		public DateTime PickUpTime { get; set; }
		//-----------------------------------
		//תאריך איסוף הזמנה
		[Required(ErrorMessage = "הכנס תאריך איסוף הזמנה ")]
		[NotMapped]
		[Display(Name = "תאריך איסוף הזמנה")]
		public DateTime PickUpDate { get; set; }
		//-----------------------------------
		//סטטוס הזמנה
		public string Status { get; set; }
		//-----------------------------------
		//הערות נוספות
		public string? Comments { get; set; }
		//-----------------------------------
		//קוד עסקה
		public string? TransactionId { get; set; }
		//-----------------------------------
		//קוד הסשן
		public string? SessionId { get; set; }
		//-----------------------------------
		//קוד התשלום
		public string? PaymentIntentId { get; set; }
		//-----------------------------------
		//שם הרוכש
		[Display(Name = "שם האיסוף")]
		[Required(ErrorMessage = "הכנס שם אוסף ההזמנה ")]
		public string PickupName { get; set; }
        //-----------------------------------
        //מספר הטלפון של הרוכש
        [StringLength(10)]
        [Range(0, int.MaxValue, ErrorMessage = "על מספר הטלפון להיות בעל 10 ספרות")]
        [Display(Name = "מספר טלפון")]
        public string PhoneNumber { get; set; }
	}
}
