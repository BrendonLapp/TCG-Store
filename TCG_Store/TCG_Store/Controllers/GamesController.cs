using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TCG_Store.Models;
using TCG_Store_DAL.DTOs;
using TCG_Store_DAL.DataAccessControllers;
using TCG_Store_DAL;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TCG_Store.Controllers
{
    [Route("api/Game")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        // GET: api/Game
        [HttpGet]
        public List<Game> Get()
        {
            List<Game> allGames = new List<Game>();
            List<GameDTO> gameDTOs = new List<GameDTO>();
            GamesDataController dataController = new GamesDataController();

            gameDTOs = dataController.GetAllGames();
            
            foreach (var game in gameDTOs)
            {
                Game incomingGame = new Game
                {
                    GameID = game.GameID,
                    GameName = game.GameName
                };
                
                allGames.Add(incomingGame);
            }

            return allGames;
        }

        // GET api/<GamesController>/5
        [HttpGet("{gameID}")]
        public Game Get(int gameID)
        {
            Game game = new Game();
            GameDTO gameDTO = new GameDTO();
            GamesDataController dataController = new GamesDataController();

            gameDTO = dataController.GetGameByID(gameID);

            game.GameID = gameDTO.GameID;
            game.GameName = gameDTO.GameName;

            return game;
        }

        // POST api/<GamesController>
        [HttpPost]
        public bool Post(Game newGame)
        {
            bool confirmation = false;

            GamesDataController dataController = new GamesDataController();

            try
            {
                confirmation = dataController.InsertIntoGame(newGame.GameName);
            }
            catch
            {
                throw new Exception("Failed to add a new game");
            }

            return confirmation;
        }
    }
}
