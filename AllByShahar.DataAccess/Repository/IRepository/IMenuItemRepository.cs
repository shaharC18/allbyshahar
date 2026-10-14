using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllByShahar.Models;

namespace AllByShahar.DataAccess.Repository.IRepository
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        void Update(MenuItem MenuItem);
        void Remove(MenuItem MenuItem);
        MenuItem GetById(int id);

        void Save();

    }
}
