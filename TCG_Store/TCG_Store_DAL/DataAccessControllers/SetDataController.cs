using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TCG_Store_DAL.DTOs;

namespace TCG_Store_DAL.DataAccessControllers
{
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

        public bool AddNonExistingSetsToDataBase(List<SetDTO> NewSets)
        {
            bool Confirmation;

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            foreach (var Set in NewSets)
            {
                SqlCommand AddNewSets = new SqlCommand
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
                    SqlValue = Set.GameID
                };
                AddNewSets.Parameters.Add(GameIDParameter);

                SqlParameter SetCodeParameter = new SqlParameter
                {
                    ParameterName = "SetCode",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.VarChar,
                    SqlValue = Set.SetCode
                };
                AddNewSets.Parameters.Add(SetCodeParameter);

                SqlParameter SetNameParameter = new SqlParameter
                {
                    ParameterName = "SetName",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.VarChar,
                    SqlValue = Set.SetName
                };
                AddNewSets.Parameters.Add(SetNameParameter);

                AddNewSets.ExecuteNonQuery();
                AddNewSets.Parameters.Clear();
            }
            Confirmation = true;
            StoreConnection.Close();
            return Confirmation;
        }
    }
}
