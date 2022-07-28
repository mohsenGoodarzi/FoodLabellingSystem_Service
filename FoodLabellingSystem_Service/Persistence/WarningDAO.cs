using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using System.Data;
using System.Data.SqlClient;
namespace FoodLabellingSystem_Service.Persistence
{
    public class WarningDAO:IWarningDAO
    {
        private readonly IConfiguration _configuration;
  
        
        public WarningDAO(IConfiguration configuration)

        {
            _configuration = configuration;
        }

        public List<Warning> getAll() {

            List<Warning> warnings = new List<Warning>();
            
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb"))) {


                connection.Open();
                if (connection.State == ConnectionState.Open) {

                    SqlCommand command = new SqlCommand("select * from Warning", connection);
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read()) {

                        string warningId = dataReader.GetString(0);
                        string message = dataReader.GetString(1);
                        string warningType = dataReader.GetString(2);
                        warnings.Add(new Warning(warningId, message, warningType));
                    }
                dataReader.Close();
                }
                connection.Close();
            }

            return warnings;

        }
        public QueryResult Add(string warningId, string message, string warningType) {

            QueryResult result = new QueryResult();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb"))) {

                connection.Open();
                if (connection.State == ConnectionState.Open) {

                    SqlCommand command = new SqlCommand("insert into warning (WarningId,Message,WarningType)values(@warningId,@message,@warningType)", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@warningId", warningId);
                    command.Parameters.AddWithValue("@message", message);
                    command.Parameters.AddWithValue("@warningType", warningType );

                    try
                    {

                        int res = command.ExecuteNonQuery();

                        if (res > 0)
                        {
                            result.Result = QueryResultType.SUCCEED;
                            result.Message = "Row(s) added";
                        }
                    }
                    catch (SqlException e) {
                        result.Message = " Error Code: " + e.ErrorCode.ToString() + " " + e.Message;
                        result.Result = QueryResultType.FAILED;
                    }
                    
                }

            }
            return result;
        }
        public QueryResult Remove(string WarningId) {

            QueryResult result = new QueryResult();
            
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("delete from warning where WarningId=@warningId;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@warningId", WarningId);

                    try
                    {
                        int res = command.ExecuteNonQuery();

                        if (res > 0)
                        {
                            result.Message = " Rwo(s) removed";
                            result.Result = QueryResultType.SUCCEED;
                        }
                        else
                        {
                            result.Result = QueryResultType.NOTHING_CHANGED;
                            result.Message = "Nothing removed.";
                        }
                    }
                    catch (SqlException e) {

                        result.Message = " Error Code: " + e.ErrorCode.ToString() + " " + e.Message;
                        result.Result = QueryResultType.FAILED;
                    }
                    
                }

            }
            return result;
        }
        public QueryResult Update(string warningId,string message,string warningType)
        {

            QueryResult result = new QueryResult();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("update warning set Message = @message,WarningType = @warningType where WarningId = @warningId", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@warningId", warningId);
                    command.Parameters.AddWithValue("@message", message);
                    command.Parameters.AddWithValue("@warningType", warningType);

                    try
                    {
                        int res = command.ExecuteNonQuery();

                        if (res > 0)
                        {
                            result.Result = QueryResultType.SUCCEED;
                        }
                        else
                        {
                            result.Result = QueryResultType.NOTHING_CHANGED;
                            result.Message = "Nothing updated.";
                        }
                    }
                    catch (SqlException e) {

                        result.Message = " Error Code: " + e.ErrorCode.ToString() + " " + e.Message;
                        result.Result = QueryResultType.FAILED;
                    }
                    
                }

            }
            return result;
        }

        public Warning getById(string warningId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("select * from Warning where WarningId=@warningId;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@warningId", warningId);


                    SqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.Read())
                    {
                        return new Warning(dataReader.GetString(0), dataReader.GetString(1),dataReader.GetString(2));
                    }
                    dataReader.Close();

                }
                connection.Close();
            }
            return new Warning();
        }
    }
}

