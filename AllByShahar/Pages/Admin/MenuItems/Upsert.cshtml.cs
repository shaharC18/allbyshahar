using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AllByShahar.DataAccess.Data;
using AllByShahar.DataAccess.Repository;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;

namespace AllByShahar.Pages.Admin.MenuItems
{
	[BindProperties]
	public class UpsertModel : PageModel
	{
		private readonly IMenuItemRepository _dbMenuItem;
		private readonly ICategoryRepository _dbCategory;
		private readonly ITypeItemRepository _dbType;

		private readonly IWebHostEnvironment _hostEnvironment;

		public MenuItem MenuItem { get; set; }
		public Category Category { get; set; }
		public Models.TypeItem TypeItem { get; set; }

		public IEnumerable<SelectListItem> CategoryList { get; set; }
		public IEnumerable<SelectListItem> TypeList { get; set; }

		public UpsertModel(IMenuItemRepository dbMenuItem, ICategoryRepository dbCategory, ITypeItemRepository dbType, IWebHostEnvironment hostEnvironment)
		{
			_dbMenuItem = dbMenuItem;
			_dbCategory = dbCategory;
			_dbType = dbType;
			_hostEnvironment = hostEnvironment;
			MenuItem = new();
		}
		public void OnGet(int? id)
		{
			if (id != null)
			{
				//Edit
				MenuItem = _dbMenuItem.GetFirstOrDefault(u => u.Id == id);
			}
			CategoryList = _dbCategory.GetAll().Select(i => new SelectListItem()
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
			TypeList = _dbType.GetAll().Select(i => new SelectListItem()
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
		}
		public async Task<IActionResult> OnPost()
		{

			string webRootPath = _hostEnvironment.WebRootPath;
			var files = HttpContext.Request.Form.Files;
			if (MenuItem.Id == 0)
			{
				//create
				string fileName_new = Guid.NewGuid().ToString();
				var uploads = Path.Combine(webRootPath, @"images\menuItems");
				var extension = Path.GetExtension(files[0].FileName);

				using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
				{
					files[0].CopyTo(fileStream);
				}
				MenuItem.Image = @"\images\menuItems\" + fileName_new + extension;
				if (MenuItem.Description == null)
					MenuItem.Description = ".";

                _dbMenuItem.Add(MenuItem);
				_dbMenuItem.Save();
			}
			else
			{
				//edit
				var objFromDb = _dbMenuItem.GetFirstOrDefault(u => u.Id == MenuItem.Id);
				if (files.Count > 0)
				{
					string fileName_new = Guid.NewGuid().ToString();
					var uploads = Path.Combine(webRootPath, @"images\menuItems");
					var extension = Path.GetExtension(files[0].FileName);

					//delete the old image
					var oldImagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
					if (System.IO.File.Exists(oldImagePath))
					{
						System.IO.File.Delete(oldImagePath);
					}
					//new upload
					using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
					{
						files[0].CopyTo(fileStream);
					}
					MenuItem.Image = @"\images\menuItems\" + fileName_new + extension;
				}
				else
				{
					MenuItem.Image = objFromDb.Image;
				}
                if (MenuItem.Description == null)
                    MenuItem.Description = ".";

                _dbMenuItem.Update(MenuItem);
				_dbMenuItem.Save();
			}

			return RedirectToPage("./Index");
		}
	}
}
