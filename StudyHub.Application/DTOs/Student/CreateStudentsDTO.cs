using StudyHub.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyHub.Application.DTOs.Student
{
	public class CreateStudentsDTO : NumenclatureDTO
	{
		[JsonIgnore]
		public string RegNumber { get; set; }
	}
}
