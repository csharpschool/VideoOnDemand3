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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            #region Admin Credentials Properties
            var email = "a@b.c";
            var password = "Test123__";

            var user = new VODUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                UserName = email,
                NormalizedUserName = email.ToUpper(),
                EmailConfirmed = true
            };
            #endregion

        }
    }
}
