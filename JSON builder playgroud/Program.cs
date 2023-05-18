using CASIITInformationWebsite;
using CASIITInformationWebsite.Common_Elements;
using CASIITInformationWebsite.Common_Elements.Csharp;
using System.Diagnostics;

//HOW TO UPDATE PREREQUISITES --------------------------------------------------
// Sample prerequisite insert
// creates prerequisite object for a given class

{
    int id = 1;
    double min_GPA = 2.5;
    int min_year = 1;
    ClassOption courses = null;
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 2;
    double min_GPA = 2.5;
    int min_year = 1;
    ClassOption courses = null;
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 3;
    double min_GPA = 2.5;
    int min_year = 1;
    ClassOption courses = null;
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}


