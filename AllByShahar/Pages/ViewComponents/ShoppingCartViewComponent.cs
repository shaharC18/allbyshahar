using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Utility;

namespace AllByShahar.Pages.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IApplicationUserRepository _applicationUser;
        private readonly IShoppingCartRepository _shoppingCart;

        public ShoppingCartViewComponent(IApplicationUserRepository applicationUser, IShoppingCartRepository shoppingCart)
        {
            _applicationUser = applicationUser;
            _shoppingCart = shoppingCart;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int count = 0;
            if (claim != null)
            {
                //user is logged in
                if (HttpContext.Session.GetInt32(SD.SessionCart) != null)
                {
                    return View(HttpContext.Session.GetInt32(SD.SessionCart));
                }
                else
                {
                    count = _shoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count;
                    HttpContext.Session.SetInt32(SD.SessionCart, count);
                    return View(count);
                }
            }
            else
            {
                HttpContext.Session.Clear();
                //user has not logged in
                return View(count);
            }
        }
    }
}
