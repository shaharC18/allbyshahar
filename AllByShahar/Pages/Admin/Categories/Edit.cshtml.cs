using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AllByShahar.DataAccess.Data;
using AllByShahar.DataAccess.Repository;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;

namespace AllByShahar.Pages.Admin.Categories
{
	[BindProperties]
	public class EditModel : PageModel
    {
        private readonly ICategoryRepository _dbCategory;

        public Category Category { get; set; }
        public EditModel(ICategoryRepository dbCategory)
        {
            _dbCategory = dbCategory;
        }
        public void OnGet(int id)
        {
            Category = _dbCategory.GetById(id);
            //Category = _db.Category.FirstOrDefault(u=>u.Id==id);
            //Category = _db.Category.SingleOrDefault(u=>u.Id==id);
            //Category = _db.Category.Where(u => u.Id == id).FirstOrDefault();
            if (Category != null)
            {
                _dbCategory.Remove(Category);
            }

        }

        public async Task<IActionResult> OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "סדר ההזמנה לא אמור להיות תואם לשם");
            }
            if (ModelState.IsValid)
            {
                _dbCategory.Update(Category);
                _dbCategory.Save();
                TempData["success"] = "הקטגוריה התעדכנה בהצלחה";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
