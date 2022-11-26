using DOMAIN.Commands;
using DOMAIN.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DOMAIN.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<GetCustomerResponse?> GetCustomerByCustomerNumberAsync(string customerNumber, CancellationToken token=default);
        public Task<GetCustomerResponse> GetCustomerByIdAsync(Guid Id, CancellationToken token = default);
        public Task<CreateCustomerResponse?> CreateCustomerAsync(CreateCustomer customer, CancellationToken token = default);
        public Task<string?> GetCustomerAccountNameAsync(string accountNumber, CancellationToken token = default);
    }
}
