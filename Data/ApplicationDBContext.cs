using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RestfulWebApi.Models;

namespace RestfulWebApi.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }    
    }
}