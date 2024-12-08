using System;

namespace Win32.Common.Services.SystemInformation
{
    /// <summary>
    ///     
    /// </summary>
    public interface ISystemInformationService : IServiceBase
    {
        /// <summary>
        ///     Gets the active hours start and end times.
        /// </summary>
        /// <returns><see cref="Tuple{StartTime, EndTime}"/>.</returns>
        (int StartTime, int EndTime) GetActiveHours();
        /// <summary>
        ///     Gets the enabled state of Windows Active Hours feature.
        /// </summary>
        /// <returns>The <see cref="EnabledStatus"/> of Active Hours.</returns>
        EnabledStatus GetActiveHoursStatus();
        /// <summary>
        ///     Gets the CPU ID of the device's processor.
        /// </summary>
        /// <returns>The string CPU ID information.</returns>
        string GetCpuId();
        /// <summary>
        ///     Gets the <see cref="DateTime"/> of the last user input.
        /// </summary>
        /// <returns>The <see cref="DateTime"/> of the last input received by the user.</returns>
        DateTime GetLastUserInput();
    }
}
