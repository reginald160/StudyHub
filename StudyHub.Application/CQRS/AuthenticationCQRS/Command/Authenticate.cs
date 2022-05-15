using MediatR;
using Microsoft.Extensions.Options;
using StudyHub.Application.ApplicationResponse;
using StudyHub.Application.Cryptography;
using StudyHub.Application.DTOs.Authentification;
using StudyHub.Application.Settings;
using StudyHub.Domain.Models;
using StudyHub.Infrastructure.Persistence.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static StudyHub.Domain.Enum.DomainEnum;

namespace StudyHub.Application.CQRS.AuthenticationCQRS.Command
{
    public class Aurthenticate
    {
        public class Command : IRequest<Response<string>>
        {
            public AuthenticationDTO Auth { get; set; }
        }

        public class Handler : IRequestHandler<Command, Response<string>>
        {

            private readonly IRepository<AccountUser> _userContext;
            private readonly ICryptographyService _cryptography;
            private readonly JwtSetting _jwtSetting;

            public Handler(IRepository<AccountUser> userContext, ICryptographyService cryptography, IOptions<JwtSetting> jwtSetting)
            {
                _userContext = userContext;
                _cryptography = cryptography;
                _jwtSetting = jwtSetting.Value;
            }

            //public async Task<Response<string>> Handle(Command request, CancellationToken cancellationToken)
            //{
            //    Response<string> _reponse = null;
            //    string role = string.Empty;
            //    try
            //    {
            //        var userDetails = _userContext.Table.FirstOrDefault(a => a.EmailAddress.Equals(request.Auth.EmailAddress));
            //        if (userDetails == null)
            //            return _reponse = new Response<string> { Status = ResponseStatus.NotFound, ResponseBody = null, ErrorResponse = new ErrorResponse { ResponseCode = "E01", ResponseMessage = "User Does not exist" } };

            //        bool isPassword = _cryptography.ValidateHash(request.Auth.Password, userDetails.HashPasswordSalt, userDetails.Password);
            //        if (!isPassword)
            //            return _reponse = new Response<string> { Status = ResponseStatus.NotFound, ResponseBody = null, ErrorResponse = new ErrorResponse { ResponseCode = "E01", ResponseMessage = "Email or password in correct" } };

            //        var token = _cryptography.BuildToken(_jwtSetting.Key, _jwtSetting.Issuer, userDetails);
            //        if (token != null)
            //            return _reponse = new Response<string> { Status = ResponseStatus.Success, ResponseBody = token, ErrorResponse = new ErrorResponse { ResponseCode = "00", ResponseMessage = "Successful Login" } };
            //        else
            //            return _reponse = new Response<string> { Status = ResponseStatus.ServerError, ResponseBody = null, ErrorResponse = new ErrorResponse { ResponseCode = "E01", ResponseMessage = "oops something happen, try again later " } };

            //    }
            //    catch (Exception ex)
            //    {
            //        _reponse = new Response<string> { Status = ResponseStatus.ServerError, ResponseBody = null, ErrorResponse = new ErrorResponse { ResponseCode = "E01", ResponseMessage = "oops something happen, try again later " } };

            //        return _reponse;
            //    }

            //}

            public async Task<Response<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userDetails = new AccountUser()
                {
                    EmailAddress = "ozougwu160@gmail.com",
                    UserId = Guid.NewGuid(),
                    Role = "SuperAdmim",
                    FirstName = "Ifeanyi",
                    LastName = "Ozougwu"
                };
                var token = _cryptography.BuildToken(_jwtSetting.Key, _jwtSetting.Issuer, userDetails);
                return  new Response<string> { Status = ResponseStatus.Success, ResponseBody = token, ErrorResponse = new ErrorResponse { ResponseCode = "E01", ResponseMessage = "Email or password in correct" } };

            

            }





        }
    }
}
