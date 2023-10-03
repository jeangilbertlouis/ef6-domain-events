using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Webhost;
using Webhost.Db;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json",true,true).Build().Get<Config>();
builder.Services.AddSingleton<Config>();

builder.Services.AddDbContext<PersonContext>(options =>
    options.UseSqlServer(config.SqlServerConnection,
        b => b.MigrationsAssembly("Webhost")));

builder.Services.AddMediatR(a =>
{
    //a.Lifetime = ServiceLifetime.Scoped;
    a.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});


builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<PersonContext>();
    //context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();