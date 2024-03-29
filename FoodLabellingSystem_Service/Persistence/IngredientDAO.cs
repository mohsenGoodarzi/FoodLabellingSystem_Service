﻿using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace FoodLabellingSystem_Service.Persistence
{
    public class IngredientDAO : IIngredientDAO
    {
        private readonly IConfiguration _configuration;

        public IngredientDAO(IConfiguration configuration)
        {

            _configuration = configuration;

        }
        public QueryResult Add(string ingredientId, string description, string ingredientTypeId, string unitId, double amount, double fat, double carbs, double protein, double calory, string warningId)
        {
            QueryResult queryResult = new QueryResult();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand sqlCommand = connection.CreateCommand();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "insert into Ingredient (ingredientId,description,"+
                        "ingredientTypeId,unitId,amount,fat,carbs,protein,calory,warningId)"+
                        "values(@ingredientId,@description,@ingredientTypeId,@unitId,@amount,"+
                        "@fat,@carbs,@protein,@calory,@warningId)";
                    
                    sqlCommand.Parameters.AddWithValue("@ingredientId", ingredientId);
                    sqlCommand.Parameters.AddWithValue("@description", description);
                    sqlCommand.Parameters.AddWithValue("@ingredientTypeId", ingredientTypeId);
                    sqlCommand.Parameters.AddWithValue("@unitId", unitId);
                    sqlCommand.Parameters.AddWithValue("@amount", amount);
                    sqlCommand.Parameters.AddWithValue("@fat", fat);
                    sqlCommand.Parameters.AddWithValue("@carbs", carbs);
                    sqlCommand.Parameters.AddWithValue("@protein", protein);
                    sqlCommand.Parameters.AddWithValue("@calory", calory);
                    sqlCommand.Parameters.AddWithValue("@warningId", warningId);


                    try
                    {
                        int result = sqlCommand.ExecuteNonQuery();
                        if (result > 0)
                        {
                            queryResult.Result = QueryResultType.SUCCEED;
                            queryResult.Message = "Row(s) Added.";
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

        public List<Ingredient> GetAll()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb"))) { 
            
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand sqlCommand = connection.CreateCommand();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "select * from Ingredient;";
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string ingredientId = dataReader.GetString(0);
                        string description = dataReader.GetString(1);
                        string ingredientTypeId = dataReader.GetString(2);
                        string unitId = dataReader.GetString(3);
                        double amount = dataReader.GetDouble(4);
                        double fat = dataReader.GetDouble(5);
                        double carbs = dataReader.GetDouble(6);
                        double protein = dataReader.GetDouble(7);
                        double calory = dataReader.GetDouble(8);
                        string warningId = dataReader.GetString(9);

                        // database makes sure that nither of them is null.
                        ingredients.Add(new Ingredient(ingredientId, description, ingredientTypeId, unitId, amount, fat, carbs, protein, calory, warningId));
                       
                    }
                }
            connection.Close();
            }
            return ingredients;
        }

        public Ingredient GetById(string ingredientId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("select * from Ingredient where IngredientId=@ingredientId;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ingredientId", ingredientId);


                    SqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.Read())
                    {
                        return new Ingredient(dataReader.GetString(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3), dataReader.GetDouble(4), dataReader.GetDouble(5), dataReader.GetDouble(6), dataReader.GetDouble(7), dataReader.GetDouble(8), dataReader.GetString(9));
                    }
                    dataReader.Close();
                }
                connection.Close();
            }
            return new Ingredient();
        }

        public QueryResult Remove(string ingredientId)
        {
            QueryResult queryResult = new QueryResult();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb"))) {

                connection.Open();
                if (connection.State == ConnectionState.Open) { 
                SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "delete from ingredient where IngredientId = @ingredientId;";
                    command.Parameters.AddWithValue("@ingredientId", ingredientId);
                    try
                    {
                        int result = command.ExecuteNonQuery();
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
                    catch (SqlException e) {
                        queryResult.Message = " Error Code: " + e.ErrorCode.ToString() + " " + e.Message;
                        queryResult.Result = QueryResultType.FAILED;
                    }
                    
                }
                connection.Close();
            }
            return queryResult;
        }

        public QueryResult Update(string ingredientId, string description, string ingredientTypeId, string unitId, double amount, double fat, double carbs, double protein, double calory, string warningId)
        {
            QueryResult queryResult = new QueryResult();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand sqlCommand = connection.CreateCommand();
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "update Ingredient set " +
                        " description = @description, ingredientTypeId = @ingredientTypeId," +
                        " unitId = @unitId, amount = @amount, fat = @fat, carbs = @carbs," +
                        " protein = @protein, calory = @calory, warningId = @warningId where ingredientId = @ingredientId;";

                    sqlCommand.Parameters.AddWithValue("@ingredientId", ingredientId);
                    sqlCommand.Parameters.AddWithValue("@description", description);
                    sqlCommand.Parameters.AddWithValue("@ingredientTypeId", ingredientTypeId);
                    sqlCommand.Parameters.AddWithValue("@unitId", unitId);
                    sqlCommand.Parameters.AddWithValue("@amount", amount);
                    sqlCommand.Parameters.AddWithValue("@fat", fat);
                    sqlCommand.Parameters.AddWithValue("@carbs", carbs);
                    sqlCommand.Parameters.AddWithValue("@protein", protein);
                    sqlCommand.Parameters.AddWithValue("@calory", calory);
                    sqlCommand.Parameters.AddWithValue("@warningId", warningId);
                    try
                    {
                        int result = sqlCommand.ExecuteNonQuery();
                        if (result > 0)
                        {
                            queryResult.Result = QueryResultType.SUCCEED;
                            queryResult.Message = "Row(s) updated.";
                        }
                        else {
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
