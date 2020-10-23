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
            List<GameDTO> AllGames = new List<GameDTO>();

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";  //@"Persist Security Info=False;Database=TCG_Store;User ID=;Password=;server=.;";
            StoreConnection.Open();

            SqlCommand SearchForAllGamesCommand = new SqlCommand
            {
                CommandText = "GetAllGames",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlDataReader DataReader;

            DataReader = SearchForAllGamesCommand.ExecuteReader();

            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    GameDTO FoundGame = new GameDTO();
                    for (int index  = 0; index < DataReader.FieldCount; index++)
                    {
                        FoundGame.GameID = int.Parse(DataReader["GameID"].ToString());
                        FoundGame.GameName = DataReader["GameName"].ToString();
                    }
                    AllGames.Add(FoundGame);
                }
            }

            DataReader.Close();
            StoreConnection.Close();

            return AllGames;
        }

        public GameDTO GetGameByID(int GameID)
        {
            GameDTO FoundGame = new GameDTO();

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand SearchForGameByIDCommand = new SqlCommand
            {
                CommandText = "GetGameByID",
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
            SearchForGameByIDCommand.Parameters.Add(GameIDParameter);

            SqlDataReader DataReader;

            DataReader = SearchForGameByIDCommand.ExecuteReader();

            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    for (int index = 0; index < DataReader.FieldCount; index++)
                    {
                        FoundGame.GameID = int.Parse(DataReader["GameID"].ToString());
                        FoundGame.GameName = DataReader["GameName"].ToString();
                    }
                }
            }

            DataReader.Close();
            StoreConnection.Close();

            return FoundGame;
        }

        public bool InsertIntoGame(string NewGameName)
        {
            bool Success;

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand InsertIntoGameCommand = new SqlCommand
            {
                CommandText = "InsertIntoGame",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter GameNameParameter = new SqlParameter
            {
                ParameterName = "GameName",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = NewGameName
            };
            InsertIntoGameCommand.Parameters.Add(GameNameParameter);

            InsertIntoGameCommand.ExecuteNonQuery();

            StoreConnection.Close();
            Success = true;
            return Success;
        }
    }
}
