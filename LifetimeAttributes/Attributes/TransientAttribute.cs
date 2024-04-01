using Microsoft.Extensions.DependencyInjection;

namespace LifetimeAttributes.Attributes;

public sealed class TransientAttribute : BaseLifetimeAttribute
{
    public TransientAttribute() : base(ServiceLifetime.Transient)
    {
    }

    public TransientAttribute(Type? implementationType = null) : base(ServiceLifetime.Transient, implementationType)
    {
    }
}
