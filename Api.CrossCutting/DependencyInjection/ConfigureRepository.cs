using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));  //Conexão com o banco fica scoped
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));
            var connectionString = "Server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=password";

            serviceCollection.AddDbContext<MyContext>(
                options => options.UseMySql(connectionString, serverVersion)
                );
        }

        
    }
}
