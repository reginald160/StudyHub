using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StudyHub.Domain.Enum.DomainEnum;

namespace StudyHub.Application.DTOs.CourseDTO
{
	public class TeachersRegistration
	{
		public Guid? CourseId { get; set; }
		public Guid? TeacherId { get; set; }
	}
}
