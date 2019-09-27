using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Contexts;

namespace VOD.Database.Services
{
    public class DbReadService : IDbReadService
    {
        private VODContext _db;
        public DbReadService(VODContext db)
        {
            _db = db;
        }

        public async Task<List<TEntity>> GetAsync<TEntity>() where TEntity : class
        {
            return await _db.Set<TEntity>().ToListAsync();
        }
    }
}
