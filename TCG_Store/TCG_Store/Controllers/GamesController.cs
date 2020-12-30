using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TCG_Store.Models;
using TCG_Store_DAL.DataAccessControllers;
using TCG_Store_DAL.DTOs;

namespace TCG_Store.Controllers
{
    /// <summary>
    /// The API Controller for the Games Endpoint
    /// </summary>
    [Route("api/v1/Games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        /// <summary>
        /// Performs a Get request for all Games in the DB
        /// </summary>
        /// <returns>List of Game Objects</returns>
        [HttpGet]
        public List<Game> Get()
        {
            List<Game> AllGames = new List<Game>();
            List<GameDTO> GameDTOs = new List<GameDTO>();
            GamesDataController DataController = new GamesDataController();

            GameDTOs = DataController.GetAllGames();
            
            foreach (var Game in GameDTOs)
            {
                Game incomingGame = new Game
                {
                    GameID = Game.GameID,
                    GameName = Game.GameName
                };
                
                AllGames.Add(incomingGame);
            }

            return AllGames;
        }

        /// <summary>
        /// Performs a Get request for a game specified by the GameID
        /// </summary>
        /// <param name="GameID">ID Of the game to be serached</param>
        /// <returns>Game Objects with the data of the searched Game</returns>
        [HttpGet("{GameID}")]
        public Game Get(int GameID)
        {
            Game Game = new Game();
            GameDTO GameDTO = new GameDTO();
            GamesDataController DataController = new GamesDataController();

            GameDTO = DataController.GetGameByID(GameID);

            Game.GameID = GameDTO.GameID;
            Game.GameName = GameDTO.GameName;

            return Game;
        }

        /// <summary>
        /// Performs a Post request containing the NewGame information
        /// </summary>
        /// <param name="NewGame">Game object created from the post body</param>
        /// <returns>Confirmation as a bool</returns>
        [HttpPost]
        public bool Post([FromBody] Game NewGame)
        {
            bool Confirmation = false;

            GamesDataController DataController = new GamesDataController();

            try
            {
                Confirmation = DataController.InsertIntoGame(NewGame.GameName);
            }
            catch
            {
                throw new Exception("Failed to add a new Game");
            }

            return Confirmation;
        }

        /// <summary>
        /// Performs a delete request based on a supplied GameID
        /// </summary>
        /// <param name="GameID">The supplied GameID</param>
        /// <returns>Cofirmation as a bool</returns>
        [Route("{GameID:int}")]
        [HttpDelete]
        public bool Delete(int GameID)
        {
            bool Confirmation = false;

            GamesDataController DataController = new GamesDataController();

            try
            {
                Confirmation = DataController.DeleteFromGame(GameID);
            }
            catch
            {
                throw new Exception("Failed to delete the game");
            }

            return Confirmation;
        }

        /// <summary>
        /// Performs a Put request containing the UpdatedGame information
        /// </summary>
        /// <param name="UpdatedGame">Game object created from the post body</param>
        /// <returns>Confirmation as a bool</returns>
        [Route("{Game}")]
        [HttpPut]
        public bool Put([FromBody] Game UpdatedGame)
        {
            bool Confirmation = false;

            GamesDataController DataController = new GamesDataController();
            GameDTO GameDTO = new GameDTO
            {
                GameID = UpdatedGame.GameID,
                GameName = UpdatedGame.GameName
            };

            try
            {
                Confirmation = DataController.UpdateGame(GameDTO);
            }
            catch
            {
                throw new Exception("Failed to update a new Game");
            }

            return Confirmation;
        }
    }
}
