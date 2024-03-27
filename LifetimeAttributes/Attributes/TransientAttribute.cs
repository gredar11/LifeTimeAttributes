using Microsoft.Extensions.DependencyInjection;

namespace LifetimeAttributes;

public class TransientAttribute : BaseLifetimeAttribute
{
    public TransientAttribute() : base(ServiceLifetime.Transient)
    {
    }

    public TransientAttribute(Type? implementationType = null) : base(ServiceLifetime.Transient, implementationType)
    {
    }
}
