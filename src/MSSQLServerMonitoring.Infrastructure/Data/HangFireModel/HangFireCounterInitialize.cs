using Microsoft.EntityFrameworkCore;
using MSSQLServerMonitoring.Domain.HangFireModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSSQLServerMonitoring.Infrastructure.Data.HangFireModel
{
    public static class HangFireCounterInitialize
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HangFireCounter>().HasData(
                new HangFireCounter { Id = 1, Counter = 0, Limit = 3}
            );
        }
    }
}
