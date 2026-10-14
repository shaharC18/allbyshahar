using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;
using AllByShahar.Models.ViewModel;
using AllByShahar.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace AllByShahar.Pages.Admin.Order
{
    [Authorize(Roles =$"{SD.ManagerRole}")]
    public class ManageOrderModel : PageModel
    {
        private readonly IOrderDetailRepository _orderDetail;
        private readonly IOrderHeaderRepository _orderHeader;


        public List<OrderDetailVM> OrderDetailVM { get; set; }

        public ManageOrderModel(IOrderDetailRepository orderDetail, IOrderHeaderRepository orderHeader)
        {
            _orderDetail = orderDetail;
            _orderHeader = orderHeader;

        }
        public void OnGet()
        {
            OrderDetailVM = new();

            List<OrderHeader> orderHeaders = _orderHeader.GetAll(u => u.Status == SD.StatusSubmitted ||
            u.Status == SD.StatusInProcess).ToList();

            foreach(OrderHeader item in orderHeaders)
            {
                OrderDetailVM individual = new OrderDetailVM()
                {
                    OrderHeader = item,
                    OrderDetails = _orderDetail.GetAll(u => u.Id == item.Id).ToList()
                };
                OrderDetailVM.Add(individual);
            }
        }

        public IActionResult OnPostOrderInProcess(int orderId)
        {
            _orderHeader.UpdateStatus(orderId, SD.StatusInProcess);
            _orderHeader.Save();
            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderReady(int orderId)
        {
            _orderHeader.UpdateStatus(orderId, SD.StatusReady);
            _orderHeader.Save();
            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderCancel(int orderId)
        {
            _orderHeader.UpdateStatus(orderId, SD.StatusCancelled);
            _orderHeader.Save();
            return RedirectToPage("ManageOrder");
        }
    }
}
