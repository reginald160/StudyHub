using System;
using System.Collections.Generic;
using System.Text;

namespace StudyHub.Domain.Models
{

    public class ErrorModel
    {
        public ErrorModel()
        {
            ErrorDate = DateTime.Now;
        }
        public Guid Id { get; set; }
        public Guid? LogId { get; set; }
        public DateTime ErrorDate { get; set; }
        public string ErrorShortDescription { get; set; }
        public string ExceptionType { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string InnerExceptionMessage { get; set; }
    }
}
