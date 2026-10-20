using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AllByShahar.Utility;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AllByShahar.DataAccess.Data;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;

namespace AllByShahar.Pages.Admin.TypeItems
{
    [Authorize(Roles = SD.ManagerRole)]
    public class IndexModel : PageModel
    {
        private readonly ITypeItemRepository _dbType;

        public IEnumerable<TypeItem> Types { get; set; }

        public IndexModel(ITypeItemRepository dbType)
        {
            _dbType = dbType;
        }

        public void OnGet()
        {
            Types = _dbType.GetAll();
        }
    }
}
