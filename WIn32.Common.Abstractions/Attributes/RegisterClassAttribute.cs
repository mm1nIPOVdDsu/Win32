using System;

namespace Win32.Common.Attributes
{
    /// <summary>
    ///     Forces the registration of a class when the class does not contain an expected interface.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class RegisterClassAttribute : Attribute { }
}
