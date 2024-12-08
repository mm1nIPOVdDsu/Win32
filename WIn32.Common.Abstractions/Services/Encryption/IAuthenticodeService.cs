namespace Win32.Common.Services.Encryption
{
    /// <summary>
    ///     Handles validation and verification of authenticode/code signing certificates.
    /// </summary>
    public interface IAuthenticodeService : IServiceBase
    {
        /// <summary>
        ///     Determines if a file is signed with a valid code signing certificate.
        /// </summary>
        /// <param name="fileName">The full name of the file to verify.</param>
        /// <returns>True if the file has a valid signature.</returns>
        bool IsTrusted(string fileName);
    }
}
