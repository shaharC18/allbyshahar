using Microsoft.AspNetCore.Mvc;
using AllByShahar.DataAccess.Repository.IRepository;

namespace AllByShahar.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MenuItemController : Controller
	{
		private readonly IMenuItemRepository _dbMenuItem;
		private readonly IWebHostEnvironment _hostEnvironment;
		public MenuItemController(IMenuItemRepository dbMenuItem, IWebHostEnvironment hostEnvironment)
		{
			_dbMenuItem = dbMenuItem;
			_hostEnvironment = hostEnvironment;	
		}
		[HttpGet]
		public IActionResult Get()
		{
			var menuItemList = _dbMenuItem.GetAll(includeProperties: "Category,TypeItem");
			return Json(new { data = menuItemList });
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var objFromDb = _dbMenuItem.GetFirstOrDefault(u => u.Id == id);
			var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, objFromDb.Image.TrimStart('\\'));
			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}
			_dbMenuItem.Remove(objFromDb);
			_dbMenuItem.Save();
			return Json(new { success = true, message = "נמחק בהצלחה" });
		}
	}
}

