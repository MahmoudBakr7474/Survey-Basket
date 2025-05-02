using FluentValidation.AspNetCore;
using MapsterMapper;
using System.Reflection;

namespace SurveyBasket;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        // Add services to the container.

        services.AddControllers();
        services
            .AddSwaggerServices()
            .AddMapsterConf()
            .AddFluentValidationConf();


        services.AddOpenApi();

        services.AddScoped<IPollService, PollService>();

        

        return services;
    }

    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddEndpointsApiExplorer();

        return services;
    }

    public static IServiceCollection AddMapsterConf(this IServiceCollection services)
    {

        // Add Mapster
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMapper>(new Mapper(mappingConfig));

        return services;
    }
    public static IServiceCollection AddFluentValidationConf(this IServiceCollection services)
    {

        services
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddFluentValidationAutoValidation();

        return services;
    }

}
