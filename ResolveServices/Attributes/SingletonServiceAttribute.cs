using System;

namespace ResolveServices.Attributes
{
    /// <summary>
    /// Class SingletonAttribute. This class cannot be inherited.
    /// Implements the <see cref="Attribute" />
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public sealed class SingletonServiceAttribute : Attribute
    {
    }
}
