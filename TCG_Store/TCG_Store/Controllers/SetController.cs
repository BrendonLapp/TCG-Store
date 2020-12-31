using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TCG_Store.Models;
using TCG_Store_DAL.APIResponseObjects;
using TCG_Store_DAL.DataAccessControllers;
using TCG_Store_DAL.APIResponseObjects.YugiohAPI;
using TCG_Store_DAL.DTOs;

namespace TCG_Store.Controllers
{
    /// <summary>
    /// The API Controller for the Sets Endpoint
    /// </summary>
    [Route("api/v1/Set")]
    [ApiController]
    public class SetController : ControllerBase
    {
        /// <summary>
        /// Performs a Get request for all sets in the DB
        /// </summary>
        /// <returns>List of Set Objects</returns>
        [HttpGet]
        public List<Set> Get()
        {
            List<Set> AllSets = new List<Set>();
            SetDataController DataController = new SetDataController();
            List<SetDTO> setDTOs = new List<SetDTO>();

            setDTOs = DataController.GetAllSets();

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

        /// <summary>
        /// Performs a Get request for a game specified by the SetID
        /// </summary>
        /// <param name="SetID">ID Of the Set to be serached</param>
        /// <returns>Set Object with the data of the searched Set</returns>
        [HttpGet("{SetID}")]
        public string Get(int SetID)
        {
            return "value";
        }

        /// <summary>
        /// Performs a Post request to the DB based on the GameID and performs Get Requests depening on the game to the external API's
        /// </summary>
        /// <param name="GameID">GameID supplied to filter the sets to be added by game</param>
        /// <returns></returns>
        [Route("PostNewSets/{GameID}")]
        [HttpPost]
        public async Task<bool> PostNewSets(int GameID)
        {
            bool Success;
            SetDataController SetDataController = new SetDataController();

            List<YgoSetAPIResponse> YgoSetResponse = new List<YgoSetAPIResponse>();
            PokemonSetAPIResponseRoot PokemonSetResponse = new PokemonSetAPIResponseRoot();
            List<SetDTO> NonExistingSets = new List<SetDTO>();
            List<SetDTO> ExistingSets = new List<SetDTO>();

            GamesDataController GameDataController = new GamesDataController();
            GameDTO GameDTO = GameDataController.GetGameByID(GameID);
            CardController CardController = new CardController();

            using (var HttpClient = new HttpClient())
            {
                switch (GameDTO.GameName)
                {
                    case "Yu-Gi-Oh":
                        using (var Response = await HttpClient.GetAsync("https://db.ygoprodeck.com/api/v7/cardsets.php"))
                        {
                            string ApiResponse = await Response.Content.ReadAsStringAsync();
                            YgoSetResponse = JsonConvert.DeserializeObject<List<YgoSetAPIResponse>>(ApiResponse);
                        }

                        foreach (var Item in YgoSetResponse)
                        {
                            SetDTO NewSet = new SetDTO();

                            NewSet.GameID = GameID;
                            NewSet.SetCode = Item.set_code;
                            NewSet.SetName = Item.set_name;
                            NewSet.ReleaseDate = Item.tcg_date;

                            NonExistingSets.Add(NewSet);
                        }

                        break;

                    case "Pokemon":
                        using (var Response = await HttpClient.GetAsync("https://api.pokemontcg.io/v1/sets"))
                        {
                            string ApiResponse = await Response.Content.ReadAsStringAsync();
                            PokemonSetResponse = JsonConvert.DeserializeObject<PokemonSetAPIResponseRoot>(ApiResponse);
                        }
                        foreach (var Item in PokemonSetResponse.sets)
                        {
                            SetDTO NewSet = new SetDTO();
                            NewSet.GameID = GameID;
                            NewSet.SetCode = Item.code;
                            NewSet.SetName = Item.name;
                            NewSet.ReleaseDate = Item.releaseDate;

                            NonExistingSets.Add(NewSet);
                        }
                        break;
                }
            }

            ExistingSets = SetDataController.GetSetsByGame(GameID);

            HashSet<string> SetCodes = new HashSet<string>(ExistingSets.Select(x => x.SetCode));
            NonExistingSets.RemoveAll(x => SetCodes.Contains(x.SetCode));

            if (NonExistingSets.Count == 0 || NonExistingSets == null)
            {
                Success = false;
            }
            else
            {
                int SetID;

                foreach (var Set in NonExistingSets)
                {
                    SetID = SetDataController.AddNonExistingSetsToDataBase(Set);

                    switch(GameDTO.GameName)
                    {
                        case "Yu-Gi-Oh":
                            await CardController.AddYugiohCards(SetID, Set.SetName, Set.SetCode);
                            break;
                        case "Pokemon":
                            await CardController.AddPokemonCards(SetID, Set.SetCode);
                            break;
                    }                    
                }
                Success = true;
            }
            
            return Success;
        }

        /// <summary>
        /// Performs a Get request based on the supplied GameID
        /// </summary>
        /// <param name="GameID">Supplied GameID</param>
        /// <returns>List of Set objects</returns>
        [Route("GetSetsByGame/{GameID}")]
        [HttpGet]
        public List<Set> GetSetsByGame(int GameID)
        {
            List<Set> SetsByGame = new List<Set>();
            List<SetDTO> SetDTOs = new List<SetDTO>();
            SetDataController DataController = new SetDataController();

            SetDTOs = DataController.GetSetsByGame(GameID);

            foreach (var item in SetDTOs)
            {
                Set incomingSet = new Set();
                incomingSet.SetID = item.SetID;
                incomingSet.SetName = item.SetName;
                incomingSet.GameID = item.GameID;
                SetsByGame.Add(incomingSet);
            }

            return SetsByGame;
        }
    }
}
