using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AllByShahar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SD.ManagerRole + "," + SD.FrontDeskRole)]
    public class OrderController : Controller
    {
		private readonly IOrderHeaderRepository _orderHeader;

		public OrderController(IOrderHeaderRepository orderHeader)
		{
			_orderHeader = orderHeader;
		}

		[HttpGet]
        public IActionResult Get(string? status=null)
        {

            var OrderHeaderList = _orderHeader.GetAll(includeProperties:"ApplicationUser");

            if(status== "cancelled")
            {
                OrderHeaderList = OrderHeaderList.Where(u => u.Status == SD.StatusCancelled || u.Status == SD.StatusRejected);
            }
            else
            {
                if (status == "completed")
                {
                    OrderHeaderList = OrderHeaderList.Where(u => u.Status == SD.StatusCompleted );
                }
                else
                {
                    if (status == "ready")
                    {
                        OrderHeaderList = OrderHeaderList.Where(u => u.Status == SD.StatusReady);
                    }
                    else
                    {
                        OrderHeaderList = OrderHeaderList.Where(u => u.Status == SD.StatusSubmitted || u.Status == SD.StatusInProcess);
                    }
                }
            }

            // מחזירים רק את השדות שהטבלה צריכה, בלי פרטי המשתמש הרגישים (סיסמה מוצפנת וכו')
            var result = OrderHeaderList.Select(u => new
            {
                id = u.Id,
                pickupName = u.PickupName,
                applicationUser = new { email = u.ApplicationUser.Email },
                orderTotal = u.OrderTotal,
                pickUpTime = u.PickUpTime
            });

            return Json(new { data = result });
        }

        
    }
}
