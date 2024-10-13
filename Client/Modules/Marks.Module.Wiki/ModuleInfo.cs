using Oqtane.Models;
using Oqtane.Modules;

namespace Marks.Module.Wiki
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Wiki",
            Description = "sample wiki",
            Version = "1.0.0",
            ServerManagerType = "Marks.Module.Wiki.Manager.WikiManager, Marks.Module.Wiki.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "Marks.Module.Wiki.Shared.Oqtane",
            PackageName = "Marks.Module.Wiki" 
        };
    }
}
