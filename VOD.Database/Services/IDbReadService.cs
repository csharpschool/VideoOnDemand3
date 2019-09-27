using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Database.Services
{
    public interface IDbReadService
    {
        Task<List<TEntity>> GetAsync<TEntity>() where TEntity : class;
    }
}
