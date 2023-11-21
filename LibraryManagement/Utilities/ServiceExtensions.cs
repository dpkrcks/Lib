using Contracts;
using LoggerService;
using System.Runtime.CompilerServices;

namespace LibraryManagement.Utilities
{
    public static class ServiceExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services) {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
