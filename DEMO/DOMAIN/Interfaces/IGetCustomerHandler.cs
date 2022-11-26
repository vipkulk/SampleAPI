using DOMAIN.Queries;
using DOMAIN.Responses;

namespace DOMAIN.Interfaces
{
    public interface IGetCustomerHandler : IRequestResponseHandler<GetCustomer,GetCustomerResponse?>
    {
    }
}
