using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExamenEric_Mullo.Models;

namespace ExamenEric_Mullo.Data
{
    public class ExamenEric_MulloContext : DbContext
    {
        public ExamenEric_MulloContext (DbContextOptions<ExamenEric_MulloContext> options)
            : base(options)
        {
        }

        public DbSet<ExamenEric_Mullo.Models.Celular> Celular { get; set; } = default!;
        public DbSet<ExamenEric_Mullo.Models.EMullo> EMullo { get; set; } = default!;
    }
}
