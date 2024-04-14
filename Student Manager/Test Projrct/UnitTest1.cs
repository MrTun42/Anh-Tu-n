using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Student_Manager.Services;
using Student_Manager.Models;
using Student_Manager.DataContext;

namespace Student_Manager.Tests
{
    public class StudentServiceTests
    {
        public class MockStudentContext : StudentContext
        {
            public int InsertStudentCallCount { get; private set; }
            public int DeleteStudentCallCount { get; private set; }
            public int UpdateStudentCallCount { get; private set; }

            public MockStudentContext(string filePath) : base(filePath) { }

            public override void InsertStudent(Student student)
            {
                InsertStudentCallCount++;
                base.InsertStudent(student);
            }

            public override void DeleteStudent(int studentId)
            {
                DeleteStudentCallCount++;
                base.DeleteStudent(studentId);
            }

            public override void UpdateStudent(int studentId, Student updatedStudent)
            {
                UpdateStudentCallCount++;
                base.UpdateStudent(studentId, updatedStudent);
            }
        }

        [Fact]
        public void AddStudent_AddsStudent_WhenContextIsNotNull()
        {
            // Arrange
            var tempFilePath = Path.GetTempFileName();
            var context = new MockStudentContext(tempFilePath);
            var service = new StudentService(context);
            var student = new Student { Id = 1, Name = "John", SBD = "S123", Gender = "Male", DateOfBirth = DateTime.Now };

            // Act
            service.AddStudent(student);
            var students = service.GetStudents();

            // Assert
            Assert.NotNull(students);
            Assert.NotEmpty(students);
            Assert.Single(students);
            Assert.Equal(student, students[0]);
            Assert.Equal(1, context.InsertStudentCallCount);
        }

        [Fact]
        public void DeleteStudent_RemovesStudent_WhenContextIsNotNull()
        {
            // Arrange
            var tempFilePath = Path.GetTempFileName();
            var context = new MockStudentContext(tempFilePath);
            var service = new StudentService(context);
            var student = new Student { Id = 1, Name = "John", SBD = "S123", Gender = "Male", DateOfBirth = DateTime.Now };
            service.AddStudent(student);

            // Act
            service.DeleteStudent(1);
            var students = service.GetStudents();

            // Assert
            Assert.NotNull(students);
            Assert.Empty(students);
            Assert.Equal(1, context.DeleteStudentCallCount);
        }

        [Fact]
        public void UpdateStudent_UpdatesStudent_WhenContextIsNotNull()
        {
            // Arrange
            var tempFilePath = Path.GetTempFileName();
            var context = new MockStudentContext(tempFilePath);
            var service = new StudentService(context);
            var student = new Student { Id = 1, Name = "John", SBD = "S123", Gender = "Male", DateOfBirth = DateTime.Now };
            service.AddStudent(student);
            var updatedStudent = new Student { Id = 1, Name = "Johnny", SBD = "S1234", Gender = "Male", DateOfBirth = DateTime.Now };

            // Act
            service.UpdateStudent(1, updatedStudent);
            var students = service.GetStudents();

            // Assert
            Assert.NotNull(students);
            Assert.NotEmpty(students);
            Assert.Single(students);
            Assert.Equal(updatedStudent.Name, students[0].Name);
            Assert.Equal(updatedStudent.SBD, students[0].SBD);
            Assert.Equal(1, context.UpdateStudentCallCount);
        }
    }
}
