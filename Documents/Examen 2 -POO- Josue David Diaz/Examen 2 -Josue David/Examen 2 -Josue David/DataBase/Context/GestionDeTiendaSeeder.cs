using Examen_2__Josue_David.Entity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Examen_2__Josue_David.DataBase.Context
{
    public class GestionDeTiendaSeeder
    {
      
            public static async Task LoadDataAsync(
              GestionDeTiendaDbContext context, ILoggerFactory loggerFactory)
            {
                try
                {
                    await LoadOrderAsync(context, loggerFactory);
                    //await LoadPackageAsync(context, loggerFactory);

                }
                catch (Exception e)
                {
                    var logger = loggerFactory.CreateLogger<GestionDeTiendaSeeder>();
                    logger.LogError(e, "Error inicializando la data del API.");
                }
            }

            public static async Task LoadOrderAsync(GestionDeTiendaDbContext context, ILoggerFactory loggerFactory)
            {
            try
            {
                var jsonFilePath = "SeedData/Order.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var Clientes = JsonConvert.DeserializeObject<List<Cliente>>(jsonContent);

                if (!await context.Clientes.AnyAsync())
                {
                    {
                        await context.Clientes.AddRangeAsync(Clientes);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<GestionDeTiendaSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed Order.");
            }
            }

        }

    }

