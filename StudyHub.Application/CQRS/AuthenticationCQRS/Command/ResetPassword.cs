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

namespace StudyHub.Application.CQRS.AuthenticationCQRS.Comm
{
    public class ResetPassword
    {
        public class Command : IRequest<Response<string>>
        {
            public ResetPasswordDTO DTO { get; set; }
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
                Response<string> response = null;

                try
                {
                    var otp = _userContext.Table.FirstOrDefault(x => x.EmailAddress.Contains(request.DTO.Email));
                    if (otp == null)
                        return response = new Response<string>
                        {
                            Status = ResponseStatus.Conflict,
                            ResponseBody = null,
                            ErrorResponse = new ErrorResponse
                            {
                                ResponseCode = "E01",
                                ResponseMessage = "InValid user. Try again!!!"
                            }
                        };

                    var expiryTime = DateTime.Now - request.DTO.TimeStamp;
                    if (expiryTime.TotalMinutes > 10)
                    {
                        return response = new Response<string>
                        {
                            Status = ResponseStatus.Conflict,
                            ResponseBody = null,
                            ErrorResponse = new ErrorResponse
                            {
                                ResponseCode = "E01",
                                ResponseMessage = "Token has expired, Try Again Later"
                            }
                        };

                    }

                    var user = _userContext.Table.FirstOrDefault(a => a.EmailAddress.ToUpper() == request.DTO.Email.ToUpper());
                    if (user == null)
                        return response = new Response<string>
                        {
                            Status = ResponseStatus.Conflict,
                            ResponseBody = null,
                            ErrorResponse = new ErrorResponse
                            {
                                ResponseCode = "E01",
                                ResponseMessage = "Something went wrong. Try again!!!"
                            }
                        };

                    var hashPasswordData = _cryptography.GenerateHash(user.Password);

                    user.Password = hashPasswordData.HashedValue;
                    user.HashPasswordSalt = hashPasswordData.Salt;
                    var passwordUpdateResponse = await _userContext.UpdateEntity(user);
                    if (passwordUpdateResponse == null)
                    {
                        return response = new Response<string>
                        {
                            Status = ResponseStatus.ServerError,
                            ResponseBody = null,
                            ErrorResponse = new ErrorResponse
                            {
                                ResponseCode = "E01",
                                ResponseMessage = "Something went wrong. Try again!!!"
                            }
                        };
                    }


                    return new Response<string> { Status = ResponseStatus.Success, ResponseBody = "A mail have been sent to the email provided. Remember to check your Spam/Junk folder." };

                }
                catch (Exception ex)
                {
                    response = new Response<string> { Status = ResponseStatus.Conflict, ResponseBody = null, ErrorResponse = new ErrorResponse { ResponseCode = "E01", ResponseMessage = "Your account has been created kindly login with your details. Failure to login with your details please contact our support at support@pivotglobaltradings.com" } };

                    return response;
                }

            }
        }

    }

    
}
