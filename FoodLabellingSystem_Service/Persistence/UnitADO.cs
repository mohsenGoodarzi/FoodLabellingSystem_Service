using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace FoodLabellingSystem_Service.Persistence
{
    public class UnitDAO :IUnitDAO
    {
        private readonly IConfiguration _configuration;

        public UnitDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlDataReader? getAll()
        {
            SqlDataReader? sqlReader = null;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {
                
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("select * from Unit", connection);
                    sqlReader = command.ExecuteReader(); 
                }
                connection.Close();
            }

            return sqlReader;

        }
        public QueryResult Add(string unitId,double toGram)
        {

            QueryResult result = new QueryResult();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("insert into warning (UnitId,ToGram)values(@unitId,@toGram)", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@unitId", unitId);
                    command.Parameters.AddWithValue("@toGram", toGram);
                 
                    try
                    {

                        int res = command.ExecuteNonQuery();

                        if (res > 0)
                        {
                            result.Result = QueryResultType.SUCCEED;
                            result.Message = "Row(s) added";
                        }
                    }
                    catch (SqlException e)
                    {
                        result.Message = e.Message;
                        result.ErrorCode = e.ErrorCode;
                        result.Result = QueryResultType.FAILED;
                    }

                }

            }
            return result;
        }
        public QueryResult Remove(string unitId)
        {

            QueryResult result = new QueryResult();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("delete warning where UnitId=@unitId;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@unitId", unitId);

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
                    catch (SqlException e)
                    {

                        result.Message = e.Message;
                        result.Result = QueryResultType.FAILED;
                        result.ErrorCode = e.ErrorCode;
                    }

                }

            }
            return result;
        }
        public QueryResult Update(string unitId,double toGram)
        {

            QueryResult result = new QueryResult();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("update unit ToGram = @toGram where UnitId = @unitId", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@unitId",unitId);
                    command.Parameters.AddWithValue("@toGram", toGram);
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
                    catch (SqlException e)
                    {
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

