using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace FoodLabellingSystem_Service.Persistence
{
    public class FoodDAO:IFoodDAO
    {
        private readonly IConfiguration _configuration;
        public FoodDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<Food> GetAll() {

            List<Food> foods = new List<Food>();
            SqlDataReader? dataReader = null;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb"))) { 

                connection.Open();
                if (connection.State == ConnectionState.Open) { 
                SqlCommand command = connection.CreateCommand();
                    command.CommandText = "select * from Food;";
                    command.CommandType = CommandType.Text;
                    dataReader = command.ExecuteReader();
                   
                    while (dataReader.Read())
                    {// description in database cannot be null.
                        string foodId = dataReader.GetString(0);
                        string description = dataReader.GetString(1);
                        string dishType = dataReader.GetString(2);
                        string cuisineType = dataReader.GetString(3);
                        string foodType = dataReader.GetString(4);
                        
                       foods.Add(new Food(foodId, description, dishType, cuisineType, foodType));
                    }
                    dataReader.Close();
                }
            
            connection.Close();
            }
            return foods;
        }

        public QueryResult Add(string foodId, string description, string dishType, string cuisineType, string foodType) {
            QueryResult queryResult = new QueryResult();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb"))) {

                connection.Open();
                if (connection.State==ConnectionState.Open) { 
                
                    SqlCommand sqlCommand = connection.CreateCommand();
                    sqlCommand.CommandText = "insert into Food(foodId,description,dishType,cuisineType,foodType) values(@foodId,@description,@dishType,@cuisineType,@foodType)";
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.AddWithValue("@foodId", foodId);
                    sqlCommand.Parameters.AddWithValue("@description", description);
                    sqlCommand.Parameters.AddWithValue("@dishType", dishType);
                    sqlCommand.Parameters.AddWithValue("@cuisineType", cuisineType);
                    sqlCommand.Parameters.AddWithValue("@foodType", foodType);
                    
                    try
                    {
                        int result = sqlCommand.ExecuteNonQuery();
                        if (result > 0)
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
        public QueryResult Remove(string foodId)
        {
            QueryResult queryResult = new QueryResult();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand sqlCommand = connection.CreateCommand();
                    sqlCommand.CommandText = "delete from Food where foodId = @foodId;";
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.AddWithValue("@foodId", foodId);
                    
                    try
                    {
                        int result = sqlCommand.ExecuteNonQuery();
                        if (result > 0)
                        {
                            queryResult.Result = QueryResultType.SUCCEED;
                            queryResult.Message = "Row(s) removed.";
                        }
                        else
                        {
                            queryResult.Result = QueryResultType.NOTHING_CHANGED;
                            queryResult.Message = "Nothing removed.";
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
        public QueryResult Update(string foodId, string description, string dishType, string cuisineType, string foodType)
        {
            QueryResult queryResult = new QueryResult();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand sqlCommand = connection.CreateCommand();
                    sqlCommand.CommandText = "update set Food foodId = @foodId,"+
                        " description = @description, dishType = @dishType,"+
                        " cuisineType = @cuisineType, foodType = @foodType;";
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.AddWithValue("@foodId", foodId);
                    sqlCommand.Parameters.AddWithValue("@description", description);
                    sqlCommand.Parameters.AddWithValue("@dishType", dishType);
                    sqlCommand.Parameters.AddWithValue("@cuisineType", cuisineType);
                    sqlCommand.Parameters.AddWithValue("@foodType", foodType);

                    try
                    {
                        int result = sqlCommand.ExecuteNonQuery();
                        if (result > 0)
                        {
                            queryResult.Result = QueryResultType.SUCCEED;
                            queryResult.Message = "Row(s) updated";
                        }
                        else {
                            queryResult.Result = QueryResultType.NOTHING_CHANGED;
                            queryResult.Message = "Nothing updated";
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

        public Food GetById(string foodId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("select * from Food where FoodId=@foodId;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@foodId", foodId);


                    SqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.Read())
                    {
                        return new Food(dataReader.GetString(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3), dataReader.GetString(4));
                    }
                    dataReader.Close();

                }
                connection.Close();
            }
          return  new Food();
        }
    }
}
 