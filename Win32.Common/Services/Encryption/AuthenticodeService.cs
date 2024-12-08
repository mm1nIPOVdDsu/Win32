using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

using Microsoft.Extensions.Logging;

using static Win32.Common.Unmanaged.WinTrust;

namespace Win32.Common.Services.Encryption
{
    /// <summary>
    ///     Handles validation and verification of authenticode/code signing certificates.
    /// </summary>
    //[Singleton]
    public class AuthenticodeService : IAuthenticodeService
    {
        private readonly Guid wintrust_action_generic_verify_v2 = new("{00AAC56B-CD44-11d0-8CC2-00C04FC295EE}");
        private readonly uint NO_SIGNATURE_FOUND = 2148204800;

        private readonly ILogger<AuthenticodeService> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AuthenticodeService"/> class.
        /// </summary>
        /// <param name="logger">An instance of a <see cref="ILogger{AuthenticodeService}"/>.</param>
        public AuthenticodeService(ILogger<AuthenticodeService> logger) => _logger = logger;

        /// <summary>
        ///     Determines if a file is signed with a valid code signing certificate.
        /// </summary>
        /// <param name="fileName">The full name of the file to verify.</param>
        /// <returns>True if the file has a valid signature.</returns>
        public bool IsTrusted(string fileName)
        {
            _logger.LogInformation("Verifying signature on the file {fileName}.", fileName);
            uint result = 0;
            var pGuid = IntPtr.Zero;
            var pData = IntPtr.Zero;
            try
            {
                using (var fileInfo = new WINTRUST_FILE_INFO(fileName, Guid.Empty))
                using (var guidPtr = new UnmanagedPointer(Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid))), AllocMethod.HGlobal))
                using (var wvtDataPtr = new UnmanagedPointer(Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WINTRUST_DATA))), AllocMethod.HGlobal))
                {
                    var data = new WINTRUST_DATA(fileInfo);
                    pGuid = guidPtr;
                    pData = wvtDataPtr;

                    Marshal.StructureToPtr(wintrust_action_generic_verify_v2, pGuid, true);
                    Marshal.StructureToPtr(data, pData, true);

                    result = WinVerifyTrust(IntPtr.Zero, pGuid, pData);
                    if (result == NO_SIGNATURE_FOUND)
                        return false;
                    if (result is not 0)
                        throw new Win32Exception((int)result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying the trust for the file {fileName}.", fileName);
            }
            finally
            {
                if (pGuid != IntPtr.Zero)
                    Marshal.Release(pGuid);
                if (pData != IntPtr.Zero)
                    Marshal.Release(pData);
            }

            return result == 0;
        }
    }
}
