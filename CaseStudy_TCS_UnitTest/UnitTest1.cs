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

        [Test]
        public void DeleteEmployee()
        {
            var employeeId = 1;
            var controller = new EmployeeController();
            var result = controller.DeleteEmployee(employeeId);
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateEmployee()
        {
            EmployeeDetails employeeDetails = new EmployeeDetails();
            employeeDetails.Name = "pavana";
            employeeDetails.Email = "pavana@yopmail.com";
            employeeDetails.Gender = "Female";
            employeeDetails.Status = "Active";
            var controller = new EmployeeController();
            var result = controller.CreateEmployee(employeeDetails);
            Assert.IsNotNull(result);
        }

        [Test]
        public void EditEmployee()
        {
            EmployeeDetails employeeDetails = new EmployeeDetails();
            employeeDetails.Name = "Sahana shaikh";
            employeeDetails.Email = "sahanashaikh@yopmail.com";
            employeeDetails.Gender = "Female";
            employeeDetails.Status = "InActive";
            employeeDetails.id = 2056;
            var controller = new EmployeeController();
            var result = controller.EditEmployee(employeeDetails);
            Assert.IsNotNull(result);
        }
    }
}