using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModules.Interfaces
{
    /// <summary>
    /// An empty interface that can be implemented by an <see cref="IEvent"/> type to tell <see cref="ModuleHost"/> that the <see cref="IEvent"/>
    /// contains sensitive <see cref="IEventInput"/> and/or <see cref="IEventOutput"/> and should not be logged by trace.
    /// </summary>
    public interface ISensitiveEvent
    {
    }
}
