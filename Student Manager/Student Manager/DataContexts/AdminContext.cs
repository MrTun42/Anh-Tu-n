using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Student_Manager.Models;

namespace Student_Manager.DataContext
{
    public class AdminContext
    {
        private int nextStudentId = 1;
        private int nextTeacherId = 1;

        public List<Student> Students { get; set; }
        public List<Admin> Teachers { get; set; }

        private readonly string filePath;

        public AdminContext(string filePath)
        {
            this.filePath = filePath;
            Students = new List<Student>();
            Teachers = new List<Admin>();

            // Populate Students and Teachers lists from CSV file
            ReadDataFromCsvAndUpdateId(filePath);
        }

        public void InsertStudent(Student student)
        {
            student.Id = nextStudentId++; // Assign the next available Id and increment the counter
            Students.Add(student);
            WriteStudentDataToCsv(filePath);
        }

        public void UpdateStudent(int studentId, Student updatedStudent)
        {
            Student existingStudent = Students.FirstOrDefault(p => p.Id == studentId);

            if (existingStudent != null)
            {
                existingStudent.Name = updatedStudent.Name;
                existingStudent.SBD = updatedStudent.SBD;
                existingStudent.Gender = updatedStudent.Gender;
                existingStudent.DateOfBirth = updatedStudent.DateOfBirth;

                WriteStudentDataToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Student with Id {studentId} not found.");
            }
        }

        public void DeleteStudent(int studentId)
        {
            Student studentToRemove = Students.FirstOrDefault(p => p.Id == studentId);

            if (studentToRemove != null)
            {
                Students.Remove(studentToRemove);
                WriteStudentDataToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Student with Id {studentId} not found.");
            }
        }

        public void InsertTeacher(Admin teacher)
        {
            teacher.Id = nextTeacherId++; // Assign the next available Id and increment the counter
            Teachers.Add(teacher);
            WriteTeacherDataToCsv(filePath);
        }

        public void UpdateTeacher(int teacherId, Admin updatedTeacher)
        {
            Admin existingTeacher = Teachers.FirstOrDefault(p => p.Id == teacherId);

            if (existingTeacher != null)
            {
                existingTeacher.Name = updatedTeacher.Name;
                existingTeacher.SBD = updatedTeacher.SBD;
                existingTeacher.Gender = updatedTeacher.Gender;
                existingTeacher.DateOfBirth = updatedTeacher.DateOfBirth;

                WriteTeacherDataToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Teacher with Id {teacherId} not found.");
            }
        }

        public void DeleteTeacher(int teacherId)
        {
            Admin teacherToRemove = Teachers.FirstOrDefault(p => p.Id == teacherId);

            if (teacherToRemove != null)
            {
                Teachers.Remove(teacherToRemove);
                WriteTeacherDataToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Teacher with Id {teacherId} not found.");
            }
        }

        // Method to read data from a CSV file and populate Students list, updating nextStudentId
        private void ReadDataFromCsvAndUpdateId(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Skip the header line
                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');

                        if (values.Length >= 5)
                        {
                            if (values[3] == "student")
                            {
                                Student student = new Student
                                {
                                    Id = int.Parse(values[0]),
                                    Name = values[1],
                                    SBD = values[2],
                                    Gender = values[3],
                                    DateOfBirth = DateTime.Parse(values[4])
                                };

                                Students.Add(student);

                                // Update nextStudentId if needed
                                if (student.Id >= nextStudentId)
                                {
                                    nextStudentId = student.Id + 1;
                                }
                            }
                            else if (values[3] == "teacher")
                            {
                                Admin teacher = new Admin
                                {
                                    Id = int.Parse(values[0]),
                                    Name = values[1],
                                    SBD = values[2],
                                    Gender = values[3],
                                    DateOfBirth = DateTime.Parse(values[4])
                                };

                                Teachers.Add(teacher);

                                // Update nextTeacherId if needed
                                if (teacher.Id >= nextTeacherId)
                                {
                                    nextTeacherId = teacher.Id + 1;
                                }
                            }
                        }
                    }
                }
            }
        }

        // Method to write Student data to CSV file
        private void WriteStudentDataToCsv(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write header
                writer.WriteLine("Id,Name,SBD,Gender,Date Of Birth");

                // Write Student data rows
                foreach (var student in Students)
                {
                    writer.WriteLine($"{student.Id},{student.Name},{student.SBD},{student.Gender},{student.DateOfBirth}");
                }
            }
        }

        // Method to write Teacher data to CSV file
        private void WriteTeacherDataToCsv(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write header
                writer.WriteLine("Id,Name,SBD,Gender,Date Of Birth");

                // Write Teacher data rows
                foreach (var teacher in Teachers)
                {
                    writer.WriteLine($"{teacher.Id},{teacher.Name},{teacher.SBD},{teacher.Gender},{teacher.DateOfBirth}");
                }
            }
        }
    }
}
