using AutoMapper;
using AutoMapper.Configuration;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.Application.ApplicationResponse;
using StudyHub.Application.CQRS.AccountUserCQRS.Command;
using StudyHub.Application.CQRS.AuthenticationCQRS.Command;
using StudyHub.Application.Cryptography;
using StudyHub.Application.DTOs.AccountUser;
using StudyHub.Application.DTOs.Authentification;
using StudyHub.Application.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator, IMapper mapper, IWebHostEnvironment hostEnvironment, ICryptographyService cryptographyServices) : base(mediator, mapper, hostEnvironment, cryptographyServices)
        {
        }

        [HttpPost("[action]")]
        [ProducesResponseType(201, Type = typeof(CreateUserDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [AdminAuth]

       
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO request)
        {
            var response = await _mediator.Send(new CreateUser.Command { user = request });

            if (response.Status == Domain.Enum.DomainEnum.ResponseStatus.Success)
                return Ok(response.ResponseBody);
            return BadRequest(response.Status);
        }


        [HttpPost("[action]")]
        [ProducesResponseType(201, Type = typeof(AuthenticationDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Login([FromBody] AuthenticationDTO request)
        {
            var response = await _mediator.Send(new Aurthenticate.Command { Auth = request });

            if (response.Status == Domain.Enum.DomainEnum.ResponseStatus.Success)
            {
                HttpContext.Session.SetString("Token", response.ResponseBody);
                return Ok(response.ResponseBody);
            }

            return BadRequest(response.Status);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Authenticate()
        {
            var response = await _mediator.Send(new Aurthenticate.Command { Auth = new AuthenticationDTO { EmailAddress ="ozougwuifwe@gamil.com", Password = "ssssss"} });
            return new ObjectResult(response.ResponseBody);
        }



    }
}
