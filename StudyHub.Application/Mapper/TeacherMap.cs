using AutoMapper;
using StudyHub.Application.DTOs.Common;
using StudyHub.Application.DTOs.Teacher;
using StudyHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.Mapper
{
	public class TeacherMap : Profile
	{
		public TeacherMap()
		{
			CreateMap<Teacher, CreateTeacherDTO>().ReverseMap();
			CreateMap<Teacher, NumenclatureDTO>().ReverseMap();
			CreateMap<Teacher, TeachersIndexDTO>().ReverseMap();
		}
	}
}
