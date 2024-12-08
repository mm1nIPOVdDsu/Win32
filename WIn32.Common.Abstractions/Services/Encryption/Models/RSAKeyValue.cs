using System;

namespace Win32.Common.Services.Encryption
{
    /// <summary>
    ///     Attributes of an <see cref="RSAKey"/>
    /// </summary>
    [Serializable]
    public class RSAKeyValue : IRSAKeyValue
    {
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        public string Modulus { get; set; } = "";
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        public string Exponent { get; set; } = "";
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        public string P { get; set; } = "";
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        public string Q { get; set; } = "";
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        public string DP { get; set; } = "";
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        public string DQ { get; set; } = "";
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        public string InverseQ { get; set; } = "";
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        public string D { get; set; } = "";
    }
}
