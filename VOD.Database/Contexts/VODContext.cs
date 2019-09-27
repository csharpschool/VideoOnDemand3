using System;
using System.Collections.Generic;
using System.Text;
using VOD.Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VOD.Database.Contexts
{
    public class VODContext : IdentityDbContext<VODUser>
    {
        public VODContext(DbContextOptions<VODContext> options) : base(options)
        {
        }

    }
}
