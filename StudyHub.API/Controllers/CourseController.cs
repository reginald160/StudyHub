using AutoMapper;
using AutoMapper.Configuration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.API.Helpers;
using StudyHub.Application.CQRS.CourseRegistrationCQRS.Command;
using StudyHub.Application.CQRS.CourseRegistrationCQRS.Query;
using StudyHub.Application.Cryptography;
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
	//[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[EnableCors("_myAllowSpecificOrigins")]

	public class CourseController : BaseController
	{
        public CourseController(IMediator mediator, IMapper mapper, IWebHostEnvironment hostEnvironment, ICryptographyService cryptographyServices) : base(mediator, mapper, hostEnvironment, cryptographyServices)
        {
        }




        /// <summary>
        /// Returns all the list of registered courses
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
		[ProducesResponseType(204)]
		[ProducesResponseType(201, Type = typeof(CourseUtilityDTO))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		[Authorize]
		public async Task<IActionResult> GetAllCourses(CancellationToken token)
		{
			var response = await _mediator.Send(new GetAllCourses.Query());
			return response.Status.Equals(APIConstants.SuccessStatus) ? Ok(response) : NotFound(response);

		}
		/// <summary>
		/// Creates new records
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>

		[HttpPost("[action]")]
		[ProducesResponseType(201, Type = typeof(CourseUtilityDTO))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Create([FromBody] CourseUtilityDTO request)
		{
			if (ModelState.IsValid)
			{
				var result = await _mediator.Send(new CreateCourse.Command { Course = request });
				return result.ResponseCode.Equals(APIConstants.Ok) ? Ok(result) : BadRequest(result);
			}
			return BadRequest(ModelState);
		}

		/// <summary>
		/// Editing/Updating of existing record
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
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
		/// <summary>
		/// Deletion of existing records
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		//[HttpDelete("[action]")]
		//[ProducesResponseType(204)]
		//[ProducesResponseType(StatusCodes.Status404NotFound)]
		//[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		////[ProducesDefaultResponseType]
		////[Authorize]
		//public async Task<IActionResult> DeleteCourse(Guid id)
		//{
		//	if (ModelState.IsValid)
		//	{

		//		var result = await _mediator.Send(new DeleteCourse.Command { Id = id });
		//		return result.ResponseCode.Equals(APIConstants.Ok) ? Ok(result) : NoContent();
		//	}
		//	return BadRequest(ModelState);

		//}

		[HttpPost("[action]")]
		[ProducesResponseType(204)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		//[ProducesDefaultResponseType]
		//[Authorize]
		public async Task<IActionResult> DeleteCourse([FromBody] UpdateCourseDTO request)
		{
			if (ModelState.IsValid)
			{

				var result = await _mediator.Send(new DeleteCourse.Command { Id = (Guid)request.Id });
				return result.ResponseCode.Equals(APIConstants.Ok) ? Ok(result) : NoContent();
			}
			return BadRequest(ModelState);

		}



		/// <summary>
		/// CONDITIONs
		/// 1. Students register a unique course
		/// 2. Not more than three courses must be registered
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
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
				return result.ResponseCode.Equals(APIConstants.Ok) ? Ok(result) : BadRequest(result);
			}
			return BadRequest(ModelState);
		}

		/// <summary>
		/// CONDITIONs
		/// 1. Teachers register a unique course
		/// 2. Not more than three courses must be registered
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost("[action]")]
		[ProducesResponseType(201, Type = typeof(TeachersRegistration))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> TeachersCourseRegistration([FromBody] TeachersRegistration request)
		{
			if (ModelState.IsValid)
			{
				var map = _mapper.Map<RegistrationDTO>(request);
				var result = await _mediator.Send(new Registration.Command { Registration = map });
				return result.ResponseCode.Equals(APIConstants.Ok) ? Ok(result) : BadRequest(result);
			}
			return BadRequest(ModelState);
		}




	}
}
