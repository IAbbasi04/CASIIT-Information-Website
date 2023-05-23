using CASIITInformationWebsite;
using CASIITInformationWebsite.Common_Elements.Csharp;
using System.Diagnostics;

Student test = SQLQuerier.SelectStudent(1);
test.Year = 0;
test.GPA = 4.5;
SQLQuerier.UpdateStudent(test);