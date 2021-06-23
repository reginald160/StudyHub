using AutoMapper;
using MediatR;
using StudyHub.Application.ApplicationResponse;
using StudyHub.Application.DTOs.Student;
using StudyHub.Application.DTOs.Teacher;
using StudyHub.Application.Helpers;
using StudyHub.Domain.Models;
using StudyHub.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudyHub.Application.CQRS.TeacherCQRS.Command
{
	public class CreateTeacher
	{
		public class Command : IRequest<Response>
		{
			public CreateTeacherDTO Teacher { get; set; }
		}

		public class Handler : IRequestHandler<Command, Response>
		{
			private readonly IMapper _mapper;
			private readonly ApplicationDbContext _context;

			public Handler(IMapper mapper, ApplicationDbContext context)
			{
				_mapper = mapper;
				_context = context;
			}

			public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
			{
			
				try
				{
					var teacher = _mapper.Map<Teacher>(request.Teacher);
					teacher.StaffCode = LogicHelper.GetStaffCode(_context);
					teacher.CreatedDate = DateTimeOffset.Now;
					await _context.Teachers.AddAsync(teacher);
					await _context.SaveChangesAsync();

					TeacherResponse response = new TeacherResponse
					{
						Name = teacher.FullName,
						RegNumber = teacher.StaffCode
					};
					return ResponseData.OnSaveResponse(response);

				}
				catch (Exception exp)
				{
					//Returns response with the full viewmodel data when the save operations fails
					return ResponseData.OnFailureResponse(request.Teacher, exp.Message);
				}

			}
		}


	}
}
