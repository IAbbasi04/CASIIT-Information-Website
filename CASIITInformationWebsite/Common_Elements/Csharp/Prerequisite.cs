using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Org.BouncyCastle.Asn1.Cmp;
using static CASIITInformationWebsite.Common_Elements.Csharp.Prerequisite.ClassOption;
using System.Transactions;

namespace CASIITInformationWebsite.Common_Elements.Csharp
{
    public class Prerequisite
    {
        private double min_GPA;
        private int min_year;
        private string classes_required  = "no classes required"; //Class ID

        public class ClassOption
        {
            public class Option : ClassOption
            {
                Class c;
                public Option( Class c)
                {
                    this.c = c;
                }

                
            }

            public class And : ClassOption
            {
                private ClassOption ClassOption1;
                private ClassOption ClassOption2;

                public And(ClassOption classOption1, ClassOption classOption2)
                {
                    ClassOption1 = classOption1;
                    ClassOption2 = classOption2;
                }
            }
            public class Or : ClassOption
            {
                private ClassOption ClassOption1;
                private ClassOption ClassOption2;

                public Or(ClassOption classOption1, ClassOption classOption2)
                {
                    ClassOption1 = classOption1;
                    ClassOption2 = classOption2;
                }
            }
            
        }
        // new Or( new And( new ClassOption(ENG101), new ClassOption(DB101) ), new And(  new ClassOption(ENG101), new ClassOption(DB101) ); 
        //ex classes required
        // (ENG101 && DB101) || (ENG201 && DB201)
        public Prerequisite(double min_GPA, int min_year, string classes_required)
        {
            this.min_GPA = min_GPA;
            this.min_year = min_year;
            this.classes_required = classes_required;
        }

    }
}