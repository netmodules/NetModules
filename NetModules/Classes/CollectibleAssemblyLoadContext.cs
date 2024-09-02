using System.Runtime.Loader;

namespace NetModules.Classes
{
    public class CollectibleAssemblyLoadContext : AssemblyLoadContext
    {
        public CollectibleAssemblyLoadContext(bool collectible)
            : base(isCollectible: collectible)
        { }

        
    }
}
