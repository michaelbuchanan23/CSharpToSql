using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//figure out how to clear a cmd.Paramaters collection
//make classes for Update, Select, Insert, and Delete

namespace CSharpToSql {
	class Program {

		//create a new list for the user class to store the user information-- declared above main program so that other methods can access it after program runs
		static List<User> users = new List<User>();

		void Run () {
			User user = new User();
			user.Id = 11;
			user.Username = "XXX2";
			user.Password = "Password";
			user.Firstname = "Vin";
			user.Lastname = "Diesel";
			user.Phone = "555-5555";
			user.Email = "vin@diesel.com";
			user.IsReviewer = true;
			user.IsAdmin = true;
			user.Active = true;
			Update(user);
		}

		static void Main(string[] args) {

			(new Program()).Run();
		}


		void Update(User user) {

			//connecting to the database prssql
			string connStr = @"server=STUDENT03\SQLEXPRESS; database=prssql; Trusted_connection=true";
			SqlConnection conn = new SqlConnection(connStr);
			conn.Open();
			if (conn.State != ConnectionState.Open) {
				throw new ApplicationException("Connection did not open");
			}
			System.Diagnostics.Debug.WriteLine("Connection Opened");

			//Update sql query
			string sql = "update [User] "
				+ "Set Username = @Username, "
				+ "Password = @Password, "
				+ "Firstname = @Firstname, "
				+ "Lastname = @Lastname, "
				+ "Phone = @Phone, "
				+ "Email = @Email, "
				+ "IsReviewer = @IsReviewer, "
				+ "IsAdmin = @IsAdmin, "
				+ "Active = @Active "
				+ "where Id = @Id;";

			//adding the user info to the sql query via the Paramters collection
			SqlCommand cmd = new SqlCommand(sql, conn);
			cmd.Parameters.Add(new SqlParameter("@Id", user.Id));
			cmd.Parameters.Add(new SqlParameter("@Username", user.Username));
			cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
			cmd.Parameters.Add(new SqlParameter("@Firstname", user.Firstname));
			cmd.Parameters.Add(new SqlParameter("@Lastname", user.Lastname));
			cmd.Parameters.Add(new SqlParameter("@Phone", user.Phone));
			cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
			cmd.Parameters.Add(new SqlParameter("@IsReviewer", user.IsReviewer));
			cmd.Parameters.Add(new SqlParameter("@IsAdmin", user.IsAdmin));
			cmd.Parameters.Add(new SqlParameter("@Active", user.Active));


			//checking to make sure the record was inserted correctly
			int recsAffected = cmd.ExecuteNonQuery();
			if (recsAffected != 1) {
				System.Diagnostics.Debug.WriteLine("Record insert failed");
			} else {
				System.Diagnostics.Debug.WriteLine("Record insert successful");
			}


			//closes the connection
			conn.Close();
		}

		void Insert(User user) {
			//connecting to the database prssql
			string connStr = @"server=STUDENT03\SQLEXPRESS; database=prssql; Trusted_connection=true";
			SqlConnection conn = new SqlConnection(connStr);
			conn.Open();
			if (conn.State != ConnectionState.Open) {
				throw new ApplicationException("Connection did not open");
			}
			System.Diagnostics.Debug.WriteLine("Connection Opened");

			//getting information from the sql table with a name prssql about the [User] table
			//string sql = "insert into [User] (Username, Password, Firstname, Lastname, Phone, Email, IsReviewer, IsAdmin, Active) " +
			//	"values ('usr', 'psw', 'fname', 'lname', 'phn', 'eml', 1, 1, 1)";
			string sql = ("insert [User] (Username, Password, Firstname, Lastname, Phone, Email, IsReviewer, IsAdmin, Active) " + //(input > 0) ? "positive" : "negative";
				"values (@Username, @Password, @Firstname, @Lastname, @Phone, @Email, @IsReviewer, @IsAdmin, @Active)");
			SqlCommand cmd = new SqlCommand(sql, conn);
			cmd.Parameters.Add(new SqlParameter("@Username", user.Username));
			cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
			cmd.Parameters.Add(new SqlParameter("@Firstname", user.Firstname));
			cmd.Parameters.Add(new SqlParameter("@Lastname", user.Lastname));
			cmd.Parameters.Add(new SqlParameter("@Phone", user.Phone));
			cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
			cmd.Parameters.Add(new SqlParameter("@IsReviewer", user.IsReviewer));
			cmd.Parameters.Add(new SqlParameter("@IsAdmin", user.IsAdmin));
			cmd.Parameters.Add(new SqlParameter("@Active", user.Active));

			int recsAffected = cmd.ExecuteNonQuery();
			if (recsAffected != 1) {
				System.Diagnostics.Debug.WriteLine("Record insert failed");
			} else {
				System.Diagnostics.Debug.WriteLine("Record insert successful");
			}


			//closes the connection
			conn.Close();

		}


		void Select() {

			//connecting to the database prssql
			string connStr = @"server=STUDENT03\SQLEXPRESS; database=prssql; Trusted_connection=true";
			SqlConnection conn = new SqlConnection(connStr);
			conn.Open();
			if (conn.State != ConnectionState.Open) {
				throw new ApplicationException("Connection did not open");
			}
			System.Diagnostics.Debug.WriteLine("Connection Opened"); //could also put "System.Diagnostics" in a using statement at the top and then use "Debug.WriteLine" here

			//getting information from the sql table with a name prssql about the [User] table
			string sql = "select * from [User]";
			//string sql = "select * from [User] where IsAdmin = 1";
			SqlCommand cmd = new SqlCommand(sql,conn);
			SqlDataReader reader = cmd.ExecuteReader();

			//navigating through the data row by row using the reader
			while (reader.Read()) { //the Read() method returns true if there is more to read and false if it there are no more rows which would then exit the while loop
				int id = reader.GetInt32(reader.GetOrdinal("Id")); //get ordinal gives us the index number for the Id column in the table and stores it in id
				string username = reader.GetString(reader.GetOrdinal("Username"));
				string password = reader.GetString(reader.GetOrdinal("Password"));
				string firstname = reader.GetString(reader.GetOrdinal("Firstname"));
				string lastname = reader.GetString(reader.GetOrdinal("Lastname"));
				string phone = reader.GetString(reader.GetOrdinal("Phone"));
				string email = reader.GetString(reader.GetOrdinal("Email"));
				bool isreviewer = reader.GetBoolean(reader.GetOrdinal("IsReviewer"));
				bool isadmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin"));
				bool active = reader.GetBoolean(reader.GetOrdinal("Active"));

				//creates new instance of user and inputs the information pulled from the database
				User user = new User();
				user.Id = id;
				user.Username = username;
				user.Password = password;
				user.Firstname = firstname;
				user.Lastname = lastname;
				user.Phone = phone;
				user.Email = email;
				user.IsReviewer = isreviewer;
				user.IsAdmin = isadmin;
				user.Active = active;

				users.Add(user);

				//print out the information above to the output window after Start Debugging (F5)
				//System.Diagnostics.Debug.WriteLine($"{id}, {username}, {password}, {firstname}, {lastname}, {phone}, {email}, {isreviewer}, {isadmin}, {active}");
			}

			//closes the connection
			conn.Close();
		}
	}
}
