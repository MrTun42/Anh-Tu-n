using Xunit;
using Student_Manager.Services;
using Student_Manager.DataContext;
using Student_Manager.Models;
using System.Collections.Generic;

namespace Student_Manager.Tests
{
    public class AdminServiceTests
    {
        [Fact]
        public void GetStudents_Returns_AllTeachers()
        {
            // Arrange
            var teachers = new List<Admin>
            {
                new Admin { Id = 1, Name = "Teacher 1", SBD = "12345", Gender = "Male", DateOfBirth = new System.DateTime(1990, 1, 1) },
                new Admin { Id = 2, Name = "Teacher 2", SBD = "54321", Gender = "Female", DateOfBirth = new System.DateTime(1995, 5, 5) }
            };
            var context = new AdminContextMock(teachers);
            var service = new AdminService(context);

            // Act
            var result = service.GetStudents();

            // Assert
            Assert.Equal(teachers, result);
        }

        [Fact]
        public void AddTeacher_Adds_NewTeacher()
        {
            // Arrange
            var teachers = new List<Admin>
            {
                new Admin { Id = 1, Name = "Teacher 1", SBD = "12345", Gender = "Male", DateOfBirth = new System.DateTime(1990, 1, 1) }
            };
            var context = new AdminContextMock(teachers);
            var service = new AdminService(context);
            var newTeacher = new Admin { Id = 2, Name = "Teacher 2", SBD = "54321", Gender = "Female", DateOfBirth = new System.DateTime(1995, 5, 5) };

            // Act
            service.AddTeacher(newTeacher);

            // Assert
            Assert.Contains(newTeacher, context.Teachers);
        }

        // Similar tests for UpdateTeacher and DeleteTeacher methods can be added
    }

    public class AdminContextMock : AdminContext
    {
        public AdminContextMock(List<Admin> teachers) : base("mockFilePath")
        {
            Teachers = teachers;
        }
    }
}
