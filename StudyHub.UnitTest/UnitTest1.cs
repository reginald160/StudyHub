using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StudyHub.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var result = new student().CheckStudent(3);

            Assert.IsNotNull(result);
        }

        
    }

    public class student
    {
        
        public bool CheckStudent(int num)
        {
            if (num % 2 == 0) return true;
            else
            {
                return false;
            }
           
        }
    }
}
