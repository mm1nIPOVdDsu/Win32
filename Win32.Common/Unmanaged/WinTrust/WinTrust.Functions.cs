using System;
using System.Runtime.InteropServices;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <summary>
        ///     Header is used by Security and Identity.
        /// </summary>
        public partial class WinTrust
        {
            /// <summary>
            ///     The WinVerifyTrust function performs a trust verification action on a specified object. The function passes the inquiry to a trust
            ///     provider that supports the action identifier, if one exists.
            /// </summary>
            /// <param name="hWnd">
            ///     Optional handle to a caller window. A trust provider can use this value to determine whether it can interact with the user.
            ///     However, trust providers typically perform verification actions without input from the user.
            /// </param>
            /// <param name="pgActionID">A pointer to a GUID structure that identifies an action and the trust provider that supports that action.</param>
            /// <param name="pWinTrustData">
            ///     A pointer that, when cast as a WINTRUST_DATA structure, contains information that the trust provider needs to process the
            ///     specified action identifier.
            /// </param>
            /// <returns>If the trust provider verifies that the subject is trusted for the specified action, the return value is zero</returns>
            [DllImport(WinTrustDll, PreserveSig = true, SetLastError = false)]
            public static extern uint WinVerifyTrust(IntPtr hWnd, IntPtr pgActionID, IntPtr pWinTrustData);
            /// <summary>
            ///     The WTHelperProvDataFromStateData function retrieves trust provider information from a specified handle. This function has no
            ///     associated import library. You must use the LoadLibrary and GetProcAddress functions to dynamically link to WinTrust.dll.
            /// </summary>
            /// <param name="hStateData">
            ///     A handle previously set by the WinVerifyTrustEx function as the hWVTStateData member of the WINTRUST_DATA structure.
            /// </param>
            /// <returns>If the function succeeds, the function returns a pointer to a CRYPT_PROVIDER_DATA structure.</returns>
            /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wintrust/nf-wintrust-wthelperprovdatafromstatedata">WTHelperProvDataFromStateData</seealso>
            [DllImport(WinTrustDll, CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr WTHelperProvDataFromStateData(IntPtr hStateData);
            /// <summary>
            ///     The WTHelperGetProvSignerFromChain function retrieves a signer or countersigner by index from the chain. This function has no
            ///     associated import library. You must use the LoadLibrary and GetProcAddress functions to dynamically link to WinTrust.dll.
            /// </summary>
            /// <param name="pProvData">A pointer to the CRYPT_PROVIDER_DATA structure that contains the signer and countersigner information.</param>
            /// <param name="idxSigner">The index of the signer. The index is zero based.</param>
            /// <param name="fCounterSigner">
            ///     If TRUE, the countersigner, as specified by idxCounterSigner, is retrieved by this function; the signer that contains the
            ///     countersigner is identified by idxSigner. If FALSE, the signer, as specified by idxSigner, is retrieved by this function.
            /// </param>
            /// <param name="idxCounterSigner">
            ///     The index of the countersigner. The index is zero based. The countersigner applies to the signer identified by idxSigner.
            /// </param>
            /// <returns>
            ///     If the function succeeds, the function returns a pointer to a CRYPT_PROVIDER_SGNR structure for the requested signer or countersigner.
            /// </returns>
            /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wintrust/nf-wintrust-wthelpergetprovsignerfromchain">WTHelperGetProvSignerFromChain</seealso>
            [DllImport(WinTrustDll, CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr WTHelperGetProvSignerFromChain(IntPtr pProvData, int idxSigner, bool fCounterSigner, int idxCounterSigner);
            /// <summary>
            ///     The WTHelperGetProvCertFromChain function retrieves a trust provider certificate from the certificate chain. This function has no
            ///     associated import library. You must use the LoadLibrary and GetProcAddress functions to dynamically link to WinTrust.dll.
            /// </summary>
            /// <param name="pSgnr">A pointer to a CRYPT_PROVIDER_SGNR structure that represents the signers.</param>
            /// <param name="idxCert">The index of the certificate. The index is zero based.</param>
            /// <returns>
            ///     If the function succeeds, the function returns a pointer to a CRYPT_PROVIDER_CERT structure that represents the trust provider certificate.
            /// </returns>
            /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wintrust/nf-wintrust-wthelpergetprovcertfromchain">WTHelperGetProvCertFromChain</seealso>
            [DllImport(WinTrustDll, CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr WTHelperGetProvCertFromChain(IntPtr pSgnr, int idxCert);
        }
    }
}
