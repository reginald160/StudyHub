using AutoMapper;
using MediatR;
using StudyHub.Application.ApplicationResponse;
using StudyHub.Application.DTOs.CourseDTO;
using StudyHub.Application.DTOs.Student;
using StudyHub.Application.Helpers;
using StudyHub.Domain.Models;
using StudyHub.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudyHub.Application.CQRS.CourseRegistrationCQRS.Command
{
	public class CreateCourse
	{
		public class Command : IRequest<Response>
		{
			public CourseUtilityDTO Course { get; set; }
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
					var course = _mapper.Map<Course>(request.Course);
					course.CreatedDate = DateTimeOffset.Now;
					await _context.Courses.AddAsync(course);
					await _context.SaveChangesAsync();

					CourseResponse response = new CourseResponse
					{
						Title = course.Title,
						Code = course.Code
					};
					return ResponseData.OnSaveResponse(response);

				}
				catch (Exception exp)
				{
					//Returns response with the full viewmodel data when the save operations fails
					return ResponseData.OnFailureResponse(request.Course, exp.Message);
				}

			}
		}


	}
}
