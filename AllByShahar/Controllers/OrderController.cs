using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using AllByShahar.DataAccess.Repository.IRepository;

namespace AllByShahar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
		private readonly IOrderHeaderRepository _orderHeader;

		public OrderController(IOrderHeaderRepository orderHeader)
		{
			_orderHeader = orderHeader;
		}

		[HttpGet]
        [Authorize]
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

            return Json(new { data = OrderHeaderList });
        }

        
    }
}
