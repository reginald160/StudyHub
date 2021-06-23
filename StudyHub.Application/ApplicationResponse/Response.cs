using StudyHub.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.ApplicationResponse
{
	public class Response
	{
		public string Requestid => LogicHelper.ResonseId();
		public int ResponseCode { get; set; }
		public string ResponseMessage { get; set; }
		public string Status { get; set; }
		public object Data { get; set; }
	}
}
