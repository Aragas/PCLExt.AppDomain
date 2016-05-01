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
        /// <param name="type"></param>
        /// <returns></returns>
        public static Assembly GetAssembly(Type type)
        {
#if DESKTOP || ANDROID || __IOS__ || MAC
            return Assembly.GetAssembly(type);
#endif

            throw NotImplementedInReferenceAssembly();
        }

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
    }
}
