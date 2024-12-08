namespace Win32.Common.Extensions
{
    /// <summary>
    ///     Extensions for <see cref="int"/>.
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        ///     Gets the number of characters in an integer (length).
        /// </summary>
        /// <param name="value">The integer value.</param>
        /// <returns>The length of the integer value.</returns>
        public static int Length(this int value)
        {
            // NOTE: This method of getting the length was compared to multiple different methods
            //       of getting the integer length. The 3 methods that were evaluated can be found
            //       here: https://tutorialdeep.com/knowhow/find-length-integer-variable-c-sharp
            //       If a better method exists, feel free to replace.
            var intLen = 0;
            var myInt = value;
            while (myInt > 0)
            {
                myInt /= 10;
                intLen++;
            }

            return intLen;
        }
    }
}
