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

namespace StudyHub.Application.CQRS.StudentCQRS.Command
{
	public class CreateStudent
	{
		public class Command : IRequest<Response>
		{
			public CreateStudentsDTO Student { get; set; }
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
					var student = _mapper.Map<Student>(request.Student);
					student.RegNumber = LogicHelper.GetStudentRegistrationNumber();
					student.CreatedDate = DateTimeOffset.Now;
					await _context.Students.AddAsync(student);
					await _context.SaveChangesAsync();

					StudentResponse response = new StudentResponse
					{
						Name = student.FullName,
						RegNumber = student.RegNumber
					};
					return ResponseData.OnSaveResponse(response);

				}
				catch (Exception exp)
				{
					//Returns response with the full viewmodel data when the save operations fails
					return ResponseData.OnFailureResponse(request.Student, exp.Message);
				}

			}
		}


	}
}
