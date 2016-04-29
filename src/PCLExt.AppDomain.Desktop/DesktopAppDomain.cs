using System;
using System.Reflection;

namespace PCLExt.AppDomain
{
    /// <summary>
    /// 
    /// </summary>
    public class DesktopAppDomain : IAppDomain
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Assembly GetAssembly(Type type) => Assembly.GetAssembly(type);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Assembly[] GetAssemblies() => System.AppDomain.CurrentDomain.GetAssemblies();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyData"></param>
        /// <returns></returns>
        public Assembly LoadAssembly(byte[] assemblyData) => Assembly.Load(assemblyData);
    }
}
