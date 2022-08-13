using FoodLabellingSystem_Service.Auth.AuthMVC.Models;
using FoodLabellingSystem_Service.Other;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FoodLabellingSystem_Service.Auth.AuthMVC.Persistence
{
    public class UserDAO : IUserDAO
    {

        private IConfiguration _configuration;

        public UserDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<IUser> GetAll()
        {
            List<IUser> users = new List<IUser>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("select * from AppUser", connection);
                    command.CommandType = CommandType.Text;
                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        string firstName = dataReader.GetString(0);
                        string lastName = dataReader.GetString(1);
                        string companyName = dataReader.GetString(2);
                        string userName = dataReader.GetString(3).TrimEnd();
                        string password = dataReader.GetString(4);
                        string email = dataReader.GetString(5);
                        string phone = dataReader.GetString(6);
                        StatusType status = Enum.Parse<StatusType>(dataReader.GetString(7));
                        RoleType role = Enum.Parse<RoleType>(dataReader.GetString(8));
                        bool emailConfirmed = dataReader.GetBoolean(9);
                        bool phoneConfirmed = dataReader.GetBoolean(10);


                        users.Add(new User(firstName, lastName, companyName, userName, password, email, phone, status, role,emailConfirmed,phoneConfirmed));
                    }
                    dataReader.Close();
                }
            }
            return users;
        }
        public QueryResult Add(string firstName, string lastName, string companyName, string userName, string password, string email, string phone, string status, string role)
        {

            QueryResult queryResult = new QueryResult();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("insert into AppUser (firstName,lastName,companyName, userName, " +
                        " password ,email,phone,status ,role) values " +
                        "(@firstName,@lastName,@companyName, @userName, @password,@email,@phone,@status ,@role)", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@companyName", companyName);
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@role", role);

                    try
                    {
                        int res = command.ExecuteNonQuery();

                        if (res > 0)
                        {
                            queryResult.Result = QueryResultType.SUCCEED;
                            queryResult.Message = "Row(s) added";
                        }
                    }
                    catch (SqlException e)
                    {

                        queryResult.Message = " Error Code: " + e.ErrorCode.ToString() + " " + e.Message;
                        queryResult.Result = QueryResultType.FAILED;
                    }

                }
                connection.Close();
            }

            return queryResult;
        }
        public QueryResult Remove(string userName)
        {

            QueryResult queryResult = new QueryResult();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("delete from AppUser where userName = @userName", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@userName", userName);
                  

                    try
                    {
                        int res = command.ExecuteNonQuery();

                        if (res > 0)
                        {
                            queryResult.Result = QueryResultType.SUCCEED;
                            queryResult.Message = "Row(s) updated.";
                        }
                        else
                        {

                            queryResult.Result = QueryResultType.NOTHING_CHANGED;
                            queryResult.Message = "Nothing updated.";
                        }
                    }
                    catch (SqlException e)
                    {

                        queryResult.Message = " Error Code: " + e.ErrorCode.ToString() + " " + e.Message;
                        queryResult.Result = QueryResultType.FAILED;
                    }

                }
                connection.Close();
            }

            return queryResult;
        }
        public QueryResult Update(string firstName, string lastName, string companyName, string userName, string password, string email, string phone, string status, string role)
        {

            QueryResult queryResult = new QueryResult();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("update AppUser set firstName = @firstName, lastName = @lastName, companyName = @companyName, password = @password,  email = @email," +
                        " phone = @phone, status = @status, role = @role " +
                        " where userName = @userName;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@companyName", companyName);
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@role", role);
                    try
                    {
                        int res = command.ExecuteNonQuery();

                        if (res > 0)
                        {
                            queryResult.Result = QueryResultType.SUCCEED;
                            queryResult.Message = "Row(s) updated.";
                        }
                        else
                        {

                            queryResult.Result = QueryResultType.NOTHING_CHANGED;
                            queryResult.Message = "Nothing updated.";
                        }
                    }
                    catch (SqlException e)
                    {

                        queryResult.Message = " Error Code: " + e.ErrorCode.ToString() + " " + e.Message;
                        queryResult.Result = QueryResultType.FAILED;
                    }

                }
                connection.Close();
            }

            return queryResult;
        }
        public User GetByEmail(string email)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("select * from AppUser where email=@email;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@email", email);

                    SqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.Read())
                    {
                        return new User(dataReader.GetString(0), dataReader.GetString(1),
                            dataReader.GetString(2), dataReader.GetString(3).TrimEnd(),
                            dataReader.GetString(4), dataReader.GetString(5),
                             dataReader.GetString(6),
                            Enum.Parse<StatusType>(dataReader.GetString(7)),
                             Enum.Parse<RoleType>(dataReader.GetString(8)),
                             dataReader.GetBoolean(9),
                             dataReader.GetBoolean(10)
                             );
                    }
                    connection.Close();
                }
                return new User();
            }

        }
        public User GetByPhone(string phone)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("select * from AppUser where phone=@phone;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@phone", phone);

                    SqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.Read())
                    {
                        return new User(dataReader.GetString(0), dataReader.GetString(1),
                            dataReader.GetString(2), dataReader.GetString(3).TrimEnd(),
                            dataReader.GetString(4), dataReader.GetString(5),
                             dataReader.GetString(6),
                            Enum.Parse<StatusType>(dataReader.GetString(7)),
                             Enum.Parse<RoleType>(dataReader.GetString(8)),
                             dataReader.GetBoolean(9),
                             dataReader.GetBoolean(10)
                             );
                    }
                    connection.Close();
                }
                return new User();
            }

        }
        public User GetByUserName(string userName)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("select * from AppUser where userName=@userName;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@userName", userName);

                    SqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.Read())
                    {
                        return new User( dataReader.GetString(0), dataReader.GetString(1),
                            dataReader.GetString(2), dataReader.GetString(3).TrimEnd(),
                            dataReader.GetString(4), dataReader.GetString(5),
                             dataReader.GetString(6),
                              Enum.Parse<StatusType>(dataReader.GetString(7)),
                            Enum.Parse<RoleType>(dataReader.GetString(8)),
                            dataReader.GetBoolean(9),
                            dataReader.GetBoolean(10));
                    }
                    connection.Close();
                }
                return new User();
            }

        }
        public QueryResult ConfirmEmail(string userName) {
            QueryResult queryResult = new QueryResult();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("update AppUser set emailConfirmed = @emailConfirmed" +
                        " where userName = @userName;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@emailConfirmed", 1);
                    command.Parameters.AddWithValue("@userName", userName);
                    try
                    {
                        int res = command.ExecuteNonQuery();

                        if (res > 0)
                        {
                            queryResult.Result = QueryResultType.SUCCEED;
                            queryResult.Message = "Row(s) updated.";
                        }
                        else
                        {

                            queryResult.Result = QueryResultType.NOTHING_CHANGED;
                            queryResult.Message = "Nothing updated.";
                        }
                    }
                    catch (SqlException e)
                    {

                        queryResult.Message = " Error Code: " + e.ErrorCode.ToString() + " " + e.Message;
                        queryResult.Result = QueryResultType.FAILED;
                    }

                }
                connection.Close();
            }

            return queryResult;
        }
        public QueryResult ConfirmPhone(string userName) {
            QueryResult queryResult = new QueryResult();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("update AppUser set phoneConfirmed = @phoneConfirmed" +
                        " where userName = @userName;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@phoneConfirmed", 1);
                    command.Parameters.AddWithValue("@userName", userName);
                    try
                    {
                        int res = command.ExecuteNonQuery();

                        if (res > 0)
                        {
                            queryResult.Result = QueryResultType.SUCCEED;
                            queryResult.Message = "Row(s) updated.";
                        }
                        else
                        {

                            queryResult.Result = QueryResultType.NOTHING_CHANGED;
                            queryResult.Message = "Nothing updated.";
                        }
                    }
                    catch (SqlException e)
                    {

                        queryResult.Message = " Error Code: " + e.ErrorCode.ToString() + " " + e.Message;
                        queryResult.Result = QueryResultType.FAILED;
                    }

                }
                connection.Close();
            }

            return queryResult;
        }
        public QueryResult UpdatePassword(string password, string userName)
        {

            QueryResult queryResult = new QueryResult();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("update AppUser set password = @password where userName = @userName;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@password", password);
                    try
                    {
                        int res = command.ExecuteNonQuery();

                        if (res > 0)
                        {
                            queryResult.Result = QueryResultType.SUCCEED;
                            queryResult.Message = "Row(s) updated.";
                        }
                        else
                        {

                            queryResult.Result = QueryResultType.NOTHING_CHANGED;
                            queryResult.Message = "Nothing updated.";
                        }
                    }
                    catch (SqlException e)
                    {

                        queryResult.Message = " Error Code: " + e.ErrorCode.ToString() + " " + e.Message;
                        queryResult.Result = QueryResultType.FAILED;
                    }

                }
                connection.Close();
            }

            return queryResult;
        }
    }
}