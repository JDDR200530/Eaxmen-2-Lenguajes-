using Examen_2_Lenguajes.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
                logger.LogError(e, "Error inicializacion la data API");
            }
        }

        public static async Task LoadUserAsync(
            UserManager<UserEntity> userManager,
            ILoggerFactory loggerFactory
            )
        {
            try
            {
                if (!await userManager.Users.AnyAsync())
                {
                    var normalUser = new UserEntity
                    {
                        FirstName = string.Empty,
                        LastName = string.Empty,
                        Email = string.Empty,
                        UserName = string.Empty,
                    };
                    await userManager.CreateAsync(normalUser, "Temporal1!");

                }
            }
            catch (Exception e) 
            {
                var looger = loggerFactory.CreateLogger<PartidaSeeder>();
                looger.LogError(e.Message);
            }
        }
        public static async Task LoadPartidaAsync(ILoggerFactory loggerFactory, PartidasDbContext context)
        {
            try
            {
                var jsonFilePath = "SeeData/Partidas.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var partidas = JsonConvert.DeserializeObject<List<PartidaEntity>>(jsonContent);

                if (!await context.Partidas.AnyAsync())
                {
                    var user = await context.UserClaims.FirstOrDefaultAsync();
                    for (int i = 0; i < partidas.Count; i++)
                    {
                        partidas[i].CreatedBy = user.UserId;
                        partidas[i].CreatedDate = DateTime.Now;
                        partidas[i].UpdatedBy = user.UserId;
                        partidas[i].UpdatedDate = DateTime.Now;
                    }
                    context.AddRange(partidas);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PartidaSeeder>();
                logger.LogError(e, "Error al Ejecutar el Seed Partidas");
            }
        }
            public static async Task LoadCuentaAsync(ILoggerFactory loggerFactory, PartidasDbContext context)
        {
            try
            {
                var jsonFilePath = "SeeData/CuentasContables.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var partidas = JsonConvert.DeserializeObject<List<PartidaEntity>>(jsonContent);

                if (!await context.Partidas.AnyAsync())
                {
                    var user = await context.UserClaims.FirstOrDefaultAsync();
                    for (int i = 0; i < partidas.Count; i++)
                    {
                        partidas[i].CreatedBy = user.UserId;
                        partidas[i].CreatedDate = DateTime.Now;
                        partidas[i].UpdatedBy = user.UserId;
                        partidas[i].UpdatedDate = DateTime.Now;
                    }
                    context.AddRange(partidas);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PartidaSeeder>();
                logger.LogError(e, "Error al Ejecutar el Seed Partidas");
            }
        }
            
            
            
     }

}
