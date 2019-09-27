using System;
using System.Collections.Generic;
using System.Text;
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

    }
}
