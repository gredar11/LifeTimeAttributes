using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LifetimeAttributes.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddClasses()
        {
            var serviceCollection = new ServiceCollection();
            var assembly = typeof(ServiceCollectionExtensions).Assembly;
            var typesWithLifetime = assembly
                .GetTypes()
                .Where(type => type.GetCustomAttribute<BaseLifetimeAttribute>() is not null).Select(type => new
                {
                    Type = type,
                    ServiceLifeTime = type.GetCustomAttributes().First(attr => attr.GetType().IsSubclassOf(typeof(BaseLifetimeAttribute)))
                });
            foreach (var typeWithLifetime in typesWithLifetime)
            {
                var descriptor = ServiceDescriptor.Describe(typeWithLifetime.Type, typeWithLifetime.Type, ServiceLifetime.Scoped);
                var attributeProperties = typeWithLifetime.ServiceLifeTime.GetType().GetProperties();
                foreach (var prop in attributeProperties)
                {
                    var valueOfProp = prop.Name == nameof(BaseLifetimeAttribute.Lifetime) 
                        ? prop.GetValue(typeWithLifetime.ServiceLifeTime) : null;
                }
            }
        }
    }
}