using System;

namespace ResolveServices.Attributes
{
    /// <summary>
    /// Class ScopedServiceAttribute. This class cannot be inherited.
    /// Implements the <see cref="Attribute" />
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public sealed class ScopedServiceAttribute : Attribute
    {
    }
}
