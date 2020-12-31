using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCG_Store.Models;
using TCG_Store_DAL.DTOs;
using TCG_Store_DAL.DataAccessControllers;
using Microsoft.AspNetCore.Mvc;

namespace TCG_Store.Controllers
{
    /// <summary>
    /// The API Controller for the Inventory Endpoint
    /// </summary>
    [Route("/api/v1/Inventory")]
    [ApiController]
    public class InventoryController
    {
        /// <summary>
        /// Performs a Get request for all inventory items in the DB
        /// </summary>
        /// <returns>List of Inventory Objects</returns>
        [HttpGet]
        public List<Inventory> Get ()
        {
            List<Inventory> InventoryItems = new List<Inventory>();
            List<InventoryDTO> InventoryDTOs = new List<InventoryDTO>();
            InventoryDataController InventoryDataController = new InventoryDataController();

            InventoryDTOs = InventoryDataController.GetAllInventoryItems();
            
            foreach (var Item in InventoryDTOs)
            {
                Inventory FoundItem = new Inventory
                {
                    InventoryID = Item.InventoryID,
                    CardID = Item.CardID,
                    SealedProductID = Item.SealedProductID,
                    QualityID = Item.QualityID,
                    Quantity = Item.Quantity,
                    FirstEdition = Item.FirstEdition
                };
                InventoryItems.Add(FoundItem);
            }

            return InventoryItems;
        }

        /// <summary>
        /// Performs a Get request for an inventory specified by the inventory id
        /// </summary>
        /// <param name="InventoryID">ID Of the inventory item to be serached</param>
        /// <returns>Inventory Object with the data of the searched inventory item</returns>
        [HttpGet("{InventoryID}")]
        public Inventory Get (int InventoryID)
        {
            Inventory InventoryItem = new Inventory();
            InventoryDTO FoundInventoryItem = new InventoryDTO();
            InventoryDataController InventoryDataController = new InventoryDataController();

            FoundInventoryItem = InventoryDataController.GetInventoryItemByID(InventoryID);

            InventoryItem.InventoryID = FoundInventoryItem.InventoryID;
            InventoryItem.CardID = FoundInventoryItem.CardID;
            InventoryItem.SealedProductID = FoundInventoryItem.SealedProductID;
            InventoryItem.QualityID = FoundInventoryItem.QualityID;
            InventoryItem.Quantity = FoundInventoryItem.Quantity;
            InventoryItem.FirstEdition = FoundInventoryItem.FirstEdition;

            return InventoryItem;
        }

        /// <summary>
        /// Performs a post request for a new inventory item
        /// </summary>
        /// <param name="NewInventoryItem">Inventory Object to be added</param>
        /// <returns>Confirmation as a bool</returns>
        [HttpPost]
        public bool Post ([FromBody] Inventory NewInventoryItem)
        {
            bool Confirmation;
            InventoryDTO NewItem = new InventoryDTO();
            InventoryDataController InventoryDataController = new InventoryDataController();

            //Idea 1:
            //Check first if the new item needs to even be added or to update instead
            //Doing this will need me to check if the Card/SealedProduct already has an ID in the database with the same quality rating and if it has a first edition as true or not
            //if it has none of those make a new one. if not update the exisitng record instead based on the inventory ID
            //Idea 2:
            //If there is an inventory ID then I dont save and instead update the quantity. Nothing else will or should get changed.
            NewItem.CardID = NewInventoryItem.CardID;
            NewItem.SealedProductID = NewInventoryItem.SealedProductID;
            NewItem.QualityID = NewInventoryItem.QualityID;
            NewItem.Quantity = NewInventoryItem.Quantity;
            NewItem.FirstEdition = NewInventoryItem.FirstEdition;

            Confirmation = InventoryDataController.AddNewInventoryItem(NewItem);

            return Confirmation;
        }

        /// <summary>
        /// Performs a put request to update the inventory item
        /// </summary>
        /// <param name="UpdatedInventory">Inventory object to be updated</param>
        /// <returns>Confirmation as a bool</returns>
        [HttpPut]
        public bool Put ([FromBody] Inventory UpdatedInventory)
        {
            bool Confirmation;

            InventoryDataController InventoryDataController = new InventoryDataController();

            Confirmation = InventoryDataController.UpdateInventoryItem(UpdatedInventory.InventoryID, UpdatedInventory.Quantity);

            return Confirmation;
        }

        /// <summary>
        /// Performs a delete request on the supplied inventory ID
        /// </summary>
        /// <param name="InventoryID">Inventory ID to delete</param>
        /// <returns>Confirmation as a bool</returns>
        [Route("{InventoryID}")]
        [HttpDelete]
        public bool Delete (int InventoryID)
        {
            bool Confirmation;

            InventoryDataController InventoryDataController = new InventoryDataController();

            Confirmation = InventoryDataController.DeleteInventoryItem(InventoryID);

            return Confirmation;
        }
    }
}
