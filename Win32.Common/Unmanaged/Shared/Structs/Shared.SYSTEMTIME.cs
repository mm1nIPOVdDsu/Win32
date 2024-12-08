using System;
using System.Runtime.InteropServices;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <summary>
        ///     Shared interactions.
        /// </summary>
        public partial class Shared
        {
            /// <summary>
            ///     Specifies a date and time, using individual members for the month, day, year, weekday, hour, minute, second, and millisecond. The time
            ///     is either in coordinated universal time (UTC) or local time, depending on the function that is being called.
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct SYSTEMTIME
            {
                /// <summary>
                ///     Initializes a new instance of the <see cref="SYSTEMTIME"/> struct.
                /// </summary>
                /// <param name="dt"><see cref="DateTime"/></param>
                public SYSTEMTIME(DateTime dt)
                {
                    dt = dt.ToUniversalTime();  // SetSystemTime expects the SYSTEMTIME in UTC
                    Year = (short)dt.Year;
                    Month = (short)dt.Month;
                    DayOfWeek = (short)dt.DayOfWeek;
                    Day = (short)dt.Day;
                    Hour = (short)dt.Hour;
                    Minute = (short)dt.Minute;
                    Second = (short)dt.Second;
                    Milliseconds = (short)dt.Millisecond;
                }

                /// <summary>
                ///     The year. The valid values for this member are 1601 through 30827.
                /// </summary>
                [MarshalAs(UnmanagedType.U2)] public short Year;
                /// <summary>
                ///     The month. The valid values for this member are 1 through 12.
                /// </summary>
                [MarshalAs(UnmanagedType.U2)] public short Month;
                /// <summary>
                ///     The day of the week. The valid values for this member are 0 through 6.
                /// </summary>
                [MarshalAs(UnmanagedType.U2)] public short DayOfWeek;
                /// <summary>
                ///     The day of the month. The valid values for this member are 1 through 31.
                /// </summary>
                [MarshalAs(UnmanagedType.U2)] public short Day;
                /// <summary>
                ///     The hour. The valid values for this member are 0 through 23.
                /// </summary>
                [MarshalAs(UnmanagedType.U2)] public short Hour;
                /// <summary>
                ///     The minute. The valid values for this member are 0 through 59.
                /// </summary>
                [MarshalAs(UnmanagedType.U2)] public short Minute;
                /// <summary>
                ///     The second. The valid values for this member are 0 through 59.
                /// </summary>
                [MarshalAs(UnmanagedType.U2)] public short Second;
                /// <summary>
                ///     The millisecond. The valid values for this member are 0 through 999.
                /// </summary>
                [MarshalAs(UnmanagedType.U2)] public short Milliseconds;
            }
        }
    }
}
