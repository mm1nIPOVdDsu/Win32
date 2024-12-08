using System;

namespace Win32.Common.Attributes
{
    /// <summary>
    ///     Attribute that marks a class as single instance in the dependency injection container.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SingletonAttribute : Attribute { }
}
