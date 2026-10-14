using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AllByShahar.DataAccess.Data;
using AllByShahar.DataAccess.Repository;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;

namespace AllByShahar.Pages.Admin.Categories
{
	[BindProperties]
	public class DeleteModel : PageModel
    {
        private readonly ICategoryRepository _dbCategory;

        public Category Category { get; set; }

        public DeleteModel(ICategoryRepository dbCategory)
        {
            _dbCategory = dbCategory;
        }
        public void OnGet(int id)
        {
            Category = _dbCategory.GetById(id);
           
        }
        public async Task<IActionResult> OnPost()
        {
            //var CategoryFromDb = _dbCategory.Find(Category.Id);


            _dbCategory.Remove(Category);
            _dbCategory.Save();
            TempData["success"] = "הקטגוריה נמחקה בהצלחה";
            return RedirectToPage("Index");

        }
    }
}


