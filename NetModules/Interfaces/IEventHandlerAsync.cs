using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetModules.Interfaces
{
    /// <summary>
    /// An instance that implements <see cref="IEventHandlerAsync{T}"/> can handle objects that implement <see cref="IEvent"/> asynchronously.
    /// </summary>
    public interface IEventHandlerAsync<T> where T : IModuleHost
    {
        /// <summary>
        /// <see cref="CanHandleAsync(IEvent)"/> works the same as <see cref="IEventHandler.CanHandle(IEvent)"/> and is designed to be implemented
        /// asynchronously, and can be invoked by the <see cref="IEventHandler"/> to see if this handler is able to handle the requested
        /// <see cref="IEvent"/>. This allows the handler to inspect the IEvent and inform the requester if it can be handled without further processing.
        /// </summary>
        /// <param name="e">The <see cref="IEvent"/> to inspect for handling.</param>
        /// <returns>True if the <see cref="IEventHandler"/> can handle this <see cref="IEvent"/>.</returns>
        Task<bool> CanHandleAsync(IEvent e);


        /// <summary>
        /// <see cref="HandleAsync(IEvent, CancellationToken)"/> works the same as <see cref="IEventHandler.Handle(IEvent)"/> and is designed to be implemented
        /// asynchronously. Pass an <see cref="IEvent"/> to this <see cref="IEventHandler"/> for it to be processed. All EventHandlers that wish to handle an
        /// instance of <see cref="IEvent"/> should handle the <see cref="IEvent"/> within this method. If the <see cref="IEvent"/> implements
        /// <see cref="ICancellable"/>, your single <see cref="CancellationToken"/> should be use across both the <see cref="ICancellable"/> <see cref="IEvent"/>
        /// and this method.
        /// </summary>
        /// <param name="e">The <see cref="IEvent"/> to be handled.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> should be used to cancel the event handling if required.</param>
        /// <returns></returns>
        Task<IEvent> HandleAsync(IEvent e, CancellationToken cancellationToken);
    }
}
