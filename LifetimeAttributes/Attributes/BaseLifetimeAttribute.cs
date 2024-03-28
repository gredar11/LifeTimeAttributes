using Microsoft.Extensions.DependencyInjection;
namespace LifetimeAttributes;

[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
public abstract class BaseLifetimeAttribute : Attribute
{

    protected BaseLifetimeAttribute(ServiceLifetime serviceLifetime)
    {
        Lifetime = serviceLifetime;
    }

    protected BaseLifetimeAttribute(ServiceLifetime serviceLifetime, Type? implementationType = null)
        : this(serviceLifetime)
    {
        ImplementationType = implementationType;
    }

    public ServiceLifetime Lifetime { get; }

    public Type? ImplementationType { get; }
}
