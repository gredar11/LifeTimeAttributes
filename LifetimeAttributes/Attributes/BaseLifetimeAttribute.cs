using Microsoft.Extensions.DependencyInjection;
namespace LifetimeAttributes;

[AttributeUsage(AttributeTargets.All)]
public abstract class BaseLifetimeAttribute : Attribute
{

    public BaseLifetimeAttribute(ServiceLifetime serviceLifetime)
    {
        Lifetime = serviceLifetime;
    }

    public ServiceLifetime Lifetime { get; }

}
