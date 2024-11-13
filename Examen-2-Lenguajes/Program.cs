using Examen_2_Lenguajes;
using Examen_2_Lenguajes.Database.Context;
using Examen_2_Lenguajes.Entity;
using Examen_2_Lenguajes.Services;
using Examen_2_Lenguajes.Services.Intefaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Llamar al método AddServices para configurar tus servicios
AddServices(builder.Services, builder.Configuration);

// Construir la aplicación
var app = builder.Build();

// Ejecutar el seeder dentro de un scope para obtener las dependencias
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        // Obtener el contexto y el UserManager
        var context = services.GetRequiredService<PartidasDbContext>();
        var userManager = services.GetRequiredService<UserManager<UserEntity>>();

        // Ejecutar el Seeder para cargar los datos
        await PartidaSeeder.LoadDataAsync(context, loggerFactory, userManager);
    }
    catch (Exception e)
    {
        // Logear cualquier error durante el proceso de carga de datos
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e, "Error al ejecutar el Seed de Data");
    }
}

// Configuración de middlewares y la ejecución del servidor web
app.UseRouting(); // Esto se puede personalizar dependiendo de tus necesidades (por ejemplo, agregar otros middleware)

await app.RunAsync();

// Este método ahora debe estar fuera de la clase Program.cs (fuera del flujo principal)

void AddServices(IServiceCollection services, IConfiguration configuration)
{
    // Configurar DbContext con tu cadena de conexión
    services.AddDbContext<PartidasDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    // Configurar Identity
    services.AddIdentity<UserEntity, IdentityRole>()
        .AddEntityFrameworkStores<PartidasDbContext>()
        .AddDefaultTokenProviders();

    // Otros servicios que puedas necesitar
    services.AddScoped<IAuditServices, AuditService>(); // Ejemplo de servicio adicional

    // Otras configuraciones de servicios necesarios
}
