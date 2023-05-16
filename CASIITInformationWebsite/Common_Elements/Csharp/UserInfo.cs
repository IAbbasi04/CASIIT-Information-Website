using System;

namespace CASIITInformationWebsite.Common_Elements.Csharp
{
	public abstract class UserInfo
	{
		private string Name;
		private int UserId;

		public UserInfo (string name, int userId)
        {
			this.Name = name;
			this.UserId = userId;
        }
	}

	public class TestStudent : UserInfo
    {
		private int Year;
		private double GPA;
		private int CounselorId;

		public TestStudent(string name, int userId, int year, double gpa, int counselorId)
        {
			this.Name = name;
			this.UserId = userId;
			this.Year = year;
			this.GPA = gpa;
			this.CounselorId = counselorId;
        }

		public string getInfo()
        {
			Console.WriteLine("Name: " + Name + "\nID: " + UserId + "\nGrade: " + Year + "\nGPA: " + GPA + "Counselor ID: " + CounselorId);
        }
    }

}
