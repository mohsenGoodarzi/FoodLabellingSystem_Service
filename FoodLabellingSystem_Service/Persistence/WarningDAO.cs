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

        public SqlDataReader? getAll() {

            SqlDataReader? sqlReader = default ;
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb"))) {

                connection.Open();
                if (connection.State == ConnectionState.Open) {

                    SqlCommand command = new SqlCommand("select * from Warning", connection);
                    sqlReader = command.ExecuteReader();       
                }
                
                connection.Close();
            }

            return sqlReader;

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
                        result.Message = e.Message;
                        result.ErrorCode = e.ErrorCode;
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

                    SqlCommand command = new SqlCommand("delete warning where WarningId=@warningId;", connection);
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

                        result.Message = e.Message;
                        result.Result = QueryResultType.FAILED;
                        result.ErrorCode = e.ErrorCode;
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
                    SqlCommand command = new SqlCommand("update warning Message = @message,WarningType = @warningType where WarningId = @warningId", connection);
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

                        result.Message = e.Message;
                        result.Result = QueryResultType.FAILED;
                        result.ErrorCode = e.ErrorCode;
                    }
                    
                }

            }
            return result;
        }
    }
}

