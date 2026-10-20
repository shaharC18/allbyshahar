using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;
using System.Security.Claims;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;
using AllByShahar.Utility;

namespace AllByShahar.Pages.Customer.Cart
{
    [Authorize]
    public class OrderConfirmationModel : PageModel
    {

		private readonly IOrderHeaderRepository _orderHeader;
		private readonly IShoppingCartRepository _shoppingCart;

		public int OrderId { get; set; }
		public OrderConfirmationModel(IOrderHeaderRepository orderHeader, IShoppingCartRepository shoppingCart)
		{
			_orderHeader = orderHeader;
			_shoppingCart = shoppingCart;
		}
		public IActionResult OnGet(int id)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			OrderHeader orderHeader = _orderHeader.GetFirstOrDefault(u => u.Id == id);
			// רק בעל ההזמנה יכול לאשר אותה
			if (orderHeader == null || orderHeader.UserId != userId)
			{
				return NotFound();
			}
			if (orderHeader.SessionId != null)
			{
				var service = new SessionService();
				Session session = service.Get(orderHeader.SessionId);
				if (session.PaymentStatus.ToLower() == "paid")
				{
					orderHeader.Status = SD.StatusSubmitted;
					orderHeader.PaymentIntentId = session.PaymentIntentId;
					_orderHeader.Save();
				}
			}
			List<ShoppingCart> shoppingCarts =
				_shoppingCart.GetAll(u => u.ApplicationUserId == orderHeader.UserId).ToList();
			_shoppingCart.RemoveRange(shoppingCarts);
			_shoppingCart.Save();
			OrderId = id;
			return Page();
		}
	}
}
