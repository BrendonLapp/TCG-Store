using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TCG_Store.Models;
using TCG_Store_DAL.DataAccessControllers;
using TCG_Store_DAL.DTOs;
using Microsoft.AspNetCore.Cors;

namespace TCG_Store.Controllers
{
    [Route("api/v1/Games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        // GET: api/Game
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

        // GET api/<GamesController>/5
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

        // POST api/<GamesController>
        //[HttpPost]
        public bool Post([FromBody] Game NewGame)
        {
            //return hhtp status code instead of bool
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
    }
}
