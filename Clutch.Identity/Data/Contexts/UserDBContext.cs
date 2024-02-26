using clutch_identity.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace clutch_identity.Data.Contexts
{
    public class UserDBContext: DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
    }
}
