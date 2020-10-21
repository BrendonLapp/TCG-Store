using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG_Store_DAL.DTOs;
using System.Data.SqlClient;
using System.Data;

namespace TCG_Store_DAL.DataAccessControllers
{
    public class GamesDataController
    {
        public List<GameDTO> GetAllGames()
        {
            List<GameDTO> allGames = new List<GameDTO>();

            SqlConnection storeConnection = new SqlConnection();
            storeConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";  //@"Persist Security Info=False;Database=TCG_Store;User ID=;Password=;server=.;";
            storeConnection.Open();

            SqlCommand searchForAllGames = new SqlCommand
            {
                CommandText = "GetAllGames",
                CommandType = CommandType.StoredProcedure,
                Connection = storeConnection
            };

            SqlDataReader dataReader;

            dataReader = searchForAllGames.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    GameDTO incomingGame = new GameDTO();
                    for (int index  = 0; index < dataReader.FieldCount; index++)
                    {
                        incomingGame.GameID = int.Parse(dataReader["GameID"].ToString());
                        incomingGame.GameName = dataReader["GameName"].ToString();
                        //switch (dataReader.GetName(index))
                        //{
                        //    case "GameID":
                        //        incomingGame.GameID = int.Parse(dataReader[index].ToString());
                        //        break;
                        //    case "GameName":
                        //        incomingGame.GameName = dataReader[index].ToString();
                        //        break;
                        //}
                    }
                    allGames.Add(incomingGame);
                }
            }

            dataReader.Close();
            storeConnection.Close();

            return allGames;
        }

        public GameDTO GetGameByID(int gameID)
        {
            GameDTO game = new GameDTO();

            SqlConnection storeConnection = new SqlConnection();
            storeConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            storeConnection.Open();

            SqlCommand searchForGameByID = new SqlCommand
            {
                CommandText = "GetGameByID",
                CommandType = CommandType.StoredProcedure,
                Connection = storeConnection
            };

            SqlParameter gameIDParameter = new SqlParameter
            {
                ParameterName = "GameID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = gameID
            };
            searchForGameByID.Parameters.Add(gameIDParameter);

            SqlDataReader dataReader;

            dataReader = searchForGameByID.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    for (int index = 0; index < dataReader.FieldCount; index++)
                    {
                        game.GameID = int.Parse(dataReader["GameID"].ToString());
                        game.GameName = dataReader["GameName"].ToString();
                    //    switch (dataReader.GetName(index))
                    //    {
                    //        case "GameID":
                    //            game.GameID = int.Parse(dataReader[index].ToString()) ;
                    //            break;
                    //        case "GameName":
                    //            game.GameName = dataReader[index].ToString();
                    //            break;
                    //    }
                    }
                }
            }

            dataReader.Close();
            storeConnection.Close();

            return game;
        }

        public bool InsertIntoGame(string newGame)
        {
            bool success;

            SqlConnection storeConnection = new SqlConnection();
            storeConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            storeConnection.Open();

            SqlCommand insertIntoGame = new SqlCommand
            {
                CommandText = "InsertIntoGame",
                CommandType = CommandType.StoredProcedure,
                Connection = storeConnection
            };

            SqlParameter GameName = new SqlParameter
            {
                ParameterName = "GameName",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = newGame
            };
            insertIntoGame.Parameters.Add(GameName);

            insertIntoGame.ExecuteNonQuery();

            storeConnection.Close();
            success = true;
            return success;
        }
    }
}
