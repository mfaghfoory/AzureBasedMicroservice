using System.Threading.Tasks;

namespace Shared.CQRS
{
    public interface IEventHandler<TEvent>
        where TEvent : IEvent
    {
        /// <summary>
        /// This method handles events
        /// </summary>
        /// <param name="payload">Payload of the event</param>
        /// <returns></returns>
        Task Handle(TEvent payload);
    }
}
