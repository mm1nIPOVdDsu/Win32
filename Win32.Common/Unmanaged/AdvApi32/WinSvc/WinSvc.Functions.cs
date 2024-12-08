using System;
using System.Runtime.InteropServices;

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
                ///     Changes the optional configuration parameters of a service.
                /// </summary>
                /// <param name="SC_HANDLE">
                ///     A handle to the service. This handle is returned by the OpenService or CreateService function and must have the
                ///     SERVICE_CHANGE_CONFIG access right. For more information, see Service Security and Access Rights.
                /// </param>
                /// <param name="dwInfoLevel">The <see cref="SERVICE_CONFIG_INFORMATION"/> to be changed.</param>
                /// <param name="lpInfo">
                ///     A pointer to the new value to be set for the configuration information. The format of this data depends on the value of the
                ///     dwInfoLevel parameter. If this value is NULL, the information remains unchanged.
                /// </param>
                /// <remarks>
                ///     The ChangeServiceConfig2 function changes the optional configuration information for the specified service in the service
                ///     control manager database. You can obtain the current optional configuration information by using the QueryServiceConfig2 function.
                /// </remarks>
                /// <returns>If the function succeeds, the return value is nonzero.</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-changeserviceconfig2a">ChangeServiceConfig2</seealso>
                [DllImport(AdvApi32Dll, SetLastError = true, CharSet = CharSet.Unicode)]
                public static extern bool ChangeServiceConfig2(IntPtr SC_HANDLE, SERVICE_CONFIG_INFORMATION dwInfoLevel, [MarshalAs(UnmanagedType.Struct)] ref SERVICE_DESCRIPTION lpInfo);
                /// <summary>
                ///     Closes a handle to a service control manager or service object.
                /// </summary>
                /// <param name="SCHANDLE">
                ///     A handle to the service control manager object or the service object to close. Handles to service control manager objects are
                ///     returned by the OpenSCManager function, and handles to service objects are returned by either the OpenService or CreateService function.
                /// </param>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-closeservicehandle">CloseServiceHandle</seealso>
                [DllImport(AdvApi32Dll, SetLastError = true)]
                public static extern void CloseServiceHandle(IntPtr SCHANDLE);
                /// <summary>
                ///     Creates a service object and adds it to the specified service control manager database.
                /// </summary>
                /// <param name="hSCManager">
                ///     A handle to the service control manager database. This handle is returned by the OpenSCManager function and must have the
                ///     SC_MANAGER_CREATE_SERVICE access right. For more information, see Service Security and Access Rights.
                /// </param>
                /// <param name="lpSvcName">The name of the service to install. The maximum string length is 256 characters.</param>
                /// <param name="lpDisplayName">
                ///     The display name to be used by user interface programs to identify the service. This string has a maximum length of 256 characters.
                /// </param>
                /// <param name="dwDesiredAccess">
                ///     The access to the service. Before granting the requested access, the system checks the access token of the calling process.
                /// </param>
                /// <param name="dwServiceType">The <see cref="SERVICE_TYPE"/>.</param>
                /// <param name="dwStartType">The <see cref="SERVICE_START_TYPE"/>.</param>
                /// <param name="dwErrorControl">The <see cref="SERVICE_ERROR_CONTROL"/>.</param>
                /// <param name="lpPathName">
                ///     The fully qualified path to the service binary file. If the path contains a space, it must be quoted so that it is correctly
                ///     interpreted. For example, "d:\my share\myservice.exe" should be specified as ""d:\my share\myservice.exe"".
                /// </param>
                /// <param name="lpLoadOrderGroup">
                ///     The names of the load ordering group of which this service is a member. Specify NULL or an empty string if the service does
                ///     not belong to a group.
                /// </param>
                /// <param name="lpdwTagId">
                ///     A pointer to a variable that receives a tag value that is unique in the group specified in the lpLoadOrderGroup parameter.
                ///     Specify NULL if you are not changing the existing tag.
                /// </param>
                /// <param name="lpDependencies">
                ///     A pointer to a double null-terminated array of null-separated names of services or load ordering groups that the system must
                ///     start before this service. Specify NULL or an empty string if the service has no dependencies. Dependency on a group means
                ///     that this service can run if at least one member of the group is running after an attempt to start all members of the group.
                /// </param>
                /// <param name="lpServiceStartName">
                ///     The name of the account under which the service should run. If the service type is SERVICE_WIN32_OWN_PROCESS, use an account
                ///     name in the form DomainName\UserName. The service process will be logged on as this user. If the account belongs to the
                ///     built-in domain, you can specify .\UserName.
                /// </param>
                /// <param name="lpPassword">
                ///     The password to the account name specified by the lpServiceStartName parameter. Specify an empty string if the account has no
                ///     password or if the service runs in the LocalService, NetworkService, or LocalSystem account.
                /// </param>
                /// <returns>If the function succeeds, the return value is a handle to the service.</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-createservicea">CreateService</seealso>
                [DllImport(AdvApi32Dll, SetLastError = true)]
                public static extern IntPtr CreateService(IntPtr hSCManager, string lpSvcName, string lpDisplayName, SERVICE_ACCESS_RIGHTS dwDesiredAccess, SERVICE_TYPE dwServiceType, SERVICE_START_TYPE dwStartType, SERVICE_ERROR_CONTROL dwErrorControl, string lpPathName, string lpLoadOrderGroup, int lpdwTagId, string lpDependencies, string lpServiceStartName, string lpPassword);
                /// <summary>
                ///     Marks the specified service for deletion from the service control manager database.
                /// </summary>
                /// <param name="SVHANDLE">
                ///     A handle to the service. This handle is returned by the OpenService or CreateService function, and it must have the DELETE
                ///     access right. For more information, see Service Security and Access Rights.
                /// </param>
                /// <returns>If the function succeeds, the return value is nonzero.</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-deleteservice">DeleteService</seealso>
                [DllImport(AdvApi32Dll, SetLastError = true)]
                public static extern int DeleteService(IntPtr SVHANDLE);
                /// <summary>
                ///     Establishes a connection to the service control manager on the specified computer and opens the specified service control
                ///     manager database.
                /// </summary>
                /// <param name="lpMachineName">
                ///     The name of the target computer. If the pointer is NULL or points to an empty string, the function connects to the service
                ///     control manager on the local computer.
                /// </param>
                /// <param name="lpDatabaseName">
                ///     The name of the service control manager database. This parameter should be set to SERVICES_ACTIVE_DATABASE. If it is NULL, the
                ///     SERVICES_ACTIVE_DATABASE database is opened by default.
                /// </param>
                /// <param name="dwDesiredAccess">
                ///     The access to the service control manager. For a list of access rights, see Service Security and Access Rights.
                /// </param>
                /// <returns>If the function succeeds, the return value is a handle to the specified service control manager database.</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-openscmanagera">OpenSCManager</seealso>
                [DllImport(AdvApi32Dll, SetLastError = true)]
                public static extern IntPtr OpenSCManager(string lpMachineName, string lpDatabaseName, SC_MANAGER_ACCESS_RIGHTS dwDesiredAccess);
                /// <summary>
                ///     Establishes a connection to the service control manager on the specified computer and opens the specified service control
                ///     manager database.
                /// </summary>
                /// <param name="lpMachineName">
                ///     The name of the target computer. If the pointer is NULL or points to an empty string, the function connects to the service
                ///     control manager on the local computer.
                /// </param>
                /// <param name="lpDatabaseName">
                ///     The name of the service control manager database. This parameter should be set to SERVICES_ACTIVE_DATABASE. If it is NULL, the
                ///     SERVICES_ACTIVE_DATABASE database is opened by default.
                /// </param>
                /// <param name="dwDesiredAccess">
                ///     The access to the service control manager. For a list of access rights, see Service Security and Access Rights.
                /// </param>
                /// <returns>If the function succeeds, the return value is a handle to the specified service control manager database.</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-openscmanager">OpenSCManager</seealso>
                [DllImport(AdvApi32Dll, SetLastError = true)]
                public static extern IntPtr OpenSCManager(string lpMachineName, string lpDatabaseName, GENERIC_RIGHTS dwDesiredAccess);
                /// <summary>
                ///     Opens an existing service.
                /// </summary>
                /// <param name="SCHANDLE">
                ///     A handle to the service control manager database. The OpenSCManager function returns this handle. For more information, see
                ///     Service Security and Access Rights.
                /// </param>
                /// <param name="lpSvcName">
                ///     The name of the service to be opened. This is the name specified by the lpServiceName parameter of the CreateService function
                ///     when the service object was created, not the service display name that is shown by user interface applications to identify the service.
                /// </param>
                /// <param name="dwNumServiceArgs">The access to the service. For a list of access rights, see Service Security and Access Rights.</param>
                /// <returns>If the function succeeds, the return value is a handle to the service.</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-openservicea">OpenService</seealso>
                [DllImport(AdvApi32Dll, SetLastError = true)]
                public static extern IntPtr OpenService(IntPtr SCHANDLE, string lpSvcName, SERVICE_ACCESS_RIGHTS dwNumServiceArgs);
                /// <summary>
                ///     Retrieves the configuration parameters of the specified service.
                /// </summary>
                /// <param name="hService">
                ///     A handle to the service. This handle is returned by the OpenService or CreateService function, and it must have the
                ///     <see cref="SERVICE_ACCESS_RIGHTS.SERVICE_QUERY_CONFIG"/> access right.
                /// </param>
                /// <param name="lpServiceConfig">
                ///     A pointer to a buffer that receives the service configuration information. The format of the data is a
                ///     <see cref="QUERY_SERVICE_CONFIG"/> structure.
                /// </param>
                /// <param name="cbBufSize">The size of the buffer pointed to by the lpServiceConfig parameter, in bytes.</param>
                /// <param name="pcbBytesNeeded">
                ///     A pointer to a variable that receives the number of bytes needed to store all the configuration information, if the function
                ///     fails with ERROR_INSUFFICIENT_BUFFER.
                /// </param>
                /// <returns>If the function succeeds, the return value is nonzero.</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-queryserviceconfiga">QueryServiceConfig</seealso>
                [DllImport(AdvApi32Dll, CharSet = CharSet.Auto, SetLastError = true)]
                public static extern bool QueryServiceConfig(IntPtr hService, out QUERY_SERVICE_CONFIG lpServiceConfig, uint cbBufSize, out uint pcbBytesNeeded);
                /// <summary>
                ///     Retrieves the optional configuration parameters of the specified service.
                /// </summary>
                /// <param name="hService">
                ///     A handle to the service. This handle is returned by the OpenService or CreateService function and must have the
                ///     <see cref="SERVICE_ACCESS_RIGHTS.SERVICE_QUERY_CONFIG"/> access right.
                /// </param>
                /// <param name="dwInfoLevel">The configuration information to be queried</param>
                /// <param name="lpBuffer">
                ///     A pointer to the buffer that receives the service configuration information. The format of this data depends on the value of
                ///     the dwInfoLevel parameter.
                /// </param>
                /// <param name="cbBufSize">The size of the structure pointed to by the lpBuffer parameter, in bytes.</param>
                /// <param name="pcbBytesNeeded">
                ///     A pointer to a variable that receives the number of bytes required to store the configuration information, if the function
                ///     fails with ERROR_INSUFFICIENT_BUFFER.
                /// </param>
                /// <returns>If the function succeeds, the return value is nonzero.</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-queryserviceconfig2a">QueryServiceConfig2</seealso>
                [DllImport(AdvApi32Dll, CharSet = CharSet.Auto, SetLastError = true)]
                public static extern bool QueryServiceConfig2(IntPtr hService, SERVICE_CONFIG_INFORMATION dwInfoLevel, IntPtr lpBuffer, uint cbBufSize, out uint pcbBytesNeeded);
                /// <summary>
                ///     Retrieves the current status of the specified service.
                /// </summary>
                /// <param name="hService">A handle to the service. This handle is returned by the OpenService or the CreateService function, and it must have the  <see cref="SERVICE_ACCESS_RIGHTS.SERVICE_QUERY_CONFIG"/> access right.</param>
                /// <param name="lpServiceStatus">A pointer to a SERVICE_STATUS structure that receives the status information.</param>
                /// <returns>If the function succeeds, the return value is nonzero.</returns>
                /// <seealso cref="QueryServiceStatus">https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-queryservicestatus</seealso>
                [DllImport(AdvApi32Dll, CharSet = CharSet.Auto, SetLastError = true)]
                public static extern bool QueryServiceStatus(IntPtr hService, out SERVICE_STATUS lpServiceStatus);
                /// <summary>
                ///     Starts a service.
                /// </summary>
                /// <param name="SVHANDLE">
                ///     A handle to the service. This handle is returned by the OpenService or CreateService function, and it must have the
                ///     SERVICE_START access right. For more information, see Service Security and Access Rights.
                /// </param>
                /// <param name="dwNumServiceArgs">
                ///     The number of strings in the lpServiceArgVectors array. If lpServiceArgVectors is NULL, this parameter can be zero.
                /// </param>
                /// <param name="lpServiceArgVectors">
                ///     The null-terminated strings to be passed to the ServiceMain function for the service as arguments. If there are no arguments,
                ///     this parameter can be NULL. Otherwise, the first argument (lpServiceArgVectors[0]) is the name of the service, followed by any
                ///     additional arguments (lpServiceArgVectors[1] through lpServiceArgVectors[dwNumServiceArgs-1]).
                /// </param>
                /// <returns>If the function succeeds, the return value is nonzero.</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-startservicea">StartService</seealso>
                [DllImport(AdvApi32Dll, SetLastError = true)]
                public static extern int StartService(IntPtr SVHANDLE, int dwNumServiceArgs, string lpServiceArgVectors);
            }
        }
    }
}
