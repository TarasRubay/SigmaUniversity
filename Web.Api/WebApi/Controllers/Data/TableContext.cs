using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers.Data
{
    public class TableContext : DbContext
    {
        private readonly IOptions<TableOptions> _options;

        public TableContext(IOptions<TableOptions> options)
        {
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_options.Value.DefaultConnectionString);
            
        }


        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<HomeTask> HomeTasks { get; set; }

    }
}
