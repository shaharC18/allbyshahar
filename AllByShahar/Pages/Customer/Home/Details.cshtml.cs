using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AllByShahar.DataAccess.Repository;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;
using AllByShahar.Utility;

namespace AllByShahar.Pages.Customer.Home
{
    [Authorize]

    public class DetailsModel : PageModel
	{
        private readonly IShoppingCartRepository _dbShoppingCart;

        private readonly IMenuItemRepository _dbMenuItem;


        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; }
        public DetailsModel(IShoppingCartRepository dbShoppingCart, IMenuItemRepository dbMenuItem)
		{
			_dbMenuItem = dbMenuItem;
            _dbShoppingCart = dbShoppingCart;


        }
		public void OnGet(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCart = new()
            {
                ApplicationUserId = claim.Value,
                MenuItem = _dbMenuItem.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,TypeItem"),
                MenuItemId = id

            };
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                ShoppingCart shoppingCartFromDb = _dbShoppingCart.GetFirstOrDefault(
                   filter: u => u.ApplicationUserId == ShoppingCart.ApplicationUserId &&
                    u.MenuItemId == ShoppingCart.MenuItemId);

                if (shoppingCartFromDb == null)
                {

                    _dbShoppingCart.Add(ShoppingCart);
                    _dbShoppingCart.Save();
					HttpContext.Session.SetInt32(SD.SessionCart,
						_dbShoppingCart.GetAll(u => u.ApplicationUserId == ShoppingCart.ApplicationUserId).ToList().Count);
				}
                else
                {
                    _dbShoppingCart.IncrementCount(shoppingCartFromDb, ShoppingCart.Count);
                }
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}