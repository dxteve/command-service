using CommandService.Models;
using CommandService.SyncDataServices;

namespace CommandService.Data
{
    public static class PrepareDb
    {
        public static void PreparePopulation(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateAsyncScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

                var platforms = grpcClient!.ReturnAllPlatforms();

                SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>()!, platforms);
            }
        }

        private static void SeedData(ICommandRepo repository, IEnumerable<Platform> platforms)
        {
            System.Console.WriteLine("--> Seeding new platforms...");

            foreach (var platform in platforms)
            {
                if (!repository.ExternalPlatformExists(platform.ExternalId))
                    repository.CreatePlatform(platform);

                repository.SaveChanges();
            }
        }
    }
}