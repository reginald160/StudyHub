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
	public class CourseConfiguration : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> builder)
		{

			builder.Property(s => s.Title).HasMaxLength(150)
				.IsRequired(true);
			builder.Property(s => s.Code).HasMaxLength(5)
			.IsRequired(true);
			builder.Property(s => s.RegistrationFee)
			.IsRequired(true);


			builder.HasData(

			new Course
			{
				Id = new Guid("94831400-F3FF-4060-B642-1E2C3879508C"),
				Title = "Introduction to Software Engineering",
				Code = "ST101",
				RegistrationFee = 2000

			},
			new Course
			{
				Id = new Guid("2303A60F-0BAC-40FA-BB2A-B27D6E43784F"),
				Title = "Softyware Quality Assurance",
				Code = "QA103",
				RegistrationFee = 4000,
			},
			new Course
			{
				Id = new Guid("1D94ED99-5150-4D10-88BE-1CB38F06ABA6"),
				Title = "Software Support",
				Code = "SP101",
				RegistrationFee = 2323
			});

		}
	}
}
