﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TCG_Store.Models;
using TCG_Store_DAL.DataAccessControllers;
using TCG_Store_DAL.DTOs;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TCG_Store.Controllers
{
    [ApiController]
    public class GamesController : ControllerBase
    {
        // GET: api/Game
        [HttpGet]
        [Route("/")]
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
        [Route("/")]
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
        [HttpPost]
        [Route("/")]
        public bool Post(Game NewGame)
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
    }
}
