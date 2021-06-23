using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Domain.Models
{
	public class BaseModel
	{
		public Guid Id { get; set; }
		public bool IsDeleted { get; set; }
		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset EditedDate { get; set; }
	}
}
