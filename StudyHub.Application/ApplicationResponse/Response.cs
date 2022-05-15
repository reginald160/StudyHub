using Microsoft.AspNetCore.Http;
using StudyHub.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.ApplicationResponse
{
	public class Response :HttpResponseMessage
	{
		public string Requestid => LogicHelper.ResonseId();
		public int ResponseCode { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ResponseMessage { get; set; }
		public string Status { get; set; }
		public object Data { get; set; }
	}

    public class Response<T> where T : class
    {
        public T ResponseBody { get; set; }
        public StudyHub.Domain.Enum.DomainEnum.ResponseStatus Status { get; set; }
        public ErrorResponse ErrorResponse { set; get; }

        public static Response<T> ResponseFormatter(StudyHub.Domain.Enum.DomainEnum.ResponseStatus  responseStatus, T responseBody = null, ErrorResponse message = null)
        {
            return new Response<T> { Status = responseStatus, ResponseBody = responseBody, ErrorResponse = message };
        }
    }
}
