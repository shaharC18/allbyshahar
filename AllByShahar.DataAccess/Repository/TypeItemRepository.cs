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
	public class TypeItemRepository : Repository<TypeItem>, ITypeItemRepository
    {
        private readonly ApplicationDbContext _db;

        public TypeItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(TypeItem TypeItem)
        {
            var objFromDb = _db.TypeItem.FirstOrDefault(u => u.Id == TypeItem.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = TypeItem.Name;
            }
        }
        public void Remove(int id)
        {
            var deleteItem = _db.TypeItem.FirstOrDefault(u => u.Id == id);
            _db.TypeItem.Remove(deleteItem);
            _db.SaveChanges();
        }
        public TypeItem GetById(int id)

        {
            return _db.TypeItem.FirstOrDefault(u => u.Id == id);

        }


    }
}
