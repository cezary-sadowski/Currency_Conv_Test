using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Currency_Conv_Test
{
    public class DBConnection : DbContext 
    {
        public DBConnection(DbContextOptions<DBConnection> options) : base(options)
        {

        }

        public DbSet<Requests> Requests { get; set; }
    }
}
