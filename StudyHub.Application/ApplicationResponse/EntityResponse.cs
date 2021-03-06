using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.ApplicationResponse
{
	/// <summary>
	/// The Response classes are used to pass response data to the controllers
	/// </summary>
	public class StudentResponse
	{
		public string Name { get; set; }
		public string RegNumber { get; set; }

	}

	public class CourseResponse
	{
		public string Title { get; set; }
		public string Code { get; set; }

	}

	public class TeacherResponse
	{
		public string Name { get; set; }
		public string RegNumber { get; set; }

	}
	public class RegistrationResponse
	{
		public string RegistrationId { get; set; }

	}
}
