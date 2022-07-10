﻿using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FoodLabellingSystem_Service.Persistence
{
    public class CuisineTypeADO:ICuisineTypeADO
    {
        public readonly IConfiguration _configuration;

        public CuisineTypeADO(IConfiguration configuration)
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
                        string? cuisineTypeId = dataReader["cuisineTypeId"].ToString();
                        string? member = dataReader["member"].ToString();

                        // database makes sure that nither of them is null.
                        if (cuisineTypeId != null && member != null)
                        warnings.Add(new CuisineType(cuisineTypeId, member));
                    }
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
                        queryResult.Result = QueryResultType.FAILED;
                        queryResult.Message = e.Message;
                        queryResult.ErrorCode = e.ErrorCode;
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

                    SqlCommand sqlCommand = new SqlCommand("delete CuisineType where CuisineTypeId = @cuisineTypeId;", connection);
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
                        queryResult.Result = QueryResultType.FAILED;
                        queryResult.Message = e.Message;
                        queryResult.ErrorCode = e.ErrorCode;
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
                        queryResult.Result = QueryResultType.FAILED;
                        queryResult.Message = e.Message;
                        queryResult.ErrorCode = e.ErrorCode;
                    }

                }
                connection.Close();
            }
            return queryResult;
        }
    }
}
