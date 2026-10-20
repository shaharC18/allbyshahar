using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;
using AllByShahar.Utility;

namespace AllByShahar.Pages.Customer.Cart
{
    [Authorize]

    public class IndexModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        private readonly IShoppingCartRepository _shoppingCart;
        private readonly IOrderDetailRepository _orderDetail;

        public double CartTotal { get; set; }
        public IndexModel(IShoppingCartRepository ShoppingCart, IOrderDetailRepository orderDetail)
        {
            _shoppingCart = ShoppingCart;
            CartTotal = 0;
            _orderDetail= orderDetail;


        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _shoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
                    includeProperties: "MenuItem,MenuItem.TypeItem,MenuItem.Category");
                foreach (var cartItem in ShoppingCartList)
                {
					CartTotal += (cartItem.MenuItem.Price* cartItem.Count);
                    CartTotal = Math.Round(CartTotal, 2);

				}
            }
        }
		// מחזיר פריט סל רק אם הוא שייך למשתמש המחובר
		private ShoppingCart? GetUserCart(int cartId)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			return _shoppingCart.GetFirstOrDefault(u => u.Id == cartId && u.ApplicationUserId == userId);
		}
		public IActionResult OnPostPlus(int cartId)
		{
			var cart = GetUserCart(cartId);
			if (cart == null)
			{
				return NotFound();
			}
			_shoppingCart.IncrementCount(cart, 1);
			return RedirectToPage("/Customer/Cart/Index");
		}
		public IActionResult OnPostMinus(int cartId)
		{
			var cart = GetUserCart(cartId);
			if (cart == null)
			{
				return NotFound();
			}
            if(cart.Count==1)
            {
				var count = _shoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count - 1;

				_shoppingCart.Remove(cart);
                _shoppingCart.Save();
				HttpContext.Session.SetInt32(SD.SessionCart, count);

			}
			else
            {
				_shoppingCart.DecrementCount(cart, 1);
			}
			
			return RedirectToPage("/Customer/Cart/Index");
		}
		public IActionResult OnPostRemove(int cartId)
		{
			var cart = GetUserCart(cartId);
			if (cart == null)
			{
				return NotFound();
			}
			var count = _shoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count - 1;

			_shoppingCart.Remove(cart);
			_shoppingCart.Save();
			HttpContext.Session.SetInt32(SD.SessionCart, count);

			return RedirectToPage("/Customer/Cart/Index");
		}
	}
}
    
