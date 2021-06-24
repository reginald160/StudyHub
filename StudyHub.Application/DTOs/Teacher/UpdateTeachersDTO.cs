using StudyHub.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.DTOs.Teacher
{
	public class UpdateTeachersDTO : NumenclatureDTO
	{
		public string StaffCode { get; set; }
	}
}
