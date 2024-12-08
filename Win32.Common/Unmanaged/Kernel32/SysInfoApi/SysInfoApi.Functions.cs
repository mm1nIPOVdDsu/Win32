using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class Kernel32
        {
            /// <summary>
            ///     SysInfoApi interactions.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/sysinfoapi/">SysInfoApi</see>
            public partial class SysInfoApi
            {
                /// <summary>
                ///     Enumerates all system firmware tables of the specified type.
                /// </summary>
                /// <param name="FirmwareTableProviderSignature">The identifier of the firmware table provider to which the query is to be directed.</param>
                /// <param name="pFirmwareTableBuffer">A pointer to a buffer that receives the list of firmware tables. If this parameter is NULL, the return value is the required buffer size.</param>
                /// <param name="BufferSize">The size of the pFirmwareTableBuffer buffer, in bytes.</param>
                /// <returns>If the function succeeds, the return value is the number of bytes written to the buffer. This value will always be less than or equal to BufferSize.</returns>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/sysinfoapi/nf-sysinfoapi-enumsystemfirmwaretables">EnumSystemFirmwareTables</see>
                [DllImport(Kernel32Dll, SetLastError = true, ExactSpelling = true)]
                public static extern uint EnumSystemFirmwareTables(FirmwareTableProviderId FirmwareTableProviderSignature, IntPtr pFirmwareTableBuffer, uint BufferSize);
                /// <summary>
                ///     Retrieves a NetBIOS or DNS name associated with the local computer.
                /// </summary>
                /// <remarks>The names are established at system startup, when the system reads them from the registry.</remarks>
                /// <param name="NameType">The type of <see cref="COMPUTER_NAME_FORMAT"/> to be retrieved.</param>
                /// <param name="lpBuffer">A pointer to a buffer that receives the computer name or the cluster virtual server name.</param>
                /// <param name="lpnSize">
                ///     On input, specifies the size of the buffer, in TCHARs. On output, receives the number of TCHARs copied to the destination
                ///     buffer, not including the terminating null character.
                /// </param>
                /// <returns>If the function succeeds, the return value is a nonzero value.</returns>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/sysinfoapi/nf-sysinfoapi-getcomputernameexa">GetComputerNameEx</see>
                [DllImport(Kernel32Dll, SetLastError = true, CharSet = CharSet.Unicode)]
                public static extern bool GetComputerNameEx(COMPUTER_NAME_FORMAT NameType, StringBuilder lpBuffer, ref uint lpnSize);
                /// <summary>
                ///     Retrieves information about the current system to an application running under WOW64.
                /// </summary>
                /// <remarks>
                ///     If the function is called from a 64-bit application, it is equivalent to the GetSystemInfo function. If the function is called
                ///     from an x86 or x64 application running on a 64-bit system that does not have an Intel64 or x64 processor (such as ARM64), it
                ///     will return information as if the system is x86 only if x86 emulation is supported (or x64 if x64 emulation is also supported).
                /// </remarks>
                /// <param name="lpSystemInfo">A pointer to a <see cref="SYSTEM_INFO"/> structure that receives the information.</param>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/sysinfoapi/nf-sysinfoapi-getnativesysteminfo">GetNativeSystemInfo</see>
                [DllImport(Kernel32Dll, SetLastError = true)]
                public static extern void GetNativeSystemInfo(out SYSTEM_INFO lpSystemInfo);
                /// <summary>
                ///     Retrieves the product type for the operating system on the local computer, and maps the type to the product types supported by
                ///     the specified operating system.
                /// </summary>
                /// <remarks>
                ///     To retrieve product type information on versions of Windows prior to the minimum supported operating systems specified in the
                ///     Requirements section, use the GetVersionEx function. You can also use the OperatingSystemSKU property of the
                ///     Win32_OperatingSystem WMI class.
                /// </remarks>
                /// <param name="dwOSMajorVersion">The major version number of the operating system. The minimum value is 6.</param>
                /// <param name="dwOSMinorVersion">The minor version number of the operating system. The minimum value is 0.</param>
                /// <param name="dwSpMajorVersion">The major version number of the operating system service pack. The minimum value is 0.</param>
                /// <param name="dwSpMinorVersion">The minor version number of the operating system service pack. The minimum value is 0.</param>
                /// <param name="pdwReturnedProductType"></param>
                /// <returns>If the function succeeds, the return value is a nonzero value.</returns>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/sysinfoapi/nf-sysinfoapi-getproductinfo">GetProductInfo</see>
                [DllImport(Kernel32Dll, SetLastError = true)]
                public static extern bool GetProductInfo(uint dwOSMajorVersion, uint dwOSMinorVersion, uint dwSpMajorVersion, uint dwSpMinorVersion, out PRODUCT_TYPE pdwReturnedProductType);
                /// <summary>
                ///     Retrieves the specified firmware table from the firmware table provider.
                /// </summary>
                /// <param name="FirmwareTableProviderSignature">The identifier of the firmware table provider to which the query is to be directed.</param>
                /// <param name="FirmwareTableID">
                ///     The identifier of the firmware table. This identifier is little endian, you must reverse the characters in the string.
                /// </param>
                /// <remarks>
                ///     FACP is an ACPI provider, as described in the Signature field of the DESCRIPTION_HEADER structure in the ACPI specification
                ///     (see the Advanced Configuration and Power Interface (ACPI) Specification. Therefore, use 'PCAF' to specify the FACP table, as
                ///     shown in the following example:
                /// </remarks>
                /// <example>retVal = GetSystemFirmwareTable('ACPI', 'PCAF', pBuffer, BUFSIZE);</example>
                /// <param name="pFirmwareTableBuffer">
                ///     A pointer to a buffer that receives the requested firmware table. If this parameter is NULL, the return value is the required
                ///     buffer size.
                /// </param>
                /// <param name="BufferSize">The size of the pFirmwareTableBuffer buffer, in bytes.</param>
                /// <returns>
                ///     If the function succeeds, the return value is the number of bytes written to the buffer. This value will always be less than
                ///     or equal to BufferSize.
                /// </returns>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/sysinfoapi/nf-sysinfoapi-getsystemfirmwaretable">GetSystemFirmwareTable</see>
                [DllImport(Kernel32Dll, SetLastError = true, ExactSpelling = true)]
                public static extern uint GetSystemFirmwareTable(FirmwareTableProviderId FirmwareTableProviderSignature, uint FirmwareTableID, IntPtr pFirmwareTableBuffer, uint BufferSize);
                /// <summary>
                ///     Retrieves information about the current system.
                /// </summary>
                /// <remarks>
                ///     To retrieve accurate information for an application running on WOW64, call the <see cref="GetNativeSystemInfo"/> function.
                /// </remarks>
                /// <param name="lpSystemInfo">A pointer to a <see cref="SYSTEM_INFO"/> structure that receives the information.</param>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/sysinfoapi/nf-sysinfoapi-getsysteminfo">GetSystemInfo</see>
                [DllImport(Kernel32Dll, SetLastError = false, ExactSpelling = true)]
                public static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);
            }
        }
    }
}