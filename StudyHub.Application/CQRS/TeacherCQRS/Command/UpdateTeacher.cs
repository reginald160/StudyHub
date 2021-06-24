using AutoMapper;
using MediatR;
using StudyHub.Application.ApplicationResponse;
using StudyHub.Application.DTOs.Common;
using StudyHub.Application.DTOs.Student;
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
	public class UpdateTeacher
	{
		public class Command : IRequest<Response>
		{
			public NumenclatureDTO Teacher { get; set; }
		}

		public class Handler : IRequestHandler<Command, Response>
		{
			private readonly IMapper _mapper;
			private readonly ApplicationDbContext _context;
			TeacherResponse response = new TeacherResponse();

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
					teacher.EditedDate = DateTimeOffset.Now;
					 _context.Teachers.Update(teacher);
					await _context.SaveChangesAsync();

					//return student data as a reponse data
					response.Name = teacher.FullName;
					return ResponseData.OnUpdateResponse(response);

				}
				catch (Exception exp)
				{
					//Returns response with the full viewmodel data when the update operations fails
					return ResponseData.OnFailureResponse(request.Teacher, exp.Message);
				}

			}
		}
	}
}
