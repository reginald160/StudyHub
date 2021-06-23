using StudyHub.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Domain.Models
{
	public class Teacher : NumenclatureModel
	{
		public Teacher()
		{
			Role = DomainConstants.TeacherRole;
		}
		public string StaffCode { get; set; }
	}
}
