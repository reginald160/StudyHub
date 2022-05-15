using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StudyHub.UnitTests
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void CanbeCancelledBy_UserIsAdmin_ReturnsTrue()
        {

            //Arrange
            var student = new Student();
            //Act
            var result = student.CancelledBy(1);
            //Assert
            Assert.IsTrue(result);
        }
    }


    public class Student
    {
        public bool CancelledBy(int numb)
        {
            if (numb == 1) return true;
            if (numb == 2) return true;

            return false;
        }
    }
}
