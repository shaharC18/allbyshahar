using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllByShahar.DataAccess.Data;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;

namespace AllByShahar.DataAccess.Repository
{
	public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
	{
        private readonly ApplicationDbContext _db;

        public MenuItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(MenuItem obj)
        {
            var objFromDb = _db.MenuItem.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
				objFromDb.Name = obj.Name;
				objFromDb.Description = obj.Description;
				objFromDb.Price = obj.Price;
				objFromDb.CategoryId = obj.CategoryId;
				objFromDb.TypeId = obj.TypeId;
				if (objFromDb.Image != null)
				{
					objFromDb.Image = obj.Image;
				}

			}
		}
        public void Remove(int id)
        {
            var deleteItem = _db.MenuItem.FirstOrDefault(u => u.Id == id);
            _db.MenuItem.Remove(deleteItem);
            _db.SaveChanges();
        }
        public MenuItem GetById(int id)

        {
            return _db.MenuItem.FirstOrDefault(u => u.Id == id);

        }


    }
}
