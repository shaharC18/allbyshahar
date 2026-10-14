using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;

namespace AllByShahar.Pages.Customer.Home
{
    public class IndexModel : PageModel
    {
        private readonly IMenuItemRepository _dbMenuItem;
        private readonly ICategoryRepository _dbCategory;
        private readonly ITypeItemRepository _dbType;


        public IndexModel(IMenuItemRepository dbMenuItem, ICategoryRepository dbCategory, ITypeItemRepository dbType, IWebHostEnvironment hostEnvironment)
        {
            _dbMenuItem = dbMenuItem;
            _dbCategory = dbCategory;
            _dbType = dbType;
        }

        public IEnumerable<MenuItem> MenuItemList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
        public IEnumerable<TypeItem> TypeList { get; set; }


        public void OnGet()
        {
            MenuItemList = _dbMenuItem.GetAll();
            CategoryList = _dbCategory.GetAll(orderby: u => u.OrderBy(c => c.DisplayOrder));
            TypeList = _dbType.GetAll();

        }
    }
}