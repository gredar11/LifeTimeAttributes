// See https://aka.ms/new-console-template for more information
using LifetimeAttributes.Attributes;
using LifetimeAttributes.Extensions;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddServicesWithLifetimeAttribute();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var someClass = serviceProvider.GetRequiredService<SomeClass>();
    }
}
[Singleton]
public class SomeClass{}
