using System;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <summary>
        ///     AdvApi32 interactions.
        /// </summary>
        public partial class Shared
        {
            /// <summary>
            ///     WinCrypt interactions.
            /// </summary>
            public partial class WinCrypt
            {
                /// <summary>
                ///     Crypt flags
                /// </summary>
                [Flags]
                public enum CRYPT_FLAGS : uint
                {
                    /// <summary>
                    ///     This option is intended for applications that are using ephemeral keys, or applications that do not require access to
                    ///     persisted private keys, such as applications that perform only hashing, encryption, and digital signature verification.
                    ///     Only applications that create signatures or decrypt messages need access to a private key. In most cases, this flag should
                    ///     be set.
                    /// </summary>
                    CRYPT_VERIFYCONTEXT,
                    /// <summary>
                    ///     Creates a new key container with the name specified by pszContainer. If pszContainer is NULL, a key container with the
                    ///     default name is created.
                    /// </summary>
                    CRYPT_NEWKEYSET,
                    /// <summary>
                    ///     By default, keys and key containers are stored as user keys. For Base Providers, this means that user key containers are
                    ///     stored in the user's profile. A key container created without this flag by an administrator can be accessed only by the
                    ///     user creating the key container and a user with administration privileges.
                    /// </summary>
                    CRYPT_MACHINE_KEYSET,
                    /// <summary>
                    ///     Delete the key container specified by pszContainer. If pszContainer is NULL, the key container with the default name is
                    ///     deleted. All key pairs in the key container are also destroyed.
                    /// </summary>
                    CRYPT_DELETEKEYSET,
                    /// <summary>
                    ///     The application requests that the CSP not display any user interface (UI) for this context. If the CSP must display the UI
                    ///     to operate, the call fails and the NTE_SILENT_CONTEXT error code is set as the last error. In addition, if calls are made
                    ///     to CryptGenKey with the CRYPT_USER_PROTECTED flag with a context that has been acquired with the CRYPT_SILENT flag, the
                    ///     calls fail and the CSP sets NTE_SILENT_CONTEXT.
                    /// </summary>
                    CRYPT_SILENT,
                    /// <summary>
                    ///     Obtains a context for a smart card CSP that can be used for hashing and symmetric key operations but cannot be used for
                    ///     any operation that requires authentication to a smart card using a PIN. This type of context is most often used to perform
                    ///     operations on an empty smart card, such as setting the PIN by using CryptSetProvParam. This flag can only be used with
                    ///     smart card CSPs.
                    /// </summary>
                    CRYPT_DEFAULT_CONTAINER_OPTIONAL
                }
                /// <summary>
                ///     The type of query hash.
                /// </summary>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptgethashparam">HASH_QUERY_TYPE</seealso>
                public enum HASH_QUERY_TYPE : uint
                {
                    /// <summary>
                    ///     An ALG_ID that indicates the algorithm specified when the hash object was created.
                    /// </summary>
                    HP_ALGID,
                    /// <summary>
                    ///     DWORD value indicating the number of bytes in the hash value. This value will vary depending on the hash algorithm.
                    /// </summary>
                    HP_HASHSIZE,
                    /// <summary>
                    ///     The hash value or message hash for the hash object specified by hHash. This value is generated based on the data supplied
                    ///     to the hash object earlier through the CryptHashData and CryptHashSessionKey functions.
                    /// </summary>
                    HP_HASHVAL
                }
            }
        }
    }
}