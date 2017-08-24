using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AssApp.Controllers.Models
{
    public class Course
    {
        [Required]
        public string Name { get; set; }
        public string T_Id { get; set; }

        public List<Student> Students = new List<Student>();
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}