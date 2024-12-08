namespace Win32.Common.Services.Encryption
{
    /// <summary>
    ///     Attributes of an <see cref="RSAKey"/>
    /// </summary>
    internal interface IRSAKeyValue
    {
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        string D { get; set; }
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        string DP { get; set; }
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        string DQ { get; set; }
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        string Exponent { get; set; }
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        string InverseQ { get; set; }
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        string Modulus { get; set; }
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        string P { get; set; }
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        string Q { get; set; }
    }
}
