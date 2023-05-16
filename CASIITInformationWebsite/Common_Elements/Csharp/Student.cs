using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CASIITInformationWebsite.Common_Elements
{
    public class Student
    {
        private string name;
        private int id;
        private int year;
        private double gpa;
        private int counselorId;
        // private Class[] classesTaken;

        public Student(string name, int id, int year, double gpa, int counselorId)
        {
            this.name = name;
            this.id = id;
            this.year = year;
            this.gpa = gpa;
            this.counselorId = counselorId;
        }

        public Student()
        {

        }

        // Id
        private void setId(int num)
        {
            id = num;
        }

        private int getId()
        {
            return id;
        }

        // year
        private void setYear(int num)
        {
            year = num;
        }

        private int getYear()
        {
            return year;
        }

        // gpa
        private void setGpa(int num)
        {
            gpa = num;
        }

        private double getGpa()
        {
            return gpa;
        }

        //counselorId
        private void setCounselorId(int num)
        {
            counselorId = num;
        }

        private int getCounselorId()
        {
            return counselorId;
        }

        // Add Student

    }
}