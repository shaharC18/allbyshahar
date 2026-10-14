using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AllByShahar.DataAccess.Data;
using AllByShahar.DataAccess.Repository;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;

namespace AllByShahar.Pages.Admin.TypeItems
{
	[BindProperties]
	public class DeleteModel : PageModel
    {
        private readonly ITypeItemRepository _dbTypeItem;

        public TypeItem TypeItem { get; set; }

        public DeleteModel(ITypeItemRepository dbType)
        {
            _dbTypeItem = dbType;
        }
        public void OnGet(int id)
        {
            TypeItem = _dbTypeItem.GetById(id);
           
        }
        public async Task<IActionResult> OnPost()
        {
            //var TypeFromDb = _dbType.Find(TypeItem.Id);


            _dbTypeItem.Remove(TypeItem);
            _dbTypeItem.Save();
            TempData["success"] = "סוג פריט נמחק בהצלחה";
            return RedirectToPage("Index");

        }
    }
}
