using Win32.Common.Providers;

namespace Win32.Common.Services
{
    /// <summary>
    ///     Defines a service.
    /// </summary>
    public interface IServiceBase { }
    /// <summary>
    ///     Defines a service with a <see cref="IProvider"/>.
    /// </summary>
    public interface IServiceBase<T> : IServiceBase where T : class, IProvider { }
}
