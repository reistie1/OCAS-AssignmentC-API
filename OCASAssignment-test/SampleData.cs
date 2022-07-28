using System;
using OCAS.Domain.Common;

namespace OCASAPI.Data
{
    public class SampleData
    {
        public SampleData(){}

        public List<Course> GetCourseList()
        {
            Guid SchoolId = Guid.NewGuid();

            List<Course> courseList = new List<Course>()
            {
                new Course(){Id = Guid.NewGuid(), CourseCode = "abc-124", CourseName = "Sample course 1", Description = "A fun and interesting course covering a wide range of topics", SchoolId = SchoolId},
                new Course(){Id = Guid.NewGuid(), CourseCode = "math-100", CourseName = "Math 100", Description = "Entry level math course covering trigonometry"},
            };

            return courseList;
        }
       

        
    }
}