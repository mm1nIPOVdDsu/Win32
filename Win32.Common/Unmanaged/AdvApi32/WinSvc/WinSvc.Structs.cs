using System.Runtime.InteropServices;

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
                ///     Contains configuration information for an installed service.
                /// </summary>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/ns-winsvc-query_service_configa">QUERY_SERVICE_CONFIG</seealso>
                public struct QUERY_SERVICE_CONFIG
                {
                    /// <summary>
                    ///     The type of service.
                    /// </summary>
                    public SERVICE_TYPE dwServiceType;
                    /// <summary>
                    ///     When to start the service.
                    /// </summary>
                    public SERVICE_START_TYPE dwStartType;
                    /// <summary>
                    ///     The severity of the error, and action taken, if this service fails to start
                    /// </summary>
                    public SERVICE_ERROR_CONTROL dwErrorControl;
                    /// <summary>
                    ///     The fully qualified path to the service binary file.
                    /// </summary>
                    public string lpBinaryPathName;
                    /// <summary>
                    ///     The name of the load ordering group to which this service belongs. If the member is NULL or an empty string, the service
                    ///     does not belong to a load ordering group.
                    /// </summary>
                    public string lpLoadOrderGroup;
                    /// <summary>
                    ///     A unique tag value for this service in the group specified by the lpLoadOrderGroup parameter. A value of zero indicates
                    ///     that the service has not been assigned a tag.
                    /// </summary>
                    public uint dwTagId;
                    /// <summary>
                    ///     A pointer to an array of null-separated names of services or load ordering groups that must start before this service
                    /// </summary>
                    public string lpDependencies;
                    /// <summary>
                    ///     If the service type is SERVICE_WIN32_OWN_PROCESS or SERVICE_WIN32_SHARE_PROCESS, this member is the name of the account
                    ///     that the service process will be logged on as when it runs.
                    /// </summary>
                    public string lpServiceStartName;
                    /// <summary>
                    ///     The display name to be used by service control programs to identify the service.
                    /// </summary>
                    public string lpDisplayName;
                }
                /// <summary>
                ///     Contains a service description.
                /// </summary>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/ns-winsvc-service_descriptiona">SERVICE_DESCRIPTION</seealso>
                [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
                public struct SERVICE_DESCRIPTION
                {
                    /// <summary>
                    ///     The description of the service. If this member is NULL, the description remains unchanged. If this value is an empty
                    ///     string (""), the current description is deleted.
                    /// </summary>
                    public string lpDescription;
                }
                /// <summary>
                ///     Contains status information for a service.
                /// </summary>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/ns-winsvc-service_status">SERVICE_STATUS</seealso>
                public struct SERVICE_STATUS
                {
                    /// <summary>
                    ///     The type of service.
                    /// </summary>
                    public SERVICE_TYPE dwServiceType;
                    /// <summary>
                    ///     The current state of the service.
                    /// </summary>
                    public SERVICE_STATE dwCurrentState;
                    /// <summary>
                    ///     The control codes the service accepts and processes in its handler function (see Handler and HandlerEx). A user interface
                    ///     process can control a service by specifying a control command in the ControlService or ControlServiceEx function. By
                    ///     default, all services accept the SERVICE_CONTROL_INTERROGATE value.
                    /// </summary>
                    public SERVICE_CONTROL_CODES dwControlsAccepted;
                    /// <summary>
                    ///     The error code the service uses to report an error that occurs when it is starting or stopping. To return an error code
                    ///     specific to the service, the service must set this value to ERROR_SERVICE_SPECIFIC_ERROR to indicate that the
                    ///     dwServiceSpecificExitCode member contains the error code. The service should set this value to NO_ERROR when it is running
                    ///     and on normal termination.
                    /// </summary>
                    public uint dwWin32ExitCode;
                    /// <summary>
                    ///     A service-specific error code that the service returns when an error occurs while the service is starting or stopping.
                    ///     This value is ignored unless the dwWin32ExitCode member is set to ERROR_SERVICE_SPECIFIC_ERROR.
                    /// </summary>
                    public uint dwServiceSpecificExitCode;
                    /// <summary>
                    ///     The check-point value the service increments periodically to report its progress during a lengthy start, stop, pause, or
                    ///     continue operation. For example, the service should increment this value as it completes each step of its initialization
                    ///     when it is starting up. The user interface program that invoked the operation on the service uses this value to track the
                    ///     progress of the service during a lengthy operation. This value is not valid and should be zero when the service does not
                    ///     have a start, stop, pause, or continue operation pending.
                    /// </summary>
                    public uint dwCheckPoint;
                    /// <summary>
                    ///     The estimated time required for a pending start, stop, pause, or continue operation, in milliseconds. Before the specified
                    ///     amount of time has elapsed, the service should make its next call to the SetServiceStatus function with either an
                    ///     incremented dwCheckPoint value or a change in dwCurrentState. If the amount of time specified by dwWaitHint passes, and
                    ///     dwCheckPoint has not been incremented or dwCurrentState has not changed, the service control manager or service control
                    ///     program can assume that an error has occurred and the service should be stopped. However, if the service shares a process
                    ///     with other services, the service control manager cannot terminate the service application because it would have to
                    ///     terminate the other services sharing the process as well.
                    /// </summary>
                    public uint dwWaitHint;
                }
            }
        }
    }
}