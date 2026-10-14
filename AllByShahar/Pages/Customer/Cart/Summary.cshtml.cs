using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;
using System.Security.Claims;
using AllByShahar.DataAccess.Repository;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;
using System.Linq;

using AllByShahar.Utility;

namespace AllByShahar.Pages.Customer.Cart
{
    [Authorize]
	[BindProperties]

	public class SummaryModel : PageModel
    {
		Random rnd=new Random();
		public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
		private readonly IShoppingCartRepository _shoppingCart;
		private readonly IApplicationUserRepository _applicationUser;
		private readonly IOrderHeaderRepository _orderHeader;
		private readonly IOrderDetailRepository _orderDetail;



		public OrderHeader OrderHeader { get; set; }
		public SummaryModel(IShoppingCartRepository ShoppingCart, IApplicationUserRepository AppUser, IOrderHeaderRepository orderHeader, IOrderDetailRepository orderDetail)
		{
			_shoppingCart = ShoppingCart;
			OrderHeader = new OrderHeader();
			_applicationUser = AppUser;
			_orderHeader = orderHeader;
			_orderDetail = orderDetail;
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
					OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
				}
				ApplicationUser applicationUser = _applicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
				OrderHeader.PickupName = applicationUser.FirstName + " " + applicationUser.LastName;
				OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
				OrderHeader.PaymentIntentId = rnd.Next().ToString()+"djthghggxijh";
			}
		}
		public IActionResult OnPost()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claim != null)
			{
				ShoppingCartList = _shoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
					includeProperties: "MenuItem,MenuItem.TypeItem,MenuItem.Category");

				foreach (var cartItem in ShoppingCartList)
				{
					OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
					OrderHeader.OrderTotal= Math.Round(OrderHeader.OrderTotal, 2);
				}

				OrderHeader.Status = SD.StatusPending;
				OrderHeader.OrderDate = System.DateTime.Now;
				OrderHeader.UserId = claim.Value;
				OrderHeader.PickUpTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString() + " " +
				OrderHeader.PickUpTime.ToShortTimeString());
                OrderHeader.PaymentIntentId = rnd.Next().ToString() + "djthgxijh";

                _orderHeader.Add(OrderHeader);
				_orderHeader.Save();
				foreach (var item in ShoppingCartList)
				{
					OrderDetails orderDetails = new()
					{
						MenuItemId = item.MenuItemId,
						OrderId = OrderHeader.Id,
						Name = item.MenuItem.Name,
						Price = item.MenuItem.Price,
						Count = item.Count
					};
					_orderDetail.Add(orderDetails);
				}
				//_shoppingCart.RemoveRange(ShoppingCartList);
				_shoppingCart.Save();
				var domain = "https://localhost:7270/";
				var options = new SessionCreateOptions
                {
					LineItems = new List<SessionLineItemOptions>(), 

					PaymentMethodTypes = new List<string>
				{
				  "card",
				},
					Mode = "payment",
					SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={OrderHeader.Id}",
					CancelUrl = domain + "customer/cart/index",
				};
				//add line items
				foreach (var item in ShoppingCartList)
				{
					var sessionLineItem = new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							//7.99->799
							UnitAmount = (long)(item.MenuItem.Price * 100),
							Currency = "ils",
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = item.MenuItem.Name
							},
						},
						Quantity = item.Count
					};
					options.LineItems.Add(sessionLineItem);
				}
				
				var service = new SessionService();
                Session session = service.Create(options);
				Response.Headers.Add("Location", session.Url);
				
				OrderHeader.SessionId = session.Id;
				OrderHeader.PaymentIntentId = session.PaymentIntentId;
				_orderHeader.Save();
				return new StatusCodeResult(303);
			}

			return Page();


		
		}
	}
}
