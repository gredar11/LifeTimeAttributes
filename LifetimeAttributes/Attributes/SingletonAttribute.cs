using Microsoft.Extensions.DependencyInjection;

namespace LifetimeAttributes;

public class SingletonAttribute : BaseLifetimeAttribute
{
    public SingletonAttribute() : base(ServiceLifetime.Singleton)
    {
    }

    public SingletonAttribute(Type? implementationType = null) : base(ServiceLifetime.Singleton, implementationType)
    {
    }
}
