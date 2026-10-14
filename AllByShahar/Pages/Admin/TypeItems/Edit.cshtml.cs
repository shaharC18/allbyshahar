using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AllByShahar.DataAccess.Data;
using AllByShahar.DataAccess.Repository;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;

namespace AllByShahar.Pages.Admin.TypeItems
{
	[BindProperties]
	public class EditModel : PageModel
    {
        private readonly ITypeItemRepository _dbType;

        public TypeItem TypeItem { get; set; }

        public EditModel(ITypeItemRepository dbType)
        {
            _dbType = dbType;
        }
        public void OnGet(int id)
        {
            TypeItem = _dbType.GetById(id);
        
            //TypeItem = _db.TypeItem.FirstOrDefault(u=>u.Id==id);
            //TypeItem = _db.TypeItem.SingleOrDefault(u=>u.Id==id);
            //TypeItem = _db.TypeItem.Where(u => u.Id == id).FirstOrDefault();
            if (TypeItem != null)
            {
                _dbType.Remove(TypeItem);
            }

        }

        public async Task<IActionResult> OnPost()
        {
            
            if (ModelState.IsValid)
            {
                _dbType.Update(TypeItem);
                _dbType.Save();
                TempData["success"] = "סוג פריט התעדכן בהצלחה";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}

