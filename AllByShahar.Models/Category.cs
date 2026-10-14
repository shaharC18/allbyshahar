using System.ComponentModel.DataAnnotations;

namespace AllByShahar.Models
{
    public class Category
    {// מחלקה לייצוג קטגוריית מוצרים

        public int Id { get; set; }//קוד קטגוריה
        //-----------------------------------
        //שם קטגוריה
		[Required(ErrorMessage = "הכנס שם ")]
		[Display(Name = " שם")]
		public string? Name { get; set; }
		//-----------------------------------
        //סדר הצגת הקטגוריה בתפריט הראשי
		[Display(Name = "סדר ההצגה")]
        [Range(1, 100, ErrorMessage = "סדר הזמנה חייב להיות בטווח 1-100")]
        public int DisplayOrder { get; set; }
    }
}
