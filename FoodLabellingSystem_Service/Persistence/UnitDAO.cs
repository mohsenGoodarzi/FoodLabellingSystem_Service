using FoodLabellingSystem_Service.Models;
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

        public List<Unit> getAll()
        {
            List<Unit> units = new List<Unit> ();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {
                
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("select * from Unit", connection);
                    SqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader != null) {

                        while (dataReader.Read())
                        {
                            string unitId = dataReader.GetString(0);
                            double toGram = dataReader.GetDouble(1);
                            units.Add(new Unit(unitId, toGram));
                        }
                        dataReader.Close();
                    }
                    
                }
                connection.Close();
            }

            return units;

        }
        public QueryResult Add(string unitId,double toGram)
        {

            QueryResult result = new QueryResult();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("insert into Unit (UnitId,ToGram)values(@unitId,@toGram)", connection);
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
                        result.Message = " Error Code: " + e.ErrorCode.ToString() + " " + e.Message;
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

                    SqlCommand command = new SqlCommand("delete from Unit where UnitId = @unitId", connection);
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

                        result.Message = " Error Code: " + e.ErrorCode.ToString() + " " + e.Message;
                        result.Result = QueryResultType.FAILED;
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
                    SqlCommand command = new SqlCommand("update Unit set ToGram = @toGram where UnitId = @unitId", connection);
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
                        result.Message = " Error Code: " + e.ErrorCode.ToString() + " " + e.Message;
                        result.Result = QueryResultType.FAILED;
                    }

                }

            }
            return result;
        }
        public Unit GetById(string unitId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("select * from Unit where UnitId=@unitId;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@unitId", unitId);

                   
                        SqlDataReader dataReader = command.ExecuteReader();

                        if (dataReader.Read())
                        {
                            return new Unit(dataReader.GetString(0), dataReader.GetDouble(1));
                        }
                    dataReader.Close();

                    }
                connection.Close();
                }

            return new Unit();
        }
    }
}

