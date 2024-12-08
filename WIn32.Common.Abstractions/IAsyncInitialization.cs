using System.Threading.Tasks;

namespace Win32.Common
{
    /// <summary>
    ///     Marks a type as requiring asynchronous initialization..
    /// </summary>
    public interface IAsyncInitialization
    {
        /// <summary>
        ///     The result of the asynchronous initialization.
        /// </summary>
        Task Initialization { get; }
    }
}
