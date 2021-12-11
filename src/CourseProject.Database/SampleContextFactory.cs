#region Using derectives

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

#endregion

namespace CourseProject.Database
{
    public class SampleContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            var connectionString =
                    @"Server=(localdb)\mssqllocaldb;Database=CourseProjectDatabase;Trusted_Connection=True;";

            optionsBuilder.UseSqlServer(connectionString,
                                        opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}