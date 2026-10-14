using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllByShahar.Models;

namespace AllByShahar.DataAccess.Repository.IRepository
{
    public interface ITypeItemRepository : IRepository<TypeItem>
    {
        void Update(TypeItem TypeItem);
        void Remove(TypeItem TypeItem);
        TypeItem GetById(int id);

        void Save();

    }
}
