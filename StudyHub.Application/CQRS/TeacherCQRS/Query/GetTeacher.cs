using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyHub.Application.ApplicationResponse;
using StudyHub.Application.DTOs.Teacher;
using StudyHub.Domain.Models;
using StudyHub.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudyHub.Application.CQRS.StudentCQRS.Query
{
	public class GetTeacher
	{
		public class Query : IRequest<Response>
		{
			public Guid Id { get; set; }
			public string StaffCode { get; set; }
		}

		public class Handler : IRequestHandler<Query, Response>
		{
			private readonly ApplicationDbContext _db;
			private readonly IMapper _mapper;

			public Handler(ApplicationDbContext db, IMapper mapper)
			{
				_db = db;
				_mapper = mapper;
			}


			public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
			{

				Response response = new Response();
				Teacher students = new Teacher();
				try
				{
					if (request.Id.Equals(null) && !String.IsNullOrWhiteSpace(request.StaffCode))
						students = await _db.Teachers.Where(x => x.StaffCode.Contains(request.StaffCode)).FirstOrDefaultAsync();
					students = await _db.Teachers.Where(x => x.Id.Equals(request.Id)).FirstOrDefaultAsync();

					var record = _mapper.Map<TeachersIndexDTO>(students);

					return ResponseData.OnSuccess(record);
				}
				catch (Exception exp)
				{
					return ResponseData.NotFoundResponse(null, exp.Message);
				}

			}
		}
	}
}
