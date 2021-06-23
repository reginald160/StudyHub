using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyHub.Application.DTOs.Common
{
	public class NumenclatureDTO
	{
		[JsonIgnore]
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		[JsonIgnore]
		public string FullName => FirstName + " " + LastName;
		public string PhoneNumber { get; set; }
		public DateTimeOffset DOB { get; set; }
	}
}
