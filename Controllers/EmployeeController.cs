using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Role_Base_Authentication_JWT.Context;
using Role_Base_Authentication_JWT.Interface;
using Role_Base_Authentication_JWT.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Role_Base_Authentication_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _Context;
        public EmployeeController(IEmployeeService context)
        {
            _Context = context;
        }
        [HttpPost]
        public Employee AddEmployee([FromBody]Employee employee)
        {
            var emp = _Context.AddEmployee(employee);
            return emp;
        }

        
        [HttpGet("{id}")]
        public List<Employee> GetAllEmployee()
        {
            var emp = _Context.GetEmployeeDetails();
            return emp;
        }

       
        
    }
}
