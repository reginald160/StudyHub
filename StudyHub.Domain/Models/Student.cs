using StudyHub.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Domain.Models
{
	public class Student : NumenclatureModel
	{
		public Student()
		{
			Role = DomainConstants.StudentRole;
		}
		public string RegNumber { get; set; }

		
		
	}
}
