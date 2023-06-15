using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiDempApp.Models;

namespace WebApiDempApp.Data
{
    public class WebApiDempAppContext : DbContext
    {
        public WebApiDempAppContext (DbContextOptions<WebApiDempAppContext> options)
            : base(options)
        {
        }

        public DbSet<WebApiDempApp.Models.Employee> Employee { get; set; } = default!;
    }
}
