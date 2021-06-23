using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyHub.Application.ApplicationResponse;
using StudyHub.Application.DTOs.Student;
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
	public class GetStudent
	{
		public class Query : IRequest<Response>
		{
			public Guid Id { get; set; }
			public string RegNumber { get; set; }
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
				Student students = new Student();
				try
				{
					if(request.Id.Equals(null) && !String.IsNullOrWhiteSpace(request.RegNumber))
						students = await _db.Students.Where(x => x.RegNumber.Contains(request.RegNumber)).FirstOrDefaultAsync();
					students = await _db.Students.Where(x => x.Id.Equals(request.Id)).FirstOrDefaultAsync();

					var record = _mapper.Map<StudentIndexDTO>(students);

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
