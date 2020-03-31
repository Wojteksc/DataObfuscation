using DataObfuscation.ConsoleClient.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace DataObfuscation.ConsoleClient
{
	class Program
	{
		static ServiceProvider provider;
		static void Main(string[] args)
		{
			var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.Build();

			ConfigureServices(new ServiceCollection(), configuration);

			var obfuscationProcess = new ObfuscationProcess(provider);
			obfuscationProcess.Execute();
		}

		private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddContext(configuration);
			services.AddServices(configuration);

			provider = services.BuildServiceProvider();
		}
	}
}
