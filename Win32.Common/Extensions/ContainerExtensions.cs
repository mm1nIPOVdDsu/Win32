using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

using Win32.Common.Attributes;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Win32.Common.Extensions
{
    /// <summary>
    ///     Extension methods for adding dependencies to an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ContainerExtensions
    {
        // TODO: Check for the latest version an assembly when searching for assemblies. 
        //       If we have compatibility version numbers, we can get the latest of the file
        //       that is binary/source compatible. When loading the container, there should
        //       be a check to get the newest version of a binary/source compatible assembly.
        // TODO: Create a child container for each of the 'Hubs' that get loaded.
        private const string RootNamespace = "Win32";

        /// <summary>
        ///     Adds found assemblies to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection"><see cref="IServiceCollection"/></param>
        /// <param name="interfaceType"></param>
        /// <param name="assemblySearchPatterns">A list of assembly search patters. See <see cref="DirectoryInfo.GetFiles()"/> for search pattern.</param>
        /// <param name="namespacesToExclude">Assembly namespaces to exclude from add to the <see cref="IServiceCollection"/> container.</param>
        public static void AddRegistrations(IServiceCollection serviceCollection, Type interfaceType, List<string> assemblySearchPatterns, List<string> namespacesToExclude) =>
            AddRegistrations(serviceCollection, new List<Type>() { interfaceType }, assemblySearchPatterns, namespacesToExclude);
        /// <summary>
        ///     Adds found assemblies to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection"><see cref="IServiceCollection"/></param>
        /// <param name="interfaceTypes"></param>
        /// <param name="assemblySearchPatterns">A list of assembly search patters. See <see cref="DirectoryInfo.GetFiles()"/> for search pattern.</param>
        /// <param name="namespacesToExclude">Assembly namespaces to exclude from add to the <see cref="IServiceCollection"/> container.</param>
        public static void AddRegistrations(IServiceCollection serviceCollection, List<Type> interfaceTypes, List<string> assemblySearchPatterns, List<string> namespacesToExclude)
        {
            //var assemblyTypes = GetAssemblyTypes(assemblySearchPatterns, namespacesToExclude);
            var searchResults = GetAllAssemblyFiles(assemblySearchPatterns);
            // get a distinct list of files
            var distinctFiles = searchResults.GroupBy(f => f.Name).Select(g => g.First()).ToList();

            foreach (var file in distinctFiles)
            {
                //// all signed interface and class types
                //if(IsValidSignedAssembly(file.FullName) == false)
                //{
                //    // TODO: Not sure what to do here...throw exception?
                //}

                foreach (var interfaceType in interfaceTypes)
                {
                    Register(serviceCollection, file, interfaceType, assemblySearchPatterns, namespacesToExclude);
                }
            }
        }
        /// <summary>
        ///     Adds found assemblies to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection"><see cref="IServiceCollection"/></param>
        /// <param name="assemblySearchPatterns">A list of assembly search patters. See <see cref="DirectoryInfo.GetFiles()"/> for search pattern.</param>
        /// <param name="namespacesToExclude">Assembly namespaces to exclude from add to the <see cref="IServiceCollection"/> container.</param>
        public static IServiceCollection? RegisterAll(IServiceCollection serviceCollection, List<string> assemblySearchPatterns, List<string> namespacesToExclude)
        {
            // TODO: this method needs to be broken up in to multiple different methods.
            if (assemblySearchPatterns is null || assemblySearchPatterns.Count == 0)
                return null;

            // get a distinct list of files matching search pattern
            var searchResults = GetAllAssemblyFiles(assemblySearchPatterns);
            Debug.WriteLine($"Found {searchResults.Count} file(s) matching file filter.");
            var distinctFiles = searchResults.GroupBy(f => f.Name).Select(g => g.First()).ToList();
            if (distinctFiles is null || distinctFiles.Any() is false)
            {
                Debug.WriteLine($"Could not find any files for the provided search patterns {string.Join(',', assemblySearchPatterns)}.");
                return serviceCollection;
            }

            Debug.WriteLine($"Found {distinctFiles.Count} file(s) to register.");
            var assemblyTypes = distinctFiles.SelectMany(df => Assembly.LoadFrom(df.FullName).GetTypes().Where(n =>
                                                        n.IsVisible is true && n.Namespace is not null &&
                                                        namespacesToExclude.All(x => x != n.Namespace)));

            // get all non-abstract classes that has at least one interface
            var assemblyClasses = assemblyTypes.Where(x => x.IsClass is true && x.IsAbstract is false && x.IsInterface is false && x.GetInterfaces().Length > 0);
            // get all interfaces
            var assemblyInterfaces = assemblyTypes.Where(x => x.IsClass is false && x.IsInterface is true);

            Debug.WriteLine($"Enumerating {assemblyClasses.Count()} classes(s).");
            foreach (var assemblyClass in assemblyClasses)
            {
                // add some spacing for the debug output.
                Debug.WriteLine("");
                if (assemblyClass.Namespace is null)
                {
                    Debug.WriteLine($"The type {assemblyClass.Name} does not have a namespace, skipping.");
                    continue;
                }
                Debug.WriteLine($"Checking if {assemblyClass.Namespace}.{assemblyClass.Name} has already been registered.");
                if (serviceCollection.Any(x => x.ImplementationType is not null && (x.ServiceType.Name == assemblyClass.Name || x.ImplementationType.Name == assemblyClass.Name) && x.ServiceType.Namespace == assemblyClass.Namespace))
                {
                    Debug.WriteLine($"{assemblyClass.Namespace}.{assemblyClass.Name} is already registered in the container, skipping.");
                    continue;
                }

                Debug.WriteLine($"Getting loaded interfaces for {assemblyClass.Name} with matching name.");
                var foundInterface = assemblyInterfaces.FirstOrDefault(x => x.Name == $"I{assemblyClass.Name}");
                if (foundInterface is null)
                {
                    Debug.WriteLine($"Checking for existance of the RegisterClass attribute.");
                    var overrideRegistration = assemblyClass.GetCustomAttributes(typeof(RegisterClassAttribute), true).Any();
                    if(overrideRegistration is true)
                    {
                        Debug.WriteLine($"Class contains the RegisterClass attribute, adding class to the container.");
                        Debug.WriteLine($"Looking for singleton attribute decorator on {assemblyClass.Name}.");
                        if (assemblyClass.GetCustomAttributes(typeof(SingletonAttribute), true).Any() is true)
                        {
                            Debug.WriteLine($"Registering the type {assemblyClass.Name} as a singleton.");
                            serviceCollection.TryAddSingleton(assemblyClass);
                        }
                        else
                        {
                            Debug.WriteLine($"Registering the type {assemblyClass.Name} as a transient.");
                            serviceCollection.TryAddTransient(assemblyClass);
                        }
                        
                        continue;
                    }
                    else
                    {
                        Debug.WriteLine($"Could not find a matching interface for {assemblyClass.Name}.");
                        continue;
                    }
                }

                // will hold all interfaces to be registered
                var interfacesToRegister = new List<Type?>() { foundInterface };

                Debug.WriteLine($"Looking for singleton attribute decorator on {assemblyClass.Name}.");
                var isSingleton = assemblyClass.GetCustomAttributes(typeof(SingletonAttribute), true).Any();

                // only register types that are from the same namespace or assemblies
                Debug.WriteLine($"Getting root namespace for type {assemblyClass.Name}.");
                var rootNamespace = assemblyClass.Namespace.Split('.')[0];

                // only getting nested interfaces that aren't generic and in the same-ish namespace to reduce complexity
                Debug.WriteLine($"Getting nested interfaces from {foundInterface.Name}.");
                var additionalInterfaces = foundInterface.GetInterfaces().Where(i => i.Namespace is not null && i.Namespace.Contains(rootNamespace) && i.IsGenericType is false);
                if (additionalInterfaces.Any() is true)
                {
                    Debug.WriteLine($"Getting {additionalInterfaces.Count()} interface(s) from loaded interfaces.");
                    var typesToRegister = assemblyInterfaces.Select(x => additionalInterfaces.FirstOrDefault(y => y.Name == x.Name && y.Namespace == x.Namespace)!).Where(x => x is not null);

                    Debug.WriteLine($"Adding {typesToRegister.Count()} to list of interfaces to register.");
                    interfacesToRegister.AddRange(typesToRegister);
                }

                // we're only registering the class so that we can register it to multiple interfaces
                Debug.WriteLine($"Registering {assemblyClass.Name} as {(isSingleton ? "a singleton" : "transient")}.");
                if (isSingleton)
                {
                    serviceCollection.TryAddSingleton(assemblyClass);
                    foreach (var interfaceToRegister in interfacesToRegister)
                    {
                        if (interfaceToRegister is null)
                        {
                            Debug.WriteLine($"Interface for {assemblyClass.Name} is null, continuing.");
                            continue;
                        }

                        if (interfaceToRegister.IsGenericType && assemblyClass.IsGenericType)
                        {
                            Debug.WriteLine($"Attempting to register {interfaceToRegister.Name} to {assemblyClass.Name} as a singleton.");
                            serviceCollection.TryAddSingleton(interfaceToRegister, assemblyClass);
                        }
                        else
                        {
                            Debug.WriteLine($"Attempting to register {interfaceToRegister.Name} to {assemblyClass.Name} with required services as a singleton.");
                            serviceCollection.TryAddSingleton(interfaceToRegister, factory => factory.GetRequiredService(assemblyClass));
                        }
                    }
                }
                else
                {
                    serviceCollection.TryAddTransient(assemblyClass);
                    foreach (var interfaceToRegister in interfacesToRegister)
                    {
                        if (interfaceToRegister is null)
                        {
                            Debug.WriteLine($"Interface for {assemblyClass.Name} is null, continuing.");
                            continue;
                        }

                        if (interfaceToRegister.IsGenericType && assemblyClass.IsGenericType)
                        {
                            Debug.WriteLine($"Attempting to register {interfaceToRegister.Name} to {assemblyClass.Name} as transient.");
                            serviceCollection.TryAddTransient(interfaceToRegister, assemblyClass);
                        }
                        else
                        {
                            Debug.WriteLine($"Attempting to register {interfaceToRegister.Name} to {assemblyClass.Name} with required services as transient.");
                            serviceCollection.TryAddTransient(interfaceToRegister, factory => factory.GetRequiredService(assemblyClass));
                        }
                    }
                }

                //Debug.WriteLine($"Getting interfaces for {assemblyClass.Name}.");
                //var classInterfaces = assemblyClass.GetInterfaces();
                //if(classInterfaces is null || classInterfaces.Any() is false)
                //{
                //    Debug.WriteLine($"{assemblyClass.Name} does not contain any interfaces, skipping.");
                //    continue;
                //}
                //Debug.WriteLine($"Registering {classInterface.Name} to {classInterface.Name} as {(isSingleton ? "a singleton" : "transient")}.");
                //// registers a single concrete type to multiple interfaces
                //// https://andrewlock.net/how-to-register-a-service-with-multiple-interfaces-for-in-asp-net-core-di/#2-implement-forwarding-using-factory-methods
                //if (isSingleton)
                //{
                //    if (classInterface.IsGenericType || assemblyType.IsGenericType)
                //    {
                //        serviceCollection = serviceCollection.AddSingleton(classInterface, assemblyType);
                //    }
                //    else
                //    {
                //        serviceCollection = serviceCollection.AddSingleton(classInterface, factory => factory.GetRequiredService(assemblyType));
                //    }
                //}
                //else
                //{
                //    if (classInterface.IsGenericType || assemblyType.IsGenericType)
                //    {
                //        serviceCollection = serviceCollection.AddTransient(classInterface, assemblyType);
                //    }
                //    else
                //    {
                //        serviceCollection = serviceCollection.AddTransient(classInterface, factory => factory.GetRequiredService(assemblyType));
                //    }

                //}

                //// only register types that are from the same namespace or assemblies
                //Debug.WriteLine("Getting root namespace for type.");
                //var rootNamespace = assemblyClass.Namespace.Split('.')[0];
            }

            return serviceCollection;
        }
        /// <summary>
        ///     Adds all found assemblies to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection"><see cref="IServiceCollection"/></param>
        /// <param name="assemblySearchPatterns">A list of assembly search patters. See <see cref="DirectoryInfo.GetFiles()"/> for search pattern.</param>
        /// <param name="namespacesToExclude">Assembly namespaces to exclude from add to the <see cref="IServiceCollection"/> container.</param>
        /// <param name="interfacesToExclude">Any class that inherits from a type is excluded from registration.</param>
        public static void RegisterAll(IServiceCollection serviceCollection, List<string> assemblySearchPatterns, List<string> namespacesToExclude, List<Type> interfacesToExclude)
        {
            if (assemblySearchPatterns is null || assemblySearchPatterns.Count == 0)
                return;

            //var assemblyTypes = GetAssemblyTypes(assemblySearchPatterns, namespacesToExclude);
            var searchResults = GetAllAssemblyFiles(assemblySearchPatterns);
            // get a distinct list of files
            var distinctFiles = searchResults.GroupBy(f => f.Name).Select(g => g.First());

            // register all the types in all found files
            foreach (var distinctFile in distinctFiles)
            {
                //// all signed interface and class types
                //if(IsValidSignedAssembly(file.FullName) == false)
                //{
                //    // TODO: Not sure what to do here...throw exception?
                //}

                var assem = Assembly.LoadFrom(distinctFile.FullName);
                // filter out namespaces we don't want
                var types = assem.GetTypes().Where(n => n.IsClass is true && n.IsNested is false && n.IsVisible is true && n.IsAbstract is false &&
                                                        n.Namespace is not null && n.Namespace.Contains(RootNamespace) &&
                                                        namespacesToExclude.All(x => x != n.Namespace));
                // register interfaces to classes
                foreach (var type in types)
                {
                    // exclude types that should be registered by another handler (MagicOnion)
                    if (type.GetInterfaces().Any(ci => interfacesToExclude.Any(ite => ci.Name.Equals(ite.Name) && ci.Assembly.Equals(ite.Assembly))))
                    {
                        Debug.WriteLine($"The type {type.Name} will be excluded based upon exclusion filter.");
                        continue;
                    }

                    // NOTE: could/should also probably check if the application has visibility to internals for the assembly getting loaded
                    // get all the interfaces of the class that are visible
                    var classInterfaces = type.GetInterfaces().Where(x => x.Namespace is not null && x.IsVisible is true &&
                                                                          interfacesToExclude.Any(ite => x.Equals(ite)) is false &&
                                                                          x.Namespace.Contains(RootNamespace));

                    // determine if the type should be registered as a singleton
                    var isSingleton = type.GetCustomAttributes(typeof(SingletonAttribute), true).Any();

                    // register class interfaces to its concrete type
                    foreach (var classInterface in classInterfaces)
                    {
                        // registers a single concrete type to multiple interfaces
                        // https://andrewlock.net/how-to-register-a-service-with-multiple-interfaces-for-in-asp-net-core-di/#2-implement-forwarding-using-factory-methods
                        if (isSingleton)
                        {
                            serviceCollection.AddSingleton(type);
                            serviceCollection.AddSingleton(classInterface, factory => factory.GetRequiredService(type));
                        }
                        else
                        {
                            serviceCollection.AddTransient(type);
                            serviceCollection.AddTransient(classInterface, factory => factory.GetRequiredService(type));
                        }
                    }
                }
            }
        }
        /// <summary>
        ///     Adds found assemblies to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection"><see cref="IServiceCollection"/></param>
        /// <param name="assemblySearchPatterns">A list of assembly search patters. See <see cref="DirectoryInfo.GetFiles()"/> for search pattern.</param>
        /// <param name="namespacesToExclude">Assembly namespaces to exclude from add to the <see cref="IServiceCollection"/> container.</param>
        [Obsolete("Use RegisterAll instead. This was meant to be a test method. Oops.")]
        public static IServiceCollection? RegisterAll2(IServiceCollection serviceCollection, List<string> assemblySearchPatterns, List<string> namespacesToExclude) =>
            RegisterAll(serviceCollection, assemblySearchPatterns, namespacesToExclude);
        /// <summary>
        ///     Adds all found assemblies to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <remarks>
        ///     Only registers types in the same assembly. References to other assemblies may not register correctly. Use <see cref="RegisterAll(IServiceCollection, List{string}, List{string})"/> 
        ///     for assemblies that have external references.
        /// </remarks>
        /// <param name="serviceCollection"><see cref="IServiceCollection"/></param>
        /// <param name="assemblySearchPatterns">A list of assembly search patters. See <see cref="DirectoryInfo.GetFiles()"/> for search pattern.</param>
        /// <param name="namespacesToExclude">Assembly namespaces to exclude from add to the <see cref="IServiceCollection"/> container.</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection? RegisterAllInSameAssmbly(IServiceCollection serviceCollection, List<string> assemblySearchPatterns, List<string> namespacesToExclude)
        {
            if (assemblySearchPatterns is null || assemblySearchPatterns.Count == 0)
                return null;

            foreach (var assemblySearchPattern in assemblySearchPatterns)
            {
                //var assemblyTypes = GetAssemblyTypes(assemblySearchPatterns, namespacesToExclude);
                var searchResults = GetAllAssemblyFiles(assemblySearchPattern);
                // get a distinct list of files
                Debug.WriteLine($"Found {searchResults.Count} file(s) matching file filter.");
                var distinctFiles = searchResults.GroupBy(f => f.Name).Select(g => g.First());

                Debug.WriteLine($"Found {distinctFiles.Count()} file(s) to register.");
                var assemblyTypes = distinctFiles.SelectMany(df => Assembly.LoadFrom(df.FullName).GetTypes().Where(n =>
                                                            ((n.IsClass is true && n.IsAbstract is false) || n.IsInterface is true) && n.IsVisible is true &&
                                                            n.GetInterfaces().Count() > 0 && n.Namespace is not null &&
                                                            namespacesToExclude.All(x => x != n.Namespace)));

                Debug.WriteLine($"Enumerating {assemblyTypes.Count()} type(s).");
                foreach (var assemblyType in assemblyTypes)
                {
                    if (assemblyType.IsInterface || assemblyType.Namespace is null)
                        continue;

                    // only register types that are from the same namespace or assemblies
                    Debug.WriteLine("Getting root namespace for type.");
                    var rootNamespace = assemblyType.Namespace.Split('.')[0];

                    // NOTE: could/should also probably check if the application has visibility to internals for the assembly getting loaded
                    // get all the interfaces of the class that are visible and do not contain generics
                    Debug.WriteLine("Getting interface types from the list of types from the found assemblies.");
                    var foundInterfaces = assemblyType.GetInterfaces().Where(x =>
                        x is not null && x.Namespace is not null && x.Namespace.Contains(rootNamespace) && x.IsVisible is true);

                    // need to get the type from the list that were found earlier, not from the type we're enumerating
                    Debug.WriteLine("Getting interfaces that are found in the list of found types.");
                    var classInterfaces = foundInterfaces.SelectMany(x => assemblyTypes.Where(at => at.Name == x.Name));

                    // determine if the type should be registered as a singleton
                    Debug.WriteLine($"Found {classInterfaces.Count()} interfaces for {assemblyType.Name}.");
                    var isSingleton = assemblyType.GetCustomAttributes(typeof(SingletonAttribute), true).Any();

                    Debug.WriteLine($"Registering {assemblyType.Name} as {(isSingleton ? "a singleton" : "transient")}.");
                    serviceCollection = isSingleton ? serviceCollection.AddSingleton(assemblyType) : serviceCollection.AddTransient(assemblyType);

                    // register class interfaces to its concrete type
                    foreach (var classInterface in classInterfaces)
                    {
                        if (classInterface.Name.Contains("IScreenCaptureService"))
                        {
                            //serviceCollection.AddTransient(typeof(ScreenCaptureService), typeof(IScreenCaptureService<Bitmap>));
                            Debug.WriteLine("");
                            //continue;
                        }

                        Debug.WriteLine($"Registering {classInterface.Name} to {classInterface.Name} as {(isSingleton ? "a singleton" : "transient")}.");
                        // registers a single concrete type to multiple interfaces
                        // https://andrewlock.net/how-to-register-a-service-with-multiple-interfaces-for-in-asp-net-core-di/#2-implement-forwarding-using-factory-methods
                        if (isSingleton)
                        {
                            Debug.WriteLine("Register type as a singleton.");
                            serviceCollection = classInterface.IsGenericType || assemblyType.IsGenericType
                                ? serviceCollection.AddSingleton(classInterface, assemblyType)
                                : serviceCollection.AddSingleton(classInterface, factory => factory.GetRequiredService(assemblyType));
                        }
                        else
                        {
                            Debug.WriteLine("Register type as a transient.");
                            serviceCollection = classInterface.IsGenericType || assemblyType.IsGenericType
                                ? serviceCollection.AddTransient(classInterface, assemblyType)
                                : serviceCollection.AddTransient(classInterface, factory => factory.GetRequiredService(assemblyType));

                        }
                    }
                }

                //// register all the types in all found files
                //foreach (var distinctFile in distinctFiles)
                //{
                //    //// all signed interface and class types
                //    //if(IsValidSignedAssembly(file.FullName) == false)
                //    //{
                //    //    // TODO: Not sure what to do here...throw exception?
                //    //}

                //    // TODO: should the assemblies all be loaded so they can registered?
                //    var assem = Assembly.LoadFrom(distinctFile.FullName);
                //    // filter out namespaces we don't want
                //    var types = assem.GetTypes().Where(n => n.IsClass is true && n.IsNested is false && n.IsVisible is true && n.IsAbstract is false &&
                //                                            n.Namespace is not null && n.Namespace.Contains(RootNamespace) &&
                //                                            namespacesToExclude.All(x => x != n.Namespace));
                //    // register interfaces to classes
                //    foreach (var type in types)
                //    {
                //        // NOTE: could/should also probably check if the application has visibility to internals for the assembly getting loaded
                //        // get all the interfaces of the class that are visible and do not contain generics
                //        var classInterfaces = type.GetInterfaces().Where(x => x.Namespace is not null &&
                //            x.Namespace.Contains(RootNamespace) && x.IsVisible is true && x.ContainsGenericParameters is false);
                //        // determine if the type should be registered as a singleton
                //        var isSingleton = type.GetCustomAttributes(typeof(SingletonAttribute), true).Any();

                //        // register class interfaces to its concrete type
                //        foreach (var classInterface in classInterfaces)
                //        {
                //            // registers a single concrete type to multiple interfaces
                //            // https://andrewlock.net/how-to-register-a-service-with-multiple-interfaces-for-in-asp-net-core-di/#2-implement-forwarding-using-factory-methods
                //            if (isSingleton)
                //            {
                //                serviceCollection.AddSingleton(type);
                //                serviceCollection.AddSingleton(classInterface, factory => factory.GetRequiredService(type));
                //            }
                //            else
                //            {
                //                serviceCollection.AddTransient(type);
                //                serviceCollection.AddTransient(classInterface, factory => factory.GetRequiredService(type));
                //            }
                //        }
                //    }
                //}
            }

            return serviceCollection;
        }

        /// <summary>
        ///     Gets all the assemblies in the current directory using a defined search pattern.
        /// </summary>
        /// <param name="assemblySearchPatterns">The search pattern used to filter files.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Thrown if the path of the executing assembly is null.</exception>
        private static List<FileInfo> GetAllAssemblyFiles(List<string> assemblySearchPatterns)
        {
            // TODO: This method could be moved to an extensions class potentially.
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (path == null)
            {
                throw new Exception("Can't get executing assembly location");
            }

            Debug.WriteLine($"Searching the directory {path}.");
            var directoryInfo = new DirectoryInfo(path);
            var fileInfos = new List<FileInfo>();
            foreach (var pattern in assemblySearchPatterns)
            {
                Debug.WriteLine($"Search for files using the filter string {pattern}.");

                // this is not the best way to do it but the number of files shouldn't be huge.
                var foundFiles = directoryInfo.GetFiles(pattern, SearchOption.AllDirectories);
                fileInfos.AddRange(foundFiles);
            }

            return fileInfos;
        }
        /// <summary>
        ///     Gets all the assemblies in the current directory using a defined search pattern.
        /// </summary>
        /// <param name="assemblySearchPattern">The search pattern used to filter files.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Thrown if the path of the executing assembly is null.</exception>
        private static List<FileInfo> GetAllAssemblyFiles(string assemblySearchPattern)
        {
            Debug.WriteLine($"Search for files using the filter string {assemblySearchPattern}.");
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (path == null)
            {
                throw new Exception("Can't get executing assembly location");
            }

            Debug.WriteLine($"Searching the directory {path}.");
            var directoryInfo = new DirectoryInfo(path);
            var fileInfos = directoryInfo.GetFiles(assemblySearchPattern, SearchOption.AllDirectories).ToList();

            return fileInfos;
        }
        /// <summary>
        ///     Registers a concrete type to multiple inherited types.
        /// </summary>
        /// <param name="serviceCollection"><see cref="IServiceCollection"/></param>
        /// <param name="file">The file who's types will be registered.</param>
        /// <param name="interfaceType">The interface type to search for.</param>
        /// <param name="assemblySearchPatterns">The filter that will be used to search for assemblies.</param>
        /// <param name="namespacesToExclude">Any namespaces to exclude from the <paramref name="file"/> when registering.</param>
        private static void Register(IServiceCollection serviceCollection, FileInfo file, Type interfaceType, List<string> assemblySearchPatterns, List<string> namespacesToExclude)
        {
            var assem = Assembly.LoadFrom(file.FullName);
            // filter out namespaces we don't want
            var types = assem.GetTypes().ToList().Where(n => n.IsClass is true && n.IsNested is false &&
                                                             namespacesToExclude.All(x => x != n.Namespace) &&
                                                             n.GetInterfaces().Any(y => y.Name == interfaceType.Name));
            foreach (var type in types)
            {
                var commonInterfaces = type.GetInterfaces().Where(x => x.Namespace is not null && x.Namespace.Contains(RootNamespace));
                var classInterface = type.GetInterfaces().FirstOrDefault(x => x.Name == $"I{type.Name}");
                if (classInterface is null)
                    continue;

                // registers a singled concrete type to multiple interfaces
                // https://andrewlock.net/how-to-register-a-service-with-multiple-interfaces-for-in-asp-net-core-di/#2-implement-forwarding-using-factory-methods
                serviceCollection.AddSingleton(type);
                serviceCollection.AddSingleton(classInterface, factory => factory.GetRequiredService(type));
                serviceCollection.AddSingleton(interfaceType, factory => factory.GetRequiredService(type));
            }
        }
    }
}
