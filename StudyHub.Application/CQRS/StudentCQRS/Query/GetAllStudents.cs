using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using StudyHub.Application.ApplicationResponse;
using StudyHub.Application.DTOs.Student;
using StudyHub.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudyHub.Application.CQRS.StudentCQRS.Query
{
	public class GetAllStudents
	{
		public class Query : IRequest<Response>
		{

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
				try
				{

					var students = _db.Students
						.ProjectTo<StudentIndexDTO>(_mapper.ConfigurationProvider)
						.OrderBy(x => x.FullName).ToList();
						//.ToListAsync(cancellationToken);

					return ResponseData.OnSuccess(students);
				}
				catch (Exception exp)
				{
					return ResponseData.NotFoundResponse(null, exp.Message);
				}

			}
		}
	}
}
