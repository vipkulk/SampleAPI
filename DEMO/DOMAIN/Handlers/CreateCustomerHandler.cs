using DOMAIN.Commands;
using DOMAIN.Interfaces;
using DOMAIN.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DOMAIN.Handlers
{
    public sealed class CreateCustomerHandler : ICreateCustomerHandler
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CreateCustomerHandler> _logger;

        public CreateCustomerHandler(ICustomerRepository customerRepository,ILogger<CreateCustomerHandler> logger)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CreateCustomerResponse?> HandleAsync(CreateCustomer request,CancellationToken token= default)
        {
            _logger.LogInformation("Customer Creation started");
            //Logic to be applied before createing customer.
            return await _customerRepository.CreateCustomerAsync(request,token);
        }
    }
}
