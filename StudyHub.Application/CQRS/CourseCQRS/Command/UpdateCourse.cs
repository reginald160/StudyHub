using AutoMapper;
using MediatR;
using StudyHub.Application.ApplicationResponse;
using StudyHub.Application.DTOs.Common;
using StudyHub.Application.DTOs.CourseDTO;
using StudyHub.Application.DTOs.Student;
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
	public class UpdateCourse
	{
		public class Command : IRequest<Response>
		{
			public UpdateCourseDTO Course { get; set; }
		}

		public class Handler : IRequestHandler<Command, Response>
		{
			private readonly IMapper _mapper;
			private readonly ApplicationDbContext _context;
			CourseResponse response = new CourseResponse();

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
					course.EditedDate = DateTimeOffset.Now;
					 _context.Courses.Update(course);
					await _context.SaveChangesAsync();

					//return student data as a reponse data
					response.Code = course.Code;
					response.Title = course.Title;
					return ResponseData.OnUpdateResponse(response);

				}
				catch (Exception exp)
				{
					//Returns response with the full viewmodel data when the update operations fails
					return ResponseData.OnFailureResponse(request.Course, exp.Message);
				}

			}
		}
	}
}
