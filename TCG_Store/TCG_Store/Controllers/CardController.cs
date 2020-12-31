using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TCG_Store.Models;
using TCG_Store_DAL.APIResponseObjects.PokemonAPI;
using TCG_Store_DAL.APIResponseObjects.YugiohAPI;
using TCG_Store_DAL.DataAccessControllers;
using TCG_Store_DAL.DTOs;

namespace TCG_Store.Controllers
{
    /// <summary>
    /// The API Controller for the Cards Endpoint
    /// </summary>
    [Route("api/v1/Cards")]
    [ApiController]
    public class CardController
    {
        /// <summary>
        /// Http Post Method for adding Yugioh Crads. Performs an HTTP request for the card sets and then saves all the new cards in that set to the DB.
        /// </summary>
        /// <param name="SetID">ID of the set being saved</param>
        /// <param name="SetName">Name of the set being saved</param>
        /// <param name="SetCode">Set Code of the set being saved</param>
        /// <returns></returns>
        [HttpPost("AddYugiohCards/{SetID}/{SetName}/{SetCode}")]
        public async Task AddYugiohCards(int SetID, string SetName, string SetCode)
        {
            bool Success;
            CardDataController CardDataController = new CardDataController();

            YugiohAPIResponseRoot YugiohResponse = new YugiohAPIResponseRoot();
            using (var HttpClient = new HttpClient())
            {
                using (var Response = await HttpClient.GetAsync("https://db.ygoprodeck.com/api/v7/cardinfo.php?cardset=" + SetName))
                {
                    string ApiResponse = await Response.Content.ReadAsStringAsync();
                    YugiohResponse = JsonConvert.DeserializeObject<YugiohAPIResponseRoot>(ApiResponse);
                }
            }

            if (YugiohResponse.data != null)
            {
                foreach (var CardData in YugiohResponse.data)
                {
                    if (CardData.card_sets != null)
                    {
                        foreach (var CardVariant in CardData.card_sets)
                        {
                           
                            CardDTO NewCard = new CardDTO
                            {
                                SetID = SetID,
                                CardName = CardData.name,
                                ElementalType = CardData.attribute,
                                SubType = CardData.race,
                                SuperType = CardData.type,
                                APIImageID = CardData.id,
                                PictureLink = "https://storage.googleapis.com/ygoprodeck.com/pics/" + CardData.id + ".jpg",
                                PictureSmallLink = "https://storage.googleapis.com/ygoprodeck.com/pics_small/" + 50781944 + ".jpg",
                                CardCodeInSet = CardVariant.set_code,
                                Price = CardVariant.set_price,
                                Rarity = CardVariant.set_rarity
                            };

                            if (NewCard.CardCodeInSet.Contains(SetCode))
                            {
                                Success = CardDataController.AddCard(NewCard);

                                if (Success == false)
                                {
                                    throw new Exception("Failed to insert a YuGiOh card into CardsInSet");
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Http Post Method for adding Pokemon Crads. Performs an HTTP request for the card sets and then saves all the new cards in that set to the DB.
        /// </summary>
        /// <param name="SetID">The ID of the set being saved</param>
        /// <param name="SetCode">The Set Code of the set being saved</param>
        /// <returns></returns>
        [HttpPost("AddPokemonCards/{SetID}/{SetCode}")]
        public async Task AddPokemonCards(int SetID, string SetCode)
        {
            bool Success;

            CardDataController CardDataController = new CardDataController();

            PokemonApiReponseRoot PokemonResponse = new PokemonApiReponseRoot();
            using (var HttpClient = new HttpClient())
            {
                Thread.Sleep(2000);
                string URL = "https://api.pokemontcg.io/v1/cards?pageSize=500&setCode=" + SetCode;
                using (var Response = await HttpClient.GetAsync(URL))
                {
                    string ApiReponse = await Response.Content.ReadAsStringAsync();
                    try
                    {
                        PokemonResponse = JsonConvert.DeserializeObject<PokemonApiReponseRoot>(ApiReponse);
                    }
                    catch
                    {
                        Thread.Sleep(2000);
                    }
                }

                foreach (var CardData in PokemonResponse.cards)
                {
                    CardDTO NewCard = new CardDTO
                    {
                        SetID = SetID,
                        CardName = CardData.name,
                        CardCodeInSet = CardData.id,
                        ElementalType = (CardData.types == null) ? "N/A" : string.Join(",", CardData.types),
                        Price = 0.00M,
                        Rarity = (CardData.rarity == null) ? "N/A" : CardData.rarity,
                        SubType = (CardData.subtype == null) ? "N/A" : CardData.subtype,
                        SuperType = CardData.supertype,
                        APIImageID = "N/A",
                        PictureLink = CardData.imageUrlHiRes,
                        PictureSmallLink = CardData.imageUrl
                    };

                    Success = CardDataController.AddCard(NewCard);
                    

                    if (Success == false)
                    {
                        throw new Exception("Failed to insert a Pokemon card into CardsInSet");
                    }
                }
            }
        }

        /// <summary>
        /// Performs a search query and builds a list of all matches to be sent to the UI of that query.
        /// </summary>
        /// <param name="SearchQuery">A user entered serach query used to find matches.</param>
        /// <returns>List of Card Objects</returns>
        [HttpGet("{SearchQuery}")]
        public List<Card> Get(string SearchQuery)
        {
            List<Card> CardsMatchingQuery = new List<Card>();
            List<CardDTO> CardDTOsMatchingQuery = new List<CardDTO>();
            CardDataController CardDataController = new CardDataController();

            CardDTOsMatchingQuery = CardDataController.SearchCardsByPartialName(SearchQuery);

            foreach (var CardDTO in CardDTOsMatchingQuery)
            {
                Card FoundCard = new Card
                {
                    CardID = CardDTO.CardID,
                    SetID = CardDTO.SetID,
                    CardCodeInSet = CardDTO.CardCodeInSet,
                    CardName = CardDTO.CardName,
                    Price = CardDTO.Price,
                    ElementalType = CardDTO.ElementalType,
                    Rarity = CardDTO.Rarity,
                    SubType = CardDTO.SubType,
                    SuperType = CardDTO.SuperType,
                    APIImageID = CardDTO.APIImageID,
                    PictureLink = CardDTO.PictureLink,
                    PictureSmallLink = CardDTO.PictureSmallLink
                };

                CardsMatchingQuery.Add(FoundCard);
            }
            return CardsMatchingQuery;
        }
    }
}
