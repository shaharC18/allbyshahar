using AllByShahar.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using AllByShahar.Models;
using AllByShahar.Models.ViewModel;
using AllByShahar.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;
using System.Collections.Generic;
using System.Linq;

namespace AllByShahar.Pages.Admin.Order
{
    [Authorize(Roles = SD.ManagerRole + "," + SD.FrontDeskRole)]
    public class OrderDetailsModel : PageModel
    {
        private readonly IOrderDetailRepository _orderDetail;
        private readonly IOrderHeaderRepository _orderHeader;

        public OrderDetailVM OrderDetailVM { get; set; }
        public OrderDetailsModel(IOrderDetailRepository orderDetail, IOrderHeaderRepository orderHeader)
        {
            _orderDetail = orderDetail;
            _orderHeader = orderHeader;

        }
        public void OnGet(int id)
        {
            OrderDetailVM = new()
            {
                OrderHeader = _orderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "ApplicationUser"),
                OrderDetails = _orderDetail.GetAll(u => u.OrderId == id).ToList()
            };
        }

        public IActionResult OnPostOrderCompleted(int orderId)
        {
            _orderHeader.UpdateStatus(orderId, SD.StatusCompleted);
            _orderHeader.Save();
            return RedirectToPage("OrderList");
        }
        public IActionResult OnPostOrderRefund(int orderId)
        {
            return RedirectToPage("OrderList");

            //OrderHeader orderHeader = _orderHeader.GetFirstOrDefault(o => o.Id == orderId);

            //var options = new RefundCreateOptions
            //{
            //    Reason = RefundReasons.RequestedByCustomer,
            //    PaymentIntent = orderHeader.PaymentIntentId
            //};

            //var service = new RefundService();
            //Refund refund = service.Create(options);

            //_orderHeader.UpdateStatus(orderId, SD.StatusRefunded);
            //_orderHeader.Save();
            //return RedirectToPage("OrderList");
        }

        public IActionResult OnPostOrderCancel(int orderId)
        {
            _orderHeader.UpdateStatus(orderId, SD.StatusCancelled);
            _orderHeader.Save();
            return RedirectToPage("OrderList");
        }
    }
}
