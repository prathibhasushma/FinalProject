using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinalProject1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FinalProject1.Data
{
    public class FinalProject1Context : IdentityDbContext
    {
        public FinalProject1Context (DbContextOptions<FinalProject1Context> options)
            : base(options)
        {
        }

        public DbSet<FinalProject1.Models.Employee> Employee { get; set; } = default!;

        public DbSet<FinalProject1.Models.Vehicle> Vehicle { get; set; }

        public DbSet<FinalProject1.Models.Route_> Route_ { get; set; }

        public DbSet<FinalProject1.Models.Allocate> Allocate { get; set; }


        

        //public DbSet<FinalProject1.Models.Allocate> Allocate { get; set; }

        //public DbSet<FinalProject1.Models.Match> Match { get; set; }

   

        //public DbSet<FinalProject1.Models.Allocate> Allocate { get; set; }

        //public DbSet<FinalProject1.Models.Match> Match { get; set; }

        //public DbSet<FinalProject1.Models.Class> Class { get; set; }

        //public dbset<finalproject1.models.allocate> allocate { get; set; }


    }
}
