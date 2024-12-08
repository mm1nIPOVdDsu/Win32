using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class AdvApi32
        {
            /// <summary>
            ///     ProcessThreadsApi interactions.
            /// </summary>
            public partial class ProcessThreadsApi
            {
                /// <summary>
                ///     Creates a new process and its primary thread. The new process runs in the security context of the user represented by the
                ///     specified token.
                /// </summary>
                /// <param name="hToken">A handle to the primary token that represents a user.</param>
                /// <param name="lpApplicationName">The name of the module to be executed.</param>
                /// <param name="lpCommandLine">The command line to be executed.</param>
                /// <param name="lpProcessAttributes">
                ///     A pointer to a SECURITY_ATTRIBUTES structure that specifies a security descriptor for the new process object and determines
                ///     whether child processes can inherit the returned handle to the process.
                /// </param>
                /// <param name="lpThreadAttributes">
                ///     A pointer to a SECURITY_ATTRIBUTES structure that specifies a security descriptor for the new thread object and determines
                ///     whether child processes can inherit the returned handle to the thread
                /// </param>
                /// <param name="bInheritHandle">
                ///     If this parameter is TRUE, each inheritable handle in the calling process is inherited by the new process.
                /// </param>
                /// <param name="dwCreationFlags">
                ///     The flags that control the priority class and the creation of the process. For a list of values, see Process Creation Flags.
                /// </param>
                /// <param name="lpEnvironment">A pointer to an environment block for the new process.</param>
                /// <param name="lpCurrentDirectory">The full path to the current directory for the process.</param>
                /// <param name="lpStartupInfo">A pointer to a STARTUPINFO or STARTUPINFOEX structure.</param>
                /// <param name="lpProcessInformation">
                ///     A pointer to a PROCESS_INFORMATION structure that receives identification information about the new process.
                /// </param>
                /// <returns>If the function succeeds, the return value is nonzero.</returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-createprocessasusera">
                ///     CreateProcessAsUser
                /// </seealso>
                [DllImport(AdvApi32Dll, EntryPoint = "CreateProcessAsUser", SetLastError = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
                public static extern bool CreateProcessAsUser(
                    IntPtr hToken,
                    string lpApplicationName,
                    string lpCommandLine,
                    IntPtr lpProcessAttributes,
                    IntPtr lpThreadAttributes,
                    bool bInheritHandle,
                    uint dwCreationFlags,
                    IntPtr lpEnvironment,
                    string lpCurrentDirectory,
                    ref STARTUPINFO lpStartupInfo,
                    out PROCESS_INFORMATION lpProcessInformation);
                /// <summary>
                ///     The OpenProcessToken function opens the access token associated with a process.
                /// </summary>
                /// <param name="ProcessHandle">A handle to the process whose access token is opened.</param>
                /// <param name="DesiredAccess">Specifies an access mask that specifies the requested types of access to the access token.</param>
                /// <param name="TokenHandle">A pointer to a handle that identifies the newly opened access token when the function returns.</param>
                /// <returns>If the function succeeds, the return value is nonzero.</returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-openprocesstoken">
                ///     OpenProcessToken
                /// </seealso>
                [DllImport(AdvApi32Dll, SetLastError = true), SuppressUnmanagedCodeSecurity]
                public static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, ref IntPtr TokenHandle);

            }
        }
    }
}
