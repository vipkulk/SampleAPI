using DOMAIN.Commands;
using DOMAIN.Interfaces;
using DOMAIN.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using SharedProject;
using System.ServiceModel;

namespace INFRASTRUCTURE
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly IOrganizationServiceAsync2 _organizationServiceAsync;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(IOrganizationServiceAsync2 organizationServiceAsync,ILogger<CustomerRepository> logger)
        {
            _organizationServiceAsync = organizationServiceAsync ?? throw new ArgumentNullException(nameof(organizationServiceAsync));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<CreateCustomerResponse?> CreateCustomerAsync(CreateCustomer customer,CancellationToken token=default)
        {
            var customerTable = new Entity(Tables.Contact)
            {
                [ContactTable.FirstName] = customer.FirstName,
                [ContactTable.LastName] = customer.LastName,
                [ContactTable.Account] = new EntityReference(Tables.Account, AccountTable.AccountNumber, customer.AccountNumber)
            };
            try
            {
                var customerRecordId = await _organizationServiceAsync.CreateAsync(customerTable, token);
                var customerRecord = await _organizationServiceAsync.RetrieveAsync(Tables.Contact, customerRecordId, new ColumnSet(ContactTable.ContactNumber),token);
                _logger.LogInformation($"Customer with {customerRecord.GetAttributeValue<string>(ContactTable.ContactNumber)} created");
                return new CreateCustomerResponse()
                {
                    CustomerNumber = customerRecord.GetAttributeValue<string>(ContactTable.ContactNumber)
                };
            }
            catch(FaultException ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }

        public Task<string?> GetCustomerAccountNameAsync(string accountNumber,CancellationToken token=default)
        {
            throw new NotImplementedException();
        }

        public async Task<GetCustomerResponse?> GetCustomerByCustomerNumberAsync(string customerNumber,CancellationToken token = default)
        {
            var contactQuery = new RetrieveRequest()
            {
                ColumnSet = new ColumnSet(ContactTable.FirstName, ContactTable.LastName, ContactTable.MobilePhone),
                Target = new EntityReference(Tables.Contact, ContactTable.ContactNumber, customerNumber)
            };
            try
            {
                var crmResponse = (RetrieveResponse)await _organizationServiceAsync.ExecuteAsync(contactQuery, token);
                return new GetCustomerResponse()
                {
                    FirstName = crmResponse.Entity.GetAttributeValue<string>(ContactTable.FirstName),
                    LastName = crmResponse.Entity.GetAttributeValue<string>(ContactTable.LastName),
                    MobilePhone = crmResponse.Entity.GetAttributeValue<string>(ContactTable.MobilePhone)
                };

            }
            catch(FaultException ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
            
        }

        public Task<GetCustomerResponse> GetCustomerByIdAsync(Guid Id,CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
