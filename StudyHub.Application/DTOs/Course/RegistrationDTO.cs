using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static StudyHub.Domain.Enum.DomainEnum;

namespace StudyHub.Application.DTOs.CourseRegistrationDTO
{
	public class RegistrationDTO
	{
		public Guid? CourseId { get; set; }
		public Guid? StudentId { get; set; }
		public Guid? TeacherId { get; set; }
		public RegistrationEntity Entity { get; set; }
		public DateTimeOffset CreatedDate { get; set; }

	}
}
