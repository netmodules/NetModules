using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModules.Interfaces
{
    /// <summary>
    /// An instance that implements <see cref="IEventTryHandler{T}"/> can handle objects that implement <see cref="IEvent"/> and return the same instance
    /// back to the caller, while returning if the <see cref="IEvent"/> was successfully marked as handled. <see cref="IEvent.Handled"/>.
    /// </summary>
    public interface IEventTryHandler<T> where T : IModuleHost
    {
        /// <summary>
        /// This method works the same as <see cref="IEventHandler.Handle(IEvent)"/>, and is used to handle an <see cref="IEvent"/> and return the same
        /// <see cref="IEvent"/> back to the caller. This is useful when instantiating an <see cref="IEvent"/> for handling within its own
        /// "if" closure and provides access to the instantiated <see cref="IEvent"/> through the outEvent parameter.
        /// </summary>
        /// <param name="inEvent">The instance of <see cref="IEvent"/> to be handled.</param>
        /// <param name="outEvent">The returned instance of <see cref="IEvent"/> to be handled.</param>
        /// <returns>True if the event was marked as handles. See <see cref="IEvent.Handled"/>.</returns>
        bool TryHandle(IEvent inEvent, out IEvent outEvent);
    }
}
