using Bogus;
using DataObfuscation.Models.Crm;

namespace DataObfuscation.Fakers.Crm
{
	public class CustomerFaker : Faker<Customer>
	{
		public CustomerFaker()
		{
			StrictMode(true);
			Ignore(p => p.Id);
			RuleFor(p => p.FirstName, f => f.Person.FirstName);
			RuleFor(p => p.LastName, f => f.Person.LastName);
		}
	}
}
