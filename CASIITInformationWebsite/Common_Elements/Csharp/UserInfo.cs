﻿using System;

namespace CASIITInformationWebsite.Common_Elements.Csharp
{
	public abstract class UserInfo
	{
		public string FirstName;
		public string MiddleName;
		public string LastName;
		public int UserId;
		public int currentSelectedTrack;
	}

	public class Student : UserInfo
	{
		public int Year;
		public double GPA;
		public int CounselorId;

		public Student(string firstName, string middleName, string lastName, int userId, int year, double gpa, int counselorId)
		{
			base.FirstName = firstName;
			base.MiddleName = middleName;
			base.LastName = lastName;
			base.UserId = userId;
			this.Year = year;
			this.GPA = gpa;
			this.CounselorId = counselorId;
		}

		/*
		public string getInfo()
		{
			string output = "Name: " + Name + "\nID: " + UserId + "\nGrade: " + Year + "\nGPA: " + GPA + "Counselor ID: " + CounselorId;
            Console.Out.WriteLine( output );
			return output;
		}
		*/
	}

	public class Counselor : UserInfo
	{
		private int CounselorId;
		//Stores the Students a Counselor by last name.
		private string[] StudentNameRange = new string[2];

		public Counselor(string firstName, string middleName, string lastName, int userId, int counselorId, string[] StudentNameRange)
		{
			base.FirstName = firstName;
			base.MiddleName = middleName;
			base.LastName = lastName;
			base.UserId = userId;
			this.CounselorId = counselorId;
			this.StudentNameRange = StudentNameRange;
		}
		/*
		public string getInfo()
		{
			Console.WriteLine("Name: " + Name + "\nID: " + UserId + "Counselor ID: " + CounselorId);
			return ("Name: " + Name + "\nID: " + UserId + "Counselor ID: " + CounselorId);
		}
		*/
	}

	public class Admin : UserInfo
	{
		private int AdminId;
		public Admin(string firstName, string middleName, string lastName, int userId, int adminId)
		{
			base.FirstName = firstName;
			base.MiddleName = middleName;
			base.LastName = lastName;
			base.UserId = userId;
			this.AdminId = adminId;
		}
		/*
		public string getInfo()
		{
			Console.WriteLine("Name: " + Name + "\nID: " + UserId);
			return ("Name: " + Name + "\nID: " + UserId);
		}
		*/
	}

}
