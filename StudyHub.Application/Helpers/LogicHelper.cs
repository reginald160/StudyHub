using StudyHub.Domain.Models;
using StudyHub.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudyHub.Application.Helpers
{
	public static class LogicHelper
	{
		public static string ResonseId()
		{
			var year = DateTime.Now.Month.ToString();
			var day = DateTime.Now.Day.ToString();
			return DateTime.Now.Year.ToString() + year + day + DateTime.Now.ToString("ddmmyyhhmmss");
		}

		public static string GetStudentRegistrationNumber()
		{
			Random random = new Random(System.DateTime.Now.Millisecond);
			int generatedRandNum = random.Next(1, 505020040);

			string generatedRandNumToString = String.Format("{0:D9}", generatedRandNum);
			const int MaxLength = 8;

			if (generatedRandNumToString.Length > MaxLength)
				generatedRandNumToString = generatedRandNumToString.Substring(1, MaxLength);

			string regNumber = "3" + generatedRandNumToString + "5";

			return regNumber;
		}
		public static string GetRegistrationId()
		{
			var year = DateTime.Now.Month.ToString();
			var day = DateTime.Now.Day.ToString();
			return DateTime.Now.Year.ToString() + year + day + DateTime.Now.ToString("ddmmyyhhmmss");
		}
		public static string GetStaffCode(ApplicationDbContext _context)
		{
			string result = String.Empty;
			string module = "Emp";

			try
			{
				int counter = 1;
				var numberSequence = _context.NumberSequences
					.Where(x => x.Module.Equals(module))
					.FirstOrDefault();

				if (numberSequence == null)
				{
					numberSequence = new NumberSequence();
					numberSequence.Module = module;
					Interlocked.Increment(ref counter);
					numberSequence.LastNumber = counter;
					numberSequence.NumberSequenceName = module;
					numberSequence.Prefix = module;

					_context.Add(numberSequence);
					_context.SaveChanges();
				}
				else
				{
					counter = numberSequence.LastNumber;

					Interlocked.Increment(ref counter);
					numberSequence.LastNumber = counter;

					_context.Update(numberSequence);
					_context.SaveChanges();
				}

				result = numberSequence.Prefix + counter.ToString().PadLeft(3, '0');
			}
			catch (Exception exp)
			{

				return exp.Message;
			}
			return result;
		}
	}

	

}
