using Microsoft.Extensions.DependencyInjection;

namespace LifetimeAttributes.Attributes;

public sealed class ScopedAttribute : BaseLifetimeAttribute
{
    public ScopedAttribute() : base(ServiceLifetime.Scoped)
    {
    }

    public ScopedAttribute(Type? implementationType = null) : base(ServiceLifetime.Scoped, implementationType)
    {
    }
}
