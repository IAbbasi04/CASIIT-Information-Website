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

        public override string ToString()
        {
            return id + "";
        }

        public bool MeetsRequisites(UserInfo user)
        {
            List<List<int>> possibleCourses = this.prerequisite.PossibleRequiredClasses();
            int[] classesTaken = SQLQuerier.PreviousClassIDs(user);
            foreach (List<int> courseSet in possibleCourses)
            {
                bool meetsRequirements = true;
                foreach( int id in courseSet)
                {
                    if (!classesTaken.Contains(id))
                    {
                        meetsRequirements = false;
                        break;
                    }

                }
                if( meetsRequirements)
                {
                    return true;
                }
            }
            return false;
        }

        public bool MeetsRequisites(Student user)
        {
            if (!((user.GPA <= prerequisite.min_GPA) && (user.Year <= prerequisite.min_year))) return false;

            List<List<int>> possibleCourses = this.prerequisite.PossibleRequiredClasses();
            int[] classesTaken = SQLQuerier.PreviousClassIDs(user);
            foreach (List<int> courseSet in possibleCourses)
            {
                bool meetsRequirements = true;
                foreach (int id in courseSet)
                {
                    if (!classesTaken.Contains(id))
                    {
                        meetsRequirements = false;
                        break;
                    }

                }
                if (meetsRequirements)
                {
                    return true;
                }
            }
            return false;
        }
    }
}