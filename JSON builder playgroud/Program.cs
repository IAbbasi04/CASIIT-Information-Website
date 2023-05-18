using CASIITInformationWebsite;
using CASIITInformationWebsite.Common_Elements.Csharp;
using System.Diagnostics;

//HOW TO UPDATE PREREQUISITES --------------------------------------------------
// Sample prerequisite insert
// creates prerequisite object for a given class
int id = 29; // target id
double min_GPA = 0.0;
int min_year = 0;
//ClassOption courses1 = new ClassOption() ;
Prerequisite preq = new Prerequisite( min_GPA , min_year );
SQLQuerier.AddPrereqsToCourse(id, preq);




