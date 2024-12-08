using System;

using static Win32.Common.Unmanaged.WinNt;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class AdvApi32
        {
            /// <summary>
            ///     WinSvc interactions.
            /// </summary>
            public partial class WinSvc
            {
                /// <summary>
                ///     The service type.
                /// </summary>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-createservicea#parameters">SERVICE_TYPE</seealso>
                public enum SERVICE_TYPE : uint
                {
                    /// <summary>
                    ///     Reserved.
                    /// </summary>
                    SERVICE_ADAPTER = 0x00000004,
                    /// <summary>
                    ///     File system driver service.
                    /// </summary>
                    SERVICE_FILE_SYSTEM_DRIVER = 0x00000002,
                    /// <summary>
                    ///     Driver service.
                    /// </summary>
                    SERVICE_KERNEL_DRIVER = 0x00000001,
                    /// <summary>
                    ///     Reserved.
                    /// </summary>
                    SERVICE_RECOGNIZER_DRIVER = 0x00000008,
                    /// <summary>
                    ///     Service that runs in its own process.
                    /// </summary>
                    SERVICE_WIN32_OWN_PROCESS = 0x00000010,
                    /// <summary>
                    ///     Service that shares a process with one or more other services. For more information, see Service Programs.
                    /// </summary>
                    SERVICE_WIN32_SHARE_PROCESS = 0x00000020,
                    /// <summary>
                    ///     The service can interact with the desktop.
                    /// </summary>
                    /// <remarks>
                    ///     If you specify either SERVICE_WIN32_OWN_PROCESS or SERVICE_WIN32_SHARE_PROCESS, and the service is running in the context of the
                    ///     LocalSystem account, you can also specify this value.
                    /// </remarks>
                    SERVICE_INTERACTIVE_PROCESS = 0x00000100
                }
                /// <summary>
                ///     The service start options.
                /// </summary>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-createservicea#parameters">SERVICE_START_TYPE</seealso>
                public enum SERVICE_START_TYPE : uint
                {
                    /// <summary>
                    ///     A service started automatically by the service control manager during system startup. For more information, see Automatically
                    ///     Starting Services.
                    /// </summary>
                    SERVICE_AUTO_START = 0x00000002,
                    /// <summary>
                    ///     A device driver started by the system loader. This value is valid only for driver services.
                    /// </summary>
                    SERVICE_BOOT_START = 0x00000000,
                    /// <summary>
                    ///     A service started by the service control manager when a process calls the StartService function. For more information, see
                    ///     Starting Services on Demand.
                    /// </summary>
                    SERVICE_DEMAND_START = 0x00000003,
                    /// <summary>
                    ///     A service that cannot be started. Attempts to start the service result in the error code ERROR_SERVICE_DISABLED.
                    /// </summary>
                    SERVICE_DISABLED = 0x00000004,
                    /// <summary>
                    ///     A device driver started by the IoInitSystem function. This value is valid only for driver services.
                    /// </summary>
                    SERVICE_SYSTEM_START = 0x00000001
                }
                /// <summary>
                ///     The current state of the service.
                /// </summary>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/ns-winsvc-service_status">SERVICE_STATE</seealso>
                public enum SERVICE_STATE : uint
                {
                    /// <summary>
                    ///     The service continue is pending.
                    /// </summary>
                    SERVICE_CONTINUE_PENDING = 0x00000005,
                    /// <summary>
                    ///     The service pause is pending.
                    /// </summary>
                    SERVICE_PAUSE_PENDING = 0x00000006,
                    /// <summary>
                    ///     The service is paused.
                    /// </summary>
                    SERVICE_PAUSED = 0x00000007,
                    /// <summary>
                    ///     The service is running.
                    /// </summary>
                    SERVICE_RUNNING = 0x00000004,
                    /// <summary>
                    ///     The service is starting.
                    /// </summary>
                    SERVICE_START_PENDING = 0x00000002,
                    /// <summary>
                    ///     The service is stopping.
                    /// </summary>
                    SERVICE_STOP_PENDING = 0x00000003,
                    /// <summary>
                    ///     The service is not running.
                    /// </summary>
                    SERVICE_STOPPED = 0x00000001,
                }
                /// <summary>
                ///     The control codes the service accepts and processes in its handler function (see Handler and HandlerEx). A user interface process can control a service by specifying a control command in the ControlService or ControlServiceEx function. By default, all services accept the SERVICE_CONTROL_INTERROGATE value.
                /// </summary>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/ns-winsvc-service_status#members">SERVICE_CONTROL_CODES</seealso>
                public enum SERVICE_CONTROL_CODES : uint
                {
                    /// <summary>
                    ///     The service is a network component that can accept changes in its binding without being stopped and restarted.
                    /// </summary>
                    SERVICE_ACCEPT_NETBINDCHANGE = 0x00000010,
                    /// <summary>
                    ///     The service can reread its startup parameters without being stopped and restarted.
                    /// </summary>
                    SERVICE_ACCEPT_PARAMCHANGE = 0x00000008,
                    /// <summary>
                    ///     The service can be paused and continued.
                    /// </summary>
                    SERVICE_ACCEPT_PAUSE_CONTINUE = 0x00000002,
                    /// <summary>
                    ///     The service can perform preshutdown tasks.
                    /// </summary>
                    SERVICE_ACCEPT_PRESHUTDOWN = 0x00000100,
                    /// <summary>
                    ///     The service is notified when system shutdown occurs.
                    /// </summary>
                    SERVICE_ACCEPT_SHUTDOWN = 0x00000004,
                    /// <summary>
                    ///     The service can be stopped.
                    /// </summary>
                    SERVICE_ACCEPT_STOP = 0x00000001,

                    /// <summary>
                    ///     The service is notified when the computer's hardware profile has changed.
                    /// </summary>
                    SERVICE_ACCEPT_HARDWAREPROFILECHANGE = 0x00000020,
                    /// <summary>
                    ///     The service is notified when the computer's power status has changed.
                    /// </summary>
                    SERVICE_ACCEPT_POWEREVENT = 0x00000040,
                    /// <summary>
                    ///     he service is notified when the computer's session status has changed.
                    /// </summary>
                    SERVICE_ACCEPT_SESSIONCHANGE = 0x00000080,
                    /// <summary>
                    ///     The service is notified when the system time has changed.
                    /// </summary>
                    SERVICE_ACCEPT_TIMECHANGE = 0x00000200,
                    /// <summary>
                    ///     The service is notified when an event for which the service has registered occurs.
                    /// </summary>
                    SERVICE_ACCEPT_TRIGGEREVENT = 0x00000400,
                    /// <summary>
                    ///     The services is notified when the user initiates a reboot.
                    /// </summary>
                    SERVICE_ACCEPT_USERMODEREBOOT = 0x00000800
                }
                /// <summary>
                ///     The configuration information type.
                /// </summary>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-changeserviceconfig2a#parameters">SERVICE_CONFIG_INFORMATION</seealso>
                public enum SERVICE_CONFIG_INFORMATION : uint
                {
                    /// <summary>
                    ///     The lpInfo parameter is a pointer to a SERVICE_DELAYED_AUTO_START_INFO structure.
                    /// </summary>
                    SERVICE_CONFIG_DELAYED_AUTO_START_INFO = 3,
                    /// <summary>
                    ///     The lpInfo parameter is a pointer to a <see cref="SERVICE_DESCRIPTION"/> structure.
                    /// </summary>
                    SERVICE_CONFIG_DESCRIPTION = 1,
                    /// <summary>
                    ///     The lpInfo parameter is a pointer to a SERVICE_FAILURE_ACTIONS structure.
                    /// </summary>
                    SERVICE_CONFIG_FAILURE_ACTIONS = 2,
                    /// <summary>
                    ///     The lpInfo parameter is a pointer to a SERVICE_FAILURE_ACTIONS_FLAG structure.
                    /// </summary>
                    SERVICE_CONFIG_FAILURE_ACTIONS_FLAG = 4,
                    /// <summary>
                    ///     	The lpInfo parameter is a pointer to a SERVICE_PREFERRED_NODE_INFO structure.
                    /// </summary>
                    SERVICE_CONFIG_PREFERRED_NODE = 9,
                    /// <summary>
                    ///     The lpInfo parameter is a pointer to a SERVICE_PRESHUTDOWN_INFO structure.
                    /// </summary>
                    SERVICE_CONFIG_PRESHUTDOWN_INFO = 7,
                    /// <summary>
                    ///     The lpInfo parameter is a pointer to a SERVICE_REQUIRED_PRIVILEGES_INFO structure.
                    /// </summary>
                    SERVICE_CONFIG_REQUIRED_PRIVILEGES_INFO = 6,
                    /// <summary>
                    ///     The lpInfo parameter is a pointer to a SERVICE_SID_INFO structure.
                    /// </summary>
                    SERVICE_CONFIG_SERVICE_SID_INFO = 5,
                    /// <summary>
                    ///     The lpInfo parameter is a pointer to a SERVICE_TRIGGER_INFO structure. This value is not supported by the ANSI version of ChangeServiceConfig2.
                    /// </summary>
                    SERVICE_CONFIG_TRIGGER_INFO = 8,
                    /// <summary>
                    ///     The lpInfo parameter is a pointer a SERVICE_LAUNCH_PROTECTED_INFO structure.
                    /// </summary>
                    SERVICE_CONFIG_LAUNCH_PROTECTED = 12
                }
                /// <summary>
                ///     The severity of the error, and action taken, if this service fails to start.
                /// </summary>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-createservicea#parameters">SERVICE_ERROR_CONTROL</seealso>
                public enum SERVICE_ERROR_CONTROL : uint
                {
                    /// <summary>
                    ///     The startup program logs the error in the event log, if possible. If the last-known-good configuration is being started, the
                    ///     startup operation fails. Otherwise, the system is restarted with the last-known good configuration.
                    /// </summary>
                    SERVICE_ERROR_CRITICAL = 0x00000003,
                    /// <summary>
                    ///     The startup program ignores the error and continues the startup operation.
                    /// </summary>
                    SERVICE_ERROR_IGNORE = 0x00000000,
                    /// <summary>
                    ///     The startup program logs the error in the event log but continues the startup operation.
                    /// </summary>
                    SERVICE_ERROR_NORMAL = 0x00000001,
                    /// <summary>
                    ///     The startup program logs the error in the event log. If the last-known-good configuration is being started, the startup operation
                    ///     continues. Otherwise, the system is restarted with the last-known-good configuration.
                    /// </summary>
                    SERVICE_ERROR_SEVERE = 0x00000002
                }
                /// <summary>
                ///     Access Rights for the Service Control Manager.
                /// </summary>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-openservicea#parameters">SC_MANAGER_ACCESS_RIGHTS</seealso>
                [Flags]
                public enum SC_MANAGER_ACCESS_RIGHTS : uint
                {
                    /// <summary>
                    ///     Includes STANDARD_RIGHTS_REQUIRED, in addition to all access rights in this table.
                    /// </summary>
                    SC_MANAGER_ALL_ACCESS = 0xF003F,
                    /// <summary>
                    ///     Required to call the CreateService function to create a service object and add it to the database.
                    /// </summary>
                    SC_MANAGER_CREATE_SERVICE = 0x0002,
                    /// <summary>
                    ///     Required to connect to the service control manager.
                    /// </summary>
                    SC_MANAGER_CONNECT = 0x0001,
                    /// <summary>
                    ///     Required to call the EnumServicesStatus or EnumServicesStatusEx function to list the services that are in the database.
                    /// </summary>
                    SC_MANAGER_ENUMERATE_SERVICE = 0x0004,
                    /// <summary>
                    ///     Required to call the LockServiceDatabase function to acquire a lock on the database.
                    /// </summary>
                    SC_MANAGER_LOCK = 0x0008,
                    /// <summary>
                    ///     Required to call the NotifyBootConfigStatus function.
                    /// </summary>
                    SC_MANAGER_MODIFY_BOOT_CONFIG = 0x0020,
                    /// <summary>
                    ///     Required to call the QueryServiceLockStatus function to retrieve the lock status information for the database.
                    /// </summary>
                    SC_MANAGER_QUERY_LOCK_STATUS = 0x0010
                }
                /// <summary>
                ///     Access rights for a service
                /// </summary>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-changeserviceconfig2a#parameters">SERVICE_ACCESS_RIGHTS</seealso>
                [Flags]
                public enum SERVICE_ACCESS_RIGHTS : uint
                {
                    /// <summary>
                    ///     Includes STANDARD_RIGHTS_REQUIRED in addition to all access rights in this table.
                    /// </summary>
                    SERVICE_ALL_ACCESS = 0xF01FF,
                    /// <summary>
                    ///     Required to call the ChangeServiceConfig or ChangeServiceConfig2 function to change the service configuration. Because this grants the caller the right to change the executable file that the system runs, it should be granted only to administrators.
                    /// </summary>
                    SERVICE_CHANGE_CONFIG = 0x0002,
                    /// <summary>
                    ///     Required to call the EnumDependentServices function to enumerate all the services dependent on the service.
                    /// </summary>
                    SERVICE_ENUMERATE_DEPENDENTS = 0x0008,
                    /// <summary>
                    ///     Required to call the ControlService function to ask the service to report its status immediately.
                    /// </summary>
                    SERVICE_INTERROGATE = 0x0080,
                    /// <summary>
                    ///     Required to call the ControlService function to pause or continue the service.
                    /// </summary>
                    SERVICE_PAUSE_CONTINUE = 0x0040,
                    /// <summary>
                    ///     Required to call the QueryServiceConfig and QueryServiceConfig2 functions to query the service configuration.
                    /// </summary>
                    SERVICE_QUERY_CONFIG = 0x0001,
                    /// <summary>
                    ///     Required to call the QueryServiceStatus or QueryServiceStatusEx function to ask the service control manager about the status of the service.
                    /// </summary>
                    SERVICE_QUERY_STATUS = 0x0004,
                    /// <summary>
                    ///     Required to call the StartService function to start the service.
                    /// </summary>
                    SERVICE_START = 0x0010,
                    /// <summary>
                    ///     Required to call the ControlService function to stop the service.
                    /// </summary>
                    SERVICE_STOP = 0x0020,
                    /// <summary>
                    ///     Required to call the ControlService function to specify a user-defined control code.
                    /// </summary>
                    SERVICE_USER_DEFINED_CONTROL = 0x0100,

                    /// <summary>
                    ///     Required to call the DeleteService function to delete the service.
                    /// </summary>
                    SERVICE_STANDARD_DELETE = STANDARD_RIGHTS.DELETE,
                    /// <summary>
                    ///     Required to call the QueryServiceObjectSecurity function to query the security descriptor of the service object.
                    /// </summary>
                    SERVICE_STANDARD_READ_CONTROL = STANDARD_RIGHTS.READ_CONTROL,
                    /// <summary>
                    ///     Required to call the SetServiceObjectSecurity function to modify the DACL member of the service object's security descriptor.
                    /// </summary>
                    SERVICE_STANDARD_WRITE_DAC = STANDARD_RIGHTS.WRITE_DAC,
                    /// <summary>
                    ///     Required to call the SetServiceObjectSecurity function to modify the Owner and Group members of the service object's security descriptor.
                    /// </summary>
                    SERVICE_STANDARD_WRITE_OWNER = STANDARD_RIGHTS.WRITE_OWNER,
                }
            }
        }
    }
}
