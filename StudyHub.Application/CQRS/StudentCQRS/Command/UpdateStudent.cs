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

namespace StudyHub.Application.CQRS.StudentCQRS.Command
{
	public class UpdateStudent
	{
		public class Command : IRequest<Response>
		{
			public NumenclatureDTO Student { get; set; }
		}

		public class Handler : IRequestHandler<Command, Response>
		{
			private readonly IMapper _mapper;
			private readonly ApplicationDbContext _context;
			StudentResponse response = new StudentResponse();

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
					student.EditedDate = DateTimeOffset.Now;
					 _context.Students.Update(student);
					await _context.SaveChangesAsync();

					//return student data as a reponse data
					response.Name = student.FullName;
					response.RegNumber = student.RegNumber;
					return ResponseData.OnUpdateResponse(response);

				}
				catch (Exception exp)
				{
					//Returns response with the full viewmodel data when the update operations fails
					return ResponseData.OnFailureResponse(request.Student, exp.Message);
				}

			}
		}
	}
}
