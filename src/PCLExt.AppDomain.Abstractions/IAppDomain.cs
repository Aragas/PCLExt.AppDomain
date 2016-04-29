using System;
using System.Reflection;

namespace PCLExt.AppDomain
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppDomain
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Assembly GetAssembly(Type type);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Assembly[] GetAssemblies();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyData"></param>
        /// <returns></returns>
        Assembly LoadAssembly(Byte[] assemblyData);
    }
}