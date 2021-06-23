using StudyHub.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyHub.Application.DTOs.Teacher
{
	public class CreateTeacherDTO
	{

		public string FirstName { get; set; }
		public string LastName { get; set; }
		[JsonIgnore]
		public string FullName => FirstName + " " + LastName;
		public string PhoneNumber { get; set; }
		public DateTimeOffset DOB { get; set; }

	}
}
