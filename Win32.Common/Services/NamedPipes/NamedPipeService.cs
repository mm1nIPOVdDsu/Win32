using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

using Microsoft.Extensions.Logging;

namespace Win32.Common.Services.NamedPipes
{
    /// <summary>
    ///     TODO: Summary
    /// </summary>
    public class NamedPipeService : INamedPipeService
    {
        private const string EXIT_STRING = "__EXIT__";

        private readonly ILogger<NamedPipeService> _logger;

        private Thread? _thread = null;

        /// <summary>
        ///     TODO: Summary
        /// </summary>
        public event Action<string>? ReceiveString = null;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NamedPipeService"/> class.
        /// </summary>
        /// <param name="logger"></param>
        public NamedPipeService(ILogger<NamedPipeService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Name of the pipe that was created.
        /// </summary>
        public string PipeName { get; private set; } = string.Empty;
        /// <summary>
        ///     True if the pipe server is running.
        /// </summary>
        public bool IsRunning => _isRunning;
        private bool _isRunning = false;

        /// <summary>
        ///     Called when data is received.
        /// </summary>
        /// <param name="text"></param>
        protected virtual void OnReceiveString(string text) => ReceiveString?.Invoke(text);
        /// <summary>
        ///     Starts a new Pipe server on a new thread
        /// </summary>
        public void StartServer(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            PipeName = name;

            var thread = new Thread((pipeName) =>
            {
                _isRunning = true;
                while (true)
                {
                    string text;
                    var pipeNameString = pipeName?.ToString();
                    if (string.IsNullOrEmpty(pipeNameString))
                        return;

                    using (var server = new NamedPipeServerStream(pipeNameString))
                    {
                        server.WaitForConnection();

                        using (var reader = new StreamReader(server))
                            text = reader.ReadToEnd();
                    }

                    if (text == EXIT_STRING)
                        break;

                    OnReceiveString(text);

                    if (_isRunning == false)
                        break;
                }
            });
            thread.Start(PipeName);
        }
        /// <summary>
        ///     Shuts down the pipe server
        /// </summary>
        public void StopServer()
        {
            _isRunning = false;
            Write(EXIT_STRING);
            Thread.Sleep(30); // give time for thread shutdown
        }
        /// <summary>
        ///     Write a client message to the pipe
        /// </summary>
        /// <param name="text"></param>
        /// <param name="connectTimeout"></param>
        /// <param name="pipeName">If the pipe has already been created, use the name to contact the running pipe.</param>
        public bool Write(string text, int connectTimeout = 300, string pipeName = "")
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));
            if (string.IsNullOrEmpty(pipeName))
                pipeName = PipeName;
            if (string.IsNullOrEmpty(pipeName))
                return false;

            using (var client = new NamedPipeClientStream(pipeName))
            {
                try { client.Connect(connectTimeout); }
                catch { return false; }

                if (!client.IsConnected)
                    return false;

                using (var writer = new StreamWriter(client))
                {
                    writer.Write(text);
                    writer.Flush();
                }
            }
            return true;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting
        ///     unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting
        ///     unmanaged resources.
        /// </summary>
        /// <param name="disposing">When disposing managed objects.</param>
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (_disposed)
                    return;

                if (disposing)
                {
                    if (_thread is not null && _thread.IsAlive)
                    {
                        _thread = null;
                    }
                }

                _disposed = true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        bool _disposed = false;
    }
}
