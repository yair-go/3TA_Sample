using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using BLAPI;
using System.Threading;
using BO;

namespace BL
{
    class BLImp : IBL //internal
    {
        IDL dl = DLFactory.GetDL();

        public BO.Student GetStudent(int id)
        {
            BO.Student studentBO = new BO.Student();

            DO.Person personDO;
            try
            {
                personDO = dl.GetPerson(id);
            }
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadStudentIdException("Student ID is illegal", ex);
            }
            personDO.CopyPropertiesTo(studentBO);
            //studentBO.ID = personDO.ID;
            //studentBO.BirthDate = personDO.BirthDate;
            //studentBO.City = personDO.City;
            //studentBO.Name = personDO.Name;
            //studentBO.HouseNumber = personDO.HouseNumber;
            //studentBO.Street = personDO.Street;
            //studentBO.PersonalStatus = (BO.PersonalStatus)(int)personDO.PersonalStatus;

            DO.Student studentDO = dl.GetStudent(id);
            studentDO.CopyPropertiesTo(studentBO);
            //studentBO.StartYear = studentDO.StartYear;
            //studentBO.Status = (BO.StudentStatus)(int)studentDO.Status;
            //studentBO.Graduation = (BO.StudentGraduate)(int)studentDO.Graduation;

            studentBO.ListOfCourses = from sic in dl.GetStudentInCourseList(sic => sic.PersonId == id)
                                      let course = dl.GetCourse(sic.CourseId)
                                      select course.CopyToStudentCourse(sic);
            //new bo.studentcourse()
            //{
            //    id = course.id,
            //    number = course.number,
            //    name = course.name,
            //    year = course.year,
            //    semester = (bo.semester)(int)course.semester,
            //    grade = sic.grade
            //};

           // var listOfCourses = dl.GetStudentInCourseList(sic => sic.PersonId == id).Select(c => dl.GetCourse(c.CourseId));
           
            return studentBO;
        }


        public IEnumerable<BO.Student> GetAllStudents()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<BO.Student> GetStudentsBy(Predicate<BO.Student> predicate)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<BO.ListedPerson> GetStudentIDs()
        {
            return from item in dl.GetStudentIDs((id, name) =>
                    {
                        try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                        return new BO.ListedPerson() { ID = id, Name = name };
                    })
                   let student = item as BO.ListedPerson
                   orderby student.ID
                   select student;
        }

        public void AddStudent(BO.Student studentBO)
        {
            DO.Student studentDO = new DO.Student();
            DO.Person personDO = new DO.Person();
            studentBO.CopyPropertiesTo(personDO);
            studentBO.CopyPropertiesTo(studentDO);
            dl.AddPerson(personDO);
            dl.AddStudent(studentDO);
        }
    }
}
