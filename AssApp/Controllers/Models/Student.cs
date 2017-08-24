using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace AssApp.Controllers.Models
{
    public class Student
    {
        [Required]
        public int Ssn { get; set; }
        public string Name { get; set; }
    }
}
