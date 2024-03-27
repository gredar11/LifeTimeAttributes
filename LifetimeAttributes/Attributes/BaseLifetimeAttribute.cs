using Microsoft.Extensions.DependencyInjection;
namespace LifetimeAttributes;

[AttributeUsage(AttributeTargets.All)]
public abstract class BaseLifetimeAttribute : Attribute
{

    public BaseLifetimeAttribute(ServiceLifetime serviceLifetime)
    {
        Lifetime = serviceLifetime;
    }

    public BaseLifetimeAttribute(ServiceLifetime serviceLifetime, Type? implementationType = null)
        : this(serviceLifetime)
    {
        ImplementationType = implementationType;
    }

    public ServiceLifetime Lifetime { get; }

    public Type? ImplementationType { get; }
}
