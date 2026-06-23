using RonaldsYRonalds.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RonaldsYRonalds.Test.Data
{
    public class ApplicationDbContextInMemory
    {
        // For testing purposes, we can use an in-memory database to avoid hitting the actual database.
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
