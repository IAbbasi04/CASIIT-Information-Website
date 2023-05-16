using Newtonsoft.Json;
namespace CASIITInformationWebsite.Common_Elements.Csharp

{
    public class Prerequisite
    {
        private double min_GPA;
        private int min_year;
        private ClassOption required_classes;
       
        // new Or( new And( new ClassOption(ENG101), new ClassOption(DB101) ), new And(  new ClassOption(ENG101), new ClassOption(DB101) ); 
        //ex classes required
        // ((ENG101 AND DB101) OR (ENG201 AND DB201))
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

        public string writeToJSON()
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }

        public Prerequisite readFromJSON(string serializedPrerequisite)
        {
            return JsonConvert.DeserializeObject<Prerequisite>(serializedPrerequisite);
        }


    }
}