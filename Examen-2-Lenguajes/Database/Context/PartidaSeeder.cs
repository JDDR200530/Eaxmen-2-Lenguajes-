using Examen_2_Lenguajes.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.WebSockets;

namespace Examen_2_Lenguajes.Database.Context
{
    public class PartidaSeeder
    {
        public static async Task LoadDataAsync(
            PartidasDbContext context,
            ILoggerFactory loggerFactory,
            UserManager<UserEntity> userManager)
        {
    
            try
            {
                await LoadUserAsync(userManager, loggerFactory);
                await LoadPartidaAsync(loggerFactory, context);
                await LoadCuentaAsync(loggerFactory, context);
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PartidaSeeder>();
                logger.LogError(e, "Error al inicializar los datos de la API.");
            }
        }

        private static async Task LoadUserAsync(
            UserManager<UserEntity> userManager,
            ILoggerFactory loggerFactory)
        {
            try
            {
                if (!await userManager.Users.AnyAsync())
                {
                    var normalUser = new UserEntity
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "johndoe@example.com",
                        UserName = "johndoe",
                    };
                    await userManager.CreateAsync(normalUser, "Temporal01!");

                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PartidaSeeder>();
                logger.LogError(e, "Error al cargar usuarios.");
            }
        }

        private static async Task LoadPartidaAsync(ILoggerFactory loggerFactory, PartidasDbContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/Partidas.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var partidas = JsonConvert.DeserializeObject<List<PartidaEntity>>(jsonContent);

                if (!await context.Partidas.AnyAsync()) { 
                    context.Partidas.AddRange(partidas);
                    await context.SaveChangesAsync();
                }
            }
            
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PartidaSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de Partidas.");
            }
        }

        public static async Task LoadCuentaAsync(ILoggerFactory loggerFactory, PartidasDbContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/CuentasContables.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var cuentas = JsonConvert.DeserializeObject<List<CuentaContableEntity>>(jsonContent);

                // Verificar si ya existen registros para evitar duplicados
                if (!await context.CuentaContables.AnyAsync())
                {
                    context.CuentaContables.AddRange(cuentas);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PartidaSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de Cuentas.");
            }
        }


    }



}
