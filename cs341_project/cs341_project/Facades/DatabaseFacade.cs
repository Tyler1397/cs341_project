﻿using cs341_project.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace cs341_project.Facades
{
    public class DatabaseFacade
    {

        private SqlConnection cnn;
        public DatabaseFacade()
        {
            string connetionString = null;
            connetionString = "Data Source=cs341database.database.windows.net;Initial Catalog=cs341database;User ID=Tyler1397;Password=Password!";
            cnn = new SqlConnection(connetionString);
        }


        public void GetUser(LoginCredentials login,LoginResult result)
        {
            result.User = new Models.User();
            string sql = "SELECT * FROM dbo.Users WHERE Username = '"+login.Username+"' AND Password = '"+login.Password+"'";
            SqlDataReader dataReader;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    result.User.Username = dataReader["Username"].ToString();
                    result.User.Password = dataReader["Password"].ToString();
                    result.User.FirstName = dataReader["First"].ToString();
                    result.User.LastName = dataReader["Last"].ToString();
                    result.User.Active = dataReader["Active"].ToString();
                    result.User.Type= dataReader["Type"].ToString();
                    result.User.Role = dataReader["Role"].ToString();
                    result.Type = dataReader["Type"].ToString();
                    result.Valid = true;
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
                return;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.ToString();
                result.Valid = false;
                return;
            }
            result.ErrorMessage = "Incorrect username and/or password";
            result.Valid = false;
        }

        public void SetupAdmin(LoginResult result)
        {
            result.Admin = new Models.Admin();
            result.Admin.Users = new LinkedList<User>();
            result.Admin.Appointments = new LinkedList<Appointment>();


            string sql = "SELECT * FROM dbo.Users";
            SqlDataReader dataReader;
            try
            {
                // Get all users on file
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    User temp = new Models.User();
                    temp.Username = dataReader["Username"].ToString();
                    temp.Password = dataReader["Password"].ToString();
                    temp.FirstName = dataReader["First"].ToString();
                    temp.LastName = dataReader["Last"].ToString();
                    temp.Active = dataReader["Active"].ToString();
                    temp.Type = dataReader["Type"].ToString();
                    temp.Role = dataReader["Role"].ToString();
                    result.Admin.Users.AddFirst(temp);
                }
                dataReader.Close();
                command.Dispose();

                // Get all appointments on file
                sql = "SELECT * FROM dbo.Appointment";
                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Appointment temp = new Appointment();
                    temp.Id = Convert.ToInt32(dataReader["Id"]);
                    temp.Date = dataReader["Date"].ToString();
                    temp.Patient = dataReader["Patient"].ToString();
                    temp.Employee = dataReader["Employee"].ToString();
                    temp.Approved = dataReader["Approved"].ToString();
                    temp.Cancelled = dataReader["Cancelled"].ToString();
                    temp.Notes = dataReader["Notes"].ToString();
                    temp.Time = dataReader["Time"].ToString();
                    result.Admin.Appointments.AddFirst(temp);
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
                return;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.ToString();
            }
        }

        public void SetupEmployee(LoginResult result)
        {
            result.Employee = new Models.Employee();
            result.Employee.FutureAppointments = new LinkedList<Appointment>();

            string sql = "SELECT * FROM dbo.Appointment WHERE Employee='"+result.User.Username+"'";
            SqlDataReader dataReader;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Appointment temp = new Models.Appointment();
                    temp.Id = Convert.ToInt32(dataReader["Id"]);
                    temp.Date = dataReader["Date"].ToString();
                    temp.Patient = dataReader["Patient"].ToString();
                    temp.Employee = dataReader["Employee"].ToString();
                    temp.Approved = dataReader["Approved"].ToString();
                    temp.Cancelled = dataReader["Cancelled"].ToString();
                    temp.Notes = dataReader["Notes"].ToString();
                    temp.Time = dataReader["Time"].ToString();

                    result.Employee.FutureAppointments.AddFirst(temp);
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public void SetupPatient(LoginResult result)
        {
            result.Patient = new Patient();
            result.Patient.FutureAppointments = new LinkedList<Appointment>();
            result.Patient.RequestedAppointments = new LinkedList<Appointment>();
            result.Patient.PastAppointments = new LinkedList<Appointment>();

            string sql = "SELECT * FROM dbo.Appointment WHERE Patient='" + result.User.Username + "'";
            SqlDataReader dataReader;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Appointment temp = new Appointment();
                    temp.Id = Convert.ToInt32(dataReader["Id"]);
                    temp.Date = dataReader["Date"].ToString();
                    temp.Patient = dataReader["Patient"].ToString();
                    temp.Employee = dataReader["Employee"].ToString();
                    temp.Approved = dataReader["Approved"].ToString();
                    temp.Cancelled = dataReader["Cancelled"].ToString();
                    temp.Notes = dataReader["Notes"].ToString();
                    temp.Time = dataReader["Time"].ToString();

                    result.Patient.FutureAppointments.AddFirst(temp);
                    result.Patient.RequestedAppointments.AddFirst(temp);
                    result.Patient.PastAppointments.AddFirst(temp);
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public Appointment AddAppointment(Appointment app)
        {
            string sql = "INSERT INTO dbo.Appointment (Date,Patient,Employee,Approved,Cancelled,Notes,Time) VALUES ('"+app.Date+"','" + app.Patient + "','" + app.Employee + "','" + app.Approved + "','" + app.Cancelled + "','" + app.Notes + "','" + app.Time+ "')";
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                command.Dispose();
                cnn.Close();
                return app;
            }
            catch (Exception ex)
            {
                app.Notes = ex.ToString();
                return app;
            }
        }

        public String AddUser(User user)
        {
            string sql;

            if (UserExists(user.Username))
            {
                return "User already exists";
            }

            /*
            if (user.Type.Equals("admin"))
            {
                user.Active = "true";
            }
            else
            {
                user.Active = "false";
            }*/

            sql = "INSERT INTO dbo.Users VALUES ('" + user.Username + "','" + user.Password + "','" + user.FirstName + "','" + user.LastName + "','" + user.Active + "','" + user.Type + "'";

            if (user.Role != null)
            {
                sql = sql+",'"+ user.Role + "');";
            }
            else
            {
                sql = sql+");";
            }

            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                command.Dispose();
                cnn.Close();
                return "worked";
            }
            catch (Exception ex)
            {
                return "Failed to create new user " + ex;
            }
        }

        public bool UserExists(string username)
        {
            bool exists = false;
            string sql = "SELECT Username FROM dbo.Users WHERE Username = '"+username+"';";
            SqlDataReader dataReader;

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

        public string DeleteAppointment(string id)
        {
            String sql = "DELETE FROM dbo.Appointment WHERE Id='"+id+"'";
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
    }
}