using AutoMapper;
using StudyHub.Application.DTOs.Common;
using StudyHub.Application.DTOs.Student;
using StudyHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.Mapper
{
	public class StudentMap : Profile
	{
		public StudentMap()
		{	
			CreateMap<Student, CreateStudentsDTO>().ReverseMap();
			CreateMap<Student, NumenclatureDTO>().ReverseMap();
			CreateMap<Student, StudentIndexDTO>().ReverseMap();
		}
	}
}
