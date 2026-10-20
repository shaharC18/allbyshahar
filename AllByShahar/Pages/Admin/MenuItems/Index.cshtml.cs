using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AllByShahar.Utility;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AllByShahar.Pages.Admin.MenuItems
{
    [Authorize(Roles = SD.ManagerRole)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
