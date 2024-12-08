using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Win32.Common.Services.Container
{
    /// <summary>
    ///     Service for managing the unity container.
    /// </summary>
    public interface IContainerService
    {
        /// <summary>
        ///     Resolves a type from the container.
        /// </summary>
        /// <typeparam name="T">An interface that a concrete type derives from.</typeparam>
        /// <returns>The <typeparamref name="T"/> from the container.</returns>
        T? Resolve<T>();
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        /// <param name="registrationType"></param>
        /// <returns></returns>
        object? Resolve(Type registrationType);

        /// <summary>
        ///     Resolves all types that registered to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">An interface that a concrete type derives from.</typeparam>
        /// <returns><see cref="List{T}"/> of <typeparamref name="T"/>.</returns>
        List<T> ResolveMany<T>();
        /// <summary>
        ///     Resolves all types that registered to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">An interface that a concrete type derives from.</typeparam>
        /// <param name="func">A user defined function to filter types in the unity container.</param>
        /// <returns><see cref="List{T}"/> of <typeparamref name="T"/>.</returns>
        List<T> ResolveMany<T>(Func<Registration, bool> func);

        /// <summary>
        ///     Asynchronously resolves a type from the container.
        /// </summary>
        /// <typeparam name="T">An interface that a concrete type derives from.</typeparam>
        /// <returns>The <see cref="Task{T}"/> from the container.</returns>
        Task<T> ResolveAsync<T>();
        /// <summary>
        ///     Asynchronously resolves a type from the container.
        /// </summary>
        /// <typeparam name="T">An interface that a concrete type derives from.</typeparam>
        /// <param name="registrationName">The unique registration name if interface is mapped to multiple types.</param>
        /// <returns>The <see cref="Task{T}"/> from the container.</returns>
        Task<T> ResolveAsync<T>(string registrationName);
        /// <summary>
        ///     Asynchronously resolves all types that registered to <typeparamref name="T"/>. 
        /// </summary>
        /// <typeparam name="T">An interface that a concrete type derives from.</typeparam>
        /// <returns><see cref="Task{TResult}"/> of <see cref="List{T}"/> of <typeparamref name="T"/>.</returns>
        Task<List<T>> ResolveManyAsync<T>();
        /// <summary>
        ///     Asynchronously resolves all types that registered to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">An interface that a concrete type derives from.</typeparam>
        /// <param name="func">A user defined function to filter types in the unity container.</param>
        /// <returns><see cref="Task{TResult}"/> of <see cref="List{T}"/> of <typeparamref name="T"/>.</returns>
        Task<List<T>> ResolveManyAsync<T>(Func<Registration, bool> func);

        /// <summary>
        ///     Gets the names of a type registration for <typeparamref name="T"/>.
        /// </summary>
        /// <returns>List of all the names for an interface registration</returns>
        List<string> RegistrationNames<T>();
        ///// <summary>
        /////     
        ///// </summary>
        //IDependencyResolver WebApiResolver { get; }
    }
}
