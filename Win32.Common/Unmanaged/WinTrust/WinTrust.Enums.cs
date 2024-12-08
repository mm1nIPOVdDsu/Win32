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
            ///     Defines the scope of memory allocation.
            /// </summary>
            public enum AllocMethod
            {
                /// <summary>
                ///     Globally available.
                /// </summary>
                HGlobal,
                /// <summary>
                ///     Limited availability.
                /// </summary>
                CoTaskMem
            };
            /// <summary>
            ///     Certificate revocation check options. This member can be set to add revocation checking to 
            ///     that done by the selected policy provider. 
            /// </summary>
            public enum RevocationCheckFlags
            {
                /// <summary>
                ///     No additional revocation checking will be done when the WTD_REVOKE_NONE flag is used 
                ///     in conjunction with the HTTPSPROV_ACTION value set in the pgActionID parameter of 
                ///     the WinVerifyTrust function. 
                /// </summary>
                None = 0,
                /// <summary>
                ///     Revocation checking will be done on the whole chain.
                /// </summary>
                WholeChain
            };

            /// <summary>
            ///     Specifies the action to be taken.
            /// </summary>
            public enum StateAction
            {
                /// <summary>
                ///     Ignore the hWVTStateData member.
                /// </summary>
                Ignore = 0,
                /// <summary>
                ///     Verify the trust of the object (typically a file) that is specified by the dwUnionChoice member.
                /// </summary>
                Verify,
                /// <summary>
                ///     Free the hWVTStateData member previously allocated with the WTD_STATEACTION_VERIFY action. 
                /// </summary>
                Close,
                /// <summary>
                ///     Write the catalog data to a WINTRUST_DATA structure and then cache that structure.
                /// </summary>
                AutoCache,
                /// <summary>
                ///     Flush any cached catalog data.
                /// </summary>
                AutoCacheFlush
            };

            /// <summary>
            ///     DWORD value that specifies trust provider settings. 
            /// </summary>
            public enum TrustProviderFlags
            {
                /// <summary>
                ///     The trust is verified in the same manner as implemented by Internet Explorer 4.0.
                /// </summary>
                UseIE4Trust = 1,
                /// <summary>
                ///     The Internet Explorer 4.0 chain functionality is not used.
                /// </summary>
                NoIE4Chain = 2,
                /// <summary>
                ///     The default verification of the policy provider, such as code signing for Authenticode, is not performed, and the certificate is assumed valid for all usages.
                /// </summary>
                NoPolicyUsage = 4,
                /// <summary>
                ///     Revocation checking is not performed.
                /// </summary>
                RevocationCheckNone = 16,
                /// <summary>
                ///     Revocation checking is performed on the end certificate only.
                /// </summary>
                RevocationCheckEndCert = 32,
                /// <summary>
                ///     Revocation checking is performed on the entire certificate chain.
                /// </summary>
                RevocationCheckChain = 64,
                /// <summary>
                ///     Revocation checking is performed on the entire certificate chain, excluding the root certificate.
                /// </summary>
                RecovationCheckChainExcludeRoot = 128,
                /// <summary>
                ///     Not supported.
                /// </summary>
                Safer = 256,
                /// <summary>
                ///     Only the hash is verified.
                /// </summary>
                HashOnly = 512,
                /// <summary>
                ///     The default operating system version checking is performed. This flag is only used for verifying catalog-signed files.
                /// </summary>
                UseDefaultOSVerCheck = 1024,
                /// <summary>
                ///     If this flag is not set, all time stamped signatures are considered valid forever.
                /// </summary>
                LifetimeSigning = 2048,
                /// <summary>
                ///     Use only the local cache for revocation checks. Prevents revocation checks over the network.
                /// </summary>
                ChacheOnlyUrlRetrieval = 4096,
                /// <summary>
                ///     Disable the use of MD2 and MD4 hashing algorithms. If a file is signed by using MD2 or MD4 and if this flag is set, an NTE_BAD_ALGID error is returned.
                /// </summary>
                DisableMd2Md4 = 8192,
                /// <summary>
                ///     If this flag is specified it is assumed that the file being verified has been downloaded from the web and has the Mark of the Web attribute. 
                /// </summary>
                Motw = 16384
            };

            /// <summary>
            ///     Specifies the kind of user interface (UI) to be used. 
            /// </summary>
            public enum UiChoice
            {
                /// <summary>
                ///     Display all UI.
                /// </summary>
                All = 1,
                /// <summary>
                ///     Display no UI.
                /// </summary>
                NoUI,
                /// <summary>
                ///     Do not display any negative UI.
                /// </summary>
                NoBad,
                /// <summary>
                ///     Do not display any positive UI.
                /// </summary>
                NoGood
            };

            /// <summary>
            ///     A DWORD value that specifies the user interface context for the WinVerifyTrust function.
            /// </summary>
            public enum UIContext
            {
                /// <summary>
                ///     Use when calling WinVerifyTrust for a file that is to be run. 
                /// </summary>
                Execute = 0,
                /// <summary>
                ///     Use when calling WinVerifyTrust for a file that is to be installed.
                /// </summary>
                Install
            };

            /// <summary>
            ///     Specifies the union member to be used and, thus, the type of object for which trust will be verified. 
            /// </summary>
            public enum UnionChoice
            {
                /// <summary>
                ///     Use the file pointed to by pFile.
                /// </summary>
                File = 1,
                /// <summary>
                ///     Use the catalog pointed to by pCatalog.
                /// </summary>
                Catalog,
                /// <summary>
                ///     Use the BLOB pointed to by pBlob.
                /// </summary>
                Blob,
                /// <summary>
                ///     Use the [WINTRUST_SGNR_INFO](/windows/desktop/api/wintrust/ns-wintrust-wintrust_sgnr_info) 
                ///     structure pointed to by pSgnr.
                /// </summary>
                Signer,
                /// <summary>
                ///     Use the certificate pointed to by pCert.
                /// </summary>
                Cert
            };
        }
    }
}
