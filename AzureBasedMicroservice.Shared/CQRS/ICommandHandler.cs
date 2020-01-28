using System.Threading.Tasks;

namespace Shared.CQRS
{
    public interface ICommandHandler<TRequest> where TRequest : IRequest
    {
        /// <summary>
        /// This method handles requests
        /// </summary>
        /// <param name="request">Payload of the request</param>
        /// <returns></returns>
        Task Handle(TRequest request);
    }
}
