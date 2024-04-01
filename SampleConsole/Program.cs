// See https://aka.ms/new-console-template for more information
using System.Data.Common;
using LifetimeAttributes;
using LifetimeAttributes.Attributes;
using LifetimeAttributes.Extensions;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddClasses();

var serviceProvider = serviceCollection.BuildServiceProvider();

Console.WriteLine("Hello, World!");

[Scoped(typeof(IDisposable))]
class SomeType
{
    
}
