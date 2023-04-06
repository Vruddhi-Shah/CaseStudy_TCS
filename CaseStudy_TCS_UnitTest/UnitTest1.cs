using CaseStudy_TCS.Controllers;
using CaseStudy_TCS.Models;

namespace CaseStudy_TCS_UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void GetEmployee()
        {
            var controller = new EmployeeController();
            var result = controller.GetEmployeeList("1",1,4,null) ;
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetEmployeeDetailById()
        {
            var employeeId = 1;
            var controller = new EmployeeController();
            var result = controller.EmployeeDetails(employeeId);
            Assert.IsNotNull(result);
        }

        private List<EmployeeDetails> GetTestEmployee()
        {
            var testEmployeeDetails = new List<EmployeeDetails>();
            testEmployeeDetails.Add(new EmployeeDetails { id = 1, Name = "Test1", Email ="Test1@gmail.com" ,Gender = "Male",Status = "Active" });
            testEmployeeDetails.Add(new EmployeeDetails { id = 2, Name = "Test2", Email = "Test2@gmail.com", Gender = "FeMale", Status = "InActive" });
            testEmployeeDetails.Add(new EmployeeDetails { id = 3, Name = "Test3", Email = "Test3@gmail.com", Gender = "Male", Status = "Active" });
            testEmployeeDetails.Add(new EmployeeDetails { id = 4, Name = "Test4", Email = "Test4@gmail.com", Gender = "FeMale", Status = "InActive" });
            testEmployeeDetails.Add(new EmployeeDetails { id = 5, Name = "Test5", Email = "Test5@gmail.com", Gender = "FeMale", Status = "InActive" });
            return testEmployeeDetails;
        }
    }
}