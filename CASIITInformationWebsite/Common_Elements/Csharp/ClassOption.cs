using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Xml.Schema;

namespace CASIITInformationWebsite.Common_Elements.Csharp
{
    public abstract class ClassOption
    {

        public ClassOption ClassOption1;
        public ClassOption ClassOption2;
        int classID;
        int type;

        /// <summary>
        /// A choice for a class
        /// </summary>
        public class Option : ClassOption
        {
            public Option(int classID)
            {
                type = 0;
                this.classID = classID;
            }

            override public string ToString()
            {
                return classID + "";
            }

        }

        /// <summary>
        /// And operator to contruct prerequisites
        /// </summary>
        public class And : ClassOption
        {
            public And(ClassOption classOption1, ClassOption classOption2)
            {
                type = 1;
                ClassOption1 = classOption1;
                ClassOption2 = classOption2;
            }

            public And(int classOption1, int classOption2)
            {
                type = 1;
                ClassOption1 = new Option(classOption1);
                ClassOption2 = new Option(classOption2);
            }

            public And(int classOption1, ClassOption classOption2)
            {
                type = 1;
                ClassOption1 = new Option(classOption1);
                ClassOption2 = classOption2;
            }

            public And(ClassOption classOption1, int classOption2)
            {
                type = 1;
                ClassOption1 = classOption1;
                ClassOption2 = new Option(classOption2);
            }

            override public string ToString()
            {
                return "(" + ClassOption1 + " AND " + ClassOption2 + ")";
            }
        }
        
        /// <summary>
        /// Or operator to construct prerequisites
        /// </summary>
        public class Or : ClassOption
        {
            public Or(ClassOption classOption1, ClassOption classOption2)
            {
                type = 2;
                ClassOption1 = classOption1;
                ClassOption2 = classOption2;
            }

            public Or(int classOption1, int classOption2)
            {
                type = 2;
                ClassOption1 = new Option(classOption1);
                ClassOption2 = new Option(classOption2);
            }

            public Or(int classOption1, ClassOption classOption2)
            {
                type = 2;
                ClassOption1 = new Option(classOption1);
                ClassOption2 = classOption2;
            }

            public Or(ClassOption classOption1, int classOption2)
            {
                type = 2;
                ClassOption1 = classOption1;
                ClassOption2 = new Option(classOption2);
            }


            override public string ToString()
            {
                return "(" + ClassOption1 + " OR " + ClassOption2 + ")";
            }
        }

        public override abstract string ToString();

        /// <summary>
        /// Returns all the possible sets of class ids that could be taken to meet the course requirement of a Prerequisite object
        /// </summary>
        /// <returns>A list of lists of class ids </returns>
        public List<List<int>> getClassSets()
        {
            int[] currentPath = new int[0];
            ClassOption currentOption = this;

            if (currentOption.GetType() == typeof(Option))
            {
                List<List<int>> main = new List<List<int>>();
                List<int> sub = new List<int>();
                sub.Add(currentOption.classID);
                main.Add(sub);
                return main;
            }

            List<List<int>> courses = getClassSets((currentOption).ClassOption1);
            List<List<int>> courses2 = getClassSets((currentOption).ClassOption2);

            if (currentOption.GetType() == typeof(And))
            {
                //adds the contents of every list in courses2 into courses 1
                foreach (List<int> c1 in courses)
                {
                    foreach (List<int> c2 in courses2)
                    {
                        foreach (int v in c2)
                        {
                            c1.Add(v);
                        }
                    }
                }
            }

            if (currentOption.GetType() == typeof(Or))
            {
                foreach (List<int> c in courses2)
                {
                    courses.Add(c);
                }
            }

            return courses;
        }

        private List<List<int>> getClassSets( ClassOption option)
        {
            List<List<int>> main = new List<List<int>>();
            if (option.GetType() == typeof(Option))
            {
                List<int> sub = new List<int>();
                sub.Add(option.classID);
                main.Add(sub);
                return main;
            }

            List<List<int>> courses = getClassSets((option).ClassOption1);
            List<List<int>> courses2 = getClassSets((option).ClassOption2);
            if (option.GetType() == typeof(And))
            {
                //adds the contents of every list in courses2 into courses 1
                foreach (List<int> c1 in courses)
                {
                    foreach (List<int> c2 in courses2)
                    {
                        foreach (int v in c2)
                        {
                            c1.Add(v);
                        }
                    }
                }
                return courses;
            }

            if (option.GetType() == typeof(Or))
            {
                foreach (List<int> c in courses)
                {
                    main.Add(c);
                }
                foreach (List<int> c in courses2)
                {
                    main.Add(c);
                }
                return main;
            }

            return main;
        }

        private static int CountTypes<T>( ClassOption initialOption )
        {
            int total = 0;
            if ( initialOption.GetType() == typeof(T))
            {
                total++;
            }
            if (initialOption.GetType() == typeof(Option))
            {
                return total;
            }
            else
            {
                total += CountTypes<T>((initialOption).ClassOption1);
                total += CountTypes<T>((initialOption).ClassOption2);
                return total;
            }

        }
    }
}