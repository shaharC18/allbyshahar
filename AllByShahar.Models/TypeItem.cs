using System.ComponentModel.DataAnnotations;

namespace AllByShahar.Models
{
	public class TypeItem
	{//מחלקה לייצוג סוג פריט
		[Key]
		public int Id { get; set; }//קוד הסוג
		//-----------------------------------
		//שם הסוג
		[Required]
		[Display(Name = " שם")]
		public string? Name { get; set; }
	}
}
