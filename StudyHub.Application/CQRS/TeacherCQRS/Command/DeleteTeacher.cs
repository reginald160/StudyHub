using AutoMapper;
using MediatR;
using StudyHub.Application.ApplicationResponse;
using StudyHub.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudyHub.Application.CQRS.TeacherCQRS.Command
{
	public class DeleteTeacher
	{
		public class Command : IRequest<Response>
		{
			public Guid Id { get; set; }
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

					var teacher = _context.Teachers.Where(x => x.Id.Equals(request.Id)).FirstOrDefault();
					_context.Remove(teacher);
					await _context.SaveChangesAsync();

					//return student data as a reponse data
					response.Name = teacher.FullName;
					response.RegNumber = teacher.StaffCode;
					return ResponseData.OnUpdateResponse(response);

				}
				catch (Exception exp)
				{
					//Returns response with the full viewmodel data when the update operations fails
					return ResponseData.OnFailureResponse(request.Id, exp.Message);
				}

			}
		}
	}
}
