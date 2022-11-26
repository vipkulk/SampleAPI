using DOMAIN.Interfaces;
using DOMAIN.Queries;
using DOMAIN.Responses;
using Microsoft.Extensions.Logging;

namespace DOMAIN.Handlers
{
    public sealed class GetCustomerHandler : IGetCustomerHandler
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<GetCustomerHandler> _logger;
        public GetCustomerHandler(ICustomerRepository customerRepository,ILogger<GetCustomerHandler> logger)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<GetCustomerResponse?> HandleAsync(GetCustomer request, CancellationToken token = default)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            _logger.LogInformation($"Inside {nameof(GetCustomerHandler)}");
            if (string.IsNullOrEmpty(request.CustomerNumber))
            {
                throw new ArgumentNullException("CustomerNumber cant be null or empty");
            }
            return await _customerRepository.GetCustomerByCustomerNumberAsync(request.CustomerNumber,token);
        }
    }
}
