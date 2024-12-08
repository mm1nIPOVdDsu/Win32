//using System;

//namespace Win32.Common.Services.CancellationToken
//{
//    /// <summary>
//    ///     Defines a service that manages cancellation tokens.
//    /// </summary>
//    public interface ICancellationTokenService : IServiceBase, IDisposable
//    {
//        /// <summary>
//        ///     If a token cancellation has been requested.
//        /// </summary>
//        bool IsCancellationRequested { get; }
//        /// <summary>
//        ///     The current cancellation token.
//        /// </summary>
//        CancellationToken Token { get; }

//        /// <summary>
//        ///     Cancels the cancellation token.
//        /// </summary>
//        void Cancel();
//        /// <summary>
//        ///     Resets the cancellation token.
//        /// </summary>
//        void Reset();
//    }
//}
