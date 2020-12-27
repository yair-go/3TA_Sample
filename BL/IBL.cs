﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;


namespace BLAPI
{
    public interface IBL
    {
        //Add Person to Course
        //get all courses for student
        //etc...

        #region Read
        Student GetStudent(int id);
        IEnumerable<Student> GetAllStudents();
        IEnumerable<ListedPerson> GetStudentIDs();

        IEnumerable<Student> GetStudentsBy(Predicate<Student> predicate);
        #endregion

        #region ADD
        void AddStudent(Student student);
        #endregion
    }
}
