using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class GrpcContext:DbContext
    {
        public GrpcContext(DbContextOptions<GrpcContext> options):base(options){}
        public DbSet<Category> Categories { get; set; }
    }
}
