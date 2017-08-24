using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssApp.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AssApp.Controllers
{

    public class StudentsController : Controller
    {
        private static List<Student> _students = new List<Student>
        {
            new Student() { Ssn = 1707952889, Name = "Gunnlaugur" },
            new Student() { Ssn = 1601872989, Name = "Arnor" }
        };
    }
}