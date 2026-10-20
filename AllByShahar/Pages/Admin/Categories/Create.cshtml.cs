using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AllByShahar.Utility;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AllByShahar.DataAccess.Data;
using AllByShahar.DataAccess.Repository;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;

namespace AllByShahar.Pages.Admin.Categories
{
    [BindProperties]
    [Authorize(Roles = SD.ManagerRole)]
    public class CreateModel : PageModel
    {
        private readonly ICategoryRepository _dbCategory;

        public Category Category { get; set; }

        public CreateModel(ICategoryRepository dbCategory)
        {
            _dbCategory = dbCategory;
        }
        public void OnGet(int id)
        {
            Category = _dbCategory.GetById(id);
        }
        public async Task<IActionResult> OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "סדר ההזמנה לא אמור להיות תואם לשם");
            }
            if (ModelState.IsValid)
            {
                _dbCategory.Add(Category);
                _dbCategory.Save();
                TempData["success"] = "הקטגוריה נוצרה בהצלחה";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}