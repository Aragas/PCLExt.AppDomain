using System;
using System.Reflection;

namespace PCLExt.AppDomain
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class AppDomain
    {
        private static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException(@"This functionality is not implemented in the portable version of this assembly.
You should reference the PCLExt.AppDomain NuGet package from your main application project in order to reference the platform-specific implementation.");


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAssemblies()
        {
#if DESKTOP || ANDROID || __IOS__ || MAC
            return System.AppDomain.CurrentDomain.GetAssemblies();
#endif

            throw NotImplementedInReferenceAssembly();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyData"></param>
        /// <returns></returns>
        public static Assembly LoadAssembly(byte[] assemblyData)
        {
#if DESKTOP || ANDROID || __IOS__ || MAC
            return Assembly.Load(assemblyData);
#endif

            throw NotImplementedInReferenceAssembly();
        }

#if DESKTOP || ANDROID || __IOS__ || MAC

        private static Assembly CurrentDomainOnAssemblyResolve(object sender, System.ResolveEventArgs args) => _AssemblyResolve?.Invoke(sender, args);

        private static ResolveEventHandler _AssemblyResolve;
        public static event ResolveEventHandler AssemblyResolve
        {
            add
            {
                var wassEmpty = _AssemblyResolve == null;
                    
                _AssemblyResolve += value;

                if(wassEmpty)
                    System.AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;
            }
            remove
            {
                _AssemblyResolve -= value;

                if (_AssemblyResolve == null)
                    System.AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomainOnAssemblyResolve;
            }
        }
#else
        public static event ResolveEventHandler AssemblyResolve
        {
            add => throw NotImplementedInReferenceAssembly();
            remove => throw NotImplementedInReferenceAssembly();
        }
#endif

    }

    /// <summary>Represents a method that handles the <see cref="E:System.AppDomain.TypeResolve" />, <see cref="E:System.AppDomain.ResourceResolve" />, or <see cref="E:System.AppDomain.AssemblyResolve" /> event of an <see cref="T:System.AppDomain" />.</summary>
    /// <param name="sender">The source of the event. </param>
    /// <param name="args">The event data. </param>
    /// <returns>The assembly that resolves the type, assembly, or resource; or <see langword="null" /> if the assembly cannot be resolved.</returns>
    public delegate Assembly ResolveEventHandler(object sender, ResolveEventArgs args);

    public partial class ResolveEventArgs : EventArgs
    {
        public string Name { get; }
        public Assembly RequestingAssembly { get; }

        public ResolveEventArgs(string name) { Name = name; }

        public ResolveEventArgs(string name, Assembly requestingAssembly) { Name = name; RequestingAssembly = requestingAssembly; }
    }

#if DESKTOP || ANDROID || __IOS__ || MAC
    public partial class ResolveEventArgs
    {
        public static implicit operator System.ResolveEventArgs(ResolveEventArgs args) => new System.ResolveEventArgs(args.Name, args.RequestingAssembly);
        public static implicit operator ResolveEventArgs(System.ResolveEventArgs args) => new ResolveEventArgs(args.Name, args.RequestingAssembly);
    }
#endif
}
