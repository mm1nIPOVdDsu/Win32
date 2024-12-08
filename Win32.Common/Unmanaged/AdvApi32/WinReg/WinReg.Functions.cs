using System;
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
            ///     WinReg interactions.
            /// </summary>
            public partial class WinReg
            {
                /// <summary>
                ///     Closes a handle to the specified registry key.
                /// </summary>
                /// <param name="hKey">A handle to the open key to be closed. The handle must have been opened by the RegCreateKeyEx, RegCreateKeyTransacted, RegOpenKeyEx, RegOpenKeyTransacted, or RegConnectRegistry function.</param>
                /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regclosekey">RegCloseKey</seealso>
                [DllImport(AdvApi32Dll, SetLastError = true)]
                public static extern int RegCloseKey(IntPtr hKey);
                /// <summary>
                ///     The RegGetKeySecurity function retrieves a copy of the security descriptor protecting the specified open registry key.
                /// </summary>
                /// <param name="hKey">A handle to an open key for which to retrieve the security descriptor.</param>
                /// <param name="SecurityInformation">A SECURITY_INFORMATION value that indicates the requested security information.</param>
                /// <param name="pSecurityDescriptor">A pointer to a buffer that receives a copy of the requested security descriptor.</param>
                /// <param name="lpcbSecurityDescriptor">A pointer to a variable that specifies the size, in bytes, of the buffer pointed to by the pSecurityDescriptor parameter. When the function returns, the variable contains the number of bytes written to the buffer.</param>
                /// <returns>If the function succeeds, the function returns ERROR_SUCCESS.</returns>
                [DllImport(AdvApi32Dll, SetLastError = true)]
                public static extern int RegGetKeySecurity(IntPtr hKey, SECURITY_INFORMATION SecurityInformation, out SECURITY_DESCRIPTOR pSecurityDescriptor, ref UInt32 lpcbSecurityDescriptor);
                /// <summary>
                ///     Notifies the caller about changes to the attributes or contents of a specified registry key.
                /// </summary>
                /// <param name="hKey">A handle to an open registry key. This handle is returned by the RegCreateKeyEx or RegOpenKeyEx function. </param>
                /// <param name="bWatchSubtree">If this parameter is TRUE, the function reports changes in the specified key and its subkeys. If the parameter is FALSE, the function reports changes only in the specified key.</param>
                /// <param name="dwNotifyFilter">A value that indicates the changes that should be reported.</param>
                /// <param name="hEvent">A handle to an event. If the fAsynchronous parameter is TRUE, the function returns immediately and changes are reported by signaling this event. If fAsynchronous is FALSE, hEvent is ignored.</param>
                /// <param name="fAsynchronous">If this parameter is TRUE, the function returns immediately and reports changes by signaling the specified event. If this parameter is FALSE, the function does not return until a change has occurred.</param>
                /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regnotifychangekeyvalue">RegNotifyChangeKeyValue</seealso>
                [DllImport(AdvApi32Dll, SetLastError = true)]
                public static extern int RegNotifyChangeKeyValue(IntPtr hKey, bool bWatchSubtree, REG_NOTIFY_FILTER dwNotifyFilter, IntPtr hEvent, bool fAsynchronous);
                /// <summary>
                ///     Opens the specified registry key. Note that key names are not case sensitive.
                /// </summary>
                /// <param name="hKey">A handle to an open registry key.</param>
                /// <param name="subKey">The name of the registry subkey to be opened.</param>
                /// <param name="options">Specifies the option to apply when opening the key.</param>
                /// <param name="samDesired">A mask that specifies the desired access rights to the key to be opened. The function fails if the security descriptor of the key does not permit the requested access for the calling process.</param>
                /// <param name="phkResult">A pointer to a variable that receives a handle to the opened key. If the key is not one of the predefined registry keys, call the RegCloseKey function after you have finished using the handle.</param>
                /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regopenkeyexa">RegOpenKeyEx</seealso>
                [DllImport(AdvApi32Dll, SetLastError = true)]
                public static extern int RegOpenKeyEx(IntPtr hKey, string subKey, uint options, uint samDesired, out IntPtr phkResult);
            }
        }
    }
}
