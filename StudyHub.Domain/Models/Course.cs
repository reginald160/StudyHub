using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Domain.Models
{
	public class Course : BaseModel
	{
		public string Title { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
		public decimal? RegistrationFee { get; set; }
		public int DurationInMonths { get; set; }
	}
}
