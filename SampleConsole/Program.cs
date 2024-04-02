// See https://aka.ms/new-console-template for more information
using LifetimeAttributes.Attributes;

[Singleton(typeof(ICloneable))]
public class Program
{
    public static void Main()
    {

    }
}
