using System.Threading.Tasks;

namespace Shared.CQRS
{
    public interface IRequestHandler<TRequest, TResponse> 
        where TRequest : IRequest
        where TResponse : class
    {
        /// <summary>
        /// This method handles requests and returns expected result
        /// </summary>
        /// <param name="request">Payload of the request</param>
        /// <returns></returns>
        Task<TResponse> Handle(TRequest request);
    }
}
