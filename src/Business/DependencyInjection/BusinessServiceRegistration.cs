using System.Reflection;
using Business.Features.OperationClaims.Services;
using Business.Features.Users.Services;
using CrossCuttingConcerns.Logging.Serilog;
using CrossCuttingConcerns.Logging.Serilog.Logger;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using DataAccess.Concrete.EfCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependencyInjection;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<ProjectDbContext>((sp, options) =>
        {
            options.UseNpgsql(connectionString);
        });
        
        #region COMMON
        // var coreModule = new CoreModule();
        // services.AddDependencyResolvers(configuration, new ICoreModule[] { coreModule });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        #endregion

        #region LOGGERS
        services.AddSingleton<LoggerServiceBase, FileLogger>();
        #endregion

        #region REPOSITORIES

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
        #endregion
        
        #region SERVICES
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IOperationClaimService, OperationClaimService>();
        #endregion

        return services;
    }
}
