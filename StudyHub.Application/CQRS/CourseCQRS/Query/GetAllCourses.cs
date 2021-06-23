using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using StudyHub.Application.ApplicationResponse;
using StudyHub.Application.DTOs.CourseDTO;
using StudyHub.Application.DTOs.Student;
using StudyHub.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudyHub.Application.CQRS.CourseRegistrationCQRS.Query
{
	public class GetAllCourses
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

					var courses =  _db.Courses
						.ProjectTo<CourseUtilityDTO>(_mapper.ConfigurationProvider)
						.OrderBy(x => x.Title).ToList();
						//.ToList(cancellationToken);

					return ResponseData.OnSuccess(courses);
				}
				catch (Exception exp)
				{
					return ResponseData.NotFoundResponse(null, exp.Message);
				}

			}
		}
	}
}
