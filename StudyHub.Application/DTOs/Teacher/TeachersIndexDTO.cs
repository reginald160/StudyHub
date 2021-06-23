using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.DTOs.Teacher
{
	public class TeachersIndexDTO
	{
		public string FullName { get; set; }
		public string PhoneNumber { get; set; }
		public string StaffCode { get; set; }
		public DateTimeOffset DOB { get; set; }
		public DateTimeOffset DateCreated { get; set; }
	}
}
