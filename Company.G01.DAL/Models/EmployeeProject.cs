using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.DAL.Models
{
	public class EmployeeProject : BaseEntity
	{
        public string Role { get; set; }
        public int HoursWorked { get; set; }
		/*-----------------Relations-------------------*/
		public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public int? ProjectId { get; set; }
        public Project? Project { get; set; }



    }
}
