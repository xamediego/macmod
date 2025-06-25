using macmod.bootstrap;
using macmod.database;
using macmod.services.implementation;
using macmod.services.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace macmod;

public class MacApi
{
    private static async Task<WebApplication> InitializeApplication(string[] args)
    {
        Console.WriteLine("Initializing application");
        
        var builder = WebApplication.CreateBuilder(args);
        
        Console.WriteLine("Is Development: " + builder.Environment.IsDevelopment());
        
        Console.WriteLine("Configuring DI Services");
        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddScoped<IProjectService, ProjectService>();
        builder.Services.AddScoped<IProjectTypeService, ProjectTypeService>();
        builder.Services.AddScoped<IGameMapService, GameMapService>();
        builder.Services.AddScoped<IProgrammingProjectService, ProgrammingProjectService>();
        
        builder.Services.AddScoped<IBlobService, BlobService>();
        Console.WriteLine("Services Configured");
        
        builder.Services.ConfigureApplicationCookie(o =>
        {
            o.ExpireTimeSpan = TimeSpan.FromDays(5);
            o.SlidingExpiration = true;
        });
        
        Console.WriteLine("Configure Database");
        builder.Configuration["DATABASE_URL"] = Environment.GetEnvironmentVariable("DATABASE_URL") ?? builder.Configuration["DATABASE_URL"];
        var dbConnection = builder.Configuration["DATABASE_URL"] ?? "";
        Console.WriteLine(dbConnection);
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite("Data Source=database.db"));
        }
        else
        {
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(dbConnection));
        }

        
        // Configure cors
        const string allowedOrigins = "ALLOWED_CORS";
        
        var frontEndCors = builder.Configuration["FrontEndBaseURL"] ?? "http://localhost:4200";
        Console.WriteLine($"Allowed cors: {allowedOrigins}");
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(allowedOrigins,
                policy =>
                {
                    policy.WithOrigins(frontEndCors)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
        });

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        []
                    }
                });
            });
        }
        
        //Start building
        var app = builder.Build();
        
        //Test DB Connection
        if (!app.Environment.IsDevelopment())
        {
            using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
            
            if (await dbContext.Database.CanConnectAsync())
            {
                Console.WriteLine("Can connect to external database");
            }
            else
            {
                Console.WriteLine("Failed establishing connection to external database");
            }
        }
        
        if (app.Environment.IsDevelopment())
        {
            using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            await DataSeeder.SeedDatabase(serviceScope);
        }
        
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetAuth API V1"); });
        
        app.UseCors(allowedOrigins);
        app.UseAuthorization();
        app.MapControllers();
        Console.WriteLine("Application initialized");

        return app;
    }

    public static async Task Main(string[] args)
    {
        await (await InitializeApplication(args)).RunAsync();
    }
    
}