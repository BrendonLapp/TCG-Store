using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TCG_Store_DAL.APIResponseObjects;
using TCG_Store_DAL.DataAccessControllers;
using TCG_Store_DAL.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TCG_Store.Controllers
{
    [Route("api/Yugioh")]
    [ApiController]
    public class YugiohController : ControllerBase
    {
        // GET: api/<YugiohController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<YugiohController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [Route("GetNewCards/{GameID}")]
        [HttpGet]
        public async Task<bool> GetNewCards (int GameID)
        {
            bool Success = false;

            YugiohAPIResponseRoot YugiohResponse = new YugiohAPIResponseRoot();
            List<YuGiOhDTO> NewYugiohCards = new List<YuGiOhDTO>();
            YugiohDataController DataController = new YugiohDataController();

            using (var HttpClient = new HttpClient())
            {
                using (var Response = await HttpClient.GetAsync("https://db.ygoprodeck.com/api/v7/cardinfo.php"))
                {
                    string ApiResponse = await Response.Content.ReadAsStringAsync();
                    YugiohResponse = JsonConvert.DeserializeObject<YugiohAPIResponseRoot>(ApiResponse);
                }
            }

            foreach (var Item in YugiohResponse.data)
            {
                YuGiOhDTO NewCard = new YuGiOhDTO
                {
                    CardName = Item.name,
                    Description = Item.desc,
                    ATK = Item.atk,
                    DEF = Item.def,
                    Type = Item.race,
                    CardType = Item.type,
                    Attribute = Item.attribute
                };
                NewYugiohCards.Add(NewCard);
            }

            //Success = DataController.AddNewCards(NewYugiohCards);
            return Success;
        }

        // POST api/<YugiohController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<YugiohController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<YugiohController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
