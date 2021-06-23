using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static StudyHub.Domain.Enum.DomainEnum;

namespace StudyHub.Domain.Models
{
	public class CourseRegistration : BaseModel
	{
		public DateTimeOffset Registration { get; set; }
		public string RegistrationId { get; set; }
		public Guid ?CourseId { get; set; }
		[JsonIgnore]
		[ForeignKey("CourseId")]
		public Course Course { get; set; }
		public Guid? TeacherId { get; set; }
		[JsonIgnore]
		[ForeignKey("TeacherId")]
		public Teacher Teacher { get; set; }
		public Guid? StudentId { get; set; }
		[JsonIgnore]
		[ForeignKey("StudentId")]
		public Student Student { get; set; }
		public RegistrationEntity Entity { get; set; }
	}
}
