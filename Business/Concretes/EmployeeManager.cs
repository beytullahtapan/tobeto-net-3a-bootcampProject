using Azure;
using Business.Abstracts;
using Business.Requests.Employees;
using Business.Responses.Employees;
using DataAccess.Abstracts;
using Entities.Concretes;

namespace Business.Concretes
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<GetAllEmployeeResponse>> GetAll()
        {
            List<GetAllEmployeeResponse> employees = new List<GetAllEmployeeResponse>();
            foreach (var employee in await _employeeRepository.GetAll())
            {
                GetAllEmployeeResponse response = new GetAllEmployeeResponse {
                    UserId = employee.Id,
                    Position = employee.Position,
                };
                employees.Add(response);
            }
            return employees;
        }

        public async Task<GetByIdEmployeeResponse> GetById(int id)
        {
            Employee employee = await _employeeRepository.Get(x => x.Id == id);
            GetByIdEmployeeResponse response = new GetByIdEmployeeResponse {
                UserId = employee.Id,
                Position = employee.Position,
                UserName = employee.UserName  
            };
            return response;
        }

        public async Task<CreateEmployeeResponse> AddAsync(CreateEmployeeRequest request)
        {
            Employee employee = new Employee {
                Position = request.Position,
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalIdentity = request.NationalIdentity,
                DateOfBirth = request.DateOfBirth,
                Password = request.Password
             };
            await _employeeRepository.Add(employee);

            CreateEmployeeResponse response = new CreateEmployeeResponse { 
                UserId= employee.Id,
                Position = employee.Position,
                Response = "Employee Eklendi."
            };
            return response;
        }

        public async Task<DeleteEmployeeResponse> DeleteAsync(DeleteEmployeeRequest request)
        {
            Employee employee = new Employee { 
                Id = request.UserId
            };
            await _employeeRepository.Delete(employee);


            DeleteEmployeeResponse response = new DeleteEmployeeResponse { 
                UserId = employee.Id,
                Response = "Employee Silindi."

            };
            return response;
        }

        public async Task<UpdateEmployeeResponse> UpdateAsync(UpdateEmployeeRequest request)
        {
            Employee employee = await _employeeRepository.Get(x => x.Id == request.UserId);
            employee.Position = request.Position;
            employee.UserName = request.UserName;
            employee.Email = request.Email;
            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.NationalIdentity = request.NationalIdentity;
            employee.DateOfBirth = request.DateOfBirth;
            employee.Password = request.Password;
            await _employeeRepository.Update(employee);

            UpdateEmployeeResponse response = new UpdateEmployeeResponse {
                UserId = employee.Id,
                Position = employee.Position,
                Response = "Güncelleme Başarılı"
            };
            return response;
        }
    }
}
