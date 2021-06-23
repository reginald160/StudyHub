using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.API.Helpers;
using StudyHub.Application.CQRS.CourseRegistrationCQRS.Command;
using StudyHub.Application.CQRS.CourseRegistrationCQRS.Query;
using StudyHub.Application.DTOs.CourseDTO;
using StudyHub.Application.DTOs.CourseRegistrationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudyHub.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public class CourseController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public CourseController(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpGet("[action]")]
		[ProducesResponseType(204)]
		[ProducesResponseType(201, Type = typeof(CourseUtilityDTO))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		//[Authorize]
		public async Task<IActionResult> GetAllCourses(CancellationToken token)
		{
			var response = await _mediator.Send(new GetAllCourses.Query());
			return response.Status.Equals(APIConstants.SuccessStatus) ? Ok(response) : NotFound(response);

		}

		[HttpPost("[action]")]
		[ProducesResponseType(201, Type = typeof(CourseUtilityDTO))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> CreateCourse([FromBody] CourseUtilityDTO request)
		{
			if (ModelState.IsValid)
			{
				var result = await _mediator.Send(new CreateCourse.Command { Course = request });
				return result.ResponseCode.Equals(APIConstants.Ok) ? Ok(result) : Unauthorized(result);
			}
			return BadRequest(ModelState);
		}


		[HttpPatch("[action]")]
		[ProducesResponseType(201, Type = typeof(UpdateCourseDTO))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseDTO request)
		{
			if (ModelState.IsValid)
			{
				var result = await _mediator.Send(new UpdateCourse.Command { Course = request });
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

				var result = await _mediator.Send(new DeleteCourse.Command { Id = id });
				return result.ResponseCode.Equals(APIConstants.Ok) ? Ok(result) : NoContent();
			}
			return BadRequest(ModelState);

		}


		[HttpPost("[action]")]
		[ProducesResponseType(201, Type = typeof(StudentRegistration))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> StudentsCourseRegistration([FromBody] StudentRegistration request)
		{
			if (ModelState.IsValid)
			{
				var map = _mapper.Map<RegistrationDTO>(request);
				var result = await _mediator.Send(new Registration.Command { Registration = map });
				return result.ResponseCode.Equals(APIConstants.Ok) ? Ok(result) : Unauthorized(result);
			}
			return BadRequest(ModelState);
		}


		[HttpPost("[action]")]
		[ProducesResponseType(201, Type = typeof(TeachersRegistration))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> TeachersCourseRegistration([FromBody] StudentRegistration request)
		{
			if (ModelState.IsValid)
			{
				var map = _mapper.Map<RegistrationDTO>(request);
				var result = await _mediator.Send(new Registration.Command { Registration = map });
				return result.ResponseCode.Equals(APIConstants.Ok) ? Ok(result) : Unauthorized(result);
			}
			return BadRequest(ModelState);
		}




	}
}
