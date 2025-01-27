using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.DAL.Models
{
	public class Position :  BaseEntity
	{
        public string Title { get; set; }
		public string Description { get; set; }
		public string SalaryRange { get; set; }
    }
}
