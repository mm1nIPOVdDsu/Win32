using System;
using System.Threading;

namespace Win32.Common.Helpers
{
    /// <summary>
    ///     TODO: Summary
    /// </summary>
    public class RetryHelper : IRetryHelper
    {
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        /// <param name="action"></param>
        /// <param name="retryIntervalInSeconds"></param>
        /// <param name="retryCount"></param>
        public void Retry(Action action, int retryIntervalInSeconds = 5, int retryCount = 5)
        {
            Retry<object?>(() =>
            {
                action();
                return null;
            }, retryIntervalInSeconds, retryCount);
        }
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="retryIntervalInSeconds"></param>
        /// <param name="retryCount"></param>
        /// <returns></returns>
        public T? Retry<T>(Func<T> func, int retryIntervalInSeconds = 5, int retryCount = 5)
        {
            var returnVal = default(T);
            for (var retry = 0; retry < retryCount; retry++)
            {
                try
                {
                    returnVal = func();
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(retryIntervalInSeconds));
                }
            }

            return returnVal;
        }
    }

    /// <summary>
    ///     TODO: Summary
    /// </summary>
    public interface IRetryHelper
    {
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        /// <param name="action"></param>
        /// <param name="retryIntervalInSeconds"></param>
        /// <param name="retryCount"></param>
        void Retry(Action action, int retryIntervalInSeconds = 5, int retryCount = 5);
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="retryIntervalInSeconds"></param>
        /// <param name="retryCount"></param>
        /// <returns></returns>
        T? Retry<T>(Func<T> func, int retryIntervalInSeconds = 5, int retryCount = 5);
    }
}
