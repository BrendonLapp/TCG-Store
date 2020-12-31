using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TCG_Store_DAL.DTOs;

namespace TCG_Store_DAL.DataAccessControllers
{
    /// <summary>
    /// Data controller for the Games 
    /// </summary>
    public class GamesDataController
    {
        /// <summary>
        /// Performs a request for the Database to get a list of all games
        /// </summary>
        /// <returns>List of GameDTO objects</returns>
        public List<GameDTO> GetAllGames()
        {
            List<GameDTO> AllGames = new List<GameDTO>();

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;"; 
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

        /// <summary>
        /// Performs a request to the database to be a single game
        /// </summary>
        /// <param name="GameID">The ID of the game being requested</param>
        /// <returns>GameDTO object with all the game details</returns>
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

        /// <summary>
        /// Inserts a new game into the database. 
        /// </summary>
        /// <param name="NewGameName">The name of the new game being saved</param>
        /// <returns>Success as a bool</returns>
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
        
        /// <summary>
        /// Deletes a single game based on the given ID
        /// </summary>
        /// <param name="GameID">The supplied GameID to be deleted</param>
        /// <returns>Success as a bool</returns>
        public bool DeleteFromGame(int GameID)
        {
            bool Success;

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand DeleteFromGameCommand = new SqlCommand
            {
                CommandText = "DeleteGame",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter GameIDParameter = new SqlParameter
            {
                ParameterName = "GameID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = GameID
            };
            DeleteFromGameCommand.Parameters.Add(GameIDParameter);

            DeleteFromGameCommand.ExecuteNonQuery();

            StoreConnection.Close();
            Success = true;
            return Success;
        }

        /// <summary>
        /// Updates a Game in the database
        /// </summary>
        /// <param name="Game">GameDTO object</param>
        /// <returns>Success as a bool</returns>
        public bool UpdateGame(GameDTO Game)
        {
            bool Success;

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand UpdateGame = new SqlCommand
            {
                CommandText = "UpdateGame",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter GameIDParameter = new SqlParameter
            {
                ParameterName = "GameID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = Game.GameID
            };
            UpdateGame.Parameters.Add(GameIDParameter);

            SqlParameter GameNameParameter = new SqlParameter
            {
                ParameterName = "GameName",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = Game.GameName
            };
            UpdateGame.Parameters.Add(GameNameParameter);

            UpdateGame.ExecuteNonQuery();

            StoreConnection.Close();

            Success = true;
            return Success;
        }
    }
}
