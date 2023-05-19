using CASIITInformationWebsite;
using CASIITInformationWebsite.Common_Elements;
using CASIITInformationWebsite.Common_Elements.Csharp;
using System.Diagnostics;

//HOW TO UPDATE PREREQUISITES --------------------------------------------------
// Sample prerequisite insert
// creates prerequisite object for a given class

{

    List<Class> courses = new List<Class>();

    Student user = SQLQuerier.SelectStudent(2);
    user.currentSelectedTrack = 1;
    Console.WriteLine(user);

    foreach (Class course in SQLQuerier.PreviousClasses(user))
    {
        Console.WriteLine(course);
    }

    courses = SQLQuerier.AllAvailableClasses(user);
    foreach (Class course in courses)
    {
        Console.WriteLine( course );
    }
}


