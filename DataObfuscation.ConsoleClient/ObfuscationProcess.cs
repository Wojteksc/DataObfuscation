using DataObfuscation.Application.Services;
using DataObfuscation.Fakers.Crm;
using DataObfuscation.Models.Crm;
using Microsoft.Extensions.DependencyInjection;

namespace DataObfuscation.ConsoleClient
{
	public class ObfuscationProcess
	{
		private ServiceProvider provider;

		public ObfuscationProcess(ServiceProvider provider)
		{
			this.provider = provider;
		}

		public void Execute()
		{
			provider.GetService<Obfuscation<Customer, CustomerFaker>>().Execute();
		}
	} 
}
