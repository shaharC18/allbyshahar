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
	public class OrderDetailRepository : Repository<OrderDetails>, IOrderDetailRepository
	{
		private readonly ApplicationDbContext _db;

		public OrderDetailRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(OrderDetails obj)
		{
			_db.OrderDetails.Update(obj);
		}

		public void Save()
		{
			_db.SaveChanges();
		}
	}
}