using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StarWarsSampleApp.Persistence;
using Xunit;

namespace StarWarsSampleApp.Tests.Infrastructure
{
    public class TestFixture
    {
        public StarWarsSampleAppDbContext Context { get; }

        public IMapper Mapper { get; }

        public string ConnectionString { get; }

        public TestFixture()
        {
            ConnectionString = GetConnectionString();
            Context = GetDbContext();
            Mapper = AutoMapperFactory.Create();
        }

        private StarWarsSampleAppDbContext GetDbContext()
        {
            var builder = new DbContextOptionsBuilder<StarWarsSampleAppDbContext>();
            builder.UseSqlServer(ConnectionString);

            var dbContext = new StarWarsSampleAppDbContext(builder.Options);

            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        private string GetConnectionString()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional:false)
                .AddEnvironmentVariables()
                .Build()
                .GetConnectionString("StarWarsSampleAppTestDatabase");
        }

        [CollectionDefinition("Test collection")]
        public class TestCollection : ICollectionFixture<TestFixture>
        {
            // This class has no code, and is never created. Its purpose is simply
            // to be the place to apply [CollectionDefinition] and all the
            // ICollectionFixture<> interfaces.
        }
    }
}
