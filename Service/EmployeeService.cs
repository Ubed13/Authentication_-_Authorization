using Role_Base_Authentication_JWT.Context;
using Role_Base_Authentication_JWT.Interface;
using Role_Base_Authentication_JWT.Models;

namespace Role_Base_Authentication_JWT.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly JwtContext _Context;
        public EmployeeService(JwtContext jwtContext) 
        {
            _Context = jwtContext;
        
        }

        public Employee AddEmployee(Employee employee)
        {
           var data = _Context.Employees.Add(employee);
            _Context.SaveChanges();
            return data.Entity;
        }

        public List<Employee> GetEmployeeDetails()
        {
            var data = _Context.Employees.ToList();
            return data;
        }
    }
}
