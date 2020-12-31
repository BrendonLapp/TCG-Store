using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TCG_Store.Models;
using TCG_Store_DAL.DataAccessControllers;
using TCG_Store_DAL.DTOs;

namespace TCG_Store.Controllers
{    
    /// <summary>
     /// The API Controller for the SealedProduct Endpoint
     /// </summary>
    [Route("api/v1/SealedProduct")]
    [ApiController]
    public class SealedProductController
    {
        /// <summary>
        /// Performs a get request for all sealed products in the database
        /// </summary>
        /// <returns>List of sealed product objects</returns>
        [HttpGet]
        public List<SealedProduct> Get()
        {
            List<SealedProduct> AllSealedProducts = new List<SealedProduct>();
            List<SealedProductDTO> SealedProductDTOs = new List<SealedProductDTO>();
            SealedProductDataController SealedProductDataController = new SealedProductDataController();
            SealedProductDTOs = SealedProductDataController.GetAllSealedProducts();

            foreach (var Product in SealedProductDTOs)
            {
                SealedProduct FoundProduct = new SealedProduct
                {
                    SealedProductID = Product.SealedProductID,
                    SetID = Product.SetID,
                    SealedProductName = Product.SealedProductName,
                    Price = Product.Price
                };
                AllSealedProducts.Add(FoundProduct);
            }

            return AllSealedProducts;
        }

        /// <summary>
        /// Performs a get request for all sealed prodcut in game
        /// </summary>
        /// <param name="GameID">The GameID to search against</param>
        /// <returns>List of SealedProduct Objects</returns>
        [Route("/GameID={GameID}")]
        [HttpGet]
        public List<SealedProduct> GetSealedProductByGame (int GameID)
        {
            List<SealedProduct> SealedProducts = new List<SealedProduct>();
            List<SealedProductDTO> SealedProductDTOs = new List<SealedProductDTO>();
            SealedProductDataController SealedProductDataController = new SealedProductDataController();
            SealedProductDTOs = SealedProductDataController.GetSealedProductsByGame(GameID);

            foreach (var Product in SealedProductDTOs)
            {
                SealedProduct FoundProduct = new SealedProduct
                {
                    SealedProductID = Product.SealedProductID,
                    SetID = Product.SetID,
                    SealedProductName = Product.SealedProductName,
                    Price = Product.Price
                };
                SealedProducts.Add(FoundProduct);
            }

            return SealedProducts;
        }

        /// <summary>
        /// Performs a get request on the provided sealed product ID
        /// </summary>
        /// <param name="SealedProductID">The sealed product ID to search for</param>
        /// <returns>Sealed product object</returns>
        [Route("/SealedProductID={SealedProductID}")]
        [HttpGet]
        public SealedProduct GetSealedProductByID (int SealedProductID)
        {
            SealedProduct FoundProduct = new SealedProduct();
            SealedProductDTO SealedProductDTO = new SealedProductDTO();
            SealedProductDataController SealedProductDataController = new SealedProductDataController();
            SealedProductDTO = SealedProductDataController.GetSealedProduct(SealedProductID);

            FoundProduct.SealedProductID = SealedProductDTO.SealedProductID;
            FoundProduct.SetID = SealedProductDTO.SetID;
            FoundProduct.SealedProductName = SealedProductDTO.SealedProductName;
            FoundProduct.Price = SealedProductDTO.Price;

            return FoundProduct;
        }

        /// <summary>
        /// Performs a post request of a new sealed product
        /// </summary>
        /// <param name="NewProduct">SealedProdcut object to be added</param>
        /// <returns>Confirmation as a bool</returns>
        [HttpPost]
        public bool Post ([FromBody] SealedProduct NewProduct)
        {
            bool Confrimation;

            SealedProductDataController SealedProductDataController = new SealedProductDataController();
            SealedProductDTO ProductDTO = new SealedProductDTO
            {
                SealedProductID = NewProduct.SealedProductID,
                SealedProductName = NewProduct.SealedProductName,
                SetID = NewProduct.SetID,
                Price = NewProduct.Price
            };

            Confrimation = SealedProductDataController.AddSealedProduct(ProductDTO);
            
            return Confrimation;
        }

        /// <summary>
        /// Performs a delete request for the provided SealedProductID
        /// </summary>
        /// <param name="SealedProductID">The provided sealed product ID</param>
        /// <returns>Confirmation as a bool</returns>
        [Route("{SealedProductID:int}")]
        [HttpDelete]
        public bool Delete (int SealedProductID)
        {
            bool Confirmation;

            SealedProductDataController SealedProductDataController = new SealedProductDataController();

            Confirmation = SealedProductDataController.DeleteSealedProduct(SealedProductID);

            return Confirmation;
        }

        /// <summary>
        /// Performs a put request for the SealedProduct to be updated
        /// </summary>
        /// <param name="UpdatedSealedProduct">Sealed product object to be updated</param>
        /// <returns>Confirmation as a bool</returns>
        [Route("{SealedProduct}")]
        [HttpPut]
        public bool Put ([FromBody] SealedProduct UpdatedSealedProduct)
        {
            bool Confirmation;

            SealedProductDataController SealedProductDataController = new SealedProductDataController();
            SealedProductDTO ProductDTO = new SealedProductDTO
            {
                SealedProductID = UpdatedSealedProduct.SealedProductID,
                SealedProductName = UpdatedSealedProduct.SealedProductName,
                SetID = UpdatedSealedProduct.SetID,
                Price = UpdatedSealedProduct.Price
            };

            Confirmation = SealedProductDataController.UpdateSealedProduct(ProductDTO);

            return Confirmation;
        }
    }
}
