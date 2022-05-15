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
	public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
	{
		public void Configure(EntityTypeBuilder<Teacher> builder)
		{
			builder.Property(s => s.FirstName).HasMaxLength(20)
					.IsRequired(true);
			builder.Property(s => s.LastName).HasMaxLength(20)
			.IsRequired(true);
			builder.Property(s => s.StaffCode).HasMaxLength(20)
			.IsRequired(true);
		}
	}
}
