using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntersheepDeeplay.Model
{
    public class IntersheepContext : DbContext
    {
        public IntersheepContext() : base()
        {

        }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
    }
}
