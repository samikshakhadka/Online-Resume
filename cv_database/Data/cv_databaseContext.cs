using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using cv_database.Models;

namespace cv_database.Data
{
    public class cv_databaseContext : DbContext
    {
        public cv_databaseContext (DbContextOptions<cv_databaseContext> options)
            : base(options)
        {
        }
        public DbSet<cv_database.Models.Information>? Information { get; set; }
        public DbSet<cv_database.Models.contact>? contact { get; set; }
        public DbSet<cv_database.Models.Skills>? Skills { get; set; }
        public DbSet<cv_database.Models.Education>? Education { get; set; }
        public DbSet<cv_database.Models.Experience>? Experience { get; set; }
       
        
       
        

       

        
    }
}
