using Microsoft.AspNetCore.Authorization;
using AllByShahar.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AllByShahar.Pages.Admin.Order
{
    [Authorize(Roles = SD.ManagerRole + "," + SD.FrontDeskRole)]
    public class OrderListModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
