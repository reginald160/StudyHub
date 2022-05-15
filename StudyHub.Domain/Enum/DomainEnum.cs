using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Domain.Enum
{
	public class DomainEnum
	{
		public enum RegistrationEntity
		{
			Teacher, Student
		}

        public enum OrderStatus
        {
            Failed,
            Completed,
            Initiated
        }

        public enum ResponseStatus
        {
            Success = 1,
            ServerError,
            NotFound,
            Conflict,
            Unauthorized,
            AwaitingVerification,
            UnSupportedFileFormat,
            PlanAlreadyExist,
            UserAlreadyExist,
            Failed
        }
    }
}
