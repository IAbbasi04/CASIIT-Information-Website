using CASIITInformationWebsite;
using CASIITInformationWebsite.Common_Elements.Csharp;
using System.Diagnostics;

//HOW TO UPDATE PREREQUISITES --------------------------------------------------
// Sample prerequisite insert
// creates prerequisite object for a given class
int id = 3; // target id
double min_GPA = 2.5;
int min_year = 0;
ClassOption courses = new ClassOption.Option(28);
Prerequisite preq = new Prerequisite( min_GPA , min_year, courses );
SQLQuerier.AddPrereqsToCourse(id, preq);




