using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.DTOs.CourseDTO
{
	public class CourseUtilityDTO
	{
		[Required]
		public string Title { get; set; }
		[Required]
		public string Code { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public decimal? RegistrationFee { get; set; }
		[Required]
		public int DurationInMonths { get; set; }
	}
}
