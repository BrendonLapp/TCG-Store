using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG_Store_DAL.DTOs;
using System.Data;

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
                        switch(DataReader.GetName(index))
                        {
                            case "SetID":
                                FoundSet.SetID = DataReader[index].ToString();
                                break;
                            case "GameID":
                                FoundSet.GameID = int.Parse(DataReader[index].ToString());
                                break;
                            case "SetName":
                                FoundSet.SetName = DataReader[index].ToString();
                                break;
                        }
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
                        switch (DataReader.GetName(index))
                        {
                            case "SetID":
                                FoundSet.SetID = DataReader[index].ToString();
                                break;
                            case "GameID":
                                FoundSet.GameID = int.Parse(DataReader[index].ToString());
                                break;
                            case "SetName":
                                FoundSet.SetName = DataReader[index].ToString();
                                break;
                        }
                    }
                    SetsDTOsByGame.Add(FoundSet);
                }
            }
            DataReader.Close();
            StoreConnection.Close();

            return SetsDTOsByGame;
        }
    }
}
