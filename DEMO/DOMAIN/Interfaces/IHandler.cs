using System.Threading;
using System.Threading.Tasks;

namespace DOMAIN.Interfaces
{
    public interface IHandler
    {
        public void Handle();
    }
    public interface IRequestHandler<in TRequest>
    {
        public void Handle(TRequest request);
    }
    public interface IResponseHandler<out TResponse>
    {
        public TResponse Handle();
    }
    public interface IRequestResponseHandler<in TRequest,TResponse>
    {
        public Task<TResponse> HandleAsync(TRequest request,CancellationToken token = default);
    }
    
}
