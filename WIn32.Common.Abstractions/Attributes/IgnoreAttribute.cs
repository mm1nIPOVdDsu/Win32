using System;

namespace Win32.Common.Attributes
{
    /// <summary>
    ///     Attribute that causes building of the dependency injection container to ignore the class when registering.
    /// </summary>
    [AttributeUsage((AttributeTargets.Class | AttributeTargets.Interface), AllowMultiple = false)]
    public class IgnoreAttribute : Attribute { }
}
