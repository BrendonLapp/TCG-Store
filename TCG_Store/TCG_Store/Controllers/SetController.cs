using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TCG_Store.Models;
using TCG_Store_DAL.DataAccessControllers;
using TCG_Store_DAL.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TCG_Store.Controllers
{
    [Route("api/Set")]
    [ApiController]
    public class SetController : ControllerBase
    {
        // GET: api/<SetController>
        [HttpGet]
        public List<Set> Get()
        {
            List<Set> AllSets = new List<Set>();
            SetDataController dataController = new SetDataController();
            List<SetDTO> setDTOs = new List<SetDTO>();

            setDTOs = dataController.GetAllSets();

            foreach (var item in setDTOs)
            {
                Set incomingSet = new Set();
                incomingSet.SetID = item.SetID;
                incomingSet.GameID = item.GameID;
                incomingSet.SetName = item.SetName;
                AllSets.Add(incomingSet);
            }

            return AllSets;
        }

        // GET api/<SetController>/5
        [HttpGet("{SetID}")]
        public string Get(int SetID)
        {
            return "value";
        }

        [Route("GetSetsByGame/{GameID}")]
        [HttpGet("{GameID}")]
        public List<Set> GetSetsByGame(int GameID)
        {
            List<Set> setsByGame = new List<Set>();
            List<SetDTO> setDTOs = new List<SetDTO>();
            SetDataController dataController = new SetDataController();

            setDTOs = dataController.GetSetsByGame(GameID);

            foreach (var item in setDTOs)
            {
                Set incomingSet = new Set();
                incomingSet.SetID = item.SetID;
                incomingSet.SetName = item.SetName;
                incomingSet.GameID = item.GameID;
                setsByGame.Add(incomingSet);
            }

            return setsByGame;
        }

        // POST api/<SetController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            //Get all sets out of the ygo API
            //send the sets to be saved into the DB
            //Need to pull in game ID, because long term I want to have a switch to pick ygo/mtg/pkmn for getting the sets
            //Should also check to make sure the set is not already in the DB and I'm only saving new sets
        }
    }
}
