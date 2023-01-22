using myShop.Application.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using myShop.Infrastructure.Persistence.Data;

namespace myShop.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private MyShopContext _dbContext;

        public GenericRepository(MyShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Add(FormattableString sqlQuery)
        {
            var result = await _dbContext.Database.SqlQuery<int>(sqlQuery).ToListAsync();
            await _dbContext.SaveChangesAsync();
            return result.FirstOrDefault();
        }

        public async Task<int> Delete(FormattableString sqlQuery)
        {
            var result = await _dbContext.Database.ExecuteSqlInterpolatedAsync(sqlQuery);
            await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<TEntity> Get(FormattableString sqlQuery)
        {
            List<TEntity> result = await _dbContext.Set<TEntity>().FromSqlInterpolated(sqlQuery).AsNoTracking().ToListAsync();

            return result.FirstOrDefault()!;
        }

        public async Task<IReadOnlyList<TEntity>> GetAll(FormattableString sqlQuery)
        {
            List<TEntity> result = await _dbContext.Set<TEntity>().FromSqlInterpolated(sqlQuery).AsNoTracking().ToListAsync();

            return result;
        }

        //public Task<int> GetCount(FormattableString sqlQuery)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<int> Update(FormattableString sqlQuery)
        {
            var result = await _dbContext.Database.ExecuteSqlInterpolatedAsync(sqlQuery);
            await _dbContext.SaveChangesAsync();
            return result;
        }
    }
}
