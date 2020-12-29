using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TCG_Store_DAL.APIResponseObjects.PokemonAPI;
using TCG_Store_DAL.APIResponseObjects.YugiohAPI;
using System.Net.Http;
using TCG_Store_DAL.DTOs;
using Newtonsoft.Json;
using TCG_Store_DAL.DataAccessControllers;
using Microsoft.AspNetCore.Mvc;
using TCG_Store.Models;

namespace TCG_Store.Controllers
{
    [Route("api/v1/Cards")]
    [ApiController]
    public class CardController
    {
        [HttpPost("AddYugiohCards/SetID={SetID}/SetName=/{SetName}/SetCode={SetCode}")]
        public async Task AddYugiohCards(int SetID, string SetName, string SetCode)
        {
            bool Success;

            YugiohAPIResponseRoot YugiohResponse = new YugiohAPIResponseRoot();
            using (var HttpClient = new HttpClient())
            {
                using (var Response = await HttpClient.GetAsync("https://db.ygoprodeck.com/api/v7/cardinfo.php?cardset=" + SetName))
                {
                    string ApiResponse = await Response.Content.ReadAsStringAsync();
                    YugiohResponse = JsonConvert.DeserializeObject<YugiohAPIResponseRoot>(ApiResponse);
                }
            }

            YugiohCardDetails yugiohCardDetails = new YugiohCardDetails();

            foreach (var CardData in YugiohResponse.data)
            {
                if (CardData.card_sets != null)
                {
                    foreach (var Card in CardData.card_sets)
                    {
                        if (Card.set_code.Contains(SetCode))
                        {
                            using (var HttpClient = new HttpClient())
                            {
                                using (var Response = await HttpClient.GetAsync("https://db.ygoprodeck.com/api/v7/cardsetsinfo.php?setcode=" + Card.set_code))
                                {
                                    string CardDetails = await Response.Content.ReadAsStringAsync();
                                    yugiohCardDetails = JsonConvert.DeserializeObject<YugiohCardDetails>(CardDetails);
                                }

                                CardDTO CardDTO = new CardDTO
                                {
                                    SetID = SetID,
                                    CardCodeInSet = yugiohCardDetails.set_code,
                                    CardName = CardData.name,
                                    Rarity = yugiohCardDetails.set_rarity,
                                    Price = yugiohCardDetails.set_price,
                                    APIImageID = yugiohCardDetails.id,
                                    SubType = CardData.race,
                                    SuperType = CardData.type,
                                    ElementalType = CardData.attribute,
                                    PictureLink = "https://storage.googleapis.com/ygoprodeck.com/pics/" + yugiohCardDetails.id + ".jpg",
                                    PictureSmallLink = "https://storage.googleapis.com/ygoprodeck.com/pics_small/" + yugiohCardDetails.id + ".jpg"
                                };

                                CardDataController CardDataController = new CardDataController();

                                Success = CardDataController.AddCard(CardDTO);

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

        [HttpPost("AddPokemonCards/SetID={SetID}/SetCode={SetCode}")]
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
