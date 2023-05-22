using System;

namespace CASIITInformationWebsite.Common_Elements.Csharp
{
	public abstract class UserInfo
	{
		public string FirstName;
		public string MiddleName;
		public string LastName;
		public int UserId;
		public string email;
		public string password;
		public int currentSelectedTrack = 1;

		public void SetEmail(string email)
		{
			this.email = email;
		}

		public void SetPassWord(string password)
		{
			this.password = password;
		}
	}

	public class Student : UserInfo
	{
		public int Year;
		public double GPA;
		public int CounselorId;

        public Student(string firstName, string lastName, int userId, int year, double gpa, int counselorId, string email, string password)
        {
            base.FirstName = firstName;
			base.LastName = lastName;
            base.UserId = userId;
            this.Year = year;
            this.GPA = gpa;
            this.CounselorId = counselorId;
			this.email = email;
			this.password = password;
        }

        public Student(string firstName, string middleName, string lastName, int userId, int year, double gpa, int counselorId, string email, string password)
		{
			base.FirstName = firstName;
			base.MiddleName = middleName;
			base.LastName = lastName;
			base.UserId = userId;
			this.Year = year;
			this.GPA = gpa;
			this.CounselorId = counselorId;
            this.email = email;
            this.password = password;
        }

		public override string ToString()
		{
			return "ID: " + UserId + "\n" +
				"Firstname: " + FirstName + "\n" +
				"Lastname: " + LastName + "\n" +
				"Middlename: " + MiddleName + "\n" +
				"Year: " + Year + "\n" +
				"GPA: " + GPA + "\n" +
				"Counselor ID: " + CounselorId + "\n";

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
		public string NameRangeStart;
		public string NameRangeEnd;

        public Counselor(string firstName, string lastName, int userId, string nameRangeStart, string nameRangeEnd, string email, string password)
        {
            base.FirstName = firstName;
            base.LastName = lastName;
            base.UserId = userId;
            //this.CounselorId = counselorId;
            this.NameRangeStart = nameRangeStart;
			this.NameRangeEnd = nameRangeEnd;
            this.email = email;
            this.password = password;
        }

        public Counselor(string firstName, string middleName, string lastName, int userId, int counselorId, string[] StudentNameRange, string email, string password)
		{
			base.FirstName = firstName;
			base.MiddleName = middleName;
			base.LastName = lastName;
			base.UserId = userId;
			this.CounselorId = counselorId;
			this.NameRangeStart = StudentNameRange[0];
			this.NameRangeEnd = StudentNameRange[1];
            this.email = email;
            this.password = password;
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

        public Admin(string firstName, string lastName, int userId, string email, string password)
        {
            base.FirstName = firstName;
            base.LastName = lastName;
            base.UserId = userId;
            this.email = email;
            this.password = password;
        }

        public Admin(string firstName, string middleName, string lastName, int userId, int adminId, string email, string password)
		{
			base.FirstName = firstName;
			base.MiddleName = middleName;
			base.LastName = lastName;
			base.UserId = userId;
			this.AdminId = adminId;
            this.email = email;
            this.password = password;
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
