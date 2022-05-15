using AutoMapper;
using AutoMapper.Configuration;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StudyHub.Application.Cryptography;
using StudyHub.Application.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using StudyHub.API.Models;

namespace StudyHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        public UserProvider UserProvider { get; set; }
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;
        protected readonly IWebHostEnvironment hostEnvironment;
        protected readonly ICryptographyService cryptographyServices;
        protected string AccessToken;
        protected UserClaim userClaim;

        protected readonly JwtSetting jwtSetting;


        public BaseController(IMediator mediator, IMapper mapper, IWebHostEnvironment hostEnvironment, ICryptographyService cryptographyServices)
        {   /// <summary>
            /// Controller Dependency Injection
            /// </summary>
            /// <param name="mediator"></param>
            /// <param name="mapper"></param>
            _mediator = mediator;
            _mapper = mapper;
            this.hostEnvironment = hostEnvironment;
            this.cryptographyServices = cryptographyServices;
        }
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var i = 0;
            if (User.Identity.IsAuthenticated && i !> 0)
            {
                var identityClaims = User.Identity as ClaimsIdentity;
                if (identityClaims != null)
                {
                    userClaim = cryptographyServices.GetClaimsPrincipal(identityClaims);
                    i++;
                }
            }

            if (!actionContext.ModelState.IsValid)
                BadRequest(actionContext.ModelState);

            //cryptographyServices.GetAccessToken(out var accesstoken);
            //if (!string.IsNullOrEmpty(accesstoken))
            //    BadRequest("Invalid Access Token");
            //AccessToken = accesstoken;

            base.OnActionExecuting(actionContext);

        }







    }
}
