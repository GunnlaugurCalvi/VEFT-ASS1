using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssApp.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using static System.Diagnostics.Debug;


namespace AssApp.Controllers
{
    [Route("api/courses")]
    public class CoursesController : Controller
    {
      
        private static List<Course> _courses = new List<Course>
        {
            new Course()
            {
                Id = 1, Name = "Veftjonustur", T_Id = "T-514-VEFT" , StartDate = "18-05-2005", EndDate = "19-05-1005",
                Students =  new List<Student> {
                    new Student()
                    {
                        Ssn = 170795,
                        Name = "Gulli"
                    },
                    new Student()
                    {
                        Ssn = 12345,
                        Name = "prumpi"
                    }
                }
                
            },
            new Course()
            {
                Id = 2, Name = "Forritun", T_Id = "T-101-FORR" , StartDate = "18-06-2005", EndDate = "19-06-1005",
                Students = new List<Student>
                {
                    new Student()
                    {
                        Ssn = 160187,
                        Name = "Arnor"
                    }
                }
            }
        };


        /*api/courses*/
        [HttpGet]
        public IActionResult GetCourses()
        {
            return Ok(_courses);
        }

        [HttpGet("{courseID:int}", Name = "GetCourseById")]
        public IActionResult GetCourseById(int courseId)
        {
            var course = _courses.SingleOrDefault(x => x.Id == courseId);

            if (course == null)
            {
                return NotFound("Course Id " + courseId + " does not exist");
            }

            return Ok(course);
        }

        [HttpPost]
        public IActionResult AddCourse([FromBody] Course course)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(412);
            }
            
            _courses.Add(course);

            // Returning 201
            return CreatedAtRoute("GetCourseById", new {courseId = course.Id}, course);
        }

        [HttpDelete("{courseId:int}")]
        
        public IActionResult DeleteCourse(int courseId)
        {
            var course = _courses.SingleOrDefault(x => x.Id == courseId);

            if (course == null)
            {
                return NotFound("Course Id " + courseId + " does not exist");
            }

            _courses.Remove(course);

            return new NoContentResult();
        }

       [HttpPut("{courseId:int}")]
        public IActionResult UpdateCourse([FromBody] Course updatedCourse, int courseId)
        {
            // Checking if input body is empty or ID mismatch between body and route 
            if (updatedCourse == null || updatedCourse.Id != courseId)
            {
                return BadRequest();
            }
            
            var course = _courses.SingleOrDefault(x => x.Id == courseId);

            if (course == null)
            {
                return NotFound("Course Id " + courseId + " does not exist");
            }

            // Virtual update
            // Deleting and creating new with updated data.
            _courses.Remove(course);
            _courses.Add(updatedCourse);

            return StatusCode(204);
        }

        [HttpGet("{courseId:int}/students", Name = "GetStudentsInCourse")]
        public IActionResult GetStudentsInCourse(int courseId)
        {
            var course = _courses.SingleOrDefault(x => x.Id == courseId);
        
            if (course == null)
            {
                return NotFound("Course Id " + courseId + " does not exist");
            }

            if (course.Students == null)
            {
                return NotFound("No students found in course");
            }

            return Ok(course.Students);
        }

        [HttpPost("{courseId:int}/students")]

        public IActionResult AddStudentToCourse([FromBody] Student newStudent , int courseId)
        {
            var course = _courses.SingleOrDefault(x => x.Id == courseId);

            if (course == null || newStudent == null)
            {
                return StatusCode(400);
            }
 
            course.Students.Add(newStudent);
            return Created("GetStudentsInCourse", newStudent);

        }

    }
}
