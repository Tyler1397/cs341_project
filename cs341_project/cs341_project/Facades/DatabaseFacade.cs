using cs341_project.Models;
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


        public string GetUser(LoginCredentials login)
        {
            string sql = "SELECT * FROM dbo.Users WHERE Username = '"+login.Username+"' AND Password = '"+login.Password+"'";
            SqlDataReader dataReader;
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    return dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + " - " + dataReader.GetValue(2);
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                return "Error with database";
            }
            return "Didnt work";
        }

    }
}