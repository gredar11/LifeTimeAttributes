// See https://aka.ms/new-console-template for more information
using System.Data.Common;
using LifetimeAttributes;
using LifetimeAttributes.Extensions;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddClasses();

var serviceProvider = serviceCollection.BuildServiceProvider();
var generic = serviceProvider.GetRequiredService<GenericClass<int,string>>();

Console.WriteLine("Hello, World!");

[Scoped]
class GenericClass<T,Y>{
    public T Prop1 { get; set; }
    public Y Prop2 { get; set; }
}
