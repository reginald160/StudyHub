using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.API.Helpers;
using StudyHub.Application.CQRS.StudentCQRS.Query;
using StudyHub.Application.CQRS.TeacherCQRS.Command;
using StudyHub.Application.CQRS.TeacherCQRS.Query;
using StudyHub.Application.DTOs.Common;
using StudyHub.Application.DTOs.Teacher;
using StudyHub.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudyHub.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TeacherController : ControllerBase
	{
		private readonly IMediator _mediator;

		public TeacherController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("[action]")]
		[ProducesResponseType(204)]
		[ProducesResponseType(201, Type = typeof(IEnumerable<TeachersIndexDTO>))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		//[Authorize]
		public async Task<IActionResult> GetAllTeachers(CancellationToken token)
		{
			var response = await _mediator.Send(new GetAllTeachers.Query());
			return response.Status.Equals(APIConstants.SuccessStatus) ? Ok(response) : NotFound(response);

		}

		[HttpGet("[action]")]
		[ProducesResponseType(204)]
		[ProducesResponseType(201, Type = typeof(IEnumerable<TeachersIndexDTO>))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		//[Authorize]
		public async Task<IActionResult> GetTeachersById(Guid id, CancellationToken token)
		{	
			if(LogicHelper.IsValidGuid(id.ToString()).Equals(true))
			{
				var response = await _mediator.Send(new GetTeacher.Query { Id = id });
				return response.Status.Equals(APIConstants.SuccessStatus) ? Ok(response) : NotFound(response);
			}
			return BadRequest(id);

		}


		[HttpGet("[action]")]
		[ProducesResponseType(204)]
		[ProducesResponseType(201, Type = typeof(IEnumerable<TeachersIndexDTO>))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		//[Authorize]
		public async Task<IActionResult> GetTeachersStaffCode(string staffCode, CancellationToken token)
		{
			var response = await _mediator.Send(new GetTeacher.Query { StaffCode = staffCode});
			return response.Status.Equals(APIConstants.SuccessStatus) ? Ok(response) : NotFound(response);

		}

		[HttpPost("[action]")]
		[ProducesResponseType(201, Type = typeof(CreateTeacherDTO))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> CreateTeacher([FromBody] CreateTeacherDTO request)
		{
			if (ModelState.IsValid)
			{
				var result = await _mediator.Send(new CreateTeacher.Command { Teacher = request });
				return result.ResponseCode.Equals(APIConstants.Ok) ? Ok(result) : BadRequest(result);
			}
			return BadRequest(ModelState);
		}


		[HttpPatch("[action]")]
		[ProducesResponseType(201, Type = typeof(UpdateTeachersDTO))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> UpdateTeacher([FromBody] UpdateTeachersDTO request)
		{
			if (ModelState.IsValid)
			{
				var result = await _mediator.Send(new UpdateTeacher.Command { Teacher = request });
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
		public async Task<IActionResult> DeleteTeacher(Guid id)
		{
			if (ModelState.IsValid)
			{

				var result = await _mediator.Send(new DeleteTeacher.Command { Id = id });
				return result.ResponseCode.Equals(APIConstants.Ok) ? Ok(result) : NoContent();
			}
			return BadRequest(ModelState);

		}
	}
}
