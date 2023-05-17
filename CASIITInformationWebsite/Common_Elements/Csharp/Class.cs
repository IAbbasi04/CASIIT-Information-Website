using CASIITInformationWebsite.Common_Elements.Csharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CASIITInformationWebsite.Common_Elements
{
    public class Class
    {
        //database columns:
        public int id;
        public string course_name;
        public double course_weight;
        public string description;
        public int dual_enrolled;
        public double hs_credit;
        public double college_credit;
        public Prerequisite prerequisite;

        public Class(int id, string course_name, double course_weight, string description, int dual_enrolled, double hs_credit, double college_credit, Prerequisite prerequisite)
        {
            this.id = id;
            this.course_name = course_name;
            this.course_weight = course_weight;
            this.description = description;
            this.dual_enrolled = dual_enrolled;
            this.hs_credit = hs_credit;
            this.college_credit = college_credit;
            this.prerequisite = prerequisite;
        }

        public string toString()
        {
            return id + "";
        }
    }
}