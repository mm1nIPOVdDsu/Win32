using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading;

using Microsoft.Extensions.Logging;
using Microsoft.Win32;

using static Win32.Common.Unmanaged.AdvApi32.WinReg;

namespace Win32.Common.Services.Registry
{
    /// <summary>
    ///     Monitors for a change made to a registry key.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class RegistryMonitorService : IRegistryMonitorService
    {
        // Adapted from: https://www.pinvoke.net/default.aspx/advapi32.regnotifychangekeyvalue

        /// <summary>
        ///     An event raised when a change occurs to the defined registry key.
        /// </summary>
        public event RegistryChangedHandler? Changed;
        /// <summary>
        ///     Event raised when an error occurs while monitoring for a change.
        /// </summary>
        public event RegistryChangedHandler? Error;

        private readonly ILogger<RegistryMonitorService> _logger;

        private RegistryNotifyFilter _filter;
        private RegistryKey? _monitorKey;
        private Thread? _monitorThread;
        private string? _registryPath;
        private RegistryHive _hive;
        private RegistryView _view;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RegistryMonitorService"/> class.
        /// </summary>
        /// <param name="logger">An instance of a <see cref="ILogger{RegistryService}"/>.</param>
        public RegistryMonitorService(ILogger<RegistryMonitorService> logger) => _logger = logger;

        /// <summary>
        ///     True if monitor has started.
        /// </summary>
        public bool Monitoring => _monitorThread is not null && _monitorThread.IsAlive;

        /// <summary>
        ///     Starts the registry monitor.
        /// </summary>
        /// <param name="hive">The registry hive of the <paramref name="registryPath"/>.</param>
        /// <param name="registryPath">The path of a key to monitor sans the hive.</param>
        /// <param name="view">32 or 64 bit registry view. Uses the system default if no value is provided.</param>
        /// <param name="filter">The type of change to filter for. Default is a change in value.</param>
        public void Start(RegistryHive hive, string registryPath, RegistryView view = RegistryView.Default, RegistryNotifyFilter filter = RegistryNotifyFilter.ValueChanged)
        {
            if (string.IsNullOrEmpty(registryPath))
                throw new ArgumentNullException(nameof(registryPath));

            lock (this)
            {
                if (Monitoring)
                {
                    _logger.LogWarning("Registry monitor is already started.");
                    return;
                }

                _registryPath = registryPath.ToUpper();
                _filter = filter;
                _hive = hive;
                _view = view;

                _logger.LogDebug("Creating new monitor thread.");
                _monitorThread ??= new Thread(new ThreadStart(MonitorThread)) { IsBackground = true };

                if (!_monitorThread.IsAlive)
                {
                    _logger.LogDebug("Starting monitor background thread.");
                    _monitorThread.Start();
                }
            }
        }
        /// <summary>
        ///     Resumes the registry monitor.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown if <see cref="Start(RegistryHive, string, RegistryView, RegistryNotifyFilter)"/> has not been called.</exception>
        public void Resume()
        {
            if (string.IsNullOrEmpty(_registryPath))
                throw new NullReferenceException("Registry path is empty. Call Start prior to resuming monitor.");
            if (Monitoring)
                throw new Exception("Monitor is already running.");

            Start(_hive, _registryPath, _view, _filter);
        }
        /// <summary>
        ///     Stops the registry monitor.
        /// </summary>
        public void Stop()
        {
            lock (this)
            {
                // removes any change listeners
                Changed = null;
                Error = null;

                _logger.LogInformation("Stopping registry monitor.");
                if (_monitorThread != null)
                {
                    _logger.LogDebug("Stopping monitor thread.");
                    _monitorThread = null;
                }

                // The "Close()" will trigger RegNotifyChangeKeyValue if it is still listening
                if (_monitorKey is not null)
                {
                    _logger.LogDebug("Closing registry key.");
                    _monitorKey.Close();
                    _monitorKey = null;
                }
            }
        }
        /// <summary>
        ///     Background functionality for listening to a registry key change.
        /// </summary>
        private void MonitorThread()
        {
            if (string.IsNullOrEmpty(_registryPath))
                throw new NullReferenceException("Registry path cannot be null or empty.");

            try
            {

                lock (this)
                {
                    _logger.LogDebug("Opening registry key.");
                    _monitorKey = RegistryKey.OpenBaseKey(_hive, _view).OpenSubKey(_registryPath);
                    if (_monitorKey is null)
                        throw new Exception($"Cannot open registry key at {_registryPath}.");
                }

                _logger.LogDebug("Getting handle of registry key.");
                var ptr = _monitorKey.Handle.DangerousGetHandle();
                if (ptr != IntPtr.Zero)
                {
                    while (true)
                    {
                        // If _monitorThread is null that probably means Dispose is being called. Don't monitor anymore.
                        if ((_monitorThread == null) || (_monitorKey == null))
                        {
                            _logger.LogDebug("Class disposed of, breaking loop.");
                            break;
                        }

                        // RegNotifyChangeKeyValue blocks until a change occurs.
                        _logger.LogDebug("Blocking until a change occurs.");
                        var result = RegNotifyChangeKeyValue(ptr, true, (REG_NOTIFY_FILTER)_filter, IntPtr.Zero, false);
                        if (result != 0)
                            throw new Win32Exception(Marshal.GetLastWin32Error());

                        if ((_monitorThread == null) || (_monitorKey == null))
                        {
                            _logger.LogDebug("Class disposed of, breaking loop.");
                            break;
                        }

                        _logger.LogDebug("Raising change event for registry change.");
                        Changed?.Invoke(this, new RegistryChangeEventArgs(this));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while monitoring for a registry change.");
                Error?.Invoke(this, new RegistryChangeEventArgs(this) { Exception = ex });
            }
            finally
            {
                _logger.LogDebug("Loop has exited, stopping monitor.");
                Stop();
            }
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
                    Stop();
                }

                _disposed = true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        bool _disposed = false;

        /// <summary>
        ///     Deconstructor for class.
        /// </summary>
        ~RegistryMonitorService()
        {
            Dispose(false);
        }
    }
}