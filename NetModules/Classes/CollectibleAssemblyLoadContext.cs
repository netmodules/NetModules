using System.Runtime.Loader;

namespace NetModules.Classes
{
    // TODO:
    // Implement the CollectibleAssemblyLoadContext to allow complete unloading of assemblies by disposal
    // when invoking the Unload() method on the AssemblyLoadContext via IModule.Unload() method.
    /// <summary>
    /// 
    /// </summary>
    public class CollectibleAssemblyLoadContext : AssemblyLoadContext
    {
        /// <summary>
        /// 
        /// </summary>
        public CollectibleAssemblyLoadContext(bool collectible)
            : base(isCollectible: collectible)
        { }
    }
}
