using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop.Application.Interface
{
    public interface IGenericRepository<TEntity> where TEntity: class 
    {
        Task<TEntity> Add(FormattableString sqlQuery);

        Task<int> Update(FormattableString sqlQuery);

        Task<int> Delete(FormattableString sqlQuery);

        Task<TEntity> Get(FormattableString sqlQuery);

        //Task<int> GetCount(FormattableString sqlQuery);

        Task<IReadOnlyList<TEntity>> GetAll(FormattableString sqlQuery);
    }
}
