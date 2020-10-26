using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TCG_Store.Models;
using TCG_Store_DAL.APIResponseObjects;
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

        [Route("GetNewSets/{GameID}")]
        [HttpGet]
        public async Task<bool> GetAllSetsInGameAsync(int GameID)
        {
            bool Success;
            SetDataController dataController = new SetDataController();

            List<SetAPIResponse> SetResponse = new List<SetAPIResponse>();
            List<SetDTO> NonExistingSets = new List<SetDTO>();
            List<SetDTO> ExistingSets = new List<SetDTO>();

            //For the futre if I am adding in Sets for MTG or PKMN. I will need to do an IF statement to select which game I need to call an API for to populate my needed information.
            //I may also need to have different API response Objects to hold each ones response in before I turn it into my SetDTO
            using (var HttpClient = new HttpClient())
            {
                using (var response = await HttpClient.GetAsync("https://db.ygoprodeck.com/api/v7/cardsets.php"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    SetResponse = JsonConvert.DeserializeObject<List<SetAPIResponse>>(apiResponse);
                }
            }

            foreach (var Item in SetResponse)
            {
                SetDTO NewSet = new SetDTO();

                NewSet.GameID = GameID;
                NewSet.SetID = Item.set_code;
                NewSet.SetName = Item.set_name;

                NonExistingSets.Add(NewSet);
            }

            ExistingSets = dataController.GetSetsByGame(GameID);

            HashSet<string> SetIDs = new HashSet<string>(ExistingSets.Select(x => x.SetID));
            NonExistingSets.RemoveAll(x => SetIDs.Contains(x.SetID));

            Success = dataController.AddNonExistingSetsToDataBase(NonExistingSets);
            return Success;
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
