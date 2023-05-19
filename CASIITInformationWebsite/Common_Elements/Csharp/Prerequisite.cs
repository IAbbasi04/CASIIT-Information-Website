using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Org.BouncyCastle.Asn1.Cmp;
using System.Transactions;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Microsoft.Ajax.Utilities;

namespace CASIITInformationWebsite.Common_Elements.Csharp
{
    public class Prerequisite
    {
        public double min_GPA;
        public int min_year; //0 - 4 for freshman to senior year 
        public ClassOption required_classes;
       
        // new Or( new And( new ClassOption(ENG101), new ClassOption(DB101) ), new And(  new ClassOption(ENG201), new ClassOption(DB201) ); 
        //ex classes required
        // ((ENG101 AND DB101) OR (ENG201 AND DB201))

        public Prerequisite()
        {
            min_GPA = 0.0;
            min_year = 0;
        }

        public Prerequisite(double min_GPA, int min_year)
        {
            this.min_GPA = min_GPA;
            this.min_year = min_year;
            required_classes = null;
        }

        public Prerequisite(double min_GPA, int min_year, ClassOption required_classes)
        {
            this.min_GPA = min_GPA;
            this.min_year = min_year;
            this.required_classes = required_classes;
        }

        public string toString()
        {
            return
                "Min_GPA: " + min_GPA +
                " Min_year: " + min_year +
                " required_classes: " + required_classes;
        }

        public static string writeJSON(Prerequisite required_classes)
        {
            string json = JsonConvert.SerializeObject(required_classes);
            return json;
        }

        public static Prerequisite readFromJSON(string serializedPrerequisite)
        {
            if (string.IsNullOrEmpty(serializedPrerequisite)) return new Prerequisite();
            return JsonConvert.DeserializeObject<Prerequisite>(serializedPrerequisite);
        }

        public List<List<int>> PossibleRequiredClasses()
        {
            if (required_classes == null) return new List<List<int>>();
            return required_classes.getClassSets();
        }

        public override string ToString()
        {
            return "\tmin GPA: " + min_GPA + "\n" +
                "\tmin Year: " + min_year + "\n" +
                "\tClasses: " + required_classes;
        }

    }
}