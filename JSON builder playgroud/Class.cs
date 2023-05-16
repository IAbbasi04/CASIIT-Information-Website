using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CASIITInformationWebsite.Common_Elements
{
    public class Class
    {
        //database columns:
        private int id;
        private string course_name;
        private double course_weight;
        private string description;
        private int dual_enrolled;
        private double hs_credit;
        private double college_credit;
        private string prerequisite;

        public string toString()
        {
            return id + "";
        }
    }
}