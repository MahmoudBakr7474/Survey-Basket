using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SurveyBasket.Authentication;
using System.Reflection;
using System.Text;

namespace SurveyBasket;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add services to the container.

        services.AddControllers();
        services.AddAuthConfig();

        //connect to DB (SQL)
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection String 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationDbContext>(option =>
        option.UseSqlServer(connectionString));

        services
            .AddSwaggerServices()
            .AddMapsterConfig()
            .AddFluentValidationConfig();


        services.AddOpenApi();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPollService, PollService>();

        

        return services;
    }

    private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddEndpointsApiExplorer();

        return services;
    }

    private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {

        // Add Mapster
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMapper>(new Mapper(mappingConfig));

        return services;
    }
    private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {

        services
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddFluentValidationAutoValidation();

        return services;
    }

    private static IServiceCollection AddAuthConfig(this IServiceCollection services)
    {
        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Z23X8So2IMPsPdZMsHPsUadFyisPgSGH")),
                    ValidIssuer = "SurveyBasketApp",
                    ValidAudience = "SurveyBasketApp users"
                };
            });
        return services;
    }
}
