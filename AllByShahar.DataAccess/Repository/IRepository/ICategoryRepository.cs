using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllByShahar.Models;

namespace AllByShahar.DataAccess.Repository.IRepository
{
	 public interface ICategoryRepository : IRepository<Category>
	{
        void Update(Category category);
        void Remove(Category category);
        Category GetById(int id);

        void Save();
    }
}
