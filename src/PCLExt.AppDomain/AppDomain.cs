using System;
using System.Collections.Generic;
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

        internal static Exception NotImplementedInReferenceAssembly() => new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the PCLExt.AppDomain NuGet package from your main application project in order to reference the platform-specific implementation.");


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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="className"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static Type GetTypeFromNameAndInterface<T>(string className, Assembly assembly)
        {
            foreach (var typeInfo in new List<TypeInfo>(assembly.DefinedTypes))
                if (typeInfo.Name == className)
                    foreach (var type in new List<Type>(typeInfo.ImplementedInterfaces))
                        if (type == typeof (T))
                            return typeInfo.AsType();

            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="className"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static Type GetTypeFromNameAndAbstract<T>(string className, Assembly assembly)
        {
            foreach (var typeInfo in new List<TypeInfo>(assembly.DefinedTypes))
                if (typeInfo.Name == className)
                    if (typeInfo.IsSubclassOf(typeof (T)))
                        return typeInfo.AsType();

            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="className"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static Type GetTypeFromName(string className, Assembly assembly)
        {
            foreach (var typeInfo in new List<TypeInfo>(assembly.DefinedTypes))
                if (typeInfo.Name == className)
                    return typeInfo.AsType();

            return null;
        }
    }
}
