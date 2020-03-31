using DataObfuscation.Application.Mappers;
using DataObfuscation.Application.Options;
using DataObfuscation.Application.Services;
using DataObfuscation.Fakers.Crm;
using DataObfuscation.Infrastructure.Data;
using DataObfuscation.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataObfuscation.ConsoleClient.Extensions
{
	public static class ServiceExtensions
	{
		public static void AddContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContextPool<DataContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
			sqlServerOptions => sqlServerOptions.CommandTimeout(int.Parse(configuration.GetSection("ConnectionStrings:CommandTimeout").Value))));
		}

		public static void AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton(AutoMapperConfig.Initialize());
			services.AddSingleton(configuration);

			services.AddScoped(typeof(IEntityRepository<>), typeof(DbEntityRepository<>));
			services.AddScoped(typeof(Obfuscation<,>), typeof(Obfuscation<,>));
			services.AddScoped<IProgressBarOptions, ObfuscationProgressBarOptions>();
		}
	}
}
