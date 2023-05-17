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
            int classID;
            public Option(int classID)
            {
                this.classID = classID;
            }

            override public string ToString()
            {
                return classID + "";
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

            public And(int classOption1, int classOption2)
            {
                ClassOption1 = new Option(classOption1);
                ClassOption2 = new Option(classOption2);
            }

            public And(int classOption1, ClassOption classOption2)
            {
                ClassOption1 = new Option(classOption1);
                ClassOption2 = classOption2;
            }

            public And(ClassOption classOption1, int classOption2)
            {
                ClassOption1 = classOption1;
                ClassOption2 = new Option(classOption2);
            }

            override public string ToString()
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

            public Or(int classOption1, int classOption2)
            {
                ClassOption1 = new Option(classOption1);
                ClassOption2 = new Option(classOption2);
            }

            public Or(int classOption1, ClassOption classOption2)
            {
                ClassOption1 = new Option(classOption1);
                ClassOption2 = classOption2;
            }

            public Or(ClassOption classOption1, int classOption2)
            {
                ClassOption1 = classOption1;
                ClassOption2 = new Option(classOption2);
            }


            override public string ToString()
            {
                return "(" + ClassOption1 + " OR " + ClassOption2 + ")";
            }
        }

        public override abstract string ToString();

        public int[][] getClassSets()
        {
            return new int[0][0];
        }
    }
}