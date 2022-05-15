using System;

namespace StudyHub.Payment.DTO
{
    public class ErrorResponseModel
    {
        public ErrorResponseModel(string errorDescription)
        {
            ErrorDescription = errorDescription;
        }

        public string ErrorDescription { get; }
    }
}