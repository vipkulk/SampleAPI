using CRMAPI.Requests;
using CRMAPI.Responses;
using DOMAIN.Commands;
using DOMAIN.Interfaces;
using DOMAIN.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        [HttpGet("{customernumber}")]
        public async Task<IActionResult> GetCustomer([FromServices] IGetCustomerHandler getCustomerHandler, string customernumber, CancellationToken token)
        {
            var customerRequest = new GetCustomer()
            {
                CustomerNumber = customernumber
            };
            var customerData = await getCustomerHandler.HandleAsync(customerRequest,token);
            if(customerData == null)
            {
                return BadRequest();
            }
            var response = new CustomerDetails()
            {
                FirstName = customerData.FirstName,
                LastName = customerData.LastName,
                MobilePhone = customerData.MobilePhone
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromServices] ICreateCustomerHandler createCustomerHandler, [FromBody] CustomerRequest customerRequest, CancellationToken token)
        {
            var request = new CreateCustomer()
            {
                FirstName = customerRequest.Firstname,
                LastName = customerRequest.LastName,
                AccountNumber = customerRequest.AccountNumber
            };
            var customerData = await createCustomerHandler.HandleAsync(request,token);
            if (customerData == null)
            {
                return BadRequest("Customer Cant be created at this moment");
            }
            var response = new CustomerResponse()
            {
                CustomerNumber = customerData.CustomerNumber
            };
            return Ok(response);
        }
    }
}
