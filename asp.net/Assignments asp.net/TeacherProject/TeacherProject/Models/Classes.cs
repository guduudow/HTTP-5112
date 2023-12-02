using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherProject.Models
{
    public class Course
    {
        //following classes define a course
        public int ClassID;
        public string ClassCode;
        public int TeacherID;
        public DateTime StartDate;
        public DateTime FinishDate;
        public string ClassName;
    }
}