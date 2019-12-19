using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StarWarsCharacters.Contexts;
using StarWarsCharactersUnitTest.Contexts;
using Microsoft.EntityFrameworkCore.InMemory;

namespace StarWarsCharactersUnitTest.Contexts
{
    public class InMemoryDbContextFactory
    {
        public ApplicationDBContext GetApplicationDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                            .UseInMemoryDatabase(databaseName: "InMemoryApplicationDatabase")
                            .Options;
            var dbContext = new ApplicationDBContext(options);

            return dbContext;
        }
    }
}
