using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;


namespace FoodLabellingSystem_Service.Persistence
{
    public class IngredientTypeDAO:IIngredientTypeDAO
    {
        private readonly IConfiguration _configuration;

        public IngredientTypeDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<IngredientType> GetAll()
        {
            List<IngredientType> ingredientTypes = new List<IngredientType>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand sqlCommand = new SqlCommand("select * from IngredientType;", connection);
                    sqlCommand.CommandType = CommandType.Text;
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read()) { 
                    string ingredientTypeId = dataReader.GetString(0);
                    string member = dataReader.GetString(1);
                        ingredientTypes.Add(new IngredientType(ingredientTypeId, member));
                    }
                    dataReader.Close();
                }
                connection.Close();
            }
            return ingredientTypes;
        }
        public QueryResult Add(string ingredientTypeId, string member)
        {
            int result = 0;
            QueryResult queryResult = new QueryResult();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb"))) {
                connection.Open();
                if (connection.State == ConnectionState.Open) {

                    SqlCommand command = new SqlCommand("insert into IngredientType(IngredientTypeId,Member)values(@ingredientTypeId,@member);", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ingredientTypeId", ingredientTypeId);
                    command.Parameters.AddWithValue("@member", member);
                    try
                    {
                        result = command.ExecuteNonQuery();

                        if (result > 0) {
                            queryResult.Message = "Row(s) added";
                            queryResult.Result = QueryResultType.SUCCEED;
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
        public QueryResult Remove(string ingredientTypeId)
        {
            int result;
            QueryResult queryResult = new QueryResult();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("delete from IngredientType where IngredientTypeId=@ingredientTypeId;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ingredientTypeId", ingredientTypeId);
                   
                    try
                    {
                        result = command.ExecuteNonQuery();
                        if (result > 0) {

                            queryResult.Message = "Row(s) removed";
                            queryResult.Result = QueryResultType.SUCCEED;
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
        public QueryResult Update(string ingredientTypeId, string member)
        {
            int result;
            QueryResult queryResult = new QueryResult();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("update IngredientType set Member=@member where IngredientTypeId=@ingredientTypeId;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ingredientTypeId", ingredientTypeId);
                    command.Parameters.AddWithValue("@member", member);
                    try
                    {
                        result = command.ExecuteNonQuery();
                        
                        if (result>0) {
                            queryResult.Message = "row(s) updated";
                            queryResult.Result = QueryResultType.SUCCEED;
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

        public IngredientType GetById(string ingredientTypeId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("select * from IngredientType where IngredientTypeId=@ingredientTypeId;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ingredientTypeId", ingredientTypeId);


                    SqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.Read())
                    {
                        return new IngredientType(dataReader.GetString(0), dataReader.GetString(1));
                    }
                    dataReader.Close();
                }
                connection.Close();
            }
            return new IngredientType();
        }

       
    }
}

