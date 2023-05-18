using CASIITInformationWebsite.Common_Elements;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CASIITInformationWebsite
{
    public partial class CourseCatalogue : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        public class Filter
        {

            public List<Func<Class, bool>> filter = new List<Func<Class, bool>>();
            private double minWeight;
            private double maxWeight;
            private bool dualEnrolled;
            private double hsCreditMin = 0;
            private double hsCreditMax = 10;
            private double collegeCreditMin = 0;
            private double collegeCreditMax = 10;
            private double minGpa = 0;
            private double maxGpa = 5;
            private int minYear = 0;
            private int maxYear = 4;

            public Filter(
                double courseWeightRangeMin = 0,
                double courseWeightRangeMax = 5.0,
                bool isDualEnrolled = false,
                double hsCreditRangeMin = 0,
                double hsCreditRangeMax = 10,
                double collegeCreditRangeMin = 0,
                double collegeCreditRangeMax = 10,
                double gpaRangeMin = 0.0,
                double gpaRangeMax = 10,
                int yearRangeMin = 0,
                int yearRangeMax = 4)
            {
                minWeight = courseWeightRangeMin;
                maxWeight = courseWeightRangeMax;
                dualEnrolled = isDualEnrolled;
                hsCreditMin = hsCreditRangeMin;
                hsCreditMax = hsCreditRangeMax;
                collegeCreditMin = collegeCreditRangeMin;
                collegeCreditMax = collegeCreditRangeMax;
                minGpa = gpaRangeMin;
                maxGpa = gpaRangeMax;
                minYear = yearRangeMin;
                maxYear = yearRangeMax;

                filter.Add(new Func<Class, bool>(CourseWeightRange));
                filter.Add(new Func<Class, bool>(DualEnrolledSelector));
                filter.Add(new Func<Class, bool>(HSCreditRange));
                filter.Add(new Func<Class, bool>(CollegeCreditRange));
                filter.Add(new Func<Class, bool>(GPARange));
                filter.Add(new Func<Class, bool>(YearRange));

            }

            private bool CourseWeightRange(Class course)
            {
                double value = course.course_weight;
                return (minWeight <= value) && ( value <= maxWeight );
            }

            private bool DualEnrolledSelector(Class course)
            {
                if (dualEnrolled) return (course.dual_enrolled == 1);
                else return true;
            }

            private bool HSCreditRange(Class course)
            {
                double value = course.hs_credit;
                return (hsCreditMin <= value) && (value <= hsCreditMax);
            }

            private bool CollegeCreditRange(Class course)
            {
                double value = course.college_credit;
                return (collegeCreditMin <= value) && (value <= collegeCreditMax);
            }

            private bool GPARange( Class course)
            {
                double value = course.prerequisite.min_GPA;
                return (minGpa <= value) && (maxGpa <= hsCreditMax);
            }

            private bool YearRange( Class course)
            {
                double value = course.prerequisite.min_year;
                return (minYear <= value) && (value <= maxYear);
            }

        }
    }
    
}