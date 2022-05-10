using Microsoft.EntityFrameworkCore;
using NasaApi.Persistence.Context;
using System;

namespace NasaApi.Moq.Mocks
{
    public class MockNeoContext
    {
        public static MyDbContext GetNeoContext()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

            var databaseContext = new MyDbContext(options);
            databaseContext.Database.EnsureCreated();

            return databaseContext;
        }

    }
}
