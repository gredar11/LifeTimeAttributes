using Microsoft.Extensions.DependencyInjection;

namespace LifetimeAttributes;

public class ScopedAttribute : BaseLifetimeAttribute
{
    public ScopedAttribute() : base(ServiceLifetime.Scoped)
    {
    }

    public ScopedAttribute(Type? implementationType = null) : base(ServiceLifetime.Scoped, implementationType)
    {
    }
}
