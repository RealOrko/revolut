using Microsoft.EntityFrameworkCore;
using WebAPI.Converters;
using WebAPI.Options;
using WebAPI.Repository;

public class Program
{
    public static bool EnableDataMigration = true;
    
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddEnvironmentVariables();

        builder.Services.AddControllers().AddJsonOptions(opt =>
            opt.JsonSerializerOptions.Converters.Add(new DateTimeConverter()));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.Configure<DataContextOptions>(
            builder.Configuration.GetSection("DataContext"));

        builder.Services.Add(new ServiceDescriptor(typeof(DataContextFactory), (sp) => new DataContextFactory(sp.GetRequiredService<IConfiguration>()), ServiceLifetime.Singleton));
        builder.Services.AddDbContextFactory<DataContext, DataContextFactory>();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthorization();
        app.MapControllers();

        if (EnableDataMigration)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var factory = services.GetRequiredService<DataContextFactory>();
                var context = factory.CreateDbContext();
                context.Database.Migrate();
            }
        }

        app.Run();    
    }
}

