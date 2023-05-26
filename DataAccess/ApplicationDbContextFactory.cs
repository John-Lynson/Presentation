using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;



namespace DataAccess
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(
                "Server=studmysql01.fhict.local;Database=dbi511796;User=dbi511796;Password=WebDb2023;",
                new MySqlServerVersion(new Version(5, 7, 26)), // gebruik de juiste versie van jouw MySQL server
                mySqlOptions => mySqlOptions
                    .CharSetBehavior(CharSetBehavior.NeverAppend));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
