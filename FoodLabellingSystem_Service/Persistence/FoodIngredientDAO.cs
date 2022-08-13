using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using System.Data.SqlClient;

namespace FoodLabellingSystem_Service.Persistence
{
    public class FoodIngredientDAO : IFoodIngredientDAO
    {
        private readonly IConfiguration _configuration;
        public FoodIngredientDAO(IConfiguration configuration) { 
        _configuration = configuration;
        }
        public QueryResult Add(string foodId, string ingredientId, string unitId, double amount)
        {
            QueryResult queryResult = new QueryResult();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb"))) { 
            connection.Open();
                if (connection.State == System.Data.ConnectionState.Open) {
                    SqlCommand command = new SqlCommand("insert into Food_Ingredient "+
                        "(foodId,ingredientId,unitId,amount) "+
                        "values (@foodId,@ingredientId,@unitId,@amount)", connection);
                    command.Parameters.AddWithValue("@foodId", foodId);
                    command.Parameters.AddWithValue("@ingredientId", ingredientId);
                    command.Parameters.AddWithValue("@unitId", unitId);
                    command.Parameters.AddWithValue("@amount", amount);
                  
                    try
                    {
                        int result = command.ExecuteNonQuery();

                        if (result > 0) {
                            queryResult.Result = QueryResultType.SUCCEED;
                            queryResult.Message = "Row(s) added.";

                        }
                    }
                    catch (SqlException e) { 
                        queryResult.Message = " Error Code: "+ e.ErrorCode.ToString()+ " "+ e.Message;
                        queryResult.Result = QueryResultType.FAILED;
                    
                    }
                
                }
                connection.Close();
            }
            return queryResult;
        }

        public List<FoodIngredient> GetAll()
        {
            List<FoodIngredient> foodIngredients = new List<FoodIngredient>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open) { 
                SqlCommand command = connection.CreateCommand();
                    command.CommandText = "select * from Food_Ingredient;";
                    command.CommandType = System.Data.CommandType.Text;
                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read()) {
                        foodIngredients.Add(new FoodIngredient(
                            dataReader.GetString(0), dataReader.GetString(1),
                            dataReader.GetString(2), dataReader.GetDouble(3),
                            dataReader.GetDouble(4), dataReader.GetDouble(5),
                            dataReader.GetDouble(6), dataReader.GetDouble(7)
                            ));
                    }
                dataReader.Close();
                }
                connection.Close() ;    
            }
            return foodIngredients;
        }

        public FoodIngredient GetById(string foodId, string ingredientId)
        {
           FoodIngredient foodIngredients = new FoodIngredient();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "select * from Food_Ingredient where FoodId = @foodId and IngredientId = @ingredientId";
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("@foodId", foodId);
                    command.Parameters.AddWithValue("@ingredientId", ingredientId);
                    SqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.Read()) {

                        return new FoodIngredient(
                                dataReader.GetString(0), dataReader.GetString(1),
                                dataReader.GetString(2), dataReader.GetDouble(3),
                                dataReader.GetDouble(4), dataReader.GetDouble(5),
                                dataReader.GetDouble(6), dataReader.GetDouble(7)
                                );
                    }
                    dataReader.Close();
                        
                }
                connection.Close();
            }
            return foodIngredients;
        }

        public QueryResult Remove(string foodId, string ingredientId)
        {
            QueryResult queryResult = new QueryResult();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("delete from Food_Ingredient " +
                        " where FoodId = @foodId and IngredientId = @ingredientId", connection);
                    command.Parameters.AddWithValue("@foodId", foodId);
                    command.Parameters.AddWithValue("@ingredientId", ingredientId);
                   
                    try
                    {
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            queryResult.Result = QueryResultType.SUCCEED;
                            queryResult.Message = "Row(s) deleted.";
                        }
                        else
                        {
                            queryResult.Result = QueryResultType.NOTHING_CHANGED;
                            queryResult.Message = "Nothing deleted.";
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

        public QueryResult Update(string foodId, string ingredientId, string unitId, double amount)
        {
            QueryResult queryResult = new QueryResult();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("update Food_Ingredient set " +
                        "UnitId = @unitId, Amount = @amount where "+
                        "foodId = @foodId and ingredientId = @ingredientId", connection);
                    
                    command.Parameters.AddWithValue("@foodId", foodId);
                    command.Parameters.AddWithValue("@ingredientId", ingredientId);
                    command.Parameters.AddWithValue("@unitId", unitId);
                    command.Parameters.AddWithValue("@amount", amount);
                   
                    try
                    {
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            queryResult.Result = QueryResultType.SUCCEED;
                            queryResult.Message = "Row(s) added.";

                        }
                        else { 
                        queryResult.Result = QueryResultType.NOTHING_CHANGED;
                            queryResult.Message = "Nothing added.";
                        
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
