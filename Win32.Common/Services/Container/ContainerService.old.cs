//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//using Win32.Common.Attributes;

//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;

//namespace Win32.Common.Services.Container
//{
//    /// <summary>
//    ///     Service for managing the unity container.
//    /// </summary>
//    [Singleton]
//    public class ContainerService : IContainerService
//    {
//        private readonly IServiceCollection _serviceCollection;
//        private readonly ILogger<ContainerService> _logger;

//        /// <summary>
//        ///     Initializes a new instance of the <see cref="ContainerService"/> class.
//        /// </summary>
//        /// <param name="logger"><see cref="ILogger{ContainerService}"/></param>
//        /// <param name="serviceCollection"><see cref="IServiceCollection"/></param>
//        public ContainerService(ILogger<ContainerService> logger, IServiceCollection serviceCollection)
//        {
//            _serviceCollection = serviceCollection;
//            _logger = logger;
//        }

//        #region Resolve Registrations
//        /// <summary>
//        ///     TODO: Summary
//        /// </summary>
//        /// <param name="registeredType">The interface to get a registration for.</param>
//        /// <returns><see cref="object"/></returns>
//        public object? Resolve(Type registeredType)
//        {
//            if (registeredType is null)
//                throw new ArgumentNullException(nameof(registeredType));
//            if (registeredType.IsInterface)
//                throw new ArgumentException("Type must be an interface.");

//            try
//            {
//                _logger.LogDebug("Getting registration for {Name}.", registeredType.Name);
//                var registration = _serviceCollection.FirstOrDefault(x => x.ServiceType == registeredType || x.ServiceType.Name == registeredType.Name);
//                if (registration is not null)
//                {
//                    _logger.LogDebug("Registration for {Name} found.", registeredType.Name);
//                    return registration.ImplementationInstance;
//                }
//            }
//            catch (Exception ex) { _logger?.LogError(ex, "Error resolving {type}", registeredType.Name); }

//            return null;
//        }
//        /// <summary>
//        ///     Resolves a type from the container.
//        /// </summary>
//        /// <typeparam name="T">An interface that a concrete type derives from.</typeparam>
//        /// <returns>The <typeparamref name="T"/> from the container.</returns>
//        /// <exception cref="MultipleRegistrationsFoundException{T}"/>
//        /// <exception cref="RegistrationNotFoundException{T}"/>
//        public T? Resolve<T>() => (T?)Resolve(typeof(T));

//        /// <summary>
//        ///     Resolves all types that registered to <typeparamref name="T"/>.
//        ///     
//        ///     Will only resolve types that are singleton instances. 
//        /// </summary>
//        /// <typeparam name="T">An interface that a concrete type derives from.</typeparam>
//        /// <returns><see cref="List{T}"/> of <typeparamref name="T"/>.</returns>
//        /// <exception cref="RegistrationNotFoundException{T}"/>
//        public List<object>? ResolveMany(Type registeredType)
//        {
//            if (registeredType is null)
//                throw new ArgumentNullException(nameof(registeredType));
//            if (registeredType.IsInterface)
//                throw new ArgumentException("Type must be an interface.");

//            _logger.LogDebug("Getting registrations for {Name}.", registeredType.Name);
//            var registrations = _serviceCollection.Where(x => x.ServiceType == registeredType || x.ServiceType.Name == registeredType.Name);
//        }



//        /// <summary>
//        ///     Resolves all types that registered to <typeparamref name="T"/>.
//        ///     
//        ///     Will only resolve types that are singleton instances. 
//        /// </summary>
//        /// <typeparam name="T">An interface that a concrete type derives from.</typeparam>
//        /// <returns><see cref="List{T}"/> of <typeparamref name="T"/>.</returns>
//        /// <exception cref="RegistrationNotFoundException{T}"/>
//        public List<T> ResolveMany<T>()
//        {
//            // Only resolve types that are singletons as we don't want to new up a bunch of types to just throw them away.
//            var registrations = ContainerManager.Container.Registrations.Where(x => x.RegisteredType == typeof(T) &&
//                                                                                    x.LifetimeManager.GetType() != typeof(PerResolveLifetimeManager));
//            if (registrations.Any() == false)
//                throw new RegistrationNotFoundException<T>();

//            // TODO: this makes some big assumptions...need to test.
//            var response = new List<T>();
//            foreach (var registration in registrations)
//                response.Add((T)ContainerManager.Container.Resolve(registration.RegisteredType));

//            return response;
//        }
//        /// <summary>
//        ///     Resolves all types that registered to <typeparamref name="T"/>.
//        /// </summary>
//        /// <typeparam name="T">An interface that a concrete type derives from.</typeparam>
//        /// <param name="func">A user defined function to filter types in the unity container.</param>
//        /// <returns><see cref="List{T}"/> of <typeparamref name="T"/>.</returns>
//        /// <exception cref="RegistrationNotFoundException{T}"/>
//        public List<T> ResolveMany<T>(Func<Registration, bool> func)
//        {
//            // Only resolve types that are singletons as we don't want to new up a bunch of types to just throw them away.
//            var registrations = ContainerManager.Container.Registrations.Where(x => x.RegisteredType == typeof(T) &&
//                                                                                    x.LifetimeManager.GetType() != typeof(PerResolveLifetimeManager));
//            if (registrations.Any() == false)
//                throw new RegistrationNotFoundException<T>();

//            var response = new List<T>();
//            foreach (var registration in registrations)
//                response.Add((T)ContainerManager.Container.Resolve(registration.RegisteredType.GetType(), registration.Name));

//            return response;
//        }
//        #endregion

//        #region Resolve Registrations Async
//        /// <summary>
//        ///     Asynchronously resolves a type from the container.
//        /// </summary>
//        /// <typeparam name="T">An interface that a concrete type derives from.</typeparam>
//        /// <returns>The <see cref="Task{T}"/> from the container.</returns>
//        /// <exception cref="MultipleRegistrationsFoundException{T}"/>
//        /// <exception cref="RegistrationNotFoundException{T}"/>
//        public async Task<T> ResolveAsync<T>() => await Task.Factory.StartNew(() => Resolve<T>());

//        /// <summary>
//        ///     Asynchronously resolves a type from the container.
//        /// </summary>
//        /// <typeparam name="T">An interface that a concrete type derives from.</typeparam>
//        /// <param name="registrationName">The unique registration name if interface is mapped to multiple types.</param>
//        /// <returns>The <see cref="Task{T}"/> from the container.</returns>
//        /// <exception cref="MultipleRegistrationsFoundException{T}"/>
//        /// <exception cref="RegistrationNotFoundException{T}"/>
//        public async Task<T> ResolveAsync<T>(string registrationName) => await Task.Factory.StartNew(() => Resolve<T>(registrationName));

//        /// <summary>
//        ///     Asynchronously resolves all types that registered to <typeparamref name="T"/>. 
//        /// </summary>
//        /// <typeparam name="T">An interface that a concrete type derives from.</typeparam>
//        /// <returns><see cref="Task{TResult}"/> of <see cref="List{T}"/> of <typeparamref name="T"/>.</returns>
//        /// <exception cref="RegistrationNotFoundException{T}"/>
//        public async Task<List<T>> ResolveManyAsync<T>() => await Task.Factory.StartNew(() => ResolveMany<T>());

//        /// <summary>
//        ///     Asynchronously resolves all types that registered to <typeparamref name="T"/>.
//        /// </summary>
//        /// <typeparam name="T">An interface that a concrete type derives from.</typeparam>
//        /// <param name="func">A user defined function to filter types in the unity container.</param>
//        /// <returns><see cref="Task{TResult}"/> of <see cref="List{T}"/> of <typeparamref name="T"/>.</returns>
//        /// <exception cref="RegistrationNotFoundException{T}"/>
//        public async Task<List<T>> ResolveManyAsync<T>(Func<Registration, bool> func) => await Task.Factory.StartNew(() => ResolveMany<T>(func));
//        #endregion

//        /// <summary>
//        ///     Gets the names of a type registration for <typeparamref name="T"/>.
//        /// </summary>
//        /// <typeparam name="T">An interface that a concrete type derives from.</typeparam>
//        /// <returns>List of all the names for an interface registration</returns>
//        public List<string> RegistrationNames<T>()
//        {
//            var type = typeof(T);
//            _logger.LogDebug("Getting names for registered type {Name}.", type.Name);
//            var names = ContainerManager.Container.Registrations.Where(x => x.RegisteredType == type).Select(x => x.Name);
//            return names.ToList();
//        }
//    }
//}
