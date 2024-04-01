using System.Reflection;
using LifetimeAttributes.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LifetimeAttributes.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddClasses(this ServiceCollection serviceCollection)
        {
            var assembly = Assembly.GetEntryAssembly() ?? throw new Exception("Assembly can't be null");


            var typesWithLifetime = assembly
                .GetTypes()
                .Where(type => type.CustomAttributes
                    .Any(attr => attr.AttributeType.IsSubclassOf(typeof(BaseLifetimeAttribute))))
                .Select(type => new
                {
                    Type = type, LifeTimeAttribute = type.GetCustomAttribute<BaseLifetimeAttribute>()
                });

            foreach (var typeWithLifetime in typesWithLifetime)
            {
                var serviceType = typeWithLifetime.Type;
                var lifetimeAttribute = typeWithLifetime.LifeTimeAttribute;
                var serviceLifetime = lifetimeAttribute.Lifetime;
                var implementationType = lifetimeAttribute.ImplementationType ?? serviceType;

                var descriptor = ServiceDescriptor.Describe(
                    typeWithLifetime.Type,
                    implementationType,
                    serviceLifetime);

                serviceCollection.TryAdd(descriptor);
            }
        }
    }
}
