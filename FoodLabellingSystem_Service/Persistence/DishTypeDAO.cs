using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FoodLabellingSystem_Service.Persistence
{
    public class DishTypeDAO : Interfaces.IDishTypeDAO
    {
        private IConfiguration _configuration;

        public DishTypeDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<DishType> GetAll() {

            List<DishType> dishTypes = new List<DishType>();
            
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb"))) {
                connection.Open();
                if (connection.State == ConnectionState.Open) {

                    SqlCommand command = new SqlCommand("select * from DishType", connection);
                    command.CommandType = CommandType.Text;
                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        string dishTypeId = dataReader.GetString(0);
                        string member = dataReader.GetString(0);

                            dishTypes.Add(new DishType(dishTypeId, member));
                    }
                    dataReader.Close();
                }
            }
            return dishTypes;
        }
        public QueryResult Add(string dishTypeId, string member) {

            QueryResult queryResult = new QueryResult();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb"))) {

                connection.Open();
                if (connection.State == ConnectionState.Open) {

                    SqlCommand command = new SqlCommand("insert into DishType (DishTypeId,Member) values (@dishTypeId,@member)", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@dishTypeId", dishTypeId);
                    command.Parameters.AddWithValue("@member", member);
                    try
                    {
                        int res = command.ExecuteNonQuery();

                        if (res > 0)
                        {
                            queryResult.Result = QueryResultType.SUCCEED;
                            queryResult.Message = "Row(s) added";
                        }
                    }
                    catch (SqlException e) {

                        queryResult.Message = " Error Code: " + e.ErrorCode.ToString() + " " + e.Message;
                        queryResult.Result = QueryResultType.FAILED;
                    }

                }
                connection.Close();
            }

                return queryResult;
        }
        public QueryResult Remove(string dishTypeId)
        {

            QueryResult queryResult = new QueryResult();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("delete from DishType where DishTypeId = @dishTypeId)", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@dishTypeId", dishTypeId);

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
        public QueryResult Update(string dishTypeId, string member)
        {

            QueryResult queryResult = new QueryResult();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("update DishType set Member = @member where DishTypeId = @dishTypeId)", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@dishTypeId", dishTypeId);
                    command.Parameters.AddWithValue("@member", member);
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

        public DishType GetById(string dishTypeId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("select * from DishType where DishTypeId=@dishTypeId;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@dishTypeId", dishTypeId);


                    SqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.Read())
                    {
                        return new DishType(dataReader.GetString(0), dataReader.GetString(1));
                    }
                    dataReader.Close();

                }
                connection.Close();
            }
            return new DishType();
        }
    }
}
