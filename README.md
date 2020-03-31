# DataObfuscation

[![Build Status](https://travis-ci.com/Wojteksc/DataObfuscation.svg?branch=master)](https://travis-ci.com/Wojteksc/DataObfuscation)

DataObfuscation is a simple console application to obfuscation data in your database. It ensures easy mapping of faker object to model object.

## Prerequisites

* .NET Core 3.1. [SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1)
* SQL Server

## Case study

1. Create model class in DataObfuscation.Models project

```c#
public class Customer : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
```

2. Create faker class in DataObfuscation.Fakers project

```` c#
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
````
(More information about creating fakers you'll find: https://github.com/bchavez/Bogus)

3. Add DbSet to your DataContext class
```` c#
public DbSet<Customer> Customers { get; set; }
````
4. Execute your service in ObfuscationProcess class
```` c#
provider.GetService<Obfuscation<Customer, CustomerFaker>>().Execute();
````

Don't forget configure your connection string :)

### Demo

![Data Obfuscation](demo/demo2.gif)



## License
[MIT](https://choosealicense.com/licenses/mit/)


