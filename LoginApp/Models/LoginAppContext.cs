using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApp.Models
{
    public class LoginAppContext : DbContext
    {
        public LoginAppContext()
        {
        }

        public LoginAppContext(DbContextOptions<LoginAppContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
