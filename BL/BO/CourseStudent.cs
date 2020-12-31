namespace BO
{/// <summary>
 ///  This BO class is based on  DO.StudentInCourse + DO.Student
 ///  It is to be used inside Course entity only - therefore it does not contain any Course data
 ///  Contains only data to be presented in the lst of students of a course 
 /// </summary>
    public class CourseStudent
    {
        public int ID { get; set; } // person ID
        public string Name { get; set; } // preson Name
        public float? Grade { get; set; }
    }
}