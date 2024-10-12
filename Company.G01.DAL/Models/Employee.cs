using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.DAL.Models
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int? Age { get; set; }
        public string Email { get; set; }
        public string? ImageName { get; set; }
        public decimal Salary { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public DateTime HireDate { get; set; }
        public int? WorkForId { get; set; } // FK
        public Department? WorkFor { get; set; } //Navigation Property

    }
}
