using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Student_Manager.Models;

namespace Student_Manager.DataContext
{
    public class StudentContext
    {
        private int nextStudentId = 1;

        public List<Student> Students { get; set; }
        private readonly string filePath;

        public StudentContext(string filePath)
        {
            this.filePath = filePath;
            Students = ReadDataFromCsvAndUpdateId(filePath);
        }

        public virtual void InsertStudent(Student student)
        {
            student.Id = nextStudentId++; // Assign the next available Id and increment the counter
            Students.Add(student);
            WriteDataToCsv(filePath);
        }

        public virtual void UpdateStudent(int studentId, Student updatedStudent)
        {
            Student existingStudent = Students.FirstOrDefault(p => p.Id == studentId);

            if (existingStudent != null)
            {
                existingStudent.Name = updatedStudent.Name;
                existingStudent.SBD = updatedStudent.SBD;
                existingStudent.Gender = updatedStudent.Gender;
                existingStudent.DateOfBirth = updatedStudent.DateOfBirth;

                WriteDataToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Student with Id {studentId} not found.");
            }
        }

        public virtual void DeleteStudent(int studentId)
        {
            Student studentToRemove = Students.FirstOrDefault(p => p.Id == studentId);

            if (studentToRemove != null)
            {
                Students.Remove(studentToRemove);
                WriteDataToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Student with Id {studentId} not found.");
            }
        }

        public virtual List<Student> ReadDataFromCsvAndUpdateId(string filePath)
        {
            Students = new List<Student>();
            nextStudentId = 1; // Reset the counter

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
                    }
                }
            }

            return Students;
        }

        private void WriteDataToCsv(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write header
                writer.WriteLine("Id,Name,SBD,Gender,Date Of Birth");

                // Write data rows
                foreach (var student in Students)
                {
                    writer.WriteLine($"{student.Id},{student.Name},{student.SBD},{student.Gender},{student.DateOfBirth}");
                }
            }
        }
    }
}
