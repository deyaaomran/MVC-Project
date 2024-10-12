using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.DAL.Models
{
    public class Department : BaseEntity
    {
        
        [Required(ErrorMessage ="Code is Required!")]
        public int Code { get; set; }
        
        [Required(ErrorMessage = "Name is Required!")]
        
        public string name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
