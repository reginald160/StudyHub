using AutoMapper;
using StudyHub.Application.DTOs.CourseDTO;
using StudyHub.Application.DTOs.CourseRegistrationDTO;
using StudyHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.Mapper
{
	public class CourseMap : Profile
	{
		public CourseMap()
		{
			CreateMap<Course, CourseUtilityDTO>().ReverseMap();
			CreateMap<Course, UpdateCourseDTO>().ReverseMap();
			CreateMap<RegistrationDTO, StudentRegistration>().ReverseMap();
			CreateMap<RegistrationDTO, TeachersRegistration>().ReverseMap();
			CreateMap<CourseRegistration, RegistrationDTO>().ReverseMap();
		}
	}
}
