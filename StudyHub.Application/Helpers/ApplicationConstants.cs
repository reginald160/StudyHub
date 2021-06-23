using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.Helpers
{
	public class ApplicationConstants
	{
		public static Random AdminRandom = new Random();
		public static bool Truth = true;
		public static bool NotTrue = false;
		public static string Error500 = "Something Went Wrong";
		public static string SuccessStatus = "Success";
		public static string FailedStatus = "Failed";
	}
}
