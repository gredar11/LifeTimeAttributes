using Microsoft.Extensions.DependencyInjection;

namespace LifetimeAttributes.Attributes;

public sealed class SingletonAttribute : BaseLifetimeAttribute
{
    public SingletonAttribute() : base(ServiceLifetime.Singleton)
    {
    }

    public SingletonAttribute(Type? implementationType = null) : base(ServiceLifetime.Singleton, implementationType)
    {
    }
}
