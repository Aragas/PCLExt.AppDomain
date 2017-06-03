using System;
using System.Reflection;

namespace PCLExt.AppDomain
{
    public class ResolveEventArgs : EventArgs
    {
        public string Name { get; }
        public Assembly RequestingAssembly { get; }

        public ResolveEventArgs(string name) { Name = name; }

        public ResolveEventArgs(string name, Assembly requestingAssembly) { Name = name; RequestingAssembly = requestingAssembly; }
    }
}