using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyHub.Domain.Models;
using StudyHub.Infrastructure.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace StudyHub.Infrastructure.Persistence.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new StudentConfiguration());
			builder.ApplyConfiguration(new CourseConfiguration());
			builder.ApplyConfiguration(new StudentConfiguration());

		}




		public DbSet<Student> Students { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<NumberSequence> NumberSequences { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<CourseRegistration> CourseRegistrations { get; set; }
	}
}
