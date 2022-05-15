using AutoMapper;
using MediatR;
using StudyHub.Application.ApplicationResponse;
using StudyHub.Application.DTOs.CourseRegistrationDTO;
using StudyHub.Application.Helpers;
using StudyHub.Domain.Models;
using StudyHub.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static StudyHub.Domain.Enum.DomainEnum;

namespace StudyHub.Application.CQRS.CourseRegistrationCQRS.Command
{
	public class Registration
	{
		public class Command : IRequest<Response>
		{
			public RegistrationDTO Registration { get; set; }
		}

		public class Handler : IRequestHandler<Command, Response>
		{
			private readonly IMapper _mapper;
			private readonly ApplicationDbContext _context;
			RegistrationResponse response = new RegistrationResponse();

			public Handler(IMapper mapper, ApplicationDbContext context)
			{
				_mapper = mapper;
				_context = context;
			}

			public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
			{	

				try
				{   //Teachers course registration
					if (!request.Registration.TeacherId.Equals(null))
					{

						int teacherRegCourseCount = _context.CourseRegistrations.Where(x => x.TeacherId.Equals(request.Registration.TeacherId))
										.Where(x => x.CourseId.Equals(request.Registration.CourseId)).Count();

						if (teacherRegCourseCount >= 1)
							return ResponseData.GlobalResponse(request.Registration, ResponseMessage.MessageOnUniqueCourse,
								ResponseStatus.Failed.ToString(), ResponseCode.Unathorized);


						int teacherRegCount = _context.CourseRegistrations
										.Where(x => x.TeacherId.Equals(request.Registration.TeacherId)).Count();
						if (teacherRegCount >= 3)
							return ResponseData.GlobalResponse(request.Registration, ResponseMessage.MessageOnCourseOverFlow,
								ResponseStatus.Failed.ToString(), ResponseCode.Unathorized);
						request.Registration.Entity = RegistrationEntity.Teacher;

					}
					//Students course registration
					else
					{
						int studentRegCourseCount = _context.CourseRegistrations.Where(x => x.StudentId.Equals(request.Registration.StudentId))
										.Where(x => x.CourseId.Equals(request.Registration.CourseId)).Count();

						if (studentRegCourseCount >= 1)
							return ResponseData.GlobalResponse(request.Registration, ResponseMessage.MessageOnUniqueCourse,
								ResponseStatus.Failed.ToString(), ResponseCode.Unathorized);

						int studentRegCount = _context.CourseRegistrations.Where(x => x.StudentId.Equals(request.Registration.StudentId)).Count();

						if (studentRegCount >= 3)
							return ResponseData.GlobalResponse(request.Registration, ResponseMessage.MessageOnCourseOverFlow,
								ResponseStatus.Failed.ToString(), ResponseCode.Unathorized);

						request.Registration.Entity = RegistrationEntity.Student;
					}

					
					var record = _mapper.Map<CourseRegistration>(request.Registration);
					record.CreatedDate = DateTimeOffset.Now;
					await _context.CourseRegistrations.AddAsync(record);
					await _context.SaveChangesAsync();

					return ResponseData.OnSuccess(record);
				}
				catch (Exception exp)
				{
					//Returns response with the full viewmodel data when the save operations fails
					return ResponseData.OnFailureResponse(request.Registration, exp.Message);
				}

			}
		}
	}
}
