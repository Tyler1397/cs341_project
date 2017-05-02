using cs341_project.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace cs341_project.Facades
{
    public class DatabaseFacade
    {

        private string connectionString;
        public DatabaseFacade()
        {
            connectionString = "Data Source=cs341database.database.windows.net;Initial Catalog=cs341database;User ID=Tyler1397;Password=Password!";
        }

        /*
       * Author: Tyler Timm
       * Description: Method takes in login credentials, and attempts to return a User, if a user is not found specify error in message
       * Params: LoginCredentials object, which contains a user and a string
       * Return: UserResult which contains a user and a string
       */
        public UserResult GetUser(LoginCredentials login)
        {
            // hash password
            String oldPass = login.Password;
            var sha1 = new SHA1CryptoServiceProvider();
            Byte[] hashedPassword = sha1.ComputeHash(Encoding.ASCII.GetBytes(login.Password));

            SqlConnection cnn = new SqlConnection(connectionString);
            UserResult output = new UserResult();

            if (UserExists(login.Username))
            {
                string sql = "SELECT * FROM dbo.AllUsers WHERE Username = '" + login.Username + "' AND Password = '" + System.Text.Encoding.Default.GetString(hashedPassword) + "'";
                SqlDataReader dataReader;

                try
                {
                    cnn.Open();
                    SqlCommand command = new SqlCommand(sql, cnn);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        switch (dataReader["Type"].ToString())
                        {
                            case "patient":
                                output.User = new User();
                                output.User.Appointments = GetAppointments(dataReader["Username"].ToString());
                                break;
                            case "admin":
                                Admin admin = new Admin();
                                admin.Users = GetAllUsers();
                                admin.Appointments = GetAllAppointments();
                                output.User = admin;
                                break;
                            case "employee":
                                Employee employee = new Employee();
                                employee.Appointments = GetAppointments(dataReader["Username"].ToString());
                                employee.Role = dataReader["Role"].ToString();
                                output.User = employee;
                                break;
                        }
                        output.User.Username = dataReader["Username"].ToString();
                        output.User.Password = dataReader["Password"].ToString();
                        output.User.FirstName = dataReader["FirstName"].ToString();
                        output.User.LastName = dataReader["LastName"].ToString();
                        output.User.Status = dataReader["Status"].ToString();
                        output.User.StartDate = dataReader["StartDate"].ToString();
                        output.User.EndDate = dataReader["EndDate"].ToString();
                        output.User.Type = dataReader["Type"].ToString();
                        output.User.Messages = GetMessages(output.User.Username);
                    }
                    dataReader.Close();
                    command.Dispose();
                    cnn.Close();
                    if(output.User == null)
                    {
                        output.Message = "Error: Username or password is incorrect";
                    }
                    if (output.User.Status.Equals("Deleted", StringComparison.Ordinal))
                    {
                        output.User = null;
                        output.Message = "This account has been deleted";
                    }
                    return output;
                }
                catch (Exception ex)
                {
                    output.Message = "Error: Problem accessing database";
                    return output;
                }
            }
            else
            {
                output.Message = "Username " + login.Username + " does not exist in System";
            }
            
            return output;
        }

        /*
        * Author: Tyler Timm
        * Description: Method returns a list containing all users in the database
        * Params: N/a
        * Return: List containing User objects
        */
        public LinkedList<User> GetAllUsers()
        {
            LinkedList<User> output = new LinkedList<User>();
            string sql = "SELECT * FROM dbo.AllUsers";
            SqlDataReader dataReader;
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    User tempUser = null;
                    switch (dataReader["Type"].ToString())
                    {
                        case "patient":
                            tempUser = new User();
                            tempUser.Appointments = GetAppointments(dataReader["Username"].ToString());
                            break;
                        case "admin":
                            Admin admin = new Admin();
                            tempUser = admin;
                            break;
                        case "employee":
                            Employee employee = new Employee();
                            employee.Role = dataReader["Role"].ToString();
                            employee.Appointments = GetAppointments(dataReader["Username"].ToString());
                            tempUser = employee;
                            break;
                    }
                    tempUser.Username = dataReader["Username"].ToString();
                    tempUser.Password = dataReader["Password"].ToString();
                    tempUser.FirstName = dataReader["FirstName"].ToString();
                    tempUser.LastName = dataReader["LastName"].ToString();
                    tempUser.Status = dataReader["Status"].ToString();
                    tempUser.StartDate = dataReader["StartDate"].ToString();
                    tempUser.EndDate = dataReader["EndDate"].ToString();
                    tempUser.Type = dataReader["Type"].ToString();

                    output.AddFirst(tempUser);

                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                return null; ;
            }
            return output;
        }
        
        /*
         * Author: Tyler Timm
         * Description: Method takes in a username, and returns a partially filled User object
         * Params: string representing user name
         * Return: User object with non-sensitive data filled out
         */
        public User GetPartialUser(string username)
        {
            string sql = "SELECT * FROM dbo.AllUsers WHERE Username = '" + username + "'";
            User output = null;
            SqlDataReader dataReader;
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    switch (dataReader["Type"].ToString())
                    {
                        case "patient":
                            output= new User();
                            break;
                        case "admin":
                            Admin admin = new Admin();
                            output = admin;
                            break;
                        case "employee":
                            Employee employee = new Employee();
                            employee.Role = dataReader["Role"].ToString();
                            output = employee;
                            break;
                    }
                    output.Username = dataReader["Username"].ToString();
                    output.FirstName = dataReader["FirstName"].ToString();
                    output.LastName = dataReader["LastName"].ToString();
                    output.Type = dataReader["Type"].ToString();
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
                return output;
            }
            catch (Exception ex)
            {
                return output;
            }
        }

        public LinkedList<Appointment> GetAllAppointments()
        {
            LinkedList<Appointment> output = new LinkedList<Appointment>();
            string sql = "SELECT * from dbo.AllAppointments";
            SqlDataReader dataReader;
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Appointment temp = new Appointment();
                    temp.Id = dataReader["Id"].ToString();
                    temp.Date = dataReader["Date"].ToString();
                    temp.Patient = GetPartialUser(dataReader["Patient"].ToString());
                    temp.Employee = GetPartialUser(dataReader["Employee"].ToString());
                    temp.Status = dataReader["Status"].ToString();
                    temp.Title = dataReader["Title"].ToString();
                    temp.Time = dataReader["Time"].ToString();
                    output.AddLast(temp);
                }
                return output;
            }
            catch (Exception ex)
            {
                output.AddLast(new Appointment());
                return output; ;
            }
        }

        /*
         * Author: Tyler Timm
         * Description: Method takes in a username, and returns all appointments associated with it
         * Params: string representing user name
         * Return: LinkedList filled with all of that users appointments
         */
        public LinkedList<Appointment> GetAppointments(string username)
        {
            LinkedList<Appointment> output = new LinkedList<Appointment>();
            string sql = "SELECT * from dbo.AllAppointments WHERE Employee ='" + username + "' OR Patient = '" + username + "'";
            SqlDataReader dataReader;
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Appointment temp = new Appointment();
                    temp.Id = dataReader["Id"].ToString();
                    temp.Date = dataReader["Date"].ToString();
                    temp.Patient = GetPartialUser(dataReader["Patient"].ToString());
                    temp.Employee = GetPartialUser(dataReader["Employee"].ToString());
                    temp.Status = dataReader["Status"].ToString();
                    temp.Title = dataReader["Title"].ToString();
                    temp.Time = dataReader["Time"].ToString();
                    output.AddLast(temp);
                }
                return output;
            }
            catch (Exception ex)
            {
                output.AddLast(new Appointment());
                return output; ;
            }
        }

        /*
        * Author: Tyler Timm
        * Description: Method takes in a username and a message, and adds a message entry into the Message table
        * Params: string representing username and string representing message
        * Return: string specifiying if the insert was successful
        */
        public String GenerateMessage(String user,String msg)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            string sql = "INSERT INTO dbo.Messages VALUES ('" + user + "','" + msg + "')";
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                command.Dispose();
                cnn.Close();
                return "success generating message";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /*
        * Author: Tyler Timm
        * Description: Method takes in a username, and returns a list of all of that users messages
        * Params: string representing username
        * Return: ILinkedList containing all of that users messages
        */
        public LinkedList<Message> GetMessages(String username)
        {
            LinkedList<Message> output = new LinkedList<Message>();
            string sql = "SELECT M.Id,M.Message from dbo.Messages M join dbo.AllUsers A on M.Username = A.Username where M.Username='" + username + "'";
            SqlDataReader dataReader;
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Message temp = new Message();
                    temp.Id = dataReader["Id"].ToString();
                    temp.Info = dataReader["Message"].ToString();
                    output.AddFirst(temp);
                }
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /*
         * Author: Tyler Timm
         * Description: Method takes an appointment, and adds it to the database
         * Params: Appointment object
         * Return: Input appointment object
         */
        public Appointment AddAppointment(Appointment app)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            string sql = "INSERT INTO dbo.AllAppointments (Date,Patient,Employee,Status,Title,Time) VALUES ('"+app.Date+"','" + app.Patient.Username + "','" + app.Employee.Username + "','" + app.Status + "','" + app.Title + "','" + app.Time + "')";
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                command.Dispose();
                cnn.Close();
                app.Id = GetAppointmentId(app.Patient.Username, app.Employee.Username, app.Date, app.Time);
                return app;
            }
            catch (Exception ex)
            {
                return app;
            }
        }

        private string GetAppointmentId(string patient,string employee,string date,string time)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            string sql = "SELECT Id FROM dbo.AllAppointments WHERE Patient ='" + patient + "' AND Employee ='" + employee + "' AND Date ='" + date + "' AND Time ='" + time + "'";
            SqlDataReader dataReader;
            try
            {
                string output = "";
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    output = dataReader["Id"].ToString();
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /*
         * Author: Tyler Timm
         * Description: Method takes a User and a role, and adds it to the database
         * Params: User object , string
         * Return: String with output message
         */
        public String AddUser(User user,String role)
        {
            //hash the password
            var sha1 = new SHA1CryptoServiceProvider();
            Byte[] hashedPassword = sha1.ComputeHash(Encoding.ASCII.GetBytes(user.Password));

            string sql;
            SqlConnection cnn = new SqlConnection(connectionString);

            // if the user already exists, return
            if (UserExists(user.Username))
            {
                return "User already exists";
            }

            sql = "INSERT INTO dbo.AllUsers (Username,Password,FirstName,LastName,Status,StartDate,Type,Role)VALUES ('" + user.Username + "','" + System.Text.Encoding.Default.GetString(hashedPassword) + "','" + user.FirstName + "','" + user.LastName + "','" + user.Status + "','" + user.StartDate + "','" + user.Type + "','"+role+"')";
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                command.Dispose();
                cnn.Close();
                GenerateMessageForMultipleUsers("New user " + user.Username + " registered on " + user.StartDate, GetAllAdmins());
                return GenerateMessage(user.Username, "Welcome to your new profile! Your username is " + user.Username + " and your password is " + user.Password);
            }
            catch (Exception ex)
            {
                return "Failed to create new user "+ System.Text.Encoding.Default.GetString(hashedPassword) +" "+ ex;
            }
        }

        /*
        * Author: Jeremy Watson,Tyler Timm
        * Description: Method takes in a username, and returns a boolean indicating whether that user is in the database or not
        * Params: string representing username
        * Return: boolean indicating if the user exists in the database
        */
        public bool UserExists(string username)
        {
            bool exists = false;
            string sql = "SELECT Username FROM dbo.AllUsers WHERE Username = '"+username+"';";
            SqlDataReader dataReader;
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    exists = true;
                }
                dataReader.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                exists = true;
            }

            return exists;
        }

        /*
        * Author: Tyler Timm
        * Description: Method takes in an id, and deletes the appointment that corresponds to it
        * Params: string representing id
        * Return: string indicating if the delete was successful or not
        */
        public string DeleteAppointment(string id)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            String sql = "DELETE FROM dbo.AllAppointments WHERE Id='"+id+"'";
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                command.Dispose();
                cnn.Close();
                return "Successfully deleted appointment";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /*
        * Author: Tyler Timm
        * Description: Method takes in an id, and sets the user's status to deleted and the end date to the current date
        * Params: string representing id
        * Return: string indicating if the update was successful or not
        */
        public string DeleteUser(string id)
        {
            DeleteMessages(id);
            SqlConnection cnn = new SqlConnection(connectionString);
            DateTime thisDay = DateTime.Today;
            String sql = "UPDATE dbo.AllUsers SET Status ='Deleted',EndDate='"+ thisDay.ToString("d")+"' WHERE Username='" + id + "'";
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                command.Dispose();
                cnn.Close();
                GenerateMessageForMultipleUsers("User " + id + " has been deleted ", GetAllAdmins());
                return "Successfully deleted user";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /*
        * Author: Tyler Timm
        * Description: Method takes in an id, and deletes all messages associated with the user
        * Params: string representing id
        * Return: string indicating if the delete was successful or not
        */
        private string DeleteMessages(string id)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            DateTime thisDay = DateTime.Today;
            String sql = "DELETE FROM dbo.Messages WHERE Username='" + id + "'";
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                command.Dispose();
                cnn.Close();
                return "Successfully deleted user's messages";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /*
        * Author: Tyler Timm
        * Description: Method takes in an id,and deletes the message with the corresponding id
        * Params: string representing id
        * Return: string indicating if the delete was successful or not
        */
        public string DeleteMessage(string id)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            DateTime thisDay = DateTime.Today;
            String sql = "DELETE FROM dbo.Messages WHERE Id='" + id + "'";
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                command.Dispose();
                cnn.Close();
                return "Successfully deleted user's message";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /*
        * Author: Tyler Timm
        * Description: Method returns a linked list containing all the usernames of the admins
        * Params: N/A
        * Return: linked list
        */
        public LinkedList<string> GetAllAdmins()
        {
            LinkedList<string> output = new LinkedList<string>();
            string sql = "SELECT * FROM dbo.AllUsers WHERE Type='admin'";
            SqlDataReader dataReader;
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    output.AddFirst(dataReader["Username"].ToString());
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
                return output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /*
        * Author: Tyler Timm
        * Description: private method takes in a message and a linked list, and generates that message for all users in the list
        * Params: string representing message, a list of users
        * Return: N/A
        */
        private void GenerateMessageForMultipleUsers(string msg,LinkedList<string> users)
        {
            foreach(string id in users)
            {
                GenerateMessage(id, msg);
            }
        }
    }
}