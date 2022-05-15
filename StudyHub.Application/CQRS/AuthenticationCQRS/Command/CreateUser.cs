using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using StudyHub.Application.ApplicationResponse;
using StudyHub.Application.Cryptography;
using StudyHub.Application.DTOs.AccountUser;
using StudyHub.Application.Settings;
using StudyHub.Domain.Models;
using StudyHub.Infrastructure.Persistence.Data;
using StudyHub.Infrastructure.Persistence.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static StudyHub.Domain.Enum.DomainEnum;

namespace StudyHub.Application.CQRS.AccountUserCQRS.Command
{
    public class CreateUser
    {
        public class Command : IRequest<Response<string>>
        {
            public CreateUserDTO user { get; set; }
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

            public async Task<Response<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                Response<string> _reponse = null;
                string role = string.Empty;
                try
                {
                    var isExsitingUser = _userContext.Table.Any(a => a.EmailAddress.ToUpper() == request.user.Email.ToUpper());
                    if (isExsitingUser)
                        return _reponse = new Response<string>
                        {
                            Status = ResponseStatus.UserAlreadyExist,
                            ResponseBody = null,
                            ErrorResponse = new ErrorResponse
                            {
                                ResponseCode = "E01",
                                ResponseMessage = "user exist. Try again with a unique email address!!!"
                            }
                        };



                    var hashPasswordData = _cryptography.GenerateHash(request.user.Password);


                    var newUser = await _userContext.InsertEntity(new AccountUser
                    {
                        FirstName = request.user.FirstName,
                        LastName = request.user.LastName,
                        EmailAddress = request.user.Email,
                        Password = hashPasswordData.HashedValue,
                        HashPasswordSalt = hashPasswordData.Salt,
                        Role = request.user.Role,
                        CreatedDate = DateTime.UtcNow,
                    });
                    if (newUser != null)
                    {
                        return _reponse = new Response<string>
                        {
                            Status = ResponseStatus.Success,
                            ResponseBody = null,
                            ErrorResponse = new ErrorResponse
                            {
                                ResponseCode = "200",
                                ResponseMessage = "Your account has been created kindly login to your email to verify your account."
                            }
                        };
                    }
                    else
                    {
                        return _reponse = new Response<string>
                        {
                            Status = ResponseStatus.ServerError,
                            ResponseBody = null,
                            ErrorResponse = new ErrorResponse
                            {
                                ResponseCode = "500",
                                ResponseMessage = "Error Occur while trying to process your request"
                            }
                        };
                    }


                }
                catch (Exception ex)
                {
                    _reponse = new Response<string> { Status = ResponseStatus.Conflict, ResponseBody = null, ErrorResponse = new ErrorResponse { ResponseCode = "E01", ResponseMessage = ex.Message } };

                    return _reponse;
                }

            }
        }
    }
}
