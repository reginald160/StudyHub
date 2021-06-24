using StudyHub.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.ApplicationResponse
{
	public static class ResponseData
	{
		public static Response OnFailureResponse(object responseData, string? responseMessage)
		{
			var message = String.Empty;
			if (String.IsNullOrWhiteSpace(responseMessage))
				message = ResponseMessage.FailedOperationMessage;

			Response response = new Response
			{
				ResponseCode = ResponseCode.FailedOperation,
				ResponseMessage = responseMessage,
				Status = ResponseStatus.Failed,
				Data = responseData

			};

			return response;
		}

		public static Response OnSaveResponse(object responseData)
		{

			Response response = new Response
			{
				ResponseCode = ResponseCode.SuccesFullOperation,
				ResponseMessage = ResponseMessage.RecordOnCreationMessage,
				Status = ResponseStatus.Success,
				Data = responseData

			};

			return response;
		}
		public static Response OnDeleteResponse(object responseData)
		{

			Response response = new Response
			{
				ResponseCode = ResponseCode.SuccesFullOperation,
				ResponseMessage = ResponseMessage.RecordOnDeleteMessage,
				Status = ResponseStatus.Success,
				Data = responseData

			};

			return response;
		}
		public static Response OnUpdateResponse(object responseData)
		{

			Response response = new Response
			{
				ResponseCode = ResponseCode.SuccesFullOperation,
				ResponseMessage = ResponseMessage.RecordOnUpdateMessage,
				Status = ResponseStatus.Success,
				Data = responseData

			};

			return response;
		}

		public static Response OnSuccess(object responseData)
		{

			Response response = new Response
			{
				ResponseCode = ResponseCode.SuccesFullOperation,
				ResponseMessage = ResponseMessage.SuccesFullOperationMessage,
				Status = ResponseStatus.Success,
				Data = responseData

			};

			return response;
		}
		public static Response NotFoundResponse(object responseData, string responseMessage)
		{

			Response response = new Response
			{
				ResponseCode = ResponseCode.NotFound,
				ResponseMessage = responseMessage,
				Status = ResponseStatus.Failed,
				Data = responseData

			};

			return response;
		}

		public static Response GlobalResponse(object responseData, string responseMessage, string status, int responseCode)
		{

			Response response = new Response
			{
				ResponseCode = responseCode,
				ResponseMessage = responseMessage,
				Status = status,
				Data = responseData

			};

			return response;
		}



	}

	public static class ResponseMessage
	{
		public static string LowDebitAmountMessage = "The Amount to be withdrwal can not be greater than current balance";
		public static string MessageOnCourseOverFlow = "You can not register more than three courses";
		public static string MessageOnUniqueCourse = "You have registered this course and can not register a course twice";
		public static string SuccesFullOperationMessage = "Successful operation";
		public static string FailedOperationMessage = "Failed operation";
		public static string RecordOnCreationMessage = "Record has been created Successfuly";
		public static string RecordOnUpdateMessage = "Record has been Updated Successfuly";
		public static string RecordOnDeleteMessage = "Record has been deleted Successfuly";

	}

	public static class ResponseStatus
	{
		public static string Failed = ApplicationConstants.FailedStatus;
		public static string Success = ApplicationConstants.SuccessStatus;
	}
	public static class ResponseCode
	{
		public static int InvalidUser = 003;
		public static int Unathorized = 004;
		public static int SuccesFullOperation = 200;
		public static int FailedOperation = 012;
		public static int NotFound = 404;
		public static int BadRequest = 400;


	}

}
