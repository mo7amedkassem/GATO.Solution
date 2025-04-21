using Gato.Repository.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Repository.identity
{
    public class AppIdentityDBContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppIdentityDBContext(DbContextOptions<AppIdentityDBContext> Options)
            :base(Options) 
        {

        }

        public DbSet<User> User { get; set; }

    }
}
