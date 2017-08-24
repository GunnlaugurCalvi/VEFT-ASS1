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
                return NotFound();
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

            return CreatedAtRoute("GetCourseById", new {courseId = course.Id}, course);
        }

        [HttpDelete("{courseId:int}")]
        
        public IActionResult DeleteCourse(int courseId)
        {
            var course = _courses.SingleOrDefault(x => x.Id == courseId);
            if (course == null)
            {
                return NotFound("Course Id: " + courseId + " not found");
            }

            _courses.Remove(course);
            return StatusCode(204);
        }

       [HttpPut("{courseId:int}")]
        public IActionResult UpdateCourse([FromBody] Course updatedCourse, int courseId)
        {
            var course = _courses.SingleOrDefault(x => x.Id == courseId);
            
            if (course == null)
            {
                return NotFound("Course Id: " + courseId + " not found");
            }

            _courses.Remove(course);

            _courses.Add(updatedCourse);

            return Created("GetCourseById", updatedCourse);
        }

        [HttpGet("{courseId:int}/students", Name = "GetStudentsInCourse")]
        public IActionResult GetStudentsInCourse(int courseId)
        {
            var course = _courses.SingleOrDefault(x => x.Id == courseId);
        
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course.Students);
        }

        [HttpPost("{courseId:int}/students")]

        public IActionResult AddStudentToCourse([FromBody] Student newbie , int courseId)
        {
            var course = _courses.SingleOrDefault(x => x.Id == courseId);

            if (course == null || newbie == null)
            {
                return StatusCode(400);
            }
 
            course.Students.Add(newbie);
            return Created("GetStudentsInCourse", newbie);

        }

    }
}
