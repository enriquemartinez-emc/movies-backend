using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using movies_backend.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
RegisterServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigureApplication(app);

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope
        .ServiceProvider.GetRequiredService<MoviesContext>()
        .Database.EnsureCreated();
    // use context
}

app.Run();

static void RegisterServices(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<MoviesContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });
}

static void ConfigureApplication(WebApplication app)
{
    app.UseSwagger(c => c.RouteTemplate = "swagger/{documentName}/swagger.json");
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie API V1"));

    app.UseAuthorization();
    app.MapControllers();
}
