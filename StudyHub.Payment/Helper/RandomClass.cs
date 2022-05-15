using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StudyHub.Payment.Helper
{
    public static class RandomClass
    {
        private static int _seed = Environment.TickCount;

        private static readonly ThreadLocal<Random> RandomWrapper = new ThreadLocal<Random>(() =>
            new Random(Interlocked.Increment(ref _seed))
        );

        // Methods

        private static Random GetThreadRandom()
        {
            return RandomWrapper.Value;
        }

        public static int GetRandomNumber(int maxValue)
        {
            return RandomWrapper.Value.Next(0, maxValue);
        }

        public static int GetRandomNumber(int minValue, int maxValue)
        {
            return RandomWrapper.Value.Next(minValue, maxValue);
        }

        public static double GetRandomFloatingPointNumber()
        {
            return RandomWrapper.Value.NextDouble();
        }

        public static string GenerateRandomAlphanumericString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                                        .Select(s => s[RandomWrapper.Value.Next(s.Length)]).ToArray());
        }

        public static string GenerateLicenceKey(string merchantName)
        {   
            
            const string chars = "ABCDEFGHIJKLMNOP@#$&*%QRSTUVWXYZ0123456789";
            return  merchantName + new string(Enumerable.Repeat(chars, 10)
                                        .Select(s => s[RandomWrapper.Value.Next(s.Length)]).ToArray());
        }

    }
}
