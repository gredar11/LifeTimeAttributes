using Microsoft.Extensions.DependencyInjection;
namespace LifetimeAttributes;

[AttributeUsage(AttributeTargets.Class)]
public abstract class BaseLifetimeAttribute : Attribute
{

    public BaseLifetimeAttribute(ServiceLifetime serviceLifetime)
    {
        Lifetime = serviceLifetime;
    }

    public ServiceLifetime Lifetime { get; }

}
