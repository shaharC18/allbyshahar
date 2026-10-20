using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AllByShahar.Utility;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol;
using AllByShahar.DataAccess.Data;
using AllByShahar.DataAccess.Repository;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;

namespace AllByShahar.Pages.Admin.TypeItems
{
    [BindProperties]
    [Authorize(Roles = SD.ManagerRole)]
    public class CreateModel : PageModel
    {
        private readonly ITypeItemRepository _dbType;

        public TypeItem TypeItem { get; set; }

        public CreateModel(ITypeItemRepository dbType)
        {
            _dbType = dbType;
        }
        public void OnGet(int id)
        {
            TypeItem = _dbType.GetById(id);
        }
        public async Task<IActionResult> OnPost()
        {
            
            if (ModelState.IsValid)
            {
                _dbType.Add(TypeItem);
                _dbType.Save();
                TempData["success"] = "סוג פריט נוצר בהצלחה";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
