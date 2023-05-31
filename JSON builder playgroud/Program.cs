using CASIITInformationWebsite;
using CASIITInformationWebsite.Common_Elements.Csharp;
using System.Diagnostics;

//HOW TO UPDATE PREREQUISITES --------------------------------------------------
// Sample prerequisite insert
// creates prerequisite object for a given class

{
    int id = 1;
    double min_GPA = 2.5;
    int min_year = 0;
    ClassOption courses = null;
    Prerequisite preq = new Prerequisite(min_GPA, min_year);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 2;
    double min_GPA = 2.5;
    int min_year = 0;
    ClassOption courses = null;
    Prerequisite preq = new Prerequisite(min_GPA, min_year);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 3;
    double min_GPA = 2.5;
    int min_year = 0;
    ClassOption courses = new ClassOption.Option(28);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 4;
    double min_GPA = 3.25;
    int min_year = 0;
    ClassOption courses = null;
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 5;
    double min_GPA = 2.5;
    int min_year = 0;
    ClassOption courses = null;
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 6;
    double min_GPA = 2.5;
    int min_year = 1;
    ClassOption courses = new ClassOption.OR(1, 2);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 7;
    double min_GPA = 2.5;
    int min_year = 1;
    ClassOption courses = new ClassOption.OR(1, 2);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 8;
    double min_GPA = 2.5;
    int min_year = 1;
    ClassOption courses = new ClassOption.OR(4, 13);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 9;
    double min_GPA = 2.5;
    int min_year = 1;
    ClassOption courses = new ClassOption.OPTION(3);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 10;
    double min_GPA = 2.5;
    int min_year = 1;
    ClassOption courses = new ClassOption.OPTION(29);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 11;
    double min_GPA = 2.5;
    int min_year = 1;
    ClassOption courses = new ClassOption.OR(4, 13);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 12;
    double min_GPA = 2.5;
    int min_year = 1;
    ClassOption courses = null;
    Prerequisite preq = new Prerequisite(min_GPA, min_year);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 13;
    double min_GPA = 3.25;
    int min_year = 0;
    ClassOption courses = null;
    Prerequisite preq = new Prerequisite(min_GPA, min_year);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 14;
    double min_GPA = 2.5;
    int min_year = 2;
    ClassOption courses = new ClassOption.Option(6);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 13;
    double min_GPA = 3.25;
    int min_year = 0;
    ClassOption courses = null;
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 14;
    double min_GPA = 2.5;
    int min_year = 2;
    ClassOption courses = new ClassOption.OPTION(6);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 15;
    double min_GPA = 2.5;
    int min_year = 2;
    ClassOption courses = new ClassOption.OPTION(6);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 16;
    double min_GPA = 2.5;
    int min_year = 2;
    ClassOption courses = new ClassOption.OPTION(6);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 17;
    double min_GPA = 2.5;
    int min_year = 2;
    ClassOption courses = null;
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 18;
    double min_GPA = 2.5;
    int min_year = 2;
    ClassOption courses = new ClassOption.OPTION(6);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 19;
    double min_GPA = 2.5;
    int min_year = 2;
    ClassOption courses = new ClassOption.OPTION(9);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 20;
    double min_GPA = 2.5;
    int min_year = 3;
    ClassOption courses = new ClassOption.OPTION(19);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 21;
    double min_GPA = 2.5;
    int min_year = 2;
    ClassOption courses = new ClassOption.OPTION(8);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 22;
    double min_GPA = 2.5;
    int min_year = 2;
    ClassOption courses = new ClassOption.OPTION(10);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 23;
    double min_GPA = 2.5;
    int min_year = 2;
    ClassOption courses = null;
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 24;
    double min_GPA = 2.5;
    int min_year = 2;
    ClassOption courses = new ClassOption.OPTION(12);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 25;
    double min_GPA = 2.5;
    int min_year = 2;
    ClassOption courses = new ClassOption.OPTION(11);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 26;
    double min_GPA = 2.5;
    int min_year = 2;
    ClassOption courses = null;
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 27;
    double min_GPA = 2.5;
    int min_year = 3;
    ClassOption courses = new ClassOption.OPTION(26);
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 28;
    double min_GPA = 0;
    int min_year = 0;
    ClassOption courses = null;
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}

{
    int id = 29;
    double min_GPA = 0;
    int min_year = 0;
    ClassOption courses = null;
    Prerequisite preq = new Prerequisite(min_GPA, min_year, courses);
    SQLQuerier.AddPrereqsToCourse(id, preq);
}