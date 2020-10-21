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
            List<SetDTO> setDTOs = new List<SetDTO>();

            SqlConnection storeConnection = new SqlConnection();
            storeConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            storeConnection.Open();

            SqlCommand GetAllSets = new SqlCommand
            {
                CommandText = "GetAllSets",
                CommandType = CommandType.StoredProcedure,
                Connection = storeConnection
            };

            SqlDataReader dataReader;

            dataReader = GetAllSets.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    SetDTO incomingSet = new SetDTO();
                    for (int index = 0; index < dataReader.FieldCount; index++)
                    {
                        switch(dataReader.GetName(index))
                        {
                            case "SetID":
                                incomingSet.SetID = dataReader[index].ToString();
                                break;
                            case "GameID":
                                incomingSet.GameID = int.Parse(dataReader[index].ToString());
                                break;
                            case "SetName":
                                incomingSet.SetName = dataReader[index].ToString();
                                break;
                        }
                    }
                    setDTOs.Add(incomingSet);
                }
            }

            dataReader.Close();
            storeConnection.Close();

            return setDTOs;
        }

        public List<SetDTO> GetSetsByGame(int GameID)
        {
            List<SetDTO> setsByGame = new List<SetDTO>();

            SqlConnection storeConnection = new SqlConnection();
            storeConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            storeConnection.Open();

            SqlCommand GetSetsByGame = new SqlCommand
            {
                CommandText = "GetSetsByGameID",
                CommandType = CommandType.StoredProcedure,
                Connection = storeConnection
            };

            SqlParameter GameIDParameter = new SqlParameter
            {
                ParameterName = "GameID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = GameID
            };
            GetSetsByGame.Parameters.Add(GameIDParameter);

            SqlDataReader dataReader;

            dataReader = GetSetsByGame.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    SetDTO incomingSet = new SetDTO();
                    for (int index = 0; index < dataReader.FieldCount; index++)
                    {
                        switch (dataReader.GetName(index))
                        {
                            case "SetID":
                                incomingSet.SetID = dataReader[index].ToString();
                                break;
                            case "GameID":
                                incomingSet.GameID = int.Parse(dataReader[index].ToString());
                                break;
                            case "SetName":
                                incomingSet.SetName = dataReader[index].ToString();
                                break;
                        }
                    }
                    setsByGame.Add(incomingSet);
                }
            }
            dataReader.Close();
            storeConnection.Close();

            return setsByGame;
        }
    }
}
