using clutch_position.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace clutch_position.Data.Contexts
{
    public class PositionDbContext : DbContext
    {
        public PositionDbContext(DbContextOptions<PositionDbContext> options) : base(options)
        {
        }

        public DbSet<Position> Positions { get; set; }
    }
}
