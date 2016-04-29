using System;
using System.Reflection;

namespace PCLExt.AppDomain
{
    /// <summary>
    /// 
    /// </summary>
    public static class AppDomain
    {
        static Lazy<IAppDomain> _instance = new Lazy<IAppDomain>(CreateInstance, System.Threading.LazyThreadSafetyMode.PublicationOnly);

        static IAppDomain CreateInstance()
        {
#if COMMON
            return new DesktopAppDomain();
#endif

            return null;
        }

        private static IAppDomain Instance
        {
            get
            {
                var ret = _instance.Value;
                if (ret == null)
                    throw NotImplementedInReferenceAssembly();
                return ret;
            }
        }

        internal static Exception NotImplementedInReferenceAssembly() => new NotImplementedException("This functionality is not implemented in the portable version of this assembly. You should reference the PCLExt.AppDomain NuGet package from your main application project in order to reference the platform-specific implementation.");


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Assembly GetAssembly(Type type) => Instance.GetAssembly(type);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAssemblies() => Instance.GetAssemblies();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyData"></param>
        /// <returns></returns>
        public static Assembly LoadAssembly(byte[] assemblyData) => Instance.LoadAssembly(assemblyData);
    }
}
