using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FoodLabellingSystem_Service.Persistence
{
    public class CuisineTypeDAO:ICuisineTypeDAO
    {
        public readonly IConfiguration _configuration;

        public CuisineTypeDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<CuisineType>  GetAll() {

            List<CuisineType> warnings = new List<CuisineType>();
           
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb"))) {

                connection.Open();
                if (connection.State == ConnectionState.Open) {

                    SqlCommand sqlCommand = new SqlCommand("select * from CuisineType", connection);
                    sqlCommand.CommandType = CommandType.Text;
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        string cuisineTypeId = dataReader.GetString(0);
                        string member = dataReader.GetString(1);
                        warnings.Add(new CuisineType(cuisineTypeId, member));
                    }
                    dataReader.Close();
                }
                connection.Close();
            }
                return warnings;
        } 
        public QueryResult Add(string cuisineTypeId, string member) {
            QueryResult queryResult = new QueryResult();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand sqlCommand = new SqlCommand("insert into CuisineType (CuisineTypeId, Member) values (@cuisineTypeId, @member);", connection);
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.AddWithValue("@cuisineTypeId", cuisineTypeId);
                    sqlCommand.Parameters.AddWithValue("@member", member);

                    try
                    {
                        int res = sqlCommand.ExecuteNonQuery();

                        if (res > 0)
                        {

                            queryResult.Result = QueryResultType.SUCCEED;
                            queryResult.Message = "Row(s) added.";
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
        public QueryResult Remove(string cuisineTypeId)
        {
            QueryResult queryResult = new QueryResult();


            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand sqlCommand = new SqlCommand("delete from CuisineType where CuisineTypeId = @cuisineTypeId;", connection);
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.AddWithValue("@cuisineTypeId", cuisineTypeId);
                   

                    try
                    {
                        int res = sqlCommand.ExecuteNonQuery();

                        if (res > 0)
                        {

                            queryResult.Result = QueryResultType.SUCCEED;
                            queryResult.Message = "Row(s) added.";
                        }
                        else
                        {

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
        public QueryResult Update(string cuisineTypeId, string member) {
            QueryResult queryResult = new QueryResult();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand sqlCommand = new SqlCommand("update CuisineType set Member = @member where CuisineTypeId = @cuisineTypeId;", connection);
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.AddWithValue("@cuisineTypeId", cuisineTypeId);
                    sqlCommand.Parameters.AddWithValue("@member", member);

                    try
                    {
                        int res = sqlCommand.ExecuteNonQuery();

                        if (res > 0)
                        {

                            queryResult.Result = QueryResultType.SUCCEED;
                            queryResult.Message = "Row(s) added.";
                        }
                        else
                        {

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

        public CuisineType GetById(string cuisineTypeId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("serverDb")))
            {

                connection.Open();
                if (connection.State == ConnectionState.Open)
                {

                    SqlCommand command = new SqlCommand("select * from CuisineType where CuisineTypeId=@cuisineTypeId;", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@cuisineTypeId", cuisineTypeId);


                    SqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.Read())
                    {
                        return new CuisineType(dataReader.GetString(0), dataReader.GetString(1));
                    }
                    dataReader.Close();

                }
                connection.Close();
            }
            return new CuisineType();
        }
    }
}

