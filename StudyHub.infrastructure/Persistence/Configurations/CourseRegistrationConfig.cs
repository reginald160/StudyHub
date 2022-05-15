using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Infrastructure.Persistence.Configurations
{
	public class CourseRegistrationConfig : IEntityTypeConfiguration<CourseRegistration>
	{
		public void Configure(EntityTypeBuilder<CourseRegistration> builder)
		{
			builder.Property(s => s.Registration)
					.IsRequired(true);
		}
	}
}
