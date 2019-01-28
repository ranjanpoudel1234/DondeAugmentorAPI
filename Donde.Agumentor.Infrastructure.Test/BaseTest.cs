using System;
using Donde.Augmentor.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Donde.Agumentor.Infrastructure.Test
{
    public class BaseTest
    {
        public DondeContext GetDondeContext()
        {
            var options = new DbContextOptionsBuilder<DondeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DondeContext(options);

            return context;
        }
    }
}
