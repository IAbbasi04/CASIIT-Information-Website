using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CASIITInformationWebsite.Common_Elements.Csharp
{
    public abstract class ClassOption
    {
        public class Option : ClassOption
        {
            Class c;
            public Option(Class c)
            {
                this.c = c;
            }

            override public string toString()
            {
                return c.toString();
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

            override public string toString()
            {
                return "(" + ClassOption1 + " AND " + ClassOption2 + ")";
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


            override public string toString()
            {
                return "(" + ClassOption1 + " OR " + ClassOption2 + ")";
            }
        }

        public abstract string toString();
    }
}