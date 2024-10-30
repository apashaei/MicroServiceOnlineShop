using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDatabaseContext
    {
        DbSet<Payment> Payments { get; set; }
        DbSet<Order> Orders { get; set; }

        int SaveChanges();


    }
}
