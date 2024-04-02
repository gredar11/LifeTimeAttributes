using Microsoft.Extensions.DependencyInjection;

namespace LifetimeAttributes.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple =true)]
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
