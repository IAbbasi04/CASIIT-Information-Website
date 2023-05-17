using CASIITInformationWebsite;
using CASIITInformationWebsite.Common_Elements.Csharp;
using System.Diagnostics;

/* HOW TO UPDATE PREREQUISITES --------------------------------------------------
// Sample prerequisite insert
// creates prerequisite object for a given class
int id = 0; // target id
int req_id1 = 1; // requisite id
int req_id2 = 2; //second requisite id
int req_id3 = 3;
int req_id4 = 4; //etc.
double min_GPA = 0.0;
int min_year = 0;

ClassOption courses1 = new ClassOption.Or( //classes (1 AND 2) OR (3 AND 4)
    new ClassOption.And(req_id1, req_id2) , 
    new ClassOption.And(req_id3, req_id4) ) ;

ClassOption courses2 = new ClassOption.And( //classes (1 AND (2 AND (3 AND 4))) 
    req_id1, 
        new ClassOption.And(req_id2,
            new ClassOption.And(req_id3, req_id4)));

Prerequisite preq = new Prerequisite( min_GPA , min_year , courses1 );

SQLQuerier.AddPrereqsToCourse(id, preq); */

