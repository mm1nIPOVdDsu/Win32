using System;

namespace Win32.Common.Services.NamedPipes
{
    /// <summary>
    ///     TODO: Summary
    /// </summary>
    public interface INamedPipeService : IServiceBase, IDisposable
    {
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        event Action<string> ReceiveString;

        /// <summary>
        ///     True if the pipe server is running.
        /// </summary>
        bool IsRunning { get; }
        /// <summary>
        ///     Name of the pipe that was created.
        /// </summary>
        string PipeName { get; }

        /// <summary>
        ///     Starts a new Pipe server on a new thread.
        /// </summary>
        /// <param name="name">The name of the pipe.</param>
        void StartServer(string name);
        /// <summary>
        ///     Shuts down the pipe server
        /// </summary>
        void StopServer();
        /// <summary>
        ///     Write a client message to the pipe
        /// </summary>
        /// <param name="text"></param>
        /// <param name="connectTimeout"></param>
        /// <param name="pipeName">If the pipe has already been created, use the name to contact the running pipe.</param>
        bool Write(string text, int connectTimeout = 300, string pipeName = "");
    }
}
