using System;

namespace CASIITInformationWebsite.Common_Elements.Csharp
{
	public abstract class UserInfo
	{
		public string Name;
		public int UserId;
	}

	public class TestStudent : UserInfo
	{
		public int Year;
		public double GPA;
		public int CounselorId;

		public TestStudent(string name, int userId, int year, double gpa, int counselorId)
		{
			base.Name = name;
			base.UserId = userId;
			this.Year = year;
			this.GPA = gpa;
			this.CounselorId = counselorId;
		}

		public string getInfo()
		{
			string output = "Name: " + Name + "\nID: " + UserId + "\nGrade: " + Year + "\nGPA: " + GPA + "Counselor ID: " + CounselorId;
            Console.Out.WriteLine( output );
			return output;
		}
	}

	public class Counselor : UserInfo
	{
		private int CounselorId;

		public Counselor(string name, int userId, int counselorId)
		{
			base.Name = name;
			base.UserId = userId;
			this.CounselorId = counselorId;
		}

		public string getInfo()
		{
			Console.WriteLine("Name: " + Name + "\nID: " + UserId + "Counselor ID: " + CounselorId);
			return ("Name: " + Name + "\nID: " + UserId + "Counselor ID: " + CounselorId);
		}
	}

	public class Admin : UserInfo
	{

		public Admin(string name, int userId)
		{
			base.Name = name;
			base.UserId = userId;
		}

		public string getInfo()
		{
			Console.WriteLine("Name: " + Name + "\nID: " + UserId);
			return ("Name: " + Name + "\nID: " + UserId);
		}
	}

}
