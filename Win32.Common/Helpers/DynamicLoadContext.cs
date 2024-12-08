using System;
using System.Reflection;
using System.Runtime.Loader;

namespace Win32.Common.Helpers
{
    /// <summary>
    ///     
    /// </summary>
    internal class DynamicLoadContext : AssemblyLoadContext
    {
        private AssemblyDependencyResolver _resolver;

        /// <summary>
        ///     
        /// </summary>
        /// <param name="pluginPath"></param>
        public DynamicLoadContext(string pluginPath) => _resolver = new AssemblyDependencyResolver(pluginPath);

        /// <summary>
        ///     Load an assembly based upon its <paramref name="assemblyName"/>.
        /// </summary>
        /// <param name="assemblyName"><see cref="AssemblyName"/></param>
        /// <returns>The <see cref="Assembly"/> with the name provided by <paramref name="assemblyName"/>.</returns>
        protected override Assembly? Load(AssemblyName assemblyName)
        {
            var assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
            return string.IsNullOrEmpty(assemblyPath) is false ? LoadFromAssemblyPath(assemblyPath) : null;
        }
        /// <summary>
        ///     Loads an unmanaged assembly based upon its <paramref name="unmanagedDllName"/>.
        /// </summary>
        /// <param name="unmanagedDllName">The name of the assembly.</param>
        /// <returns>An <see cref="IntPtr"/> to the unmanaged assembly.</returns>
        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            if (string.IsNullOrEmpty(unmanagedDllName))
                throw new ArgumentNullException(nameof(unmanagedDllName));

            var libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            return string.IsNullOrEmpty(libraryPath) is false ? LoadUnmanagedDllFromPath(libraryPath) : IntPtr.Zero;
        }
    }
}
