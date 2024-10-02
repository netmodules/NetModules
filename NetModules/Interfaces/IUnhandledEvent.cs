using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModules.Interfaces
{
    /// <summary>
    /// An empty interface that is inherited by UnhandledEvent. This helps with checking that a type implementing
    /// <see cref="IEvent"/> can/should be handled in the event that it is not marked as handled. <see cref="IEvent.Handled"/>
    /// </summary>
    public interface IUnhandledEvent
    {
    }
}
