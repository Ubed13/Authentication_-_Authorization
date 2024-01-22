using Role_Base_Authentication_JWT.Models;

namespace Role_Base_Authentication_JWT.Interface
{
    public interface IEmployeeService
    {
        public List<Employee> GetEmployeeDetails();
        public Employee AddEmployee(Employee employee);
    }
}
