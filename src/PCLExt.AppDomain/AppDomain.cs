using System;
using System.Reflection;

namespace PCLExt.AppDomain
{
    /// <summary>
    /// 
    /// </summary>
    public static class AppDomain
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
        public static event ResolveEventHandler AssemblyResolve
        {
            add { System.AppDomain.CurrentDomain.AssemblyResolve += DelegateUtility.Cast<System.ResolveEventHandler>(value); }
            remove { System.AppDomain.CurrentDomain.AssemblyResolve -= DelegateUtility.Cast<System.ResolveEventHandler>(value); }
        }
        private static class DelegateUtility
        {
            public static T Cast<T>(Delegate source) where T : class => Cast(source, typeof(T)) as T;
            public static Delegate Cast(Delegate source, Type type)
            {
                if (source == null)
                    return null;
                var delegates = source.GetInvocationList();
                if (delegates.Length == 1)
                    return Delegate.CreateDelegate(type, delegates[0].Target, delegates[0].Method);

                var delegatesDest = new Delegate[delegates.Length];
                for (var nDelegate = 0; nDelegate < delegates.Length; nDelegate++)
                    delegatesDest[nDelegate] = Delegate.CreateDelegate(type, delegates[nDelegate].Target, delegates[nDelegate].Method);

                return Delegate.Combine(delegatesDest);
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
}
