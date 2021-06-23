using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Domain.Models
{
	public abstract class NumenclatureModel : BaseModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { get; set; }
		public string PhoneNumber { get; set; }
		public string Role { get; set; }
		public DateTimeOffset DOB { get; set; }
	}
}
