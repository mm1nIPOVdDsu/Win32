//using System.Threading;

//using Win32.Common.Abstractions.Services.CancellationToken;

//namespace Win32.Common.Services
//{
//    /// <summary>
//    ///     Assists in the management of cancellation tokens.
//    /// </summary>
//    public sealed class CancellationTokenService : ICancellationTokenService
//    {
//        private CancellationTokenSource _cancellationTokenSource;

//        /// <summary>
//        ///     Initializes a new instance of the <see cref="CancellationTokenService"/> class.
//        /// </summary>
//        public CancellationTokenService() => _cancellationTokenSource = new CancellationTokenSource();

//        /// <summary>
//        ///     Returns the current <see cref="CancellationToken"/>.
//        /// </summary>
//        public CancellationToken Token => GetCancellationTokenSource().Token;

//        /// <summary>
//        ///     True if a cancellation has been requested.
//        /// </summary>
//        public bool IsCancellationRequested => _cancellationTokenSource.IsCancellationRequested;

//        /// <summary>
//        ///     Cancels the token.
//        /// </summary>
//        public void Cancel() => _cancellationTokenSource?.Cancel();
//        /// <summary>
//        ///     Resets the token (news up <see cref="CancellationTokenSource"/>).
//        /// </summary>
//        public void Reset() => _cancellationTokenSource = new CancellationTokenSource();
//        /// <summary>
//        ///     Disposes of the <see cref="CancellationTokenSource"/>.
//        /// </summary>
//        public void Dispose() => _cancellationTokenSource?.Dispose();

//        /// <summary>
//        ///     Gets the <see cref="CancellationTokenSource"/> used to create <see cref="CancellationToken"/>.
//        /// </summary>
//        /// <returns><see cref="CancellationTokenSource"/></returns>
//        private CancellationTokenSource GetCancellationTokenSource()
//        {
//            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
//            {
//                return _cancellationTokenSource;
//            }

//            _cancellationTokenSource?.Dispose();
//            _cancellationTokenSource = new CancellationTokenSource();

//            return _cancellationTokenSource;
//        }
//    }
//}
