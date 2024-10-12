using AutoMapper;
using Company.G01.DAL.Models;
using Company.G01.PL.ViewModels.Employee;

namespace Company.G01.PL.Mapping.Employees
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
