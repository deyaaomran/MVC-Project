using Company.G01.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.ViewModels.Employee
{
    public class EmployeeViewModel
    {
        public int id { get; set; }
        // Client Side Validtion
        [Required(ErrorMessage = "Name Is Required !")]
        public string Name { get; set; }

        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
        public string Address { get; set; }

        [Range(25, 60, ErrorMessage = "Age Must Be Between 25 And 60")]
        public int? Age { get; set; }

        [DataType(DataType.EmailAddress)]
        // [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Salary Is Required !")]

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Phone(ErrorMessage = "Phone Number Must Be Like 010-0xxxxxxx")]
        public string PhoneNumber { get; set; }
        public bool IsActived { get; set; }
        public DateTime HireDate { get; set; }
        public int? WorkForId { get; set; } // FK

        public Department? WorkFor { get; set; } //Navigation Property

        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
    }
}
