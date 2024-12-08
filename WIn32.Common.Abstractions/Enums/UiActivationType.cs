using System;

namespace Win32.Common.Enums
{
    /// <summary>
    ///     Defines the type of input that will activate the controls of an application.
    /// </summary>
    [Flags]
    public enum UiActivationType : int
    {
        /// <summary>
        ///     An input type is not selected or defined.
        /// </summary>
        None = 0,
        /// <summary>
        ///     Dwell input is used to activate the controls of an application.
        /// </summary>
        Dwell = 1,
        /// <summary>
        ///     Switch input is used to activate the controls of an application.
        /// </summary>
        Switch = 2,
        /// <summary>
        ///     Scan input is used to activate the controls of an application.
        /// </summary>
        Scan = 4,
        /// <summary>
        ///     Select and Progress input is used to activate the controls of an application.
        /// </summary>
        /// <remarks>AB: I am not 100% sure honestly that is a setting Travis added.</remarks>
        SelectAndProgress = 8,
    }
}
