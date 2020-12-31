using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TCG_Store_DAL.DTOs;

namespace TCG_Store_DAL.DataAccessControllers
{
    /// <summary>
    /// Data controller for sets
    /// </summary>
    public class SetDataController
    {
        public List<SetDTO> GetAllSets()
        {
            List<SetDTO> SetDTOs = new List<SetDTO>();

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand GetAllSetsCommand = new SqlCommand
            {
                CommandText = "GetAllSets",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlDataReader DataReader;

            DataReader = GetAllSetsCommand.ExecuteReader();

            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    SetDTO FoundSet = new SetDTO();
                    for (int index = 0; index < DataReader.FieldCount; index++)
                    {
                        FoundSet.SetID = int.Parse(DataReader[index].ToString());
                        FoundSet.GameID = int.Parse(DataReader[index].ToString());
                        FoundSet.SetCode = DataReader[index].ToString();
                        FoundSet.SetName = DataReader[index].ToString();
                    }
                    SetDTOs.Add(FoundSet);
                }
            }

            DataReader.Close();
            StoreConnection.Close();

            return SetDTOs;
        }

        public List<SetDTO> GetSetsByGame(int GameID)
        {
            List<SetDTO> SetsDTOsByGame = new List<SetDTO>();

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand GetSetsByGameCommand = new SqlCommand
            {
                CommandText = "GetSetsByGameID",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter GameIDParameter = new SqlParameter
            {
                ParameterName = "GameID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = GameID
            };
            GetSetsByGameCommand.Parameters.Add(GameIDParameter);

            SqlDataReader DataReader;

            DataReader = GetSetsByGameCommand.ExecuteReader();

            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    SetDTO FoundSet = new SetDTO();
                    for (int index = 0; index < DataReader.FieldCount; index++)
                    {
                        FoundSet.SetID = int.Parse(DataReader["SetID"].ToString());
                        FoundSet.GameID = int.Parse(DataReader["GameID"].ToString());
                        FoundSet.SetCode = DataReader["SetCode"].ToString();
                        FoundSet.SetName = DataReader["SetName"].ToString();
                    }
                    SetsDTOsByGame.Add(FoundSet);
                }
            }
            DataReader.Close();
            StoreConnection.Close();

            return SetsDTOsByGame;
        }

        public int AddNonExistingSetsToDataBase(SetDTO NewSet)
        {
            int SetIDToPass;

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand AddNewSet = new SqlCommand
            {
                CommandText = "InsertIntoSet",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter GameIDParameter = new SqlParameter
            {
                ParameterName = "GameID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = NewSet.GameID
            };
            AddNewSet.Parameters.Add(GameIDParameter);

            SqlParameter SetCodeParameter = new SqlParameter
            {
                ParameterName = "SetCode",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = NewSet.SetCode
            };
            AddNewSet.Parameters.Add(SetCodeParameter);

            SqlParameter SetNameParameter = new SqlParameter
            {
                ParameterName = "SetName",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = NewSet.SetName
            };
            AddNewSet.Parameters.Add(SetNameParameter);

            SqlParameter ReleaseDateParameter = new SqlParameter
            {
                ParameterName = "ReleaseDate",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = NewSet.ReleaseDate
            };
            AddNewSet.Parameters.Add(ReleaseDateParameter);

            SetIDToPass = Convert.ToInt32(AddNewSet.ExecuteScalar().ToString());
            
            StoreConnection.Close();

            return SetIDToPass;
        }
    }
}
