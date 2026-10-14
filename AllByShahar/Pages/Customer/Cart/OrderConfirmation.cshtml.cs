using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;
using AllByShahar.Utility;

namespace AllByShahar.Pages.Customer.Cart
{
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
		public void OnGet(int id)
		{
			OrderHeader orderHeader = _orderHeader.GetFirstOrDefault(u => u.Id == id);
			if (orderHeader.SessionId != null)
			{
				var service = new SessionService();
				Session session = service.Get(orderHeader.SessionId);
				if (session.PaymentStatus.ToLower() == "paid")
				{
					orderHeader.Status = SD.StatusSubmitted;
					_orderHeader.Save();
				}
			}
			List<ShoppingCart> shoppingCarts =
				_shoppingCart.GetAll(u => u.ApplicationUserId == orderHeader.UserId).ToList();
			_shoppingCart.RemoveRange(shoppingCarts);
			_shoppingCart.Save();
			OrderId = id;

		}
	}
}
