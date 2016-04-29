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
        private static IAppDomain _instance;
        private static IAppDomain Instance
        {
            get
            {
                if (_instance == null)
                {
#if COMMON
                    _instance = new DesktopAppDomain();
#endif
                }
                return _instance;
            }
            set { _instance = value; }
        }


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
