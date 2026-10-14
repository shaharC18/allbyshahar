using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AllByShahar.DataAccess.Data;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;

namespace AllByShahar.Pages.Admin.Categories
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ICategoryRepository _dbCategory;

        public IEnumerable<Category> Categories { get; set; }

        public IndexModel(ICategoryRepository dbCategory)
        {
            _dbCategory = dbCategory;
        }

        public void OnGet()
        {
            Categories = _dbCategory.GetAll();
        }
    }
}
