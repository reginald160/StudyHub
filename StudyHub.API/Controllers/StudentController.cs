using AutoMapper;
using AutoMapper.Configuration;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.API.Helpers;
using StudyHub.Application.CQRS.StudentCQRS.Command;
using StudyHub.Application.CQRS.StudentCQRS.Query;
using StudyHub.Application.Cryptography;
using StudyHub.Application.DTOs.Common;
using StudyHub.Application.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudyHub.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : BaseController
	{
        public StudentController(IMediator mediator, IMapper mapper, IWebHostEnvironment hostEnvironment, ICryptographyService cryptographyServices) : base(mediator, mapper, hostEnvironment, cryptographyServices)
        {
        }


        /// <summary>
        /// Gets the list of all rergistered students
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
		[ProducesResponseType(204)]
		[ProducesResponseType(201, Type = typeof(IEnumerable<StudentIndexDTO>))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		//[Authorize]
		public async Task<IActionResult> GetAllStudents(CancellationToken token)
		{
			var response = await _mediator.Send(new GetAllStudents.Query());
			return response.Status.Equals(APIConstants.SuccessStatus) ? Ok(response) : NotFound(response);

		}

		[HttpGet("[action]")]
		[ProducesResponseType(204)]
		[ProducesResponseType(201, Type = typeof(IEnumerable<StudentIndexDTO>))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		//[Authorize]
		public async Task<IActionResult> GetStudentsById(Guid id, CancellationToken token)
		{
			var response = await _mediator.Send(new GetStudent.Query { Id = id });
			return response.Status.Equals(APIConstants.SuccessStatus) ? Ok(response) : NotFound(response);

		}


		[HttpGet("[action]")]
		[ProducesResponseType(204)]
		[ProducesResponseType(201, Type = typeof(IEnumerable<StudentIndexDTO>))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		//[Authorize]
		public async Task<IActionResult> GetStudentByRegNumber(string regNumber, CancellationToken token)
		{
			var response = await _mediator.Send(new GetStudent.Query { RegNumber = regNumber });
			return response.Status.Equals(APIConstants.SuccessStatus) ? Ok(response) : NotFound(response);

		}

		[HttpPost("[action]")]
		[ProducesResponseType(201, Type = typeof(CreateStudentsDTO))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> CreateStudent([FromBody] CreateStudentsDTO request)
		{
			if (ModelState.IsValid)
			{
				var result = await _mediator.Send(new CreateStudent.Command { Student = request });
				return result.ResponseCode.Equals(APIConstants.Ok) ? Ok(result) : BadRequest(result);
			}
			return BadRequest(ModelState);
		}


		[HttpPatch("[action]")]
		[ProducesResponseType(201, Type = typeof(UpdatStudentDTO))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> UpdateStudent([FromBody] UpdatStudentDTO request)
		{
			if (ModelState.IsValid)
			{
				var result = await _mediator.Send(new UpdateStudent.Command { Student = request });
				return result.ResponseCode.Equals(APIConstants.Ok) ? Ok(result) : NoContent();
			}
			return BadRequest(ModelState);
		}

		[HttpDelete("[action]")]
		[ProducesResponseType(204)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		//[Authorize]
		public async Task<IActionResult> DeleteCourse(Guid id)
		{
			if (ModelState.IsValid)
			{

				var result = await _mediator.Send(new DeleteStudent.Command { Id = id });
				return result.ResponseCode.Equals(APIConstants.Ok) ? Ok(result) : NoContent();
			}
			return BadRequest(ModelState);

		}
	}
}
