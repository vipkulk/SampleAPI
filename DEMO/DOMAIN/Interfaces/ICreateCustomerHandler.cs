using DOMAIN.Commands;
using DOMAIN.Responses;

namespace DOMAIN.Interfaces
{
    public interface ICreateCustomerHandler:IRequestResponseHandler<CreateCustomer,CreateCustomerResponse?>
    {
    }
}
